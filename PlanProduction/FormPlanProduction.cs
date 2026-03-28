using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static PlanProduction.Common;

namespace PlanProduction
{
    public partial class FormPlanProduction : Form
    {
        private FormConfig settings;

        // 現在表示中の手配先コードを格納する変数
        private string selectedOdCd;
        private OdCdSetting OdCdSetting;

        public FormPlanProduction()
        {
            InitializeComponent();

            this.KeyPreview = true;                     // フォームでキーイベントを受け取る設定

        }

        private void FormPlanProduction_Load(object sender, EventArgs e)
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
                this.splitContainer上下.SplitterDistance = s.SplitterSubVerticalDistance;
                this.splitContainer計画と実績.SplitterDistance = s.SplitterSubHorizontalDistance;
            }

            selectedOdCd = DataStore.DefaultOdCd;
            OdCdSetting = DataStore.OdCdSettings.FirstOrDefault(s => s.OdCd == selectedOdCd) ?? new OdCdSetting();
            ReCreateODCDButtons();

            monthCalendar1.BoldedDates = new[]
            {
                new DateTime(2026, 3, 10),
                new DateTime(2026, 3, 15),
                new DateTime(2026, 3, 20)
            };



        }

        // フォームの状態を保存
        private void FormPlanProduction_FormClosing(object sender, FormClosingEventArgs e)
        {
            settings = FormSettingsLoad(); // 他のフォームで変更された可能性があるので、最新の状態を読み込む
            string key = this.Name;
            if (!settings.Forms.ContainsKey(key)) settings.Forms[key] = new FormSettings();
            var s = settings.Forms[key];
            s.X = this.Location.X;
            s.Y = this.Location.Y;
            s.Width = this.Width;
            s.Height = this.Height;
            s.SplitterMainDistance = splitContainerMain.SplitterDistance;
            s.SplitterSubVerticalDistance = splitContainer上下.SplitterDistance;
            s.SplitterSubHorizontalDistance = splitContainer計画と実績.SplitterDistance;
            Common.FormSettingsSave(settings);
        }

        // 左パネルにボタンを作成
        private void ReCreateODCDButtons()
        {
            // アプリケーション設定ファイルをグループ化
            var grouped = DataStore.dtKM5010kai.AsEnumerable()
                .Where(r => r.Field<bool>("CHECKED") == true)
                .GroupBy(r => new
                {
                    OdCd = r.Field<string>("ODCD"),
                    Name = r.Field<string>("ODRNM")
                })
                .Select(g => new
                {
                    ODCD = g.Key.OdCd,
                    ODRNM = g.Key.Name,
                    DisplayName = g.Key.OdCd + "：" + g.Key.Name
                })
                .OrderByDescending(x => x.ODCD)
                .ToList();
            ;
            // 既存のボタンを削除
            splitContainerMain.Panel1.Controls.Clear();
            // ボタンの作成
            int buttonCount = grouped.Count();
            for (int i = 0; i < buttonCount; i++)
            {
                var btn = CreateTileButton(grouped[i].ODCD, grouped[i].ODRNM);
                splitContainerMain.Panel1.Controls.Add(btn);
                // スペーサーを挟む
                var spacer = new Panel();
                spacer.Height = 10;
                spacer.Dock = DockStyle.Top;
                splitContainerMain.Panel1.Controls.Add(spacer);
            }
        }
        private Button CreateTileButton(string odcd, string odrnm)
        {
            var btn = new Button();

            btn.Name = odcd;
            btn.Text = odcd + "\n" + odrnm;
            btn.Height = 80;
            btn.Dock = DockStyle.Top;

            //            btn.Font = new Font("Meiryo", 12, FontStyle.Bold);
            btn.BackColor = (odcd == selectedOdCd) ? Color.LightGreen : Color.LightGray;
            btn.ForeColor = Color.Black;

            btn.FlatAppearance.BorderSize = 0;

            btn.Margin = new Padding(5);

            btn.Click += (s, e) =>
            {
                selectedOdCd = odcd;
                OdCdSetting = DataStore.OdCdSettings.FirstOrDefault(s => s.OdCd == selectedOdCd) ?? new OdCdSetting();
                foreach (Control ctrl in splitContainerMain.Panel1.Controls)
                {
                    if (ctrl is Button)
                        ctrl.BackColor = (ctrl.Name == selectedOdCd) ? Color.LightBlue: Color.LightGray;
                }
            };
            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = Color.LightBlue;
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = (btn.Text.Contains(selectedOdCd)) ? Color.LightGreen : Color.LightGray;
            };
            return btn;
        }

        // フォームのショットカットイベント
        private void FormPlanProduction_KeyDown(object sender, KeyEventArgs e)
        {
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

        // 「設定画面」をモーダルで呼び出す
        private void buttonSettings_Click(object sender, EventArgs e)
        {
            FormSrttings fm = new FormSrttings();
            fm.ShowDialog();
            // アプリケーション設定が変更されているかを判定
            if (!DataTableEquals_MultiKey(DataStore.originalKM5010kai, DataStore.dtKM5010kai, "ODCD", "WKGRCD"))
            {
                DataStore.originalKM5010kai = DataStore.dtKM5010kai.Copy();
                ReCreateODCDButtons();
            }
            // アプリケーション設定が変更されているかを判定
            if (DataStore.originalDefaultOdCd != DataStore.DefaultOdCd)
            {
                selectedOdCd = DataStore.DefaultOdCd;
                DataStore.originalDefaultOdCd = selectedOdCd;
                ReCreateODCDButtons();
            }
        }

        // アプリケーション設定が変更されているかを判定
        private bool DataTableEquals_MultiKey(DataTable dt1, DataTable dt2, string key1, string key2)
        {
            if (dt1 is null) return false;
            if (dt1.Rows.Count != dt2.Rows.Count) return false; // 行数が違えば即NG

            // dt2 を検索しやすいように Dictionary 化
            var dict2 = dt2.AsEnumerable()
                           .ToDictionary(
                               r => (r[key1], r[key2]),
                               r => r
                           );
            foreach (DataRow row1 in dt1.Rows)
            {
                var key = (row1[key1], row1[key2]);

                // 同じキーが dt2 に存在しない
                if (!dict2.TryGetValue(key, out DataRow row2))
                    return false;

                // 各列の値を比較
                if (!row1.ItemArray.SequenceEqual(row2.ItemArray))
                    return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new FormPlanEntry("6031A", "鈴木", "70");
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var frm = new FormOrderList(true, OdCdSetting);
            frm.Show();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime d = e.Start;
            MessageBox.Show(d.ToShortDateString());
        }

    }// FormPlanProduction

}

