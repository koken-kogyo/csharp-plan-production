using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlanProduction
{
    public partial class FormAchieveList : Form
    {
        // 現在表示中の手配先コードを格納する変数
        private readonly OdCdSetting OdCdSetting;

        public FormAchieveList(OdCdSetting odcdsetting)
        {
            InitializeComponent();

            this.OdCdSetting = odcdsetting;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormAchieveList_Load(object sender, EventArgs e)
        {
            DateTime first = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime last = first.AddMonths(1).AddDays(-1);

            // カレンダーコントロールの初期設定
            monthCalendar1.SelectionStart = first;
            monthCalendar1.SelectionEnd = last;
            monthCalendar1.DateChanged += MonthCalendar1_DateChanged; // 日付範囲の変更イベント追加

            // データグリッドの初期設定
                // ヘッダー関連
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(
                    dataGridView1.ColumnHeadersDefaultCellStyle.Font.FontFamily, 10.5f);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.ColumnHeadersHeight = 32;
                // 行高さ
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.RowTemplate.Height = 32;

            ShowAchieveList();
        }

        private void MonthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            ShowAchieveList();
        }

        private void ShowAchieveList()
        {
            DataTable dt = new();
            var odcd = OdCdSetting.OdCd;
            var s = monthCalendar1.SelectionStart;
            var e = monthCalendar1.SelectionEnd;
            if (!DBAccessor.GetKD8020List(ref dt, odcd, s, e)) return;
            dataGridView1.DataSource = dt;

            // データグリッドの詳細設定
            //dataGridView1.Columns["ODCD"].HeaderText = "手配先コード";
            //dataGridView1.Columns["PLANDT"].HeaderText = "実績日付";
            //dataGridView1.Columns["TYPE"].Visible = false;
            //dataGridView1.Columns["STARTTIME"].HeaderText = "開始時刻";
            //dataGridView1.Columns["ENDTIME"].HeaderText = "終了時刻";
            //dataGridView1.Columns["HIRUKADO"].HeaderText = "お昼稼働";
            //dataGridView1.Columns["KYUKEIKADO"].HeaderText = "休憩稼働";
            //dataGridView1.Columns["PIKAPIKA"].HeaderText = "ピカピカ";
            //dataGridView1.Columns["HAYAHIRU"].HeaderText = "早昼";
            //dataGridView1.Columns["NOTE"].HeaderText = "備考";
            //dataGridView1.Columns["TTLQTY"].HeaderText = "本数";
            //dataGridView1.Columns["CTHOUR"].HeaderText = "CT時間";
            //dataGridView1.Columns["WORKHOUR"].HeaderText = "就業時間";
            //dataGridView1.Columns["OPEHOUR"].HeaderText = "稼働時間";
            //dataGridView1.Columns["BREAKTIME"].HeaderText = "休憩時間";
            //dataGridView1.Columns["EQURATE"].HeaderText = "設備稼働率";
            //dataGridView1.Columns["AVA"].HeaderText = "可動率";
        }

        private void ButtonAchieveExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("データが存在しません．", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            int m = monthCalendar1.SelectionStart.Month;
            string saveFullPath = @$"{desktop}\実績一覧_{OdCdSetting.OdCd}_{m}月.xlsx";
            if (Path.Exists(saveFullPath))
            {
                if (MessageBox.Show($"既にファイルが存在しています．\n上書きしてもよろしいですか？", "上書き確認",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    File.Delete(saveFullPath);
                }
                else
                {
                    return;
                }
            }
            // 実績一覧Excelを出力し別プロセスで開く
            Common.PrintAchieve(ref dataGridView1, saveFullPath);
        }
    }
}
