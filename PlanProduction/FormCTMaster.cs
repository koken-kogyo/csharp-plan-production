using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PlanProduction
{
    public partial class FormCTMaster : Form
    {
        private FormConfig settings;
        private readonly OdCdSetting OdCdSetting;
        private DataTable dt = new();           // KM5030
        private DataTable dtMaster = new();     // D0410

        // --- 変更された行を記録するセット ---
        private readonly HashSet<int> changedRows = [];

        public FormCTMaster(OdCdSetting OdCdSetting)
        {
            InitializeComponent();

            this.KeyPreview = true;                     // フォームでキーイベントを受け取る設定

            this.OdCdSetting = OdCdSetting;

            // イベント登録
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;
            dataGridView1.RowPostPaint += DataGridView1_RowPostPaint;
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
            }

            labelMasterTitle.Text = " [KM5030] 標準作業時間マスタ ";
            labelMasterTitle.Text += $"【{OdCdSetting.OdCd}】 {DataStore.M0300Map[OdCdSetting.OdCd]} ／ {OdCdSetting.KtCd}";
            labelReadTitle.Text = "未登録品番一覧";

            // 抽出条件を生成
            var condition = "('" + OdCdSetting.OdCd + OdCdSetting.KtCd + "')";
            // 標準作業時間マスタ取得
            DBAccessor.ReadKM5030(ref dt, condition);
            // 手配品番マスタ取得
            DBAccessor.ReadD0410ConvertToMaster(ref dtMaster, condition);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.DataSource = dt;

            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView2.DataSource = dtMaster;
            if (dtMaster.Rows.Count > 0)
            {
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                foreach (DataGridViewColumn col in dataGridView2.Columns) col.ReadOnly = true;
            }
            toolStripStatusLabel1.Text = string.Empty;
        }
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                dataGridView1.Columns["VALDTF"].Visible = false;
                dataGridView1.Columns["WKSEQ"].Visible = false;
                dataGridView1.Columns["VALDTF"].Visible = false;
                dataGridView1.Columns["INSTID"].Visible = false;
                dataGridView1.Columns["INSTDT"].Visible = false;
                dataGridView1.Columns["UPDTID"].Visible = false;
                dataGridView1.Columns["UPDTDT"].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                foreach (DataGridViewColumn col in dataGridView1.Columns) col.ReadOnly = true;
                var ct = dataGridView1.Columns["CT"];
                ct.ReadOnly = false;                                                        // 変更可能
                ct.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;  // データの右寄せ
                ct.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;   // 列ヘッダー右寄せ
                ct.SortMode = DataGridViewColumnSortMode.NotSortable;                       // ソート機能を無効化
                ct.DefaultCellStyle.Format = "N1";                                          // 小数点以下1桁
                dataGridView1.Columns["NOTE"].ReadOnly = false;                             // 変更可能

            }
        }


        // 行番号を付ける
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
        // 行番号を付ける
        private void DataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
        // キーボードショートカット
        private void FormCTMaster_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
        // 変更された行数をステータスに表示
        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // 行番号を記録（重複なし）
            changedRows.Add(e.RowIndex);

            // StatusStripに表示
            toolStripStatusLabel1.Text = $"変更された行数：{changedRows.Count}";
        }

        // 「閉じる」ボタン
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 「保存」ボタン
        private void ButtonSaveClose_Click(object sender, EventArgs e)
        {
            // 標準作業時間マスタ更新
            if (DBAccessor.SaveKM5030(ref dt))
            {
                toolStripStatusLabel1.Text = "マスタを更新しました．";
                // 変更行数カウンタのリセット
                changedRows.Clear();

                // データソースの再読み込み
                dt.Rows.Clear();
                var condition = "('" + OdCdSetting.OdCd + OdCdSetting.KtCd + "')";
                DBAccessor.ReadKM5030(ref dt, condition);
                dataGridView1.DataSource = dt;
                //// 登録が完了したら一旦「品番」列でソート
                //dataGridView1.Sort(dataGridView1.Columns["HMCD"], ListSortDirection.Ascending);
                //// そして「品番」列の自動ソートは解除
                //dataGridView1.Columns["HMCD"].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        // 「追加」ボタン（未登録品番一覧から標準作業時間マスタに品番を移動）
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count <= 0)
            {
                MessageBox.Show("追加対象の品番を選択してください．");
                return;
            }
            int insertCnt = 0;
            foreach (DataGridViewCell cel in dataGridView2.SelectedCells)
            {
                // マスタ側に登録
                var newrow = dt.NewRow();
                newrow["ODCD"] = OdCdSetting.OdCd;
                newrow["WKGRCD"] = OdCdSetting.KtCd;
                newrow["HMCD"] = cel.Value;
                newrow["VALDTF"] = DateTime.Today;
                newrow["WKSEQ"] = Common.DEFAULT_WKSEQ;
                newrow["CT"] = 0;
                newrow["INSTID"] = Common.UserId;
                newrow["INSTDT"] = DateTime.Now;
                newrow["UPDTID"] = Common.UserId;
                newrow["UPDTDT"] = DateTime.Now;
                dt.Rows.Add(newrow);
                // 未登録品番一覧から削除
                DataRow[] rows = dtMaster.Select($"HMCD='{cel.Value}'");
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
            Common.FormSettingsSave(settings);
        }

    }
}
