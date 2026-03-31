using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PlanProduction
{
    public partial class FormPlanEntry : Form
    {
        // フォーム起動時の値
        private FormConfig settings;

        // モードレスなのでインスタンスを保持する
        private readonly FormOrderList formOrderList;

        // 現在表示中の手配先コードを格納する変数
        private readonly OdCdSetting OdCdSetting;
        private double 可動率;

        // DataTable を保持するフィールドを作る
        private readonly DataTable dt = new();

        // UnDoアクション
        Stack<UndoAction> undoStack = new();                    // 元に戻すスタック
        object beforeEditValue = null;                          // Undoセル編集用の「編集中の値」
        private Point dragStartPoint;
        private DataGridViewCell dragSourceCell;
        private bool isRowHeaderDrag = false;
        private bool isCancelAddEvent = false;

        // コンストラクタ
        public FormPlanEntry(OdCdSetting OdCdSetting)
        {
            InitializeComponent();

            this.KeyPreview = true;

            // 手配一覧を開く
            formOrderList = new FormOrderList(OdCdSetting, OnSelectedList);         // ← ★ コールバックを渡す
            formOrderList.Show();                                                   // モードレス
            formOrderList.BringToFront();

            // 表示対象の手配先コードをこのフォームに保持
            this.OdCdSetting = OdCdSetting;

            // イベント登録
            dataGridViewPlan.CellValueChanged += DataGridViewPlan_CellValueChanged; // 列2と列3が変更されたら終了時刻再計算
            // 「計画入力」イベント登録（共用）
            dataGridViewPlan.CellBeginEdit += DataGridView_CellBeginEdit;           // セル編集開始
            dataGridViewPlan.CellEndEdit += DataGridView_CellEndEdit;               // 小文字大文字変換
            dataGridViewPlan.RowPostPaint += DataGridView_RowPostPaint;             // 行番号と矢印
            dataGridViewPlan.MouseDown += DataGridView_MouseDown;                   // ドラッグ＆ドロップ
            dataGridViewPlan.MouseMove += DataGridView_MouseMove;                   // ドラッグ＆ドロップ
            dataGridViewPlan.DragOver += DataGridView_DragOver;                     // ドラッグ＆ドロップ
            dataGridViewPlan.DragDrop += DataGridView_DragDrop;                     // ドラッグ＆ドロップ
            dataGridViewPlan.AllowDrop = true;                                      // ドラッグ＆ドロップ
            dataGridViewPlan.RowsAdded += DataGridView_RowsAdded;                   // 新規行追加
            // 「実績入力」イベント登録（共用）
            dataGridViewAchieve.CellBeginEdit += DataGridView_CellBeginEdit;        // セル編集開始
            dataGridViewAchieve.CellEndEdit += DataGridView_CellEndEdit;            // 小文字大文字変換
            dataGridViewAchieve.RowPostPaint += DataGridView_RowPostPaint;          // 行番号と矢印
            dataGridViewAchieve.MouseDown += DataGridView_MouseDown;                // ドラッグ＆ドロップ
            dataGridViewAchieve.MouseMove += DataGridView_MouseMove;                // ドラッグ＆ドロップ
            dataGridViewAchieve.DragOver += DataGridView_DragOver;                  // ドラッグ＆ドロップ
            dataGridViewAchieve.DragDrop += DataGridView_DragDrop;                  // ドラッグ＆ドロップ
            dataGridViewAchieve.AllowDrop = true;                                   // ドラッグ＆ドロップ
            dataGridViewAchieve.RowsAdded += DataGridView_RowsAdded;                // 新規行追加

        }

        // 「コールバック関数」（別画面の「手配一覧」で選択されたアイテムを受け取る為）
        private void OnSelectedList(string mode, List<SelectedItem> list)
        {
            DataGridView dgv = (mode == "Plan") ? dataGridViewPlan : dataGridViewAchieve;
            foreach (var item in list)
            {
                dgv.Rows.Add(item.HmCd, item.CT, item.SumQty);
            }
            if (mode == "Plan") CalculatePlan(); // 「計画リスト」に追加された場合のみ終了時刻を再計算
        }

        // 「初期化処理」（表示される直前に一度だけ）
        private void FormPlanEntry_Load(object sender, EventArgs e)
        {
            // フォームの状態を復元
            settings = Common.FormSettingsLoad();
            string key = this.Name;
            if (settings.Forms.TryGetValue(key, out FormSettings s))
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(s.X, s.Y);
                this.Size = new Size(s.Width, s.Height);
                splitContainerMain.SplitterDistance = s.SplitterMainDistance;
            }

            // 初期表示
            labelTitleOdCd.Text = $"【{OdCdSetting.OdCd}】 {DataStore.M0300Map[OdCdSetting.OdCd]}";
            textBoxPlan可動率.Text = (string.IsNullOrEmpty(OdCdSetting.Ava)) ? "70" : OdCdSetting.Ava;
            可動率 = double.TryParse(OdCdSetting.Ava, out double result) ? 1 / result * 100 : 1.4286; // デフォルトは70%で1.4286倍
            textBoxPlanStartTime.Text = (string.IsNullOrEmpty(OdCdSetting.StartTime)) ? "08:15" : OdCdSetting.StartTime;
            textBoxPlanQty.Text = "0";
            textBoxPlanCT.Text = "0.0";
            textBoxPlanKdo.Text = "0.0";
            //checkBoxPlanピカピカ.Checked 金曜日なら

            // 計画リストの初期設定
            dataGridViewPlan.EnableHeadersVisualStyles = false;
            dataGridViewPlan.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            var ctP = dataGridViewPlan.Columns["PlanCT"];
            ctP.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;         // ヘッダー中央寄せ
            ctP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;          // セル右寄せ
            ctP.DefaultCellStyle.Format = "N1";                                                 // CTは小数点以下1桁
            var qtyP = dataGridViewPlan.Columns["Plan本数"];
            qtyP.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;        // ヘッダー右寄せ
            qtyP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;         // セル右寄せ
            string[] colNamesPlan = ["Plan開始時刻", "Plan終了時刻", "Plan休憩時間"];
            foreach (string colName in colNamesPlan)
            {
                var col = dataGridViewPlan.Columns[colName];
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;     // ヘッダー中央寄せ
                col.DefaultCellStyle.Alignment = (colName != "Plan休憩時間") ?
                    DataGridViewContentAlignment.MiddleCenter :
                    DataGridViewContentAlignment.MiddleRight;
                col.HeaderCell.Style.Font = new Font(
                    dataGridViewPlan.ColumnHeadersDefaultCellStyle.Font.FontFamily, 7.0f);      // 小さいサイズ
                col.ReadOnly = true;
            }
            dataGridViewPlan.RowTemplate.Height = 30;
            dataGridViewPlan.Rows[dataGridViewPlan.NewRowIndex].Height = 30;

            // 実績リストの初期設定
            dataGridViewAchieve.EnableHeadersVisualStyles = false;
            dataGridViewAchieve.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            var ctA = dataGridViewAchieve.Columns["AchieveCT"];
            ctA.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;         // ヘッダー中央寄せ
            ctA.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;          // セル右寄せ
            ctA.DefaultCellStyle.Format = "N1";                                                 // CTは小数点以下1桁
            var qtyA = dataGridViewAchieve.Columns["Achieve本数"];
            qtyA.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;        // ヘッダー右寄せ
            qtyA.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;         // セル右寄せ
            string[] colNamesAchieve = ["Achieve開始時刻", "Achieve終了時刻", "Achieve休憩時間"];
            foreach (string colName in colNamesAchieve)
            {
                var col = dataGridViewAchieve.Columns[colName];
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;     // ヘッダー中央寄せ
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;     // セル中央寄せ
                col.HeaderCell.Style.Font = new Font(
                    dataGridViewAchieve.ColumnHeadersDefaultCellStyle.Font.FontFamily, 7.0f);   // 小さいサイズ
            }
            dataGridViewAchieve.RowTemplate.Height = 30;
            dataGridViewAchieve.Rows[dataGridViewAchieve.NewRowIndex].Height = 30;
        }

        // 行番号を付ける（1 から始める）
        private void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (e.RowIndex == dgv.NewRowIndex) return; // ★ 新規行は行番号を描画しない、「※」は描画

            // ★ 通常行は矢印を上書きして消す
            Rectangle rect = new(
                e.RowBounds.Left + 1,
                e.RowBounds.Top + 1,
                dgv.RowHeadersWidth - 2,
                e.RowBounds.Height - 2);
            e.Graphics.FillRectangle(SystemBrushes.Control, rect);

            // 行番号を描画
            string rowNumber = (e.RowIndex + 1).ToString();
            var headerBounds = new Rectangle(
                e.RowBounds.Left,
                e.RowBounds.Top,
                dataGridViewPlan.RowHeadersWidth,
                e.RowBounds.Height);
            TextRenderer.DrawText(
                e.Graphics,
                rowNumber,
                dataGridViewPlan.Font,
                headerBounds,
                Color.Black,
                TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
        }

        private void FormPlanEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            formOrderList?.Close(); // 計画入力と同時に手配一覧も閉じる

            settings = Common.FormSettingsLoad(); // 他のフォームで変更された可能性があるので、最新の状態を読み込む
            string key = this.Name;
            if (!settings.Forms.ContainsKey(key)) settings.Forms[key] = new FormSettings();
            var s = settings.Forms[key];
            s.X = this.Location.X;
            s.Y = this.Location.Y;
            s.Width = this.Width;
            s.Height = this.Height;
            s.SplitterMainDistance = splitContainerMain.SplitterDistance;
            Common.FormSettingsSave(settings);
        }

        // キーボードショートカット
        private void FormOrderList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                ButtonUndo_Click(sender, e);    // ボタンの「元に戻す」を呼び出す
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }



        /*
         * ドラッグ＆ドロップ関連
         * 
         */
        // 「行移動」「セル入れ替え」ドラッグ開始判定
        private void DataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            var dgv = (DataGridView)sender;
            dragStartPoint = e.Location;

            var hit = dgv.HitTest(e.X, e.Y);

            if (hit.RowIndex >= 0 && hit.RowIndex != dgv.NewRowIndex)
            {
                if (hit.Type == DataGridViewHitTestType.RowHeader)
                {
                    // 行ヘッダー → 「行移動」モード
                    isRowHeaderDrag = true;
                    dragSourceCell = dgv[0, hit.RowIndex]; // 行番号だけ覚えておけばOK

                    // マウスダウンした行を選択
                    dgv.ClearSelection();
                    dgv.Rows[hit.RowIndex].Selected = true;
                }
                else if (hit.ColumnIndex >= 0)
                {
                    // 通常セル → 「セル入れ替え」モード
                    isRowHeaderDrag = false;
                    //dragSourceCell = dgv[hit.ColumnIndex, hit.RowIndex];（今作では使用せず）
                }
            }
        }
        // 「行移動」「セル入れ替え」マウス移動
        private void DataGridView_MouseMove(object sender, MouseEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dragSourceCell == null)
                return;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (Math.Abs(e.X - dragStartPoint.X) > SystemInformation.DragSize.Width ||
                    Math.Abs(e.Y - dragStartPoint.Y) > SystemInformation.DragSize.Height)
                {
                    dgv.DoDragDrop(dragSourceCell, DragDropEffects.Move);
                }
            }
        }
        // ドラッグ中のカーソルの形
        private void DataGridView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        // 「行移動」「セル入れ替え」ドロップ処理
        private void DataGridView_DragDrop(object sender, DragEventArgs e)
        {
            if (dragSourceCell == null)
            {
                isRowHeaderDrag = false;
                return;
            }

            var dgv = (DataGridView)sender;
            Point clientPoint = dgv.PointToClient(new Point(e.X, e.Y));
            var hit = dgv.HitTest(clientPoint.X, clientPoint.Y);

            if (isRowHeaderDrag && (hit.RowIndex < 0 || hit.RowIndex == dgv.NewRowIndex))
            {
                // ----------------------------------------
                // 行ヘッダー（データ範囲外） → 「行削除」
                // ----------------------------------------
                ButtonDelete_Click(sender, e);
            }
            else if (isRowHeaderDrag)
            {
                // ----------------------------------------
                // 行ヘッダー（データ範囲内） → 「行移動」
                // ----------------------------------------
                int sourceRow = dragSourceCell.RowIndex;
                int targetRow = hit.RowIndex;

                if (sourceRow == targetRow)
                    return;

                // Undo情報スタック
                var action = new UndoAction()
                {
                    dgv = dgv,
                    Type = UndoType.RowMove,
                    SourceRow = sourceRow,
                    TargetRow = targetRow
                };
                undoStack.Push(action);

                // 「行移動」
                isCancelAddEvent = true;
                var row = dgv.Rows[sourceRow];
                dgv.Rows.RemoveAt(sourceRow);
                dgv.Rows.Insert(targetRow, row);
                isCancelAddEvent = false;

                dgv.ClearSelection();
                dgv.Rows[targetRow].Selected = true;
            }
            else
            {
                // ----------------------------------------
                // 通常セル → 「セル入れ替え」（今作では使用せず）
                // ----------------------------------------
                //if (hit.ColumnIndex < 0)
                //    return;

                //DataGridViewCell targetCell = dgv[hit.ColumnIndex, hit.RowIndex];

                //if (targetCell == dragSourceCell)
                //    return;

                //// Undo情報スタック
                //var action = new UndoAction()
                //{
                //    dgv = dgv,
                //    Type = UndoType.CellSwap,
                //    Row1 = dragSourceCell.RowIndex,
                //    Col1 = dragSourceCell.ColumnIndex,
                //    Value1 = dragSourceCell.Value,
                //    Row2 = targetCell.RowIndex,
                //    Col2 = targetCell.ColumnIndex,
                //    Value2 = targetCell.Value
                //};
                //undoStack.Push(action);

                //// 実際に値の入れ替え
                //object temp = dragSourceCell.Value;
                //dragSourceCell.Value = targetCell.Value;
                //targetCell.Value = temp;

                //dgv.CurrentCell = targetCell;
            }
            dragSourceCell = null;
            isRowHeaderDrag = false;
            CalculatePlan();
        }
        // 「セル編集」開始
        private void DataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var dgv = (DataGridView)sender;
            beforeEditValue = dgv[e.ColumnIndex, e.RowIndex].Value;
        }
        // 「セル編集」終了
        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            var afterValue = dgv[e.ColumnIndex, e.RowIndex].Value;

            // 品番列のみ小文字を大文字に変換
            if (e.ColumnIndex == 0 && afterValue != null)
            {
                afterValue = afterValue.ToString().ToUpper();
                dgv[e.ColumnIndex, e.RowIndex].Value = afterValue;
            }

            // 値が変わっていなければ何もしない
            if ((beforeEditValue == null && afterValue == null) ||
                (beforeEditValue != null && beforeEditValue.Equals(afterValue)))
            {
                return;
            }

            // Undo 情報を積む
            var action = new UndoAction()
            {
                dgv = dgv,
                Type = UndoType.CellEdit,
                EditRow = e.RowIndex,
                EditCol = e.ColumnIndex,
                OldValue = beforeEditValue,
                NewValue = afterValue
            };

            undoStack.Push(action);
        }
        // 「列2:CT」または「列3:本数」の変更時、終了時刻再計算
        private void DataGridViewPlan_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 || e.ColumnIndex == 2) CalculatePlan();
        }
        // 「行追加」
        private void DataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            var dgv = (DataGridView)sender;

            // プログラム中で発生した「行追加」イベントは無視
            if (isCancelAddEvent) return;

            // 新規行（※）で入力された場合と、プログラムで入力された場合に異なる
            var idx = (dgv.Rows[e.RowIndex].IsNewRow) ? e.RowIndex - 1 : e.RowIndex;

            // UndoAction を作成
            UndoAction action = new()
            {
                dgv = dgv,
                Type = UndoType.RowInsert,
                InsertRowIndex = idx
            };

            undoStack.Push(action);
        }
        // 「元に戻す」ボタン
        private void ButtonUndo_Click(object sender, EventArgs e)
        {
            if (undoStack == null) return;
            if (undoStack.Count == 0) return;

            var action = undoStack.Pop();

            try
            {
                isCancelAddEvent = true;
                switch (action.Type)
                {
                    case UndoType.RowMove:
                        // 「行移動」を元に戻す
                        var row = action.dgv.Rows[action.TargetRow];
                        action.dgv.Rows.RemoveAt(action.TargetRow);
                        action.dgv.Rows.Insert(action.SourceRow, row);
                        break;

                    case UndoType.CellSwap:
                        // 「セル入れ替え」を元に戻す（今作では使用せず）
                        action.dgv[action.Col1, action.Row1].Value = action.Value1;
                        action.dgv[action.Col2, action.Row2].Value = action.Value2;
                        break;

                    case UndoType.CellEdit:
                        // 「セル編集」を元に戻す
                        action.dgv[action.EditCol, action.EditRow].Value = action.OldValue;
                        action.dgv.CurrentCell = action.dgv[action.EditCol, action.EditRow];
                        break;

                    case UndoType.RowDelete:
                        // 「行削除」を元に戻す
                        action.dgv.Rows.Insert(action.DeletedRowIndex, action.DeletedRowValues);
                        action.dgv.CurrentCell = action.dgv[1, action.DeletedRowIndex];
                        break;
                    case UndoType.RowInsert:
                        // 挿入された行を削除する
                        action.dgv.Rows.RemoveAt(action.InsertRowIndex);
                        break;
                }
                isCancelAddEvent = false;
                CalculatePlan();
            }
            catch (Exception)
            {
                MessageBox.Show("想定外の動作ですm(__)m\nお困りであればシステム担当者に連絡してください"
                    , action.Type.ToString() + "を元に戻す処理"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // 「行削除」ボタン
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            var dgv = (DataGridView)sender;
            int rowIndex = dgv.CurrentRow.Index;
            if (rowIndex < 0 || rowIndex >= dgv.Rows.Count) return;
            var row = dgv.Rows[rowIndex];

            // 行の値を配列に保存
            object[] values = new object[row.Cells.Count];
            for (int i = 0; i < row.Cells.Count; i++)
                values[i] = row.Cells[i].Value;

            // Undo 情報を積む
            var action = new UndoAction()
            {
                dgv = dgv,
                Type = UndoType.RowDelete,
                DeletedRowIndex = rowIndex,
                DeletedRowValues = values
            };
            undoStack.Push(action);

            // 実際に削除
            dgv.Rows.RemoveAt(rowIndex);
        }




        // 「計画印刷」
        private void ButtonPlanPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("まだ作り込めていません");
        }
        // 「計画クリア」
        private void ButtonPlanClear_Click(object sender, EventArgs e)
        {
            dataGridViewPlan.Rows.Clear();

            // UndoStachからdataGridViewPlanに関するアクションを削除
            Stack<UndoAction> newStack = new Stack<UndoAction>();
            while (undoStack.Count > 0)
            {
                var action = undoStack.Pop();
                if (action.dgv == dataGridViewPlan) continue;
                newStack.Push(action);
            }
            // 元の順序を保つためにもう一度反転
            undoStack = new Stack<UndoAction>(newStack);
        }
        // 「実績へコピー」
        private void ButtonPlanCopy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("まだ作り込めていません");
        }
        // 「実績クリア」
        private void ButtonAchieveClear_Click(object sender, EventArgs e)
        {
            dataGridViewAchieve.Rows.Clear();

            // UndoStachからdataGridViewPlanに関するアクションを削除
            Stack<UndoAction> newStack = new Stack<UndoAction>();
            while (undoStack.Count > 0)
            {
                var action = undoStack.Pop();
                if (action.dgv == dataGridViewAchieve) continue;
                newStack.Push(action);
            }
            // 元の順序を保つためにもう一度反転
            undoStack = new Stack<UndoAction>(newStack);
        }
        // 「保存して閉じる」
        private void ButtonSaveClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show("まだ作り込めていません");
        }
        // 「閉じる」ボタン
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }



        // 「可動率」関連
        private void TextBoxPlan可動率_Enter(object sender, EventArgs e)
        {
            textBoxPlan可動率.SelectAll();
        }
        private void TextBoxPlan可動率_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (double.TryParse(textBoxPlan可動率.Text, out double result))
                {
                    可動率 = 1 / result * 100;
                }
                else
                {
                    可動率 = 1.4286; // デフォルトは70%で1.4286倍
                    textBoxPlan可動率.Text = "70";
                }
                CalculatePlan();
                textBoxPlan可動率.SelectAll();
                e.Handled = true;
            }
        }
        private void TextBoxPlan可動率_MouseDown(object sender, MouseEventArgs e)
        {
            textBoxPlan可動率.SelectAll();
        }
        // 「開始時刻」関連
        private void TextBoxPlanStartTime_Enter(object sender, EventArgs e)
        {
            textBoxPlanStartTime.SelectAll();
        }
        private void TextBoxPlanStartTime_MouseDown(object sender, MouseEventArgs e)
        {
            textBoxPlanStartTime.SelectAll();
        }
        private void TextBoxPlanStartTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (DateTime.TryParse(textBoxPlanStartTime.Text, out _)) CalculatePlan();
                textBoxPlanStartTime.SelectAll();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                if (DateTime.TryParse(textBoxPlanStartTime.Text, out DateTime dt))
                {
                    int minutes = (e.KeyCode == Keys.Up) ? 1 :
                                  (e.KeyCode == Keys.Down) ? -1 :
                                  (e.KeyCode == Keys.Left) ? 10 :
                                  (e.KeyCode == Keys.Right) ? -10 : 0;
                    textBoxPlanStartTime.Text = dt.AddMinutes(minutes).ToString("HH:mm");
                    CalculatePlan();
                }
                textBoxPlanStartTime.SelectAll();
                e.Handled = true;
            }
        }
        private void TextBoxPlanStartTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (e.KeyChar == '\b') return;  // バックスペースはOK
            if (!char.IsDigit(e.KeyChar))   // 数字以外は入力させない
            {
                e.Handled = true;
                return;
            }
            // 2文字入力した時点で自動的に「:」を挿入
            if (tb.Text.Length == 1)
            {
                tb.Text += e.KeyChar + ":";
                tb.SelectionStart = tb.Text.Length;   // カーソルを末尾へ移動
                e.Handled = true;                    // 入力済みなので処理完了
            }
            // 2文字入力した時点で自動的に「:」を挿入
            if (tb.Text.Length == 2 && e.KeyChar != ':')
            {
                tb.Text += ":" + e.KeyChar;
                tb.SelectionStart = tb.Text.Length;   // カーソルを末尾へ移動
                e.Handled = true;                    // 入力済みなので処理完了
            }
        }



        // 「計画チェックボックス」群
        private void CheckBoxPlanChecked(object sender, EventArgs e)
        {
            CalculatePlan();
        }
        // 「実績入力画面」を表示／非表示
        private void CheckBoxHiddenAchieve_CheckedChanged(object sender, EventArgs e)
        {
            return; // 今開発している時ではない
            //splitContainerMain.Panel2Collapsed = checkBoxHiddenAchieve.Checked;
        }



        /// <summary>
        /// このアプリケーションの重要なメソッド
        /// 
        /// 各行の開始時刻 + (CT * 本数 * 可動率) を計算して終了時刻を算出
        /// 休憩時間を考慮した終了時刻に修正して計画一覧を作成
        /// </summary>
        private void CalculatePlan()
        {
            double 合計本数 = 0.0;
            double CT稼働時間 = 0.0;
            double 計画稼働時間 = 0.0;
            for (int i = 0; i < dataGridViewPlan.Rows.Count; i++)
            {
                var row = dataGridViewPlan.Rows[i]; // ここで通常処理

                if (row.IsNewRow) continue;         // 新規行はスキップ

                // 計画開始時刻を取得（1行目はテキストボックスの開始時刻、2行目以降は前行の終了時刻）
                row.Cells["Plan開始時刻"].Value = (i == 0) ?
                    textBoxPlanStartTime.Text :
                    dataGridViewPlan.Rows[i - 1].Cells["Plan終了時刻"].Value?.ToString();
                string startTimeStr = row.Cells["Plan開始時刻"].Value?.ToString();
                if (!DateTime.TryParse(startTimeStr, out DateTime startTime))
                {
                    row.Cells["Plan終了時刻"].Value = startTimeStr; // 無効な開始時刻の場合
                    continue;
                }

                // 計画CTを取得
                string ctStr = row.Cells["PlanCT"].Value?.ToString();
                if (!double.TryParse(ctStr, out double ct))
                {
                    row.Cells["Plan終了時刻"].Value = startTimeStr; // 無効なCTの場合
                    continue;
                }

                // 一旦計画終了時刻を計算
                double 本数 = (row.Cells["Plan本数"].Value != null) ? Convert.ToDouble(row.Cells["Plan本数"].Value) : 0.0;
                double adjustedCt = ct * 本数 * 可動率; // 可動率を考慮してCTを調整
                DateTime endTime = startTime.AddSeconds(adjustedCt);

                // 休憩時間を算出
                double 休憩 = 休憩時間算出(startTime, endTime,
                    checkBoxPlanお昼稼働.Checked, checkBoxPlan休憩稼働.Checked,
                    checkBoxPlanピカピカ.Checked, checkBoxPlan早昼.Checked);
                //（算出した休憩時間を加算した終了時刻で2回目のチェックを通す）
                double 休憩2 = 休憩時間算出(startTime, endTime.AddSeconds(休憩),
                    checkBoxPlanお昼稼働.Checked, checkBoxPlan休憩稼働.Checked,
                    checkBoxPlanピカピカ.Checked, checkBoxPlan早昼.Checked);
                row.Cells["Plan休憩時間"].Value = 休憩2;

                // 休憩時間を加味した計画終了時刻をセルにセット（秒は切り上げ）
                if (endTime.AddSeconds(休憩2).Second > 0) 休憩2 += 60;
                row.Cells["Plan終了時刻"].Value = endTime.AddSeconds(休憩2).ToString("HH:mm");

                // 作業者をセット（初回の場合）
                if (row.Cells["Plan作業者"].Value == null)
                    row.Cells["Plan作業者"].Value = OdCdSetting.TanName;

                // サマリー
                合計本数 += 本数;
                CT稼働時間 += ct * 本数;
                計画稼働時間 += adjustedCt;
            }
            textBoxPlanQty.Text = 合計本数.ToString("#,0");
            textBoxPlanCT.Text = (CT稼働時間 / 3600).ToString("N1");
            textBoxPlanKdo.Text = (計画稼働時間 / 3600).ToString("N1");
            textBoxPlanEndTime.Text = (dataGridViewPlan.Rows.Count > 1) ?
                dataGridViewPlan.Rows[^2].Cells["Plan終了時刻"].Value?.ToString() :
                textBoxPlanStartTime.Text;
        }

        /// <summary>
        /// 休憩時間を算出
        /// </summary>
        /// <param name="stdt"></param>
        /// <param name="eddt"></param>
        /// <returns>休憩時間(秒)</returns>
        private static double 休憩時間算出(DateTime stdt, DateTime eddt,
            bool 昼稼働, bool 休憩稼働, bool ピカピカ, bool 早昼)
        {
            // 時刻部分だけを取り出す
            TimeSpan startTime = stdt.TimeOfDay;
            TimeSpan endTime = eddt.TimeOfDay;

            // 対象のルールを加算していく変数
            double sum = 0;

            // 休憩ルール一覧
            var rules = new List<(TimeSpan start, TimeSpan end, double minutes, bool enabled)>
            {
                (new TimeSpan(10,10,0), new TimeSpan(10,20,0), 10, 休憩稼働),
                (new TimeSpan(11,30,0), new TimeSpan(12,15,0), (昼稼働) ? 0 : 45, !早昼),
                (new TimeSpan(12,15,0), new TimeSpan(13,00,0), (昼稼働) ? 0 : 45, 早昼),
                (new TimeSpan(13,00,0), new TimeSpan(13,15,0), 15, !ピカピカ),
                (new TimeSpan(13,00,0), new TimeSpan(13,15,0), 15, !ピカピカ),
                (new TimeSpan(15,00,0), new TimeSpan(15,10,0), 10, 休憩稼働),
                (new TimeSpan(17,10,0), new TimeSpan(17,15,0), 5,  休憩稼働),
                (new TimeSpan(19,15,0), new TimeSpan(19,20,0), 5,  休憩稼働),
            };

            // 合致したルールの休憩時間を加算
            foreach (var (start, end, minutes, enabled) in rules)
            {
                if (enabled) continue; // チェックされていたらルール無視

                if (startTime < start && start < endTime)
                    sum += minutes * 60;
            }
            return sum;
        }

    }
}
