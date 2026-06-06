using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PlanProduction
{
    public partial class FormOrderList : Form
    {
        private FormConfig settings;

        // フォーム間連携アクションコールバック
        private readonly Action<string, List<SelectedItem>> callback;

        // 現在表示中の手配先コードを格納する変数
        private readonly OdCdSetting OdCdSetting;
        private double 可動率;
        private int 処理モード;      // 1:手配リスト, 2:CTマスタ

        // DataTable を保持するフィールドを作る
        private DataTable dtDataSource = new();     // 手配一覧
        private DataTable dtKM5020GroupBy = new();
        private DataTable dtKM5030GroupBy = new();
        private DataTable dtCTMaster = new();
        private DataTable dtD0520 = new();          // 前工程在庫情報

        // 複数列選択用のセルリスト
        private readonly List<DataGridViewCell> selectedCells = [];

        // DataGridViewの並び替え第２キー
        private int? lastClickedColumnIndex = null;
        private bool lastSortAscending = true;   // true = ASC, false = DESC

        public FormOrderList(OdCdSetting OdCdSetting, Action<string, List<SelectedItem>> callback)
        {
            InitializeComponent();

            this.KeyPreview = true;                     // フォームでキーイベントを受け取る設定
            this.OdCdSetting = OdCdSetting;             // 表示対象の手配先コードをこのフォームに保持
            this.処理モード = 1;

            if (callback == null)
            {
                // 直接呼び出された場合は、計画と実績の追加ボタンは無効化
                buttonAddPlan.Enabled = false;
                buttonAddPlan.BackColor = Color.LightGray;
                buttonAddAchieve.Enabled = false;
                buttonAddAchieve.BackColor = Color.LightGray;
            }
            else
            {
                this.callback = callback;               // コールバックを受け取る
            }
        }

        private void FormOrderList_Load(object sender, EventArgs e)
        {
            // コントロールにイベントを登録
            dataGridView1.DataBindingComplete += DataGridView1_DataBindingComplete;
            dataGridView1.RowPostPaint += DataGridView1_RowPostPaint;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            dataGridView1.CellMouseDown += DataGridView1_CellMouseDown;
            dataGridView1.ColumnHeaderMouseClick += DataGridView1_ColumnHeaderMouseClick;
            //dataGridView1.SortCompare += DataGridView1_SortCompare;
            buttonRefresh.Click += ButtonRefresh_Click;

            // コントロールの初期化
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;

            // フォームの状態を復元
            settings = Common.FormSettingsLoad();
            string key = this.Name + OdCdSetting.OdCd;
            if (settings.Forms.TryGetValue(key, out FormSettings fs))
            {
                if (fs.X >= 0 && fs.Y >= 0)
                {
                    this.StartPosition = FormStartPosition.Manual;
                    this.Location = new Point(fs.X, fs.Y);
                    this.Size = new Size(fs.Width, fs.Height);
                }
            }

            var os = OdCdSetting;

            // マスタの読み込み
            DBAccessor.ReadKM5020GroupBy(ref dtKM5020GroupBy, os.OdCd, os.KtCd);    // 段取りマスタ取得
            DBAccessor.ReadKM5030GroupBy(ref dtKM5030GroupBy, os.OdCd, os.KtCd);    // 標準作業時間マスタ取得
            DBAccessor.ReadKM50305020(ref dtCTMaster, os.OdCd, os.KtCd);            // CTマスター取得

            // コントロールに初期値をセット
            labelTitleOdCd.Text = $"【{os.OdCd}】 {DataStore.M0300Map[os.OdCd]}";
            textBox可動率.Text = (string.IsNullOrEmpty(os.Ava)) ? "70" : os.Ava;
            可動率 = double.TryParse(os.Ava, out double result) ? 1 / result * 100 : 1.4286; // デフォルトは70%で1.4286倍

            // 初期表示
            RefreshOrderList();
        }



        // オーダーリスト作成
        private void RefreshOrderList()
        {
            処理モード = 1;
            panel1.Enabled = true;
            dtDataSource = new DataTable();
            dtD0520 = new DataTable();  // 在庫情報はリアルタイムに取得
            var s = OdCdSetting;

            bool ret;
            if (s.OdCd.Substring(0, 3) == "603")
            {
                // ベンダーは手配日程データから＋５営業日分を取得
                ret = DBAccessor.ReadD0440Pivot(ref dtDataSource, OdCdSetting);
            }
            else if (OdCdSetting.OdCd == "60460")
            {
                // EWUベンダーの手配データ取得 (60420, 60430)
                ret = DBAccessor.ReadD0410Pivot(ref dtDataSource, OdCdSetting, "EWUBEND");
            }
            else
            {
                // 手配データ取得
                ret = DBAccessor.ReadD0410Pivot(ref dtDataSource, OdCdSetting);
            }
            if (ret)
            {
                DBAccessor.ReadD0520FromPrevious(ref dtDataSource, ref dtD0520);  // 前工程の在庫を調べて取得
                MargeDataTable(ref dtDataSource, ref dtD0520);                    // 手配データにマスタ情報と在庫情報を付与

                // 完成したデータテーブルをバインディング
                dataGridView1.DataSource = dtDataSource;

                // 手配リスト順番がSQLでは難しかったので、バインディング後に並び替える
                int key1 = (s.SortOrder1 == 1) ? dataGridView1.Columns["品番"].Index :
                           (s.SortOrder1 == 2) ? dataGridView1.Columns["優先度"].Index :
                           (s.SortOrder1 == 3) ? dataGridView1.Columns["段取内容"].Index : dataGridView1.Columns["品番"].Index;
                int key2 = (s.SortOrder2 == 1) ? dataGridView1.Columns["品番"].Index :
                           (s.SortOrder2 == 2) ? dataGridView1.Columns["優先度"].Index :
                           (s.SortOrder2 == 3) ? dataGridView1.Columns["段取内容"].Index : -1;
                string col1 = dataGridView1.Columns[key1].DataPropertyName;
                string sortExpression;
                if (key2 >= 0 && key2 < dataGridView1.Columns.Count)
                {
                    string col2 = dataGridView1.Columns[key2].DataPropertyName;
                    sortExpression = $"{col1} ASC, {col2} ASC";
                }
                else
                {
                    sortExpression = $"{col1} ASC";
                }
                dtDataSource.DefaultView.Sort = sortExpression;

                // 初期状態として「前回クリック列」を設定しておく
                lastClickedColumnIndex = key1;
                lastSortAscending = true;
            }
            else
            {
                buttonChangeView.Enabled = false;
                RefreshCTMaster();
            }
        }
        // CTマスタリスト作成
        private void RefreshCTMaster()
        {
            処理モード = 2;
            panel1.Enabled = false;
            dataGridView1.DataSource = dtCTMaster;
        }
        // CTと前工程在庫情報と在庫情報をくっつける
        private void MargeDataTable(ref DataTable dt, ref DataTable d0520)
        {
            dt.Columns.Add("前在", typeof(int));
            dt.Columns["前在"].SetOrdinal(8);    // 列を途中に割り込ませる
            dt.Columns.Add("CT", typeof(double));
            dt.Columns.Add("段取内容", typeof(string));
            dt.Columns.Add("段取時間", typeof(double));
            foreach (DataRow row in dt.Rows)
            {
                //
                // LinqにはLikeが無いので、手配KTCDとマスタWKGRCDのワイルドカードを正規表現に変換してマッチングする
                //
                // KM5030:CT
                var find5030Row = dtKM5030GroupBy.AsEnumerable()
                    .Where(r =>
                        r.Field<string>("ODCD") == row["ODCD"].ToString() &&
                        System.Text.RegularExpressions.Regex.IsMatch(
                            row["KTCD"].ToString(),
                            r.Field<string>("WKGRCD").Replace("%", ".*").Replace("_", ".") + "$"
                        ) &&
                        r.Field<string>("HMCD") == row["品番"].ToString()
                    )
                    .ToList();
                if (find5030Row == null || find5030Row.Count == 0)
                {
                    row["CT"] = 0;
                }
                else
                {
                    row["CT"] = find5030Row[0]["CT"];
                }
                // KM5020:作業内容、段取時間
                var find5020Row = dtKM5020GroupBy.AsEnumerable()
                    .Where(r =>
                        r.Field<string>("ODCD") == row["ODCD"].ToString() &&
                        System.Text.RegularExpressions.Regex.IsMatch(
                            row["KTCD"].ToString(),
                            r.Field<string>("WKGRCD").Replace("%", ".*").Replace("_", ".") + "$"
                        ) &&
                        r.Field<string>("HMCD") == row["品番"].ToString()
                    )
                    .ToList();
                if (find5020Row == null || find5020Row.Count == 0)
                {
                    row["段取内容"] = "";
                    row["段取時間"] = 0;
                }
                else
                {
                    row["段取内容"] = find5020Row[0]["WORK"];
                    row["段取時間"] = find5020Row[0]["SETUPTMMP"];
                }
                // D0520:前工程在庫
                if (d0520.Rows.Count > 0)
                {
                    var findZaiko = d0520.AsEnumerable()
                        .Where(r => r.Field<string>("HMCD") == row["品番"].ToString())
                        .ToList();
                    if (findZaiko == null || findZaiko.Count == 0)
                    {
                        continue;
                    }
                    else
                    {
                        int v = findZaiko[0]["MZAIQTY"].ToIntOrDefault();
                        if (v != 0) row["前在"] = v;
                    }
                }
            }
        }
        // データバインド完了後に行ヘッダーを設定しないといけない
        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dgv = dataGridView1;

            if (処理モード == 1)
            {
                // 列 0〜7 と CTを行ヘッダーと同じ色にする
                string[] colNames = ["優先度", "品番", "ODCD", "KTSEQ", "KTCD", "品目略称", "作業内容", "作業注釈", "前在", "段取内容", "段取時間"];
                foreach (string col in colNames)
                {
                    dgv.Columns[col].DefaultCellStyle.BackColor = dgv.RowHeadersDefaultCellStyle.BackColor;
                    dgv.Columns[col].SortMode = DataGridViewColumnSortMode.Programmatic; // ソート機能を自前
                }
                // 手配列の設定（幅、右揃え、ソート機能なし）
                for (int col = dgv.Columns["前在"].Index; col < dgv.Columns["CT"].Index; col++)
                {
                    //dataGridView1.Columns[col].Width = 50;
                    dgv.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;   // データの右寄せ
                    dgv.Columns[col].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;   // 列ヘッダー右寄せ
                    dgv.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;                       // ソート機能を無効化
                    // 当日背景色の設定
                    if (dgv.Columns[col].HeaderText == DateTime.Today.ToString("MM/dd"))
                    {
                        dgv.Columns[col].DefaultCellStyle.BackColor = Color.FromArgb(235, 255, 235);
                    }
                }
            }
            else
            {
                string[] colNames = ["品番", "適用開始日", "順序", "段取内容", "段取時間"];
                foreach (string col in colNames)
                {
                    dgv.Columns[col].DefaultCellStyle.BackColor = dgv.RowHeadersDefaultCellStyle.BackColor;
                }
            }
            // Double型小数点の設定
            var ct = dgv.Columns["CT"];
            ct.HeaderText = "　CT";
            ct.DefaultCellStyle.BackColor = dgv.RowHeadersDefaultCellStyle.BackColor;
            ct.DefaultCellStyle.Format = "N1"; // 小数点以下1桁
            ct.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ct.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            var dt = dgv.Columns["段取時間"];
            dt.DefaultCellStyle.Format = "N1"; // 小数点以下1桁
            dt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dt.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            // 列ヘッダーのフォントサイズを小さくする
            dt.HeaderCell.Style.Font = new Font(
                dgv.ColumnHeadersDefaultCellStyle.Font.FontFamily, 9.0f);  // 小さいサイズ
            dgv.Columns["段取内容"].HeaderCell.Style.Font = new Font(
                dgv.ColumnHeadersDefaultCellStyle.Font.FontFamily, 9.0f);  // 小さいサイズ
            // 列幅自動調整
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // チェックの状態を復元
            string key = this.Name + OdCdSetting.OdCd;
            if (settings.Forms.TryGetValue(key, out FormSettings s))
            {
                if (s.Flg1 == 0 && 処理モード == 1)
                {
                    dgv.Columns["ODCD"].Visible = false;                  // ODCDは非表示
                    dgv.Columns["KTCD"].Visible = false;                  // KTCDは非表示
                }
                if (s.Flg2 == 0 && 処理モード == 1) dgv.Columns["品目略称"].Visible = false;
                if (s.Flg3 == 0 && 処理モード == 1) dgv.Columns["作業内容"].Visible = false;
                if (s.Flg4 == 0 && 処理モード == 1) dgv.Columns["KTSEQ"].Visible = false;
                if (s.Flg6 == 0 && 処理モード == 1) dgv.Columns["作業注釈"].Visible = false;
                checkBoxPKey.Checked = (s.Flg1 == 1);
                checkBoxHMRNM.Checked = (s.Flg2 == 1);
                checkBoxWKNOTE.Checked = (s.Flg3 == 1);
                checkBoxKTSEQ.Checked = (s.Flg4 == 1);
                checkBoxDANDORI.Checked = (s.Flg5 == 1);
                checkBoxWKCOMMENT.Checked = (s.Flg6 == 1);
            }
        }
        // 行番号を付ける（1 から始める）
        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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



        // フォームの状態を保存
        private void FormOrderList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (処理モード != 1) return;
            settings = Common.FormSettingsLoad(); // 他のフォームで変更された可能性があるので、最新の状態を読み込む
            string key = this.Name + OdCdSetting.OdCd;
            if (!settings.Forms.ContainsKey(key)) settings.Forms[key] = new FormSettings();
            var s = settings.Forms[key];
            s.X = this.Location.X;
            s.Y = this.Location.Y;
            var f = (FormOrderList)sender;
            s.Width = f.Width;
            s.Height = f.Height;
            s.Flg1 = (checkBoxPKey.Checked) ? 1 : 0;
            s.Flg2 = (checkBoxHMRNM.Checked) ? 1 : 0;
            s.Flg3 = (checkBoxWKNOTE.Checked) ? 1 : 0;
            s.Flg4 = (checkBoxKTSEQ.Checked) ? 1 : 0;
            s.Flg5 = (checkBoxDANDORI.Checked) ? 1 : 0;
            s.Flg6 = (checkBoxWKCOMMENT.Checked) ? 1 : 0;
            Common.FormSettingsSave(settings);
        }
        // キーボードショートカット
        private void FormOrderList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                ButtonAddPlan_Click(sender, e);
                this.Focus();
            }
            else if (e.KeyCode == Keys.F5)
            {
                ButtonRefresh_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F6)
            {
                ButtonAddAchieve_Click(sender, e);
                this.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }



        // （おまけ処理）選択した作業時間を算出して表示
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (処理モード != 1) return;
            double sumProduct = 0;

            // 選択されたセルの行インデックスを取得（重複除去）
            var selectedRows = dataGridView1.SelectedCells
                .Cast<DataGridViewCell>()
                .Select(c => c.RowIndex)
                .Distinct();

            foreach (int rowIndex in selectedRows)
            {
                var row = dataGridView1.Rows[rowIndex];

                double ct = Convert.ToDouble(row.Cells["CT"].Value);

                for (int col = row.Cells["前在"].ColumnIndex + 1; col < row.Cells["CT"].ColumnIndex; col++)
                {
                    if (row.Cells[col].Selected)
                    {
                        if (string.IsNullOrEmpty(row.Cells[col].Value.ToString())) continue;
                        double qty = Convert.ToDouble(row.Cells[col].Value);
                        sumProduct += qty * ct * 可動率;
                    }
                }
            }

            textBox作業時間.Text = (sumProduct / 3600).ToString("N2");
        }
        // （おまけ処理）並び替え
        private void DataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0) return;
            if (処理モード == 1)
            {
                var dgv = dataGridView1;
                if (e.ColumnIndex > dgv.Columns["前在"].Index &&
                    e.ColumnIndex < dgv.Columns["CT"].Index)
                {
                    // Ctrl が押されていなければ選択クリア
                    bool isCtrl = (ModifierKeys & Keys.Control) == Keys.Control;
                    if (!isCtrl)
                    {
                        selectedCells.Clear();
                        dataGridView1.ClearSelection();
                    }

                    // 手配日付列のすべてのセルを選択に追加（新規行も含む）
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        DataGridViewCell cell = row.Cells[e.ColumnIndex];

                        if (!selectedCells.Contains(cell))
                            selectedCells.Add(cell);

                        cell.Selected = true;
                    }
                }
                else
                {
                    var dt = (DataTable)dataGridView1.DataSource;
                    int current = e.ColumnIndex;
                    string colCurrent = dataGridView1.Columns[current].DataPropertyName;

                    string sortExpression;

                    // 同じ列をクリックした場合 → 昇順／降順を反転
                    if (lastClickedColumnIndex == current)
                    {
                        lastSortAscending = !lastSortAscending;
                        string dir = lastSortAscending ? "ASC" : "DESC";
                        sortExpression = $"{colCurrent} {dir}";
                    }
                    else
                    {
                        // 別の列をクリックした場合 → 複合キーソート
                        string colPrevious = null;

                        if (lastClickedColumnIndex != null)
                        {
                            colPrevious = dataGridView1.Columns[lastClickedColumnIndex.Value].DataPropertyName;
                        }

                        // 今回列は ASC 固定（初回クリック時）
                        if (colPrevious != null)
                        {
                            sortExpression = $"{colCurrent} ASC, {colPrevious} ASC";
                        }
                        else
                        {
                            sortExpression = $"{colCurrent} ASC";
                        }

                        // 新しい列をクリックしたので昇順にリセット
                        lastSortAscending = true;
                    }
                    // ソート実行
                    dt.DefaultView.Sort = sortExpression;

                    // 今回の列を保持
                    lastClickedColumnIndex = current;
                }
            }
        }
        // （おまけ処理）クリップボード処理
        private void DataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (dataGridView1.SelectedCells.Count > 1)
                {
                    List<string> selectedData = [];
                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        if (cell.Value == null) continue;
                        selectedData.Add(string.IsNullOrEmpty(cell.Value.ToString()) ? "" : cell.Value.ToString());
                    }
                    Clipboard.SetText(string.Join("\n", selectedData));
                }
                else
                {
                    int col = dataGridView1.CurrentCell.ColumnIndex;
                    string ht = dataGridView1.Columns[col].HeaderText;
                    string val = dataGridView1[col, e.RowIndex].Value.ToString();
                    if (val != "")
                    {
                        Clipboard.SetText(val);
                    }
                }
            }
        }



        /*
         * チェックボックス関連
         */
        // 「主キー」の表示／非表示
        private void CheckBoxPKey_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0 || 処理モード != 1) return;
            dataGridView1.Columns["ODCD"].Visible = checkBoxPKey.Checked;
            dataGridView1.Columns["KTCD"].Visible = checkBoxPKey.Checked;
        }
        // 「品目略称」の表示／非表示
        private void CheckBoxHMRNM_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0 || 処理モード != 1) return;
            dataGridView1.Columns["品目略称"].Visible = checkBoxHMRNM.Checked;
        }
        // 「作業内容」の表示／非表示
        private void CheckBoxWKNOTE_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0 || 処理モード != 1) return;
            dataGridView1.Columns["作業内容"].Visible = checkBoxWKNOTE.Checked;
        }
        // 「KTSEQ」の表示／非表示
        private void CheckBoxKTSEQ_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0 || 処理モード != 1) return;
            dataGridView1.Columns["KTSEQ"].Visible = checkBoxKTSEQ.Checked;
        }
        // 「段取」の表示／非表示
        private void CheckBoxDANDORI_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0 || 処理モード != 1) return;
            dataGridView1.Columns["段取内容"].Visible = checkBoxDANDORI.Checked;
            dataGridView1.Columns["段取時間"].Visible = checkBoxDANDORI.Checked;
        }
        // 「作業注釈」の表示／非表示
        private void CheckBoxWKCOMMENT_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0 || 処理モード != 1) return;
            dataGridView1.Columns["作業注釈"].Visible = checkBoxWKCOMMENT.Checked;
        }



        /*
         * テキストボックス関連
         */
        private void TextBox可動率_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (double.TryParse(textBox可動率.Text, out double result))
                {
                    可動率 = 1 / result * 100;
                }
                else
                {
                    可動率 = 1.4286; // デフォルトは70%で1.4286倍
                    textBox可動率.Text = "70";
                }
                DataGridView1_SelectionChanged(sender, e);
                textBox可動率.SelectAll();
                e.Handled = true;
            }
        }

        private void TextBox可動率_Enter(object sender, EventArgs e)
        {
            textBox可動率.SelectAll();
        }

        private void TextBox可動率_MouseDown(object sender, MouseEventArgs e)
        {
            textBox可動率.SelectAll();
        }



        /*
         * ボタン関連
         */
        // 「再読み込み」ボタン
        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            RefreshOrderList();
        }
        // 「パネル切り替え」ボタン
        private void ButtonChangeView_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            if (処理モード == 1)
            {
                RefreshCTMaster();
            }
            else
            {
                RefreshOrderList();
            }
        }
        // 「閉じる」ボタン
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        // 「計画に追加」ボタン
        private void ButtonAddPlan_Click(object sender, EventArgs e)
        {
            // 「計画リスト」にDTO（データ転送オブジェクト）を渡す
            var list = MakeSelectedItemList();
            callback("Plan", list);
            if (処理モード != 1) this.Focus();
        }
        // 「実績に追加」ボタン
        private void ButtonAddAchieve_Click(object sender, EventArgs e)
        {
            // 「実績リスト」にDTO（データ転送オブジェクト）を渡す
            var list = MakeSelectedItemList();
            callback("Achieve", list);
            if (処理モード != 1) this.Focus();
        }
        // DTO（データ転送オブジェクト）の生成
        private List<SelectedItem> MakeSelectedItemList()
        {
            var dgv = dataGridView1;
            List<SelectedItem> list = [];

            if (処理モード == 1)
            {
                // 品番＋順序ごとの合計結果を保存する辞書
                Dictionary<(string, int), int> summary = [];

                // 選択セルの並び替え
                var query = from DataGridViewCell c in dgv.SelectedCells
                            orderby c.RowIndex, c.ColumnIndex
                            select c;
                // 手配数をサマリー
                foreach (DataGridViewCell c in query)
                {
                    if (c.Visible == false) continue;                                               // 非表示列
                    if (c.Value == null || string.IsNullOrEmpty(c.Value.ToString())) continue;      // データなし
                    if (c.ColumnIndex <= dataGridView1.Columns["前在"].Index ||
                        c.ColumnIndex >= dataGridView1.Columns["CT"].Index) continue;               // 手配日以外を無視

                    int qty = 0;
                    if (c.Value != null && int.TryParse(c.Value.ToString(), out int v)) qty = v;
                    string hmcd = dataGridView1.Rows[c.RowIndex].Cells["品番"].Value.ToString();
                    DataRow[] result = dtCTMaster.Select($"品番='{hmcd}'");
                    if (result.Length == 0)
                    {
                        int wkseq = Common.DEFAULT_WKSEQ;
                        if (summary.ContainsKey((hmcd, wkseq)))
                        {
                            summary[(hmcd, wkseq)] += qty;
                        }
                        else
                        {
                            summary[(hmcd, wkseq)] = qty;
                        }
                    }
                    else
                    {
                        // CTMasterの順序で分割
                        foreach (DataRow row in result)
                        {
                            int wkseq = row["順序"].ToIntOrDefault();
                            if (summary.ContainsKey((hmcd, wkseq)))
                            {
                                summary[(hmcd, wkseq)] += qty;
                            }
                            else
                            {
                                summary[(hmcd, wkseq)] = qty;
                            }
                        }
                    }
                }
                //// DTO（データ転送オブジェクト）の生成
                foreach (var ((hmcd, wkseq), sumqty) in summary)
                {
                    DataRow[] rows = dtCTMaster.Select($"品番='{hmcd}' AND 順序={wkseq}");
                    double ct = (rows.Length == 0) ? 0 : rows[0]["CT"].ToDoubleOrDefault();
                    double dt = (rows.Length == 0) ? 0 : rows[0]["段取時間"].ToDoubleOrDefault();
                    string work = (rows.Length == 0) ? "" : rows[0]["段取内容"].ToString();
                    list.Add(new SelectedItem
                    {
                        HmCd = hmcd,
                        WkSeq = wkseq,
                        SumQty = sumqty,
                        CT = ct,
                        DT = dt,
                        Work = work
                    });
                }
            }
            else
            {
                var selectedRows = dgv.SelectedCells
                    .Cast<DataGridViewCell>()
                    .Select(c => c.RowIndex)
                    .Distinct()
                    .OrderBy(r => r);
                foreach (int rowIndex in selectedRows)
                {
                    string hmcd = dgv.Rows[rowIndex].Cells["品番"].Value.ToString();
                    int seq = dgv.Rows[rowIndex].Cells["順序"].Value.ToIntOrDefault();
                    double ct = dgv.Rows[rowIndex].Cells["CT"].Value.ToDoubleOrDefault();
                    double dt = dgv.Rows[rowIndex].Cells["段取時間"].Value.ToDoubleOrDefault();
                    string work = dgv.Rows[rowIndex].Cells["段取内容"].Value.ToString();
                    list.Add(new SelectedItem
                    {
                        HmCd = hmcd,
                        WkSeq = seq,
                        SumQty = 0,
                        CT = ct,
                        DT = dt,
                        Work = work
                    });
                }
            }
            return list;
        }

    }
}
