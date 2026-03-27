using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PlanProduction
{
    public partial class FormOrderList : Form
    {
        private Common.FormConfig settings;

        // 現在表示中の手配先コードを格納する変数
        private string selectedOdCd;

        // DataTable を保持するフィールドを作る
        private DataTable dt = new DataTable();
        private DataTable dtKM5030 = new DataTable();

        public FormOrderList(bool readOnly, string selectedOdCd)
        {
            InitializeComponent();

            this.KeyPreview = true;                     // フォームでキーイベントを受け取る設定

            // 表示対象の手配先コードをこのフォームに保持
            this.selectedOdCd = selectedOdCd;

            // 使用可能ボタンの設定
            buttonAddPlan.Enabled = !readOnly;
            buttonAddAchieve.Enabled = !readOnly;

            // イベント登録
            dataGridView1.DataBindingComplete += DataGridView1_DataBindingComplete;
            dataGridView1.RowPostPaint += dataGridView1_RowPostPaint;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            buttonRefresh.Click += ButtonRefresh_Click;
        }

        private void FormOrderList_Load(object sender, EventArgs e)
        {
            // フォームの状態を復元
            settings = Common.FormSettingsLoad();
            string key = this.Name;
            if (settings.Forms.ContainsKey(key))
            {
                var s = settings.Forms[key];
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(s.X, s.Y);
                this.Size = new Size(s.Width, s.Height);
            }

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;

            // 抽出条件の作成
            var condition = ExtracConditions();
            // 手配データ取得
            DBAccessor.ReadD0410(ref dt, condition);
            // 標準作業時間マスタ取得
            DBAccessor.ReadKM5030(ref dtKM5030, condition);
            // CTをくっつける
            MargeDataTable(ref dt, ref dtKM5030);
            dataGridView1.DataSource = dt;
        }

        // CTをくっつける
        private void MargeDataTable(ref DataTable dt, ref DataTable km5030)
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
                if (findRow == null)
                {
                    row["CT"] = 0;
                }
                else
                {
                    row["CT"] = findRow[0]["CT"];
                }
            }
        }

        /// <summary>
        /// 手配ファイルからの抽出条件を作成
        /// </summary>
        /// <returns>ODCD+KTCD In 句（例）('0631ABETP01',,,'0631ABETP11')</returns>
        private string ExtracConditions()
        {
            var row = DataStore.dtKM5010kai.AsEnumerable()
                .Where(r => r.Field<bool>("CHECKED") == true && r.Field<string>("ODCD") == selectedOdCd)
                .Select(s => "'" + s.Field<string>("ODCD") + s.Field<string>("WKGRCD") + "'")
                .ToList();
            string s = "(" + string.Join(",", row) + ")";
            return s;
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
            dataGridView1.Columns["ODCD"].Visible = false;                  // ODCDは非表示
            dataGridView1.Columns["KTCD"].Visible = false;                  // KTCDは非表示
            dataGridView1.Columns["CT"].DefaultCellStyle.Format = "N1";     // CTは小数点以下1桁
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        // 行番号を付ける（1 から始める）
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
            settings = Common.FormSettingsLoad(); // 他のフォームで変更された可能性があるので、最新の状態を読み込む
            string key = this.Name;
            if (!settings.Forms.ContainsKey(key)) settings.Forms[key] = new Common.FormSettings();
            var s = settings.Forms[key];
            s.X = this.Location.X;
            s.Y = this.Location.Y;
            s.Width = this.Width;
            s.Height = this.Height;
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
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

                double price = Convert.ToDouble(row.Cells["CT"].Value);

                for (int col = 6; col < dataGridView1.ColumnCount - 1; col++)
                {
                    if (row.Cells[col].Selected)
                    {
                        if (string.IsNullOrEmpty(row.Cells[col].Value.ToString())) continue;
                        double qty = Convert.ToDouble(row.Cells[col].Value);
                        sumProduct += qty * price;
                    }
                }
            }

            textBox作業時間.Text = (sumProduct / 3600).ToString("N2");
        }
        // 再読み込み
        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
        }


        private void BtnLoadChart_Click(object sender, EventArgs e)
        {
        }

    }
}
