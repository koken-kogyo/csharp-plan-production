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
        private Common.FormConfig settings;

        // 現在表示中の手配先コードを格納する変数
        private string selectedOdCd;

        // DataTable を保持するフィールドを作る
        private DataTable dt = new DataTable();

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
                this.splitContainerMain.SplitterDistance = s.SplitterMainDistance;
            }

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;

            // 手配データ取得
            var condition = ExtracConditions();
            DBAccessor.ReadD0410(ref dt, condition);
            dataGridView1.DataSource = dt;


            // チャートデータの更新
            UpdateChart();

        }

        // オラクルの抽出条件を作成
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
            for (int col = 5; col < dataGridView1.Columns.Count; col++)
            {
                //dataGridView1.Columns[col].Width = 50;
                dataGridView1.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;   // データの右寄せ
                dataGridView1.Columns[col].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;   // 列ヘッダー右寄せ
                dataGridView1.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;                       // ソート機能を無効化
            }
            dataGridView1.Columns["KTCD"].Visible = false; // KTCDは非表示
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
            s.SplitterMainDistance = splitContainerMain.SplitterDistance;
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

        // 再読み込み
        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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

    }
}
