using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlanProduction
{
    public partial class FormCTMaster : Form
    {
        private FormConfig settings;
        private readonly OdCdSetting OdCdSetting;
        private DataTable dtKM5020 = new();         // KM5020
        private DataTable dtKM5030 = new();         // KM5030
        private DataTable dtD0410 = new();          // D0410

        // イベント制御フラグ
        private bool _syncing = false;  // Scrollイベントの同期制御フラグ
        private bool _changing = false; // CurrentCellChangedイベントの同期制御フラグ

        // --- 変更された行を記録するセット ---
        private readonly HashSet<int> changedCTRows = [];
        private readonly HashSet<int> changedDTRows = [];

        public FormCTMaster(OdCdSetting OdCdSetting)
        {
            InitializeComponent();

            this.KeyPreview = true;                     // フォームでキーイベントを受け取る設定

            this.OdCdSetting = OdCdSetting;

            // イベント登録
            // 標準作業時間マスタ
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            dataGridView1.DataBindingComplete += DataGridView1_DataBindingComplete;
            dataGridView1.RowPostPaint += DataGridView_RowPostPaint;
            dataGridView1.Scroll += DataGridView1_Scroll;
            dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;
            dataGridView1.KeyDown += DataGridView_KeyDown;
            dataGridView1.CellMouseDown += DataGridView_CellMouseDown;
            // 段取りマスタ
            dataGridView3.CellValueChanged += DataGridView3_CellValueChanged;
            dataGridView3.DataBindingComplete += DataGridView3_DataBindingComplete;
            dataGridView3.RowPostPaint += DataGridView_RowPostPaint;
            dataGridView3.Scroll += DataGridView3_Scroll;
            dataGridView3.CurrentCellChanged += DataGridView3_CurrentCellChanged;
            dataGridView3.EditingControlShowing += DataGridView3_EditingControlShowing;
            dataGridView3.KeyDown += DataGridView_KeyDown;
            dataGridView3.CellMouseDown += DataGridView_CellMouseDown;
        }

        private void FormCTMasterMainte_Load(object sender, EventArgs e)
        {
            // フォームの状態を復元
            settings = Common.FormSettingsLoad();
            string key = this.Name;
            if (settings.Forms.TryGetValue(key, out FormSettings s))
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(s.X, s.Y);
                this.Size = new Size(s.Width, s.Height);
                this.splitContainer1.SplitterDistance = s.SplitterMainDistance;
                this.splitContainer2.SplitterDistance = s.SplitterSubVerticalDistance;
            }

            this.Text = $"[生産計画] CTマスタメンテナンス - 【{OdCdSetting.OdCd}】 {DataStore.M0300Map[OdCdSetting.OdCd]} - {OdCdSetting.KtCd}";
            labelCTMasterTitle.Text = " [KM5030] 標準作業時間マスタ ";
            labelDTMasterTitle.Text = " [KM5020] 段取りマスタ ";
            labelReadTitle.Text = "未登録品番一覧";

            // 抽出条件を生成
            var condition = "('" + OdCdSetting.OdCd + OdCdSetting.KtCd + "')";
            // 標準作業時間マスタ取得
            DBAccessor.ReadKM5030(ref dtKM5030, OdCdSetting.OdCd, OdCdSetting.KtCd);
            // 段取りマスタ取得
            DBAccessor.ReadKM5020(ref dtKM5020, OdCdSetting.OdCd, OdCdSetting.KtCd);
            // 未登録品番一覧のデータ取得
            //if (OdCdSetting.OdCd.Substring(0, 4) == "6031")
            //{
            //    // 手配品番マスタ取得
            //    DBAccessor.ReadD0440ConvertToMaster(ref dtD0410, OdCdSetting.OdCd, OdCdSetting.KtCd);
            //}
            if (OdCdSetting.OdCd == "60460")
            {
                // 手配品番マスタ取得
                DBAccessor.ReadD0410ConvertToMaster(ref dtD0410, OdCdSetting.OdCd, OdCdSetting.KtCd, "EWUBEND");
            }
            else
            {
                // 手配品番マスタ取得
                DBAccessor.ReadD0410ConvertToMaster(ref dtD0410, OdCdSetting.OdCd, OdCdSetting.KtCd);
            }

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.DataSource = dtKM5030;

            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView3.DataSource = dtKM5020;

            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView2.DataSource = dtD0410;
            if (dtD0410.Rows.Count > 0)
            {
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                foreach (DataGridViewColumn col in dataGridView2.Columns) col.ReadOnly = true;
            }
            toolStripStatusLabel1.Text = $"標準作業時間マスタ {dtKM5030.Rows.Count}件 / 段取りマスタ {dtKM5020.Rows.Count}件";
        }

        private void FormCTMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            settings = Common.FormSettingsLoad(); // 他のフォームで変更された可能性があるので、最新の状態を読み込む
            string key = this.Name;
            if (!settings.Forms.ContainsKey(key)) settings.Forms[key] = new FormSettings();
            var s = settings.Forms[key];
            s.X = this.Location.X;
            s.Y = this.Location.Y;
            s.Width = this.Width;
            s.Height = this.Height;
            s.SplitterMainDistance = this.splitContainer1.SplitterDistance;
            s.SplitterSubVerticalDistance = this.splitContainer2.SplitterDistance;
            Common.FormSettingsSave(settings);
        }

        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtKM5030.Rows.Count > 0)
            {
                foreach (DataGridViewColumn col in dataGridView1.Columns) col.ReadOnly = true;
                dataGridView1.Columns["ODCD"].Visible = false;
                dataGridView1.Columns["WKGRCD"].Visible = false;
                dataGridView1.Columns["HMCD"].HeaderText = "品番";
                dataGridView1.Columns["VALDTF"].Visible = false;
                dataGridView1.Columns["WKSEQ"].HeaderText = "順序";
                var ct = dataGridView1.Columns["CT"];
                ct.HeaderText = "　　CT";                                                   // 列幅自動調整に対抗
                ct.ReadOnly = false;                                                        // （変更可）
                ct.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;   // データの右寄せ
                ct.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;   // 列ヘッダー右寄せ
                ct.SortMode = DataGridViewColumnSortMode.NotSortable;                       // ソート機能を無効化
                ct.DefaultCellStyle.Format = "N1";                                          // 小数点以下1桁
                dataGridView1.Columns["NOTE"].ReadOnly = false;                             // （変更可）
                dataGridView1.Columns["INSTID"].Visible = false;
                dataGridView1.Columns["INSTDT"].Visible = false;
                dataGridView1.Columns["UPDTID"].Visible = false;
                dataGridView1.Columns["UPDTDT"].Visible = false;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                int.TryParse(r.Cells["WKSEQ"].Value?.ToString(), out int wkseq);
                if (wkseq > 10)
                {
                    r.Cells["HMCD"].Style.BackColor = Color.WhiteSmoke;
                    r.Cells["WKSEQ"].Style.BackColor = Color.WhiteSmoke;
                }
                double.TryParse(r.Cells["CT"].Value.ToString(), out double ct);
                if (ct == 0)
                {
                    r.Cells["CT"].Style.BackColor = Color.LightCoral;
                }
            }
        }
        private void DataGridView3_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtKM5020.Rows.Count > 0)
            {
                foreach (DataGridViewColumn col in dataGridView3.Columns) col.ReadOnly = true;
                dataGridView3.Columns["ODCD"].Visible = false;
                dataGridView3.Columns["WKGRCD"].Visible = false;
                dataGridView3.Columns["HMCD"].HeaderText = "品番";
                dataGridView3.Columns["VALDTF"].Visible = false;
                dataGridView3.Columns["WKSEQ"].HeaderText = "順序";
                dataGridView3.Columns["WORK"].ReadOnly = false;
                dataGridView3.Columns["WORK"].HeaderText = "段取内容";
                var ct = dataGridView3.Columns["SETUPTMMP"];
                ct.HeaderText = "段取り時間";
                ct.ReadOnly = false;                                                        // 変更可能
                ct.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;   // データの右寄せ
                ct.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;   // 列ヘッダー右寄せ
                ct.SortMode = DataGridViewColumnSortMode.NotSortable;                       // ソート機能を無効化
                ct.DefaultCellStyle.Format = "N1";                                          // 小数点以下1桁
                dataGridView3.Columns["SETUPTMSP"].Visible = false;
                dataGridView3.Columns["NOTE"].ReadOnly = false;                             // 変更可能
                dataGridView3.Columns["INSTID"].Visible = false;
                dataGridView3.Columns["INSTDT"].Visible = false;
                dataGridView3.Columns["UPDTID"].Visible = false;
                dataGridView3.Columns["UPDTDT"].Visible = false;

                dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            foreach (DataGridViewRow r in dataGridView3.Rows)
            {
                int.TryParse(r.Cells["WKSEQ"].Value?.ToString(), out int wkseq);
                if (wkseq > 10)
                {
                    r.Cells["HMCD"].Style.BackColor = Color.WhiteSmoke;
                    r.Cells["WKSEQ"].Style.BackColor = Color.WhiteSmoke;
                }
            }
        }


        // （おまけ処理）行番号を付ける
        private void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string rowNumber = (e.RowIndex + 1).ToString();
            // 行ヘッダーの描画範囲
            var headerBounds = new Rectangle(
                e.RowBounds.Left,
                e.RowBounds.Top,
                dataGridView1.RowHeadersWidth,
                e.RowBounds.Height);
            // 行番号を描画
            TextRenderer.DrawText(
                e.Graphics,
                rowNumber,
                dataGridView1.Font,
                headerBounds,
                Color.Black,
                TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
        }

        // （おまけ処理）同期スクロール
        private void DataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if (dataGridView1.FirstDisplayedScrollingRowIndex > dataGridView3.Rows.Count - 1) return;
            if (_syncing) return;
            _syncing = true;

            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                dataGridView3.FirstDisplayedScrollingRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex;
            }

            _syncing = false;
        }
        private void DataGridView3_Scroll(object sender, ScrollEventArgs e)
        {
            if (_syncing) return;
            _syncing = true;

            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView3.FirstDisplayedScrollingRowIndex;
            }

            _syncing = false;
        }

        // （おまけ処理）セル選択の同期
        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (_changing) return;
            int row = dataGridView1.CurrentCell?.RowIndex ?? -1;
            if (row >= 0 && dataGridView3.Rows.Count > row)
            {
                _changing = true;
                dataGridView3.CurrentCell = dataGridView3.Rows[row].Cells["HMCD"];
            }
            _changing = false;
        }
        private void DataGridView3_CurrentCellChanged(object sender, EventArgs e)
        {
            if (_changing) return;
            int row = dataGridView3.CurrentCell?.RowIndex ?? -1;
            if (row >= 0 && dataGridView1.Rows.Count > row)
            {
                _changing = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[row].Cells["HMCD"];
            }
            _changing = false;

            // 日本語制御
            if (dataGridView3.CurrentCell != null)
            {
                if (dataGridView3.CurrentCell.ColumnIndex >= 5)
                {
                    dataGridView3.BeginEdit(true);  // セルの編集開始を強制 → EditingControlShowing を発生させIMEを変更
                    dataGridView3.EndEdit();        // セルを通常の状態に戻す
                }
            }
        }

        // （おまけ処理）日本語制御
        private void DataGridView3_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox tb)
            {
                int col = dataGridView3.CurrentCell.ColumnIndex;
                if (col == 5 || col == 8)
                    tb.ImeMode = ImeMode.Hiragana;   // ← 日本語入力ON
                else
                    tb.ImeMode = ImeMode.Disable;    // ← 日本語入力OFF
            }
        }

        // （おまけ処理）右クリックでクリップボードにコピー
        private void DataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var dgv = (DataGridView)sender;
                if (dgv.SelectedRows.Count > 0)
                {
                    List<string> selectedData = new List<string>();
                    foreach (DataGridViewRow selectedRow in dgv.SelectedRows)
                    {
                        List<string> rowData = new List<string>();
                        foreach (DataGridViewCell cell in selectedRow.Cells)
                        {
                            rowData.Add((string.IsNullOrEmpty(cell.Value.ToString())) ? "" : cell.Value.ToString());
                        }
                        selectedData.Add(string.Join("\t", rowData));
                    }
                    Clipboard.SetText(string.Join("\n", selectedData));
                    toolStripStatusLabel1.Text = $"選択行をコピーしました．";
                }
                else
                {
                    int col = dgv.CurrentCell.ColumnIndex;
                    string ht = dgv.Columns[col].HeaderText;
                    string val = dgv[col, e.RowIndex].Value.ToString();
                    if (val != "")
                    {
                        Clipboard.SetText(val);
                        toolStripStatusLabel1.Text = $"{ht} [ {val} ] をコピーしました．";
                    }
                }
            }
        }

        // （おまけ処理）Ctrl + V で貼り付け
        private async void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                var dgv = (DataGridView)sender;
                this.UseWaitCursor = true;
                Application.DoEvents();
                await Task.Delay(300); // 0.3秒待ってから実行
                try
                {
                    PasteClipboardToDataGridView(dgv);
                }
                finally
                {
                    this.UseWaitCursor = false;
                }
                e.Handled = true;
            }
        }
        private void PasteClipboardToDataGridView(DataGridView dgv)
        {
            if (Clipboard.ContainsText())
            {
                string clipboardText = Clipboard.GetText();
                string[] lines = clipboardText.Split('\n');

                int startRow = dgv.CurrentCell.RowIndex;
                int startCol = dgv.CurrentCell.ColumnIndex;

                for (int i = 0; i < lines.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(lines[i])) continue;

                    string[] cells = lines[i].TrimEnd('\r').Split('\t');

                    int row = startRow + i;
                    if (row >= dgv.Rows.Count) dgv.Rows.Add();

                    for (int j = 0; j < cells.Length; j++)
                    {
                        int col = startCol + j;
                        if (col >= dgv.Columns.Count) break;

                        if (!dgv[col, row].ReadOnly)
                        {
                            dgv[col, row].Value = cells[j];
                        }
                    }
                }
            }
        }

        // （おまけ処理）キーボードショートカット
        private void FormCTMaster_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (textBoxHmCd.Text.Length > 0)
                {
                    ButtonClearFilter_Click(null, null);
                }
                else
                {
                    Close();
                }
            }
            else if (e.KeyCode == Keys.F9)
            {
                ButtonSaveClose_Click(null, null);
            }
            else if (e.Alt && e.KeyCode == Keys.C)
            {
                ButtonAddCT_Click(null, null);
            }
            else if (e.Alt && e.KeyCode == Keys.A)
            {
                ButtonAddHMCD_Click(null, null);
            }
            else if (e.Alt && e.KeyCode == Keys.S)
            {
                buttonAddWKSEQ_Click(null, null);
            }
        }

        // （おまけ処理）品番フィルタリング＆貼付＆クリア
        private void TextBoxHmCd_TextChanged(object sender, EventArgs e)
        {
            dtKM5030.DefaultView.RowFilter = $"HMCD LIKE '{textBoxHmCd.Text}%'";
            dtKM5020.DefaultView.RowFilter = $"HMCD LIKE '{textBoxHmCd.Text}%'";
            dtD0410.DefaultView.RowFilter = $"HMCD LIKE '{textBoxHmCd.Text}%'";
        }
        private void ButtonPasteFilter_Click(object sender, EventArgs e)
        {
            textBoxHmCd.Text = Clipboard.GetText().Replace("\r\n", "");
        }
        private void ButtonClearFilter_Click(object sender, EventArgs e)
        {
            textBoxHmCd.Text = "";
            textBoxHmCd.Focus();
        }

        // （おまけ処理）未登録品番フィルタリング
        private void TextBoxHmCd2_TextChanged(object sender, EventArgs e)
        {
            dtD0410.DefaultView.RowFilter = $"HMCD LIKE '{textBoxHmCd2.Text}%'";
        }
        private void ButtonFilterClear2_Click(object sender, EventArgs e)
        {
            textBoxHmCd2.Text = "";
            textBoxHmCd2.Focus();
        }

        // （おまけ処理）変更されたセルに対し ①行番号の保存、②背景色ハイライトを行い、ステータスへの変更件数表示を行う
        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            changedCTRows.Add(e.RowIndex);  // 行番号を記録（HashSetで重複なし）
            dataGridView1.CurrentCell.Style.BackColor = Color.LightYellow; // 変更があったことを視覚的に示す
            toolStripStatusLabel1.Text = $"変更された行 [KM5030]:{changedCTRows.Count}件 / [KM5020]:{changedDTRows.Count}件";
        }
        private void DataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            changedDTRows.Add(e.RowIndex); // 行番号を記録（HashSetで重複なし）
            dataGridView3.CurrentCell.Style.BackColor = Color.LightYellow; // 変更があったことを視覚的に示す
            toolStripStatusLabel1.Text = $"変更された行 [KM5030]:{changedCTRows.Count}件 / [KM5020]:{changedDTRows.Count}件";
        }

        // 「CTに追加」ボタン
        private void ButtonAddCT_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count <= 0)
            {
                MessageBox.Show("追加対象の品番を選択してください．");
                return;
            }
            int insertCnt = 0;
            foreach (DataGridViewCell cel in dataGridView2.SelectedCells)
            {
                // 標準作業時間マスタに登録
                var newct = dtKM5030.NewRow();
                newct["ODCD"] = OdCdSetting.OdCd;
                newct["WKGRCD"] = OdCdSetting.KtCd;
                newct["HMCD"] = cel.Value;
                newct["VALDTF"] = DateTime.Today;
                newct["WKSEQ"] = Common.DEFAULT_WKSEQ;
                newct["CT"] = 0;
                newct["INSTID"] = Common.UserId;
                newct["INSTDT"] = DateTime.Now;
                newct["UPDTID"] = Common.UserId;
                newct["UPDTDT"] = DateTime.Now;
                dtKM5030.Rows.Add(newct);
                changedCTRows.Add(dtKM5030.Rows.Count - 1);
                // 未登録品番一覧から削除
                DataRow[] rows = dtD0410.Select($"HMCD='{cel.Value}'");
                if (rows.Length > 0) rows[0].Delete();
                insertCnt++;
            }

            // 追加した行の最後のセルにフォーカス
            dataGridView1.CurrentCell = dataGridView1.Rows[^1].Cells["CT"];
            dataGridView1.Rows[^1].Cells["CT"].Selected = true;
            // そのセルを編集状態にする
            dataGridView1.BeginEdit(true);
            // 列幅自動調整
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            toolStripStatusLabel1.Text = $"{insertCnt} 件を追加しました（まだ保存はされていません）";
        }

        // 「段取に追加」ボタン
        private void ButtonAddDT_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count <= 0)
            {
                MessageBox.Show("追加対象の品番を選択してください．");
                dataGridView1.Focus();
                return;
            }
            // 行番号一覧を取得
            var rowIndexes = dataGridView1.SelectedCells
                .Cast<DataGridViewCell>()
                .Select(c => c.RowIndex)
                .Distinct()
                .OrderBy(r => r);
            int insertCnt = 0;
            foreach (int row in rowIndexes)
            {
                string hmcd = dataGridView1[2, row].Value.ToString();
                DateTime valdtf = dataGridView1[3, row].Value.ToDateTimeOrDefaultToday();
                int wkseq = dataGridView1[4, row].Value.ToIntOrDefault();
                var rec = dtKM5020.Select($"HMCD='{hmcd}' and WKSEQ={wkseq}");
                if (rec == null) throw new Exception($"{hmcd}の登録で異常が発生しました．");
                if (rec.Length == 0)
                {
                    // 段取りマスタに登録
                    var newdt = dtKM5020.NewRow();
                    newdt["ODCD"] = OdCdSetting.OdCd;
                    newdt["WKGRCD"] = OdCdSetting.KtCd;
                    newdt["HMCD"] = hmcd;
                    newdt["VALDTF"] = valdtf;
                    newdt["WKSEQ"] = wkseq;
                    newdt["SETUPTMMP"] = 0;
                    newdt["SETUPTMSP"] = 0;
                    newdt["INSTID"] = Common.UserId;
                    newdt["INSTDT"] = DateTime.Now;
                    newdt["UPDTID"] = Common.UserId;
                    newdt["UPDTDT"] = DateTime.Now;
                    dtKM5020.Rows.Add(newdt);
                    changedDTRows.Add(dtKM5020.Rows.Count - 1);
                    insertCnt++;
                    if (dataGridView1.Rows.Count - 1 > row)
                        dataGridView1.CurrentCell = dataGridView1[2, row + 1];
                }
                else
                {
                    MessageBox.Show("既に登録されています．", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        // 「CT段取に追加」ボタン（未登録品番一覧から標準作業時間マスタに品番を移動）
        private void ButtonAddHMCD_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count <= 0)
            {
                MessageBox.Show("追加対象の品番を選択してください．");
                return;
            }
            int insertCnt = 0;
            foreach (DataGridViewCell cel in dataGridView2.SelectedCells)
            {
                // 標準作業時間マスタに登録
                var newct = dtKM5030.NewRow();
                newct["ODCD"] = OdCdSetting.OdCd;
                newct["WKGRCD"] = OdCdSetting.KtCd;
                newct["HMCD"] = cel.Value;
                newct["VALDTF"] = DateTime.Today;
                newct["WKSEQ"] = Common.DEFAULT_WKSEQ;
                newct["CT"] = 0;
                newct["INSTID"] = Common.UserId;
                newct["INSTDT"] = DateTime.Now;
                newct["UPDTID"] = Common.UserId;
                newct["UPDTDT"] = DateTime.Now;
                dtKM5030.Rows.Add(newct);
                changedCTRows.Add(dtKM5030.Rows.Count - 1);
                // 段取りマスタに登録
                var newdt = dtKM5020.NewRow();
                newdt["ODCD"] = OdCdSetting.OdCd;
                newdt["WKGRCD"] = OdCdSetting.KtCd;
                newdt["HMCD"] = cel.Value;
                newdt["VALDTF"] = DateTime.Today;
                newdt["WKSEQ"] = Common.DEFAULT_WKSEQ;
                newdt["SETUPTMMP"] = 0;
                newdt["SETUPTMSP"] = 0;
                newdt["INSTID"] = Common.UserId;
                newdt["INSTDT"] = DateTime.Now;
                newdt["UPDTID"] = Common.UserId;
                newdt["UPDTDT"] = DateTime.Now;
                dtKM5020.Rows.Add(newdt);
                changedDTRows.Add(dtKM5020.Rows.Count - 1);
                // 未登録品番一覧から削除
                DataRow[] rows = dtD0410.Select($"HMCD='{cel.Value}'");
                if (rows.Length > 0) rows[0].Delete();
                insertCnt++;
            }

            // 追加した行の最後のセルにフォーカス
            dataGridView1.CurrentCell = dataGridView1.Rows[^1].Cells["CT"];
            dataGridView1.Rows[^1].Cells["CT"].Selected = true;
            // そのセルを編集状態にする
            dataGridView1.BeginEdit(true);
            // 列幅自動調整
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            toolStripStatusLabel1.Text = $"{insertCnt} 件を追加しました（まだ保存はされていません）";
        }

        // 「順序追加」ボタン
        private void buttonAddWKSEQ_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count <= 0)
            {
                MessageBox.Show("追加対象の品番を選択してください．");
                return;
            }
            // 標準作業時間マスタに追加
            DataRow src5030 = ((DataRowView)dataGridView1.CurrentRow.DataBoundItem).Row; // フィルター後でも正しく取得
            var newRow5030 = dtKM5030.NewRow();
            newRow5030.ItemArray = src5030.ItemArray.Clone() as object[];
            string hmcd = newRow5030["HMCD"].ToString();
            int seq = Convert.ToInt32(src5030["WKSEQ"]) + 10;
            while (dtKM5030.Select($"HMCD='{hmcd}' and WKSEQ={seq}").Length != 0)
            {
                seq += 10;
            }
            newRow5030["WKSEQ"] = seq;
            newRow5030["INSTID"] = Common.UserId;
            newRow5030["INSTDT"] = DateTime.Now;
            newRow5030["UPDTID"] = Common.UserId;
            newRow5030["UPDTDT"] = DateTime.Now;
            dtKM5030.Rows.Add(newRow5030);
            changedCTRows.Add(dtKM5030.Rows.Count - 1);
            // 段取りマスタに追加
            string findOdCd = src5030["ODCD"].ToString();
            string findWkGrCd = src5030["WKGRCD"].ToString();
            string findHmcd = src5030["HMCD"].ToString();
            int findSeq = src5030["WKSEQ"].ToIntOrDefault();
            DataRow[] src5020 = dtKM5020.Select($"ODCD='{findOdCd}' and WKGRCD='{findWkGrCd}' and HMCD='{findHmcd}' and WKSEQ={findSeq}");
            if (src5020.Length > 0)
            {
                var newRow5020 = dtKM5020.NewRow();
                newRow5020.ItemArray = src5020[0].ItemArray.Clone() as object[];
                newRow5020["WKSEQ"] = seq;
                newRow5020["INSTID"] = Common.UserId;
                newRow5020["INSTDT"] = DateTime.Now;
                newRow5020["UPDTID"] = Common.UserId;
                newRow5020["UPDTDT"] = DateTime.Now;
                dtKM5020.Rows.Add(newRow5020);
                changedDTRows.Add(dtKM5020.Rows.Count - 1);
            }
            // 追加した行の最後のセルにフォーカス
            int last = dataGridView1.Rows.Count - 1;
            dataGridView1.FirstDisplayedScrollingRowIndex = last;
            dataGridView1.Rows[last].Cells["WKSEQ"].Selected = true;
        }

        // 「保存」ボタン
        private async void ButtonSaveClose_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            try
            {
                // 標準作業時間マスタ更新
                if (changedCTRows.Count > 0)
                {
                    bool ret = await Task.Run(() => DBAccessor.SaveKM5030(ref dtKM5030));
                    if (!ret) return;
                    changedCTRows.Clear();
                    dtKM5030.Rows.Clear();
                    // データソースの再読み込み
                    DBAccessor.ReadKM5030(ref dtKM5030, OdCdSetting.OdCd, OdCdSetting.KtCd);
                    dataGridView1.DataSource = dtKM5030;
                    toolStripStatusLabel1.Text = "マスタを更新しました．";
                }
                // 段取りマスタ更新
                if (changedDTRows.Count > 0)
                {
                    bool ret = await Task.Run(() => DBAccessor.SaveKM5020(ref dtKM5020));
                    if (!ret) return;
                    changedDTRows.Clear();
                    dtKM5020.Rows.Clear();
                    // データソースの再読み込み
                    DBAccessor.ReadKM5020(ref dtKM5020, OdCdSetting.OdCd, OdCdSetting.KtCd);
                    dataGridView3.DataSource = dtKM5020;
                    toolStripStatusLabel1.Text = "マスタを更新しました．";
                }
                textBoxHmCd.SelectionStart = 0;
                textBoxHmCd.SelectionLength = textBoxHmCd.Text.Length;
                textBoxHmCd.Focus();
            }
            finally
            {
                this.UseWaitCursor = false;
            }
        }

        // 「閉じる」ボタン
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
