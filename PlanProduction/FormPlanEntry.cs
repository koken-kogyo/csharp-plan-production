using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PlanProduction
{
    public partial class FormPlanEntry : Form
    {
        // メインフォームへ値を返すイベント
        public event Action<bool> IsUpdated;
        
        // 現在表示中の手配先コードを格納する変数
        private readonly OdCdSetting OdCdSetting;
        private readonly DateTime PlanDate;
        private double 可動率;

        // フォームのサイズ等セッティング
        private FormConfig settings;

        // 手配一覧をモードレスで起動させるのでインスタンスを保持する
        private readonly FormOrderList formOrderList;

        // 状態フラグ
        private bool isPlanChanged = false;
        private bool isAchieveChanged = false;

        // 時刻入力用
        TextBox editingTextBox = null;
        bool backspacePressed = false;

        // UnDoアクション
        Stack<UndoAction> undoStack = new();                    // 元に戻すスタック
        object beforeEditValue = null;                          // Undoセル編集用の「編集中の値」
        private Point dragStartPoint;
        private DataGridViewCell dragSourceCell;
        private bool isRowHeaderDrag = false;
        private bool isCancelAddEvent = false;

        // コンストラクタ
        public FormPlanEntry(DateTime plandate, OdCdSetting odcdsetting)
        {
            InitializeComponent();

            this.KeyPreview = true;

            // 表示対象の手配先コードと計画日付をこのフォームに保持
            this.OdCdSetting = odcdsetting;
            this.PlanDate = plandate;

            // 手配一覧を開く
            formOrderList = new FormOrderList(OdCdSetting, OnSelectedList);         // ← ★ コールバックを渡す
            formOrderList.Show();                                                   // モードレス
            formOrderList.BringToFront();

            // イベント登録
            dataGridViewPlan.CellValueChanged += DataGridViewPlan_CellValueChanged;         // 列2:CTと列3:本数が変更されたら終了時刻再計算
            dataGridViewAchieve.CellValueChanged += DataGridViewAchieve_CellValueChanged;   // 列2:CT～列5:終了が変更されたら休憩時刻と可動率再計算

            // 「計画入力」イベント登録（共用）
            dataGridViewPlan.KeyDown += DataGridView_KeyDown;
            dataGridViewPlan.CellBeginEdit += DataGridView_CellBeginEdit;           // セル編集開始
            dataGridViewPlan.EditingControlShowing += DataGridView_EditingControlShowing;
            dataGridViewPlan.CellEndEdit += DataGridView_CellEndEdit;               // 小文字大文字変換
            dataGridViewPlan.RowPostPaint += DataGridView_RowPostPaint;             // 行番号と矢印
            dataGridViewPlan.MouseDown += DataGridView_MouseDown;                   // ドラッグ＆ドロップ
            dataGridViewPlan.MouseMove += DataGridView_MouseMove;                   // ドラッグ＆ドロップ
            dataGridViewPlan.DragOver += DataGridView_DragOver;                     // ドラッグ＆ドロップ
            dataGridViewPlan.DragDrop += DataGridView_DragDrop;                     // ドラッグ＆ドロップ
            dataGridViewPlan.AllowDrop = true;                                      // ドラッグ＆ドロップ
            dataGridViewPlan.RowsAdded += DataGridView_RowsAdded;                   // 新規行追加
            // 「実績入力」イベント登録（共用）
            dataGridViewAchieve.KeyDown += DataGridView_KeyDown;
            dataGridViewAchieve.CellBeginEdit += DataGridView_CellBeginEdit;        // セル編集開始
            dataGridViewAchieve.EditingControlShowing += DataGridView_EditingControlShowing;
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
            if (mode == "Plan")
            {
                foreach (var item in list)
                {
                    dgv.Rows.Add(item.HmCd, item.CT, item.SumQty);
                }
                CalculatePlan(); // 「計画リスト」に追加された場合のみ終了時刻を再計算
                isPlanChanged = true;
            }
            else
            {
                foreach (var item in list)
                {
                    dgv.Rows.Add(item.HmCd, item.CT, item.SumQty, null, null, null, null, OdCdSetting.TanName);
                    dgv.Rows[^2].Cells["AchieveCT"].Style.BackColor = 
                        (item.CT.ToDoubleOrDefault() == 0.0) ? Color.LightCoral : Color.White;
                    dgv.Rows[^2].Cells["Achieve本数"].Style.BackColor = 
                        (item.SumQty.ToIntOrDefault() == 0) ? Color.LightCoral : Color.White;
                }
                if (dgv.Rows.Count > 0)
                {
                    dgv.Rows[0].Cells["Achieve開始時刻"].Value = OdCdSetting.StartTime;
                    dgv.CurrentCell = dgv.Rows[0].Cells["Achieve終了時刻"];
                }
                isAchieveChanged = true;
            }
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
            labelTitleDate.Text = $"【{PlanDate:M/d}】 計画と実績";
            textBoxPlanStartTime.Text = (string.IsNullOrEmpty(OdCdSetting.StartTime)) ? "08:15" : OdCdSetting.StartTime;
            textBoxPlanEndTime.Text = "";
            textBoxPlanQty.Text = "0";
            textBoxPlanCT.Text = "0.0";
            textBoxPlanOpe.Text = "0.0";
            textBoxPlan可動率.Text = (string.IsNullOrEmpty(OdCdSetting.Ava)) ? "70" : OdCdSetting.Ava;
            可動率 = double.TryParse(OdCdSetting.Ava, out double result) ? 1 / result * 100 : 1.4286; // デフォルトは70%で1.4286倍
            checkBoxPlanお昼稼働.Checked = false;
            checkBoxPlan休憩稼働.Checked = false;
            checkBoxPlanピカピカ.Checked = false;
            checkBoxPlan早昼.Checked = false;

            textBoxAchieveStartTime.Text = (string.IsNullOrEmpty(OdCdSetting.StartTime)) ? "08:15" : OdCdSetting.StartTime;
            textBoxAchieveEndTime.Text = "";
            textBoxAchieveQty.Text = "0";
            textBoxAchieveCT.Text = "0.0";
            textBoxAchieveOpe.Text = "0.0";
            textBoxAchieve可動率.Text = "";
            checkBoxAchieveお昼稼働.Checked = false;
            checkBoxAchieve休憩稼働.Checked = false;
            checkBoxAchieveピカピカ.Checked = false;
            checkBoxAchieve早昼.Checked = false;
            if (PlanDate.DayOfWeek == DayOfWeek.Friday)
            {
                checkBoxPlanピカピカ.Checked = true;
                checkBoxAchieveピカピカ.Checked = true;
            }

            // 計画リストの初期設定
            dataGridViewPlan.EnableHeadersVisualStyles = false;
            dataGridViewPlan.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;

            string[] colNames1Plan = ["PlanCT", "Plan本数", "Plan可動率"];
            foreach (string colName in colNames1Plan)
            {
                var col = dataGridViewPlan.Columns[colName];
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;     // ヘッダー中央寄せ
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;      // セル右寄せ
            }
            dataGridViewPlan.Columns["PlanCT"].DefaultCellStyle.Format = "N1";                  // CTは小数点以下1桁

            string[] colNames2Plan = ["Plan開始時刻", "Plan終了時刻", "Plan休憩時間"];
            foreach (string colName in colNames2Plan)
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

            string[] colNames1Achieve = ["AchieveCT", "Achieve本数", "Achieve可動率"];
            foreach (string colName in colNames1Achieve)
            {
                var col = dataGridViewAchieve.Columns[colName];
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;     // ヘッダー中央寄せ
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;      // セル右寄せ
            }
            dataGridViewAchieve.Columns["AchieveCT"].DefaultCellStyle.Format = "N1";            // CTは小数点以下1桁

            string[] colNames2Achieve = ["Achieve開始時刻", "Achieve終了時刻", "Achieve休憩時間"];
            foreach (string colName in colNames2Achieve)
            {
                var col = dataGridViewAchieve.Columns[colName];
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;     // ヘッダー中央寄せ
                col.DefaultCellStyle.Alignment = (colName != "Achieve休憩時間") ?
                    DataGridViewContentAlignment.MiddleCenter :
                    DataGridViewContentAlignment.MiddleRight;
                col.HeaderCell.Style.Font = new Font(
                    dataGridViewAchieve.ColumnHeadersDefaultCellStyle.Font.FontFamily, 7.0f);   // ヘッダー小さいサイズ
            }
            dataGridViewAchieve.RowTemplate.Height = 30;
            dataGridViewAchieve.Rows[dataGridViewAchieve.NewRowIndex].Height = 30;

            // データベースデータの初期表示
            InitialPlanProduction();
        }

        // データベースデータの初期表示
        private void InitialPlanProduction()
        {
            // 計画入力データの設定
            var paramPlan = new SaveOptions
            {
                OdCd = OdCdSetting.OdCd,
                PlanDate = PlanDate,
                Type = "P"
            };
            if (!DBAccessor.GetKD8020(ref paramPlan)) return;
            if (paramPlan.開始時刻 != null || paramPlan.終了時刻 != null)
            {
                checkBoxPlanお昼稼働.Checked = paramPlan.昼稼働;
                checkBoxPlan休憩稼働.Checked = paramPlan.休憩稼働;
                checkBoxPlanピカピカ.Checked = paramPlan.ピカピカ;
                checkBoxPlan早昼.Checked = paramPlan.早昼;
                textBoxPlanQty.Text = paramPlan.合計本数.ToString();
                textBoxPlanCT.Text = paramPlan.CT合計時間.ToString("N2");
                textBoxPlanOpe.Text = paramPlan.合計稼働時間.ToString("N2");
                textBoxPlan可動率.Text = (paramPlan.可動率 != 0) ? paramPlan.可動率.ToString("F0") : "";
                textBoxPlanStartTime.Text = paramPlan.開始時刻.ToString();
                textBoxPlanEndTime.Text = paramPlan.終了時刻.ToString();
            }
            dataGridViewPlan.Rows.Clear(); // ここから明細
            DataTable planDt = new();
            if (!DBAccessor.GetKD8030(ref planDt, paramPlan)) return;
            foreach (DataRow row in planDt.Rows)
            {
                //string No = row["No"].ToString();
                string hmcd = row["品番"].ToString();
                double ct = row["CT"].ToDoubleOrDefault();
                int qty = row["本数"].ToIntOrDefault();
                string starttime = DateTime.Parse(row["開始時刻"].ToString()).ToString("HH:mm");
                string endtime = DateTime.Parse(row["終了時刻"].ToString()).ToString("HH:mm");
                int breaktime = row["休憩時間"].ToIntOrDefault();
                string tannm = row["作業者"].ToString();
                string note = row["備考"].ToString();
                double ava = row["可動率"].ToDoubleOrDefault();
                dataGridViewPlan.Rows.Add(hmcd, ct, qty, starttime, endtime, breaktime, ava, tannm, note);
            }

            // 実績入力データの設定
            var paramAchieve = new SaveOptions
            {
                OdCd = OdCdSetting.OdCd,
                PlanDate = PlanDate,
                Type = "J"
            };
            if (!DBAccessor.GetKD8020(ref paramAchieve)) return;
            if (paramAchieve.開始時刻 != null || paramAchieve.終了時刻 != null)
            {
                checkBoxAchieveお昼稼働.Checked = paramAchieve.昼稼働;
                checkBoxAchieve休憩稼働.Checked = paramAchieve.休憩稼働;
                checkBoxAchieveピカピカ.Checked = paramAchieve.ピカピカ;
                checkBoxAchieve早昼.Checked = paramAchieve.早昼;
                textBoxAchieveQty.Text = paramAchieve.合計本数.ToString();
                textBoxAchieveCT.Text = paramAchieve.CT合計時間.ToString("N2");
                textBoxAchieveOpe.Text = paramAchieve.合計稼働時間.ToString("N2");
                textBoxAchieve可動率.Text = (paramAchieve.可動率 != 0) ? paramAchieve.可動率.ToString("F0") : "";
                textBoxAchieveStartTime.Text = paramAchieve.開始時刻.ToString();
                textBoxAchieveEndTime.Text = paramAchieve.終了時刻.ToString();
            }
            dataGridViewAchieve.Rows.Clear(); // ここから明細
            DataTable achieveDt = new();
            if (!DBAccessor.GetKD8030(ref achieveDt, paramAchieve)) return;
            foreach (DataRow row in achieveDt.Rows)
            {
                //string No = row["No"].ToString();
                string hmcd = row["品番"].ToString();
                double ct = row["CT"].ToDoubleOrDefault();
                int qty = row["本数"].ToIntOrDefault();
                string starttime = DateTime.Parse(row["開始時刻"].ToString()).ToString("HH:mm");
                string endtime = DateTime.Parse(row["終了時刻"].ToString()).ToString("HH:mm");
                int breaktime = row["休憩時間"].ToIntOrDefault();
                string tannm = row["作業者"].ToString();
                string note = row["備考"].ToString();
                double ava = row["可動率"].ToDoubleOrDefault();
                dataGridViewAchieve.Rows.Add(hmcd, ct, qty, starttime, endtime, breaktime, ava, tannm, note);
            }
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
         * データグリッド関連
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
                isPlanChanged = true;
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
                isPlanChanged = true;
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
        // Deleteキーでセルの値を消す
        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                var dgv = (DataGridView)sender;
                dgv.CurrentCell.Value = string.Empty;
            }
        }
        // 「セル編集」開始
        private void DataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var dgv = (DataGridView)sender;
            beforeEditValue = dgv[e.ColumnIndex, e.RowIndex].Value;
        }
        // 「コントロール出現」
        private void DataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (e.Control is TextBox tb)
            {
                // 前のイベントを解除（多重登録防止）
                if (editingTextBox != null)
                {
                    editingTextBox.TextChanged -= HMCDTextBox_TextChanged;
                    editingTextBox.TextChanged -= TimeTextBox_TextChanged;
                    editingTextBox.KeyDown -= TimeTextBox_KeyDown;
                }

                editingTextBox = tb;

                // ★列番号が 3,4 のときだけイベント登録
                int col = dgv.CurrentCell.ColumnIndex;
                if (col == 0)
                    editingTextBox.TextChanged += HMCDTextBox_TextChanged;
                if (col == 3 || col == 4)
                {
                    editingTextBox.TextChanged += TimeTextBox_TextChanged;
                    editingTextBox.KeyDown += TimeTextBox_KeyDown;
                }
            }
        }
        // 「セル編集」終了
        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            var afterValue = dgv[e.ColumnIndex, e.RowIndex].Value;

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
            isPlanChanged = true;
            
        }
        // 「列2:CT」または「列3:本数」の変更時、終了時刻再計算
        private void DataGridViewPlan_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
            {
                CalculatePlan();
                isPlanChanged = true;
            }
        }
        // 「列2:CT」～「列5:終了時刻」の変更時、休憩時間と可動率再計算
        private void DataGridViewAchieve_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridViewAchieve.Rows[e.RowIndex];

            // CT、本数がゼロの場合に背景に赤色を設定
            if (row.Cells["AchieveCT"].ColumnIndex == e.ColumnIndex)
            {
                row.Cells["AchieveCT"].Style.BackColor = 
                    (row.Cells["AchieveCT"].Value.ToDoubleOrDefault() == 0.0) ? Color.LightCoral : Color.White;
            }
            if (row.Cells["Achieve本数"].ColumnIndex == e.ColumnIndex)
            {
                row.Cells["Achieve本数"].Style.BackColor = 
                    (row.Cells["Achieve本数"].Value.ToIntOrDefault() == 0) ? Color.LightCoral : Color.White;
            }
            // 列1～4がすべて入力済みかチェック
            if (1 <= e.ColumnIndex && e.ColumnIndex <= 4)
            {
                bool filled =
                    !string.IsNullOrWhiteSpace(Convert.ToString(row.Cells[1].Value)) &&
                    !string.IsNullOrWhiteSpace(Convert.ToString(row.Cells[2].Value)) &&
                    !string.IsNullOrWhiteSpace(Convert.ToString(row.Cells[3].Value)) &&
                    !string.IsNullOrWhiteSpace(Convert.ToString(row.Cells[4].Value));
                if (filled)
                {
                    dataGridViewAchieve.CellValueChanged -= DataGridViewAchieve_CellValueChanged;
                    CalculateAchieve(e.RowIndex, true);
                    dataGridViewAchieve.CellValueChanged += DataGridViewAchieve_CellValueChanged;
                }
                isAchieveChanged = true;
            }
            // 列5休憩時間が手動で更新された場合再計算
            if (row.Cells["Achieve休憩時間"].ColumnIndex == e.ColumnIndex)
            {
                CalculateAchieve(e.RowIndex, false);
                isAchieveChanged = true;
            }
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
            isPlanChanged = true;
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
            isPlanChanged = true;
        }
        // 「元に戻す」ボタン
        private void ButtonUndo_Click(object sender, EventArgs e)
        {
            if (undoStack == null) return;
            if (undoStack.Count == 0)
            {
                isPlanChanged = false;
                isAchieveChanged = false;
                return;
            }
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
        // データグリッド上のコントロール個別処理
        private void HMCDTextBox_TextChanged(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            tb.Text = tb.Text.ToUpper();
            tb.SelectionStart = tb.Text.Length; // カーソルを末尾へ
        }
        private void TimeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            backspacePressed = (e.KeyCode == Keys.Back);
        }
        private void TimeTextBox_TextChanged(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (beforeEditValue?.ToString() == tb.Text) return;

            // 入力中にカーソルが飛ばないようにする
            //int pos = tb.SelectionStart;

            // 2文字入力したら ":" を自動挿入
            if (tb.Text.Length == 2 && !tb.Text.Contains(':') && !backspacePressed)
            {
                tb.Text += ":";
                tb.SelectionStart = tb.Text.Length; // カーソルを末尾へ
            }
            // 3文字中に ":" が無かったらを自動挿入（バックスペース後）
            if (tb.Text.Length == 3 && !tb.Text.Contains(':') && !backspacePressed)
            {
                tb.Text = string.Concat(tb.Text.AsSpan(0, 2), ":", tb.Text.AsSpan(2, 1));
                tb.SelectionStart = tb.Text.Length; // カーソルを末尾へ
            }
            if (tb.Text.Length > 5)
            {
                tb.Text = tb.Text[..5];
                tb.SelectionStart = tb.Text.Length; // カーソルを末尾へ
            }
        }
        // データグリッド上のコントロールの中でEnterキーを押したときに
        // オーバーライドして割り込み → キーをキャッチして次（右側）のセルへ移動
        // 最終列の場合は次の行の最初の入力セルへ移動
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && dataGridViewAchieve.IsCurrentCellInEditMode)
            {
                dataGridViewAchieve.EndEdit();

                int currentRow = dataGridViewAchieve.CurrentCell.RowIndex;
                int currentColumn = dataGridViewAchieve.CurrentCell.ColumnIndex;
                // 列2:本数 → 列3:開始時刻 → 列4:終了時刻 → 次の行の列2:本数 へ
                if (2 <= currentColumn && currentColumn < 4)
                {
                    dataGridViewAchieve.CurrentCell = dataGridViewAchieve[currentColumn + 1, currentRow];
                    dataGridViewAchieve.BeginEdit(true);
                    return true;
                }
                else if (currentColumn == 4)
                {
                    int startCol = (dataGridViewAchieve[0, currentRow + 1].Value == null) ? 0 : 2;
                    dataGridViewAchieve.CurrentCell = dataGridViewAchieve[startCol, currentRow + 1];
                    dataGridViewAchieve.BeginEdit(true);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }



        /*
         * コマンドボタン関連
         * 
         */
        // 「計画印刷」と「計画保存」
        private void ButtonPlanPrint_Click(object sender, EventArgs e)
        {
            if (dataGridViewPlan.Rows.Count <= 1)
            {
                Common.MessageBox2.Show("計画リストを作成して下さい．", "計画印刷", 800, MessageBoxIcon.Warning);
                return;
            }
            string odcd = OdCdSetting.OdCd;
            if (!Common.CreateSaveFullPath(odcd, PlanDate, out string SaveFullPath))
            {
                if (MessageBox.Show($"既にファイルが存在しています．\n上書きしてもよろしいですか？\n\n{SaveFullPath}", "上書き確認",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    File.Delete(SaveFullPath);
                }
                else
                {
                    return;
                }
            }
            bool ret = Common.PrintPlan(ref dataGridViewPlan, odcd, PlanDate, textBoxPlan可動率.Text, OdCdSetting.FullPath, SaveFullPath);
            // 「計画保存」
            if (ret) ButtonPlanSave_Click(sender, e);
        }
        // 「計画クリア」
        private void ButtonPlanClear_Click(object sender, EventArgs e)
        {
            dataGridViewPlan.Rows.Clear();

            // UndoStachからdataGridViewPlanに関するアクションを削除
            Stack<UndoAction> newStack = new();
            while (undoStack.Count > 0)
            {
                var action = undoStack.Pop();
                if (action.dgv == dataGridViewPlan) continue;
                newStack.Push(action);
            }
            // 元の順序を保つためにもう一度反転
            undoStack = new Stack<UndoAction>(newStack);
            isPlanChanged = false;
        }
        // 「実績へコピー」
        private void ButtonPlanCopy_Click(object sender, EventArgs e)
        {
            if (dataGridViewPlan.Rows.Count <= 1)
            {
                Common.MessageBox2.Show("計画リストを作成して下さい．", "実績へコピー", 800, MessageBoxIcon.Warning);
                return;
            } else if (dataGridViewAchieve.Rows.Count > 1)
            {
                if (MessageBox.Show("既に実績リストにデータがあります．\n上書きしてもよろしいですか？", "上書き確認",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    ButtonAchieveClear_Click(sender, e);
                }
            }
            foreach (DataGridViewRow row in dataGridViewPlan.Rows)
            {
                if (row.IsNewRow) continue;
                int colCount = row.Cells.Count;
                object[] values = new object[colCount];
                for (int i = 0; i < colCount; i++)
                {
                    if (i < 3 || i > 6)
                        values[i] = row.Cells[i].Value;
                }
                dataGridViewAchieve.Rows.Add(values);
            }
        }
        // 「実績クリア」
        private void ButtonAchieveClear_Click(object sender, EventArgs e)
        {
            dataGridViewAchieve.Rows.Clear();

            // UndoStachからdataGridViewPlanに関するアクションを削除
            Stack<UndoAction> newStack = new();
            while (undoStack.Count > 0)
            {
                var action = undoStack.Pop();
                if (action.dgv == dataGridViewAchieve) continue;
                newStack.Push(action);
            }
            // 元の順序を保つためにもう一度反転
            undoStack = new Stack<UndoAction>(newStack);
            isAchieveChanged = false;
        }
        // 「計画登録」
        private void ButtonPlanSave_Click(object sender, EventArgs e)
        {
            // 計画登録更新
            if (isPlanChanged && dataGridViewPlan.Rows.Count > 1)
            {
                int totalQty = int.TryParse(textBoxPlanQty.Text, out int v) ? v : 0;
                double totalCT = double.TryParse(textBoxPlanCT.Text, out double w) ? w : 0.0;
                double totalOpe = double.TryParse(textBoxPlanOpe.Text, out double x) ? x : 0.0;
                double ava = double.TryParse(textBoxPlan可動率.Text, out double y) ? y : 0.0;
                var opt = new SaveOptions
                {
                    OdCd = OdCdSetting.OdCd,
                    PlanDate = PlanDate,
                    Type = "P",
                    開始時刻 = textBoxPlanStartTime.Text,
                    終了時刻 = textBoxPlanEndTime.Text,
                    昼稼働 = checkBoxPlanお昼稼働.Checked,
                    休憩稼働 = checkBoxPlan休憩稼働.Checked,
                    ピカピカ = checkBoxPlanピカピカ.Checked,
                    早昼 = checkBoxPlan早昼.Checked,
                    所感 = "",
                    合計本数 = totalQty,
                    CT合計時間 = totalCT,
                    合計稼働時間 = totalOpe,
                    可動率 = ava
                };
                DBAccessor.SaveDataGridView(ref dataGridViewPlan, opt);

                // 親フォームの再描画イベント発火
                IsUpdated?.Invoke(true);
            }
        }
        // 「実績登録」
        private void ButtonSaveClose_Click(object sender, EventArgs e)
        {
            if (dataGridViewAchieve.Rows.Count <= 1)
            {
                Common.MessageBox2.Show("データがありません．", "計画と実績", 800, MessageBoxIcon.Warning);
            }
            else if (!isAchieveChanged)
            {
                Common.MessageBox2.Show("変更ありません．．", "計画と実績", 800, MessageBoxIcon.Warning);
            }
            else
            {
                int totalQty = int.TryParse(textBoxAchieveQty.Text, out int v) ? v : 0;
                double totalCT = double.TryParse(textBoxAchieveCT.Text, out double w) ? w : 0.0;
                double totalOpe = double.TryParse(textBoxAchieveOpe.Text, out double x) ? x : 0.0;
                double ava = double.TryParse(textBoxAchieve可動率.Text, out double y) ? y : 0.0;
                var opt = new SaveOptions
                {
                    OdCd = OdCdSetting.OdCd,
                    PlanDate = PlanDate,
                    Type = "J",
                    開始時刻 = textBoxAchieveStartTime.Text,
                    終了時刻 = textBoxAchieveEndTime.Text,
                    昼稼働 = checkBoxAchieveお昼稼働.Checked,
                    休憩稼働 = checkBoxAchieve休憩稼働.Checked,
                    ピカピカ = checkBoxAchieveピカピカ.Checked,
                    早昼 = checkBoxAchieve早昼.Checked,
                    所感 = "",
                    合計本数 = totalQty,
                    CT合計時間 = totalCT,
                    合計稼働時間 = totalOpe,
                    可動率 = ava
                };
                DBAccessor.SaveDataGridView(ref dataGridViewAchieve, opt);
                
                // 親フォームの再描画イベント発火
                IsUpdated?.Invoke(true);

                Common.MessageBox2.Show("実績登録しました．", "計画と実績", 1000, MessageBoxIcon.Information);
            }
        }
        // 「閉じる」ボタン
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }



        /*
         * テキストボックス関連
         * 
         */
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



        /*
         * チェックボックス関連
         * 
         */
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



        /*
         * ビジネスロジック関連
         * 
         */
        /// <summary>
        /// このアプリケーションの重要なメソッド①Plan
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

                // CT、本数がゼロの場合に背景に赤色を設定
                row.Cells["PlanCT"].Style.BackColor = (ct == 0.0) ? Color.LightCoral : Color.White;
                row.Cells["Plan本数"].Style.BackColor = (本数 == 0.0) ? Color.LightCoral : Color.White;

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
            textBoxPlanCT.Text = (CT稼働時間 / 3600).ToString("N2");
            textBoxPlanOpe.Text = (計画稼働時間 / 3600).ToString("N2");
            textBoxPlanEndTime.Text = (dataGridViewPlan.Rows.Count > 1) ?
                dataGridViewPlan.Rows[^2].Cells["Plan終了時刻"].Value?.ToString() :
                textBoxPlanStartTime.Text;
        }

        /// <summary>
        /// このアプリケーションの重要なメソッド②Achieve
        /// 
        /// 各行の入力後に休憩時間と可動率を計算
        /// 最終終了時刻までのサマリー結果を取得し再表示
        /// </summary>
        private void CalculateAchieve(int rowindex, bool 休憩時間自動算出)
        {
            var targetRow = dataGridViewAchieve.Rows[rowindex]; // ここで通常処理
            // 計画CTを取得
            string ctStr = targetRow.Cells["AchieveCT"].Value?.ToString();
            if (!double.TryParse(ctStr, out double 今回CT))
            {
                MessageBox.Show("CTの形式が不正です。", "可動率計算", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 本数を取得
            double 今回本数 = (targetRow.Cells["Achieve本数"].Value != null) ? Convert.ToDouble(targetRow.Cells["Achieve本数"].Value) : 0.0;
            // 開始時刻を算出
            string startTimeStr = targetRow.Cells["Achieve開始時刻"].Value?.ToString();
            if (!DateTime.TryParse(startTimeStr, out DateTime startTime))
            {
                MessageBox.Show("開始時刻の形式が不正です。", "可動率計算", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 終了時刻を算出
            string endTimeStr = targetRow.Cells["Achieve終了時刻"].Value?.ToString();
            if (!DateTime.TryParse(endTimeStr, out DateTime endTime))
            {
                MessageBox.Show("終了時刻の形式が不正です。", "可動率計算", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 休憩時間を算出
            double 今回休憩;
            if (休憩時間自動算出)
            {
                今回休憩 = 休憩時間算出(startTime, endTime,
                    checkBoxAchieveお昼稼働.Checked, checkBoxAchieve休憩稼働.Checked,
                    checkBoxAchieveピカピカ.Checked, checkBoxAchieve早昼.Checked);
                targetRow.Cells["Achieve休憩時間"].Value = 今回休憩;
            }
            else
            {
                今回休憩 = targetRow.Cells["Achieve休憩時間"].Value.ToDoubleOrDefault();
            }
            // １行目の開始時刻をテキストボックスの開始時刻にセット
            if (rowindex == 0)
            {
                if (textBoxPlanStartTime.Text != startTimeStr) textBoxPlanStartTime.Text = startTimeStr;
            }
            // 次行の開始時刻を今回の終了時刻にセット
            if (!dataGridViewAchieve.Rows[rowindex + 1].IsNewRow)
            {
                dataGridViewAchieve[targetRow.Cells["Achieve開始時刻"].ColumnIndex, rowindex + 1].Value = targetRow.Cells["Achieve終了時刻"].Value;
            }
            // 開始終了時刻から可動率を算出
            TimeSpan span = endTime.TimeOfDay - startTime.TimeOfDay;
            double 今回可動率 = (span.TotalSeconds > 0) ? (今回CT * 今回本数) / (span.TotalSeconds - 今回休憩) * 100 : 0.0;
            targetRow.Cells["Achieve可動率"].Value = 今回可動率.ToString("N0");

            // ループしてトータル時間を算出（開始時刻・終了時刻が入力された行まで）
            double 合計本数 = 0.0;
            double CT稼働時間 = 0.0;
            double 計画稼働時間 = 0.0;
            string 最終終了時刻 = string.Empty;
            for (int i = 0; i < dataGridViewAchieve.Rows.Count; i++)
            {
                var row = dataGridViewAchieve.Rows[i]; // ここで通常処理

                if (row.IsNewRow) continue;         // 新規行はスキップ

                string str1 = row.Cells["Achieve開始時刻"].Value?.ToString();
                string str2 = row.Cells["Achieve終了時刻"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(str1) || string.IsNullOrWhiteSpace(str2))
                    continue; // 開始終了時刻のどちらかが空ならスキップ

                最終終了時刻 = row.Cells["Achieve終了時刻"].Value?.ToString();

                // サマリー
                double ct = row.Cells["AchieveCT"].Value.ToDoubleOrDefault();
                int 本数 = row.Cells["Achieve本数"].Value.ToIntOrDefault();
                double 可動率 = row.Cells["Achieve可動率"].Value.ToDoubleOrDefault();
                合計本数 += 本数;
                CT稼働時間 += ct * 本数;
                計画稼働時間 += (可動率 > 0) ? ct * 本数 * (1 / 可動率 * 100) : 0;
            }
            textBoxAchieveQty.Text = 合計本数.ToString("#,0");
            textBoxAchieveCT.Text = (CT稼働時間 / 3600).ToString("N2");
            textBoxAchieveOpe.Text = (計画稼働時間 / 3600).ToString("N2");
            textBoxAchieve可動率.Text = (CT稼働時間 / 計画稼働時間 * 100).ToString("N0");
            textBoxAchieveEndTime.Text = 最終終了時刻;
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
