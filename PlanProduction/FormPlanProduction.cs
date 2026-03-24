using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace PlanProduction
{
    public partial class FormPlanProduction : Form
    {

        Stack<UndoAction> undoStack = new Stack<UndoAction>();  // 元に戻すスタック
        object beforeEditValue = null;                          // Undoセル編集用の「編集中の値」
        // ドラッグドロップ用
        private Point dragStartPoint;
        private DataGridViewCell dragSourceCell;
        private bool isRowHeaderDrag = false;

        // DataTable を保持するフィールドを作る
        private DataTable dt = new DataTable();

        public FormPlanProduction()
        {
            InitializeComponent();

            // 設定ファイル読み込み
            LoadAppSettings();

            this.KeyPreview = true;                     // フォームでキーイベントを受け取る設定

            dataGridView1.AllowDrop = true;             // ドラッグドロップ有効
            dataGridView1.AllowUserToAddRows = false;   // 新規行追加なし

            // サンプルデータ作成
            dt.Columns.Add("HMCD", typeof(string));
            dt.Columns.Add("3/17", typeof(int));
            dt.Columns.Add("3/18", typeof(int));
            dt.Columns.Add("3/19", typeof(int));
            dt.Columns.Add("CT", typeof(int));
            dt.Rows.Add("129486-59150B", 30, 0, 30, 45);
            dt.Rows.Add("T1855-70743", 40, 40, 40, 120);
            dt.Rows.Add("V1311-69223", 20, 0, 0, 125);

            // 行ヘッダーの設定（品番の幅と背景色）
            dataGridView1.RowHeadersWidth = 140;
            dataGridView1.EnableHeadersVisualStyles = false; // Visual Styles を無効化し背景色を反映させる
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.LightGray;
            // データの設定
            dataGridView1.DataSource = dt;
            UpdateChart();

            // イベント登録
            dataGridView1.MouseDown += DataGridView1_MouseDown;
            dataGridView1.MouseMove += DataGridView1_MouseMove;
            dataGridView1.DragOver += DataGridView1_DragOver;
            dataGridView1.DragDrop += DataGridView1_DragDrop;
        }

        // アプリケーション設定ファイルの読み込み
        private void LoadAppSettings()
        {
            string fileName = Common.CONFIG_FILE_LS;
            //var config = new AppSettings();

            //if (File.Exists(fileName))
            //{
            //    string jsonString = File.ReadAllText(@fileName);
            //    config = JsonSerializer.Deserialize<AppSettings>(jsonString);
            //    Common.OdCd = config.OdCd;
            //    Common.KtCd = config.KtCd;
            //    Common.Ava = config.Ava;
            //    Console.WriteLine($"JSONファイルを読み込みました:\n{jsonString}");
            //}
        }

        // データバインド完了後に行ヘッダーを設定しないといけない
        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // データ列の設定（幅、右揃え、ソート機能なし）
            for (int col = 0; col < dataGridView1.Columns.Count; col++)
            {
                dataGridView1.Columns[col].Width = 50;
                dataGridView1.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;   // データの右寄せ
                dataGridView1.Columns[col].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;   // 列ヘッダー右寄せ
                dataGridView1.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;                       // ソート機能を無効化
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.HeaderCell.Value = row.Cells[0].Value?.ToString();
            }
            dataGridView1.Columns[0].Visible = false; // 1列目は非表示
        }

        // フォームのショットカットイベント
        private void FormPlanProduction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                ButtonUndo_Click(sender, e);    // ボタンの「元に戻す」を呼び出す
                e.Handled = true;
            }
            if (e.KeyCode == Keys.D)
            {
                ButtonDelete_Click(sender, e);  // ボタンの「行削除」を呼び出す
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();             // アプリケーション全体を終了
                e.Handled = true;
            }
        }

        // アプリケーションの終了
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // 「元に戻す」ボタン
        private void ButtonUndo_Click(object sender, EventArgs e)
        {
            if (undoStack.Count == 0)
                return;

            var action = undoStack.Pop();

            switch (action.Type)
            {
                case UndoType.RowMove:
                    // 「行移動」を元に戻す（DataTableを弄らないといけない）
                    //var row = dataGridView1.Rows[action.TargetRow];
                    //dataGridView1.Rows.RemoveAt(action.TargetRow);
                    //dataGridView1.Rows.Insert(action.SourceRow, row);
                    MoveRowInDataTable(action.SourceRow, action.TargetRow);
                    break;

                case UndoType.CellSwap:
                    // 「セル入れ替え」を元に戻す
                    dataGridView1[action.Col1, action.Row1].Value = action.Value1;
                    dataGridView1[action.Col2, action.Row2].Value = action.Value2;
                    break;

                case UndoType.CellEdit:
                    // 「セル編集」を元に戻す
                    dataGridView1[action.EditCol, action.EditRow].Value = action.OldValue;
                    break;

                case UndoType.RowDelete:
                    // 「行削除」を元に戻す（DataTableを弄らないといけない）
                    //dataGridView1.Rows.Insert(action.DeletedRowIndex, action.DeletedRowValues);
                    //int idx = action.DeletedRowIndex;
                    //dataGridView1.Rows[idx].HeaderCell.Value = action.DeletedRowHeader;
                    DataRow dgvRow = dt.NewRow();
                    dgvRow.ItemArray = action.DeletedRowValues;
                    dt.Rows.InsertAt(dgvRow, action.DeletedRowIndex);
                    dataGridView1.DataSource = dt;

                    // 行ヘッダーを再設定
                    foreach (DataGridViewRow dgRow in dataGridView1.Rows)
                    {
                        if (!dgRow.IsNewRow)
                            dgRow.HeaderCell.Value = dgRow.Cells["HMCD"].Value?.ToString();
                    }

                    // 移動先を選択
                    dataGridView1.CurrentCell = dataGridView1[1, action.DeletedRowIndex];

                    break;
            }
            UpdateChart();
        }

        // 「行削除」ボタン
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentRow.Index;

            if (rowIndex < 0 || rowIndex >= dataGridView1.Rows.Count)
                return;

            var row = dataGridView1.Rows[rowIndex];

            // 行の値を配列に保存
            object[] values = new object[row.Cells.Count];
            for (int i = 0; i < row.Cells.Count; i++)
                values[i] = row.Cells[i].Value;

            // Undo 情報を積む
            var action = new UndoAction()
            {
                Type = UndoType.RowDelete,
                DeletedRowIndex = rowIndex,
                DeletedRowHeader = row.HeaderCell.Value?.ToString(),
                DeletedRowValues = values
            };
            undoStack.Push(action);

            // 実際に削除（dtも削除される）
            dataGridView1.Rows.RemoveAt(rowIndex);
            UpdateChart();
        }

        // 「行移動」「セル入れ替え」ドラッグ開始判定
        private void DataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            dragStartPoint = e.Location;

            var hit = dataGridView1.HitTest(e.X, e.Y);

            if (hit.RowIndex >= 0)
            {
                if (hit.Type == DataGridViewHitTestType.RowHeader)
                {
                    // 行ヘッダー → 「行移動」モード
                    isRowHeaderDrag = true;
                    dragSourceCell = dataGridView1[0, hit.RowIndex]; // 行番号だけ覚えておけばOK

                    // マウスダウンした行を選択
                    dataGridView1.ClearSelection(); // 他の選択を解除
                    dataGridView1.Rows[hit.RowIndex].Selected = true;
                }
                else if (hit.ColumnIndex >= 0)
                {
                    // 通常セル → 「セル入れ替え」モード
                    isRowHeaderDrag = false;
                    dragSourceCell = dataGridView1[hit.ColumnIndex, hit.RowIndex];
                }
            }
        }

        // 「行移動」「セル入れ替え」マウス移動
        private void DataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragSourceCell == null)
                return;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (Math.Abs(e.X - dragStartPoint.X) > SystemInformation.DragSize.Width ||
                    Math.Abs(e.Y - dragStartPoint.Y) > SystemInformation.DragSize.Height)
                {
                    dataGridView1.DoDragDrop(dragSourceCell, DragDropEffects.Move);
                }
            }
        }

        private void DataGridView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        // 「行移動」「セル入れ替え」ドロップ処理
        private void DataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            if (dragSourceCell == null)
                return;

            Point clientPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));
            var hit = dataGridView1.HitTest(clientPoint.X, clientPoint.Y);

            if (hit.RowIndex < 0)
                return;

            if (isRowHeaderDrag)
            {
                // -------------------------
                // 行ヘッダー → 「行移動」
                // -------------------------
                int sourceRow = dragSourceCell.RowIndex;
                int targetRow = hit.RowIndex;

                if (sourceRow == targetRow)
                    return;

                // Undo情報スタック
                var action = new UndoAction()
                {
                    Type = UndoType.RowMove,
                    SourceRow = sourceRow,
                    TargetRow = targetRow
                };
                undoStack.Push(action);

                // 「行移動」（DataTableを弄らないといけない）
                MoveRowInDataTable(sourceRow, targetRow);
                //var row = dataGridView1.Rows[sourceRow];
                //dataGridView1.Rows.RemoveAt(sourceRow);
                //dataGridView1.Rows.Insert(targetRow, row);

                dataGridView1.ClearSelection();
                dataGridView1.Rows[targetRow].Selected = true;
            }
            else
            {
                // -------------------------
                // 通常セル → 「セル入れ替え」
                // -------------------------
                if (hit.ColumnIndex < 0)
                    return;

                DataGridViewCell targetCell = dataGridView1[hit.ColumnIndex, hit.RowIndex];

                if (targetCell == dragSourceCell)
                    return;

                // Undo情報スタック
                var action = new UndoAction()
                {
                    Type = UndoType.CellSwap,
                    Row1 = dragSourceCell.RowIndex,
                    Col1 = dragSourceCell.ColumnIndex,
                    Value1 = dragSourceCell.Value,
                    Row2 = targetCell.RowIndex,
                    Col2 = targetCell.ColumnIndex,
                    Value2 = targetCell.Value
                };
                undoStack.Push(action);

                // 実際に値の入れ替え（dtも入れ替えされる）
                object temp = dragSourceCell.Value;
                dragSourceCell.Value = targetCell.Value;
                targetCell.Value = temp;

                dataGridView1.CurrentCell = targetCell;
            }
            dragSourceCell = null;
            UpdateChart();
        }

        // 「行移動」「行移動を元に戻す」（DataTableを弄らないといけない）
        private void MoveRowInDataTable(int sourceIndex, int targetIndex)
        {
            if (sourceIndex == targetIndex)
                return;

            // 行データをコピー
            DataRow row = dt.NewRow();
            row.ItemArray = dt.Rows[sourceIndex].ItemArray;

            // 元の行を削除
            dt.Rows.RemoveAt(sourceIndex);

            // 挿入位置を調整（削除の影響を考慮）
            //if (targetIndex > sourceIndex)
            //    targetIndex--;

            // 新しい位置に挿入
            dt.Rows.InsertAt(row, targetIndex);

            // DataGridView を更新
            dataGridView1.DataSource = dt;

            // 行ヘッダーを再設定
            foreach (DataGridViewRow dgRow in dataGridView1.Rows)
            {
                if (!dgRow.IsNewRow)
                    dgRow.HeaderCell.Value = dgRow.Cells["HMCD"].Value?.ToString();
            }

            // 移動先を選択
            dataGridView1.CurrentCell = dataGridView1[1, targetIndex];
        }

        // 「セル編集」開始
        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            beforeEditValue = dataGridView1[e.ColumnIndex, e.RowIndex].Value;
        }

        // 「セル編集」終了
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var afterValue = dataGridView1[e.ColumnIndex, e.RowIndex].Value;

            // 値が変わっていなければ何もしない
            if ((beforeEditValue == null && afterValue == null) ||
                (beforeEditValue != null && beforeEditValue.Equals(afterValue)))
            {
                return;
            }

            // Undo 情報を積む
            var action = new UndoAction()
            {
                Type = UndoType.CellEdit,
                EditRow = e.RowIndex,
                EditCol = e.ColumnIndex,
                OldValue = beforeEditValue,
                NewValue = afterValue
            };

            undoStack.Push(action);
            UpdateChart();
        }

        // グラフの更新
        private void UpdateChart()
        {
            try
            {
                //chart1.Series.Clear();
                //chart1.ChartAreas.Clear();

                // データ集計
                //var summarized = SumQtyTimesCT_ByDate(ref dt, 可動率);

                // Series 作成
                //Series series = new Series("日付別合計")
                //{
                //    ChartType = SeriesChartType.Bar,    // 棒グラフ
                //    XValueMember = "Date",              // 横軸
                //    YValueMembers = "Total",            // 縦軸
                //    IsValueShownAsLabel = true,         // 棒に値を表示
                //    ToolTip = "#VALX : #VAL",           // ホバーでツールチップ
                //};
                //series.SmartLabelStyle.Enabled = false; // SmartLabel の自動回避を無効化

                // Areas作成
                //var area = new ChartArea("Ava");
                //area.AxisY.Maximum = 10;                // 縦軸の最大値を固定（10時間）
                //area.AxisX.IsReversed = true;           // X軸の上下反転
                //area.AxisX.MajorGrid.Enabled = false;   // X軸のグリッド線を消す
                //area.AxisX.MajorTickMark.Enabled = false;//日付の横の線を消す

                //chart1.Series.Add(series);
                //chart1.ChartAreas.Add(area);
                //chart1.DataSource = summarized;
                //chart1.DataBind();
                // 最大値を超す場合を考慮
                //foreach (var p in chart1.Series[0].Points)
                //{
                //    var v = p.YValues[0];
                //    if (v < 8)
                //    {
                //        p["BarLabelStyle"] = "Outside"; // 短い棒は外に
                //        p.LabelForeColor = Color.Black;
                //    }
                //    else if (v <= 11)
                //    {
                //        p["BarLabelStyle"] = "Center";  // 11時間以下は棒の中央
                //        p.LabelForeColor = Color.White;
                //    }
                //    else
                //    {
                //        p["BarLabelStyle"] = "Bottom";  // その他は棒の最後に表示
                //        p.LabelForeColor = Color.White;
                //    }
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("グラフ表示中にエラー: " + ex.Message);
            }
        }

        private void BtnLoadChart_Click(object sender, EventArgs e)
        {
            UpdateChart();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            FormSrttings fm = new FormSrttings();
            fm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new FormPlanEntry("6031A", "鈴木", "70");
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var frm = new FormOrderList();
            frm.Show();
        }
    }// FormPlanProduction

}

