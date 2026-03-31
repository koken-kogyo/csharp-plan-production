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

        // DataTable を保持するフィールドを作る
        private DataTable dt = new();
        private DataTable dtKM5030 = new();

        // 複数列選択用のセルリスト
        private readonly List<DataGridViewCell> selectedCells = [];


        public FormOrderList(OdCdSetting OdCdSetting, Action<string, List<SelectedItem>> callback)
        {
            InitializeComponent();

            this.KeyPreview = true;                     // フォームでキーイベントを受け取る設定
            this.OdCdSetting = OdCdSetting;             // 表示対象の手配先コードをこのフォームに保持

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
            buttonRefresh.Click += ButtonRefresh_Click;
            buttonRefresh.Click += ButtonRefresh_Click;

            // コントロールの初期化
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;

            // フォームの状態を復元
            settings = Common.FormSettingsLoad();
            string key = this.Name;
            if (settings.Forms.TryGetValue(key, out FormSettings s))
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(s.X, s.Y);
                this.Size = new Size(s.Width, s.Height);
            }

            // コントロールに初期値をセット
            labelTitleOdCd.Text = $"【{OdCdSetting.OdCd}】 {DataStore.M0300Map[OdCdSetting.OdCd]}";
            textBox可動率.Text = (string.IsNullOrEmpty(OdCdSetting.Ava)) ? "70" : OdCdSetting.Ava;
            可動率 = double.TryParse(OdCdSetting.Ava, out double result) ? 1 / result * 100 : 1.4286; // デフォルトは70%で1.4286倍

            // 表示するデータを取得
            var condition = DataStore.ExtracConditions(OdCdSetting.OdCd);   // 抽出条件の作成            
            DBAccessor.ReadD0410Pivot(ref dt, condition);                   // 手配データ取得
            DBAccessor.ReadKM5030(ref dtKM5030, condition);                 // 標準作業時間マスタ取得
            // 表示するデータテーブルを編集（CTをくっつける）
            MargeDataTable(ref dt, ref dtKM5030);

            dataGridView1.DataSource = dt;
        }
        // ロード後の初期表示
        private void FormOrderList_Shown(object sender, EventArgs e)
        {
            // チェックの状態を復元
            string key = this.Name;
            if (settings.Forms.TryGetValue(key, out FormSettings s))
            {
                if (s.Flg1 == 0)
                {
                    dataGridView1.Columns["ODCD"].Visible = false;                  // ODCDは非表示
                    dataGridView1.Columns["KTCD"].Visible = false;                  // KTCDは非表示
                }
                if (s.Flg1 == 1) checkBoxPKey.Checked = true;
                if (s.Flg2 == 0) dataGridView1.Columns["HMRNM"].Visible = false;
                if (s.Flg2 == 1) checkBoxHMRNM.Checked = true;
                if (s.Flg3 == 0) dataGridView1.Columns["WKNOTE"].Visible = false;
                if (s.Flg3 == 1) checkBoxWKNOTE.Checked = true;
            }
        }
        // フォームの状態を保存
        private void FormOrderList_FormClosing(object sender, FormClosingEventArgs e)
        {
            settings = Common.FormSettingsLoad(); // 他のフォームで変更された可能性があるので、最新の状態を読み込む
            string key = this.Name;
            if (!settings.Forms.ContainsKey(key)) settings.Forms[key] = new FormSettings();
            var s = settings.Forms[key];
            s.X = this.Location.X;
            s.Y = this.Location.Y;
            s.Width = this.Width;
            s.Height = this.Height;
            s.Flg1 = (checkBoxPKey.Checked) ? 1 : 0;
            s.Flg2 = (checkBoxHMRNM.Checked) ? 1 : 0;
            s.Flg3 = (checkBoxWKNOTE.Checked) ? 1 : 0;
            Common.FormSettingsSave(settings);
        }
        // キーボードショートカット
        private void FormOrderList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }


        // CTをくっつける
        private static void MargeDataTable(ref DataTable dt, ref DataTable km5030)
        {
            dt.Columns.Add("CT", typeof(double));
            foreach (DataRow row in dt.Rows)
            {
                var findRow = km5030.AsEnumerable()
                    .Where(r =>
                        r.Field<string>("ODCD") == row["ODCD"].ToString() &&
                        r.Field<string>("WKGRCD") == row["KTCD"].ToString() &&
                        r.Field<string>("HMCD") == row["HMCD"].ToString()
                    )
                    .OrderByDescending(x => x.Field<string>("ODCD"))
                    .ToList();
                if (findRow == null || findRow.Count == 0)
                {
                    row["CT"] = 0;
                }
                else
                {
                    row["CT"] = findRow[0]["CT"];
                }
            }
        }

        // データバインド完了後に行ヘッダーを設定しないといけない
        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // データ列の設定（幅、右揃え、ソート機能なし）
            for (int col = 6; col < dataGridView1.Columns.Count; col++)
            {
                //dataGridView1.Columns[col].Width = 50;
                dataGridView1.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;   // データの右寄せ
                dataGridView1.Columns[col].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;   // 列ヘッダー右寄せ
                dataGridView1.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;                       // ソート機能を無効化
            }
            dataGridView1.Columns["CT"].DefaultCellStyle.Format = "N1";     // CTは小数点以下1桁
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
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

                for (int col = 6; col < dataGridView1.ColumnCount - 1; col++)
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
        // 再読み込み
        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            MessageBox.Show("まだ作れていません", "未実装");
        }

        // 「主キー」の表示／非表示
        private void CheckBoxPKey_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0) return;
            dataGridView1.Columns["ODCD"].Visible = checkBoxPKey.Checked;
            dataGridView1.Columns["KTCD"].Visible = checkBoxPKey.Checked;
        }
        // 「品目略称」の表示／非表示
        private void CheckBoxHMRNM_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0) return;
            dataGridView1.Columns["HMRNM"].Visible = checkBoxHMRNM.Checked;
        }
        // 「作業内容」の表示／非表示
        private void CheckBoxWKNOTE_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0) return;
            dataGridView1.Columns["WKNOTE"].Visible = checkBoxWKNOTE.Checked;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

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

        private void ButtonAddPlan_Click(object sender, EventArgs e)
        {
            // 「計画リスト」にDTO（データ転送オブジェクト）を渡す
            var list = MakeSelectedItemList();
            callback("Plan", list);
        }
        private void ButtonAddAchieve_Click(object sender, EventArgs e)
        {
            // 「実績リスト」にDTO（データ転送オブジェクト）を渡す
            var list = MakeSelectedItemList();
            callback("Achieve", list);
        }

        private List<SelectedItem> MakeSelectedItemList()
        {
            // 品番ごとの合計結果を保存する辞書
            Dictionary<string, int> summary = [];
            Dictionary<string, double> ct = [];

            // 選択セルの並び替え
            var query = from DataGridViewCell c in dataGridView1.SelectedCells
                        orderby c.RowIndex, c.ColumnIndex
                        select c;

            foreach (DataGridViewCell c in query)
            {
                string hmcd = dataGridView1.Rows[c.RowIndex].Cells["HMCD"].Value.ToString();
                int qty = 0;
                if (c.Value != null && int.TryParse(c.Value.ToString(), out int v)) qty = v;
                if (summary.ContainsKey(hmcd))
                {
                    summary[hmcd] += qty;
                }
                else
                {
                    summary[hmcd] = qty;
                }
                if (!ct.ContainsKey(hmcd))
                {
                    ct[hmcd] = 0.0;
                    if (double.TryParse(dataGridView1.Rows[c.RowIndex].Cells["CT"].Value.ToString(), out double d)) ct[hmcd] = d;
                }
            }
            // DTO（データ転送オブジェクト）に変換
            var list = summary.Select(kv => new SelectedItem
            {
                HmCd = kv.Key,
                SumQty = kv.Value,
                CT = ct[kv.Key]
            }).ToList();
            return list;
        }

        private void DataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.ColumnIndex < 0) return;
            int col = e.ColumnIndex;

            bool isCtrl = (ModifierKeys & Keys.Control) == Keys.Control;

            // Ctrl が押されていなければ選択クリア
            if (!isCtrl)
            {
                selectedCells.Clear();
                dataGridView1.ClearSelection();
            }

            // この列のすべてのセルを追加（新規行も含む）
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCell cell = row.Cells[col];

                if (!selectedCells.Contains(cell))
                    selectedCells.Add(cell);

                cell.Selected = true;
            }
        }

    }
}
