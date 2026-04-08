using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static PlanProduction.Common;

namespace PlanProduction
{
    public partial class FormPlanProduction : Form
    {
        // 現在表示中の手配先コードを格納する変数
        private string selectedOdCd;
        private OdCdSetting OdCdSetting;
        private DateTime PlanDate;

        // フォームのサイズ等セッティング
        private FormConfig settings;

        // 表示データ
        private DataTable planDt = new();
        private DataTable achieveDt = new();

        // コンストラクタ
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
            if (settings.Forms.TryGetValue(key, out FormSettings s))
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(s.X, s.Y);
                this.Size = new Size(s.Width, s.Height);
                this.splitContainerMain.SplitterDistance = s.SplitterMainDistance;
                this.splitContainer上下.SplitterDistance = s.SplitterSubVerticalDistance;
                this.splitContainer計画と実績.SplitterDistance = s.SplitterSubHorizontalDistance;
            }

            PlanDate = DateTime.Now.Date;
            selectedOdCd = DataStore.DefaultOdCd;
            OdCdSetting = DataStore.OdCdSettings.FirstOrDefault(s => s.OdCd == selectedOdCd) ?? new OdCdSetting();
            ReCreateODCDButtons();

            monthCalendar1.MaxSelectionCount = 1; // 単一日付選択に制限
            monthCalendar1.SelectionStart = PlanDate;
            monthCalendar1.SelectionEnd = monthCalendar1.SelectionStart;
            monthCalendar1.DateSelected += MonthCalendar1_DateSelected; // 日付選択イベントの追加

            // コントロールの初期化
            dataGridViewPlan.EnableHeadersVisualStyles = false;
            dataGridViewPlan.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridViewPlan.RowHeadersVisible = false;
            dataGridViewPlan.DataBindingComplete += DataGridViewPlan_DataBindingComplete;

            dataGridViewAchieve.EnableHeadersVisualStyles = false;
            dataGridViewAchieve.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridViewAchieve.RowHeadersVisible = false;
            dataGridViewAchieve.DataBindingComplete += DataGridViewAchieve_DataBindingComplete;


            // 初期表示
            RefreshPlanProduction();
        }

        // 初期表示
        private void RefreshPlanProduction()
        {
            // タイトル設定
            labelTitle.Text = $"【{PlanDate:M/d}】 計画と実績";

            // 登録された計画日のカレンダーをBoldにする
            List<DateTime> planDates = DBAccessor.GetRegistDates();
            monthCalendar1.BoldedDates = [.. planDates];

            // 計画入力データの設定
            var paramPlan = new SaveOptions
            {
                OdCd = OdCdSetting.OdCd,
                PlanDate = PlanDate,
                Type = "P"
            };
            checkBoxPlanお昼稼働.Checked = false;
            checkBoxPlan休憩稼働.Checked = false;
            checkBoxPlanピカピカ.Checked = false;
            checkBoxPlan早昼.Checked = false;
            textBoxPlan可動率.Text = "";
            if (!DBAccessor.GetKD8020(ref paramPlan)) return;
            checkBoxPlanお昼稼働.Checked = paramPlan.昼稼働;
            checkBoxPlan休憩稼働.Checked = paramPlan.休憩稼働;
            checkBoxPlanピカピカ.Checked = paramPlan.ピカピカ;
            checkBoxPlan早昼.Checked = paramPlan.早昼;
            textBoxPlan可動率.Text = (paramPlan.可動率 != 0) ? paramPlan.可動率.ToString("F0") : "";
            planDt.Rows.Clear(); // ここから明細
            if (!DBAccessor.GetKD8030(ref planDt, paramPlan)) return;
            dataGridViewPlan.DataSource = planDt;

            // 実績入力データの設定
            var aparam = new SaveOptions
            {
                OdCd = OdCdSetting.OdCd,
                PlanDate = PlanDate,
                Type = "J"
            };
            checkBoxAchieveお昼稼働.Checked = false;
            checkBoxAchieve休憩稼働.Checked = false;
            checkBoxAchieveピカピカ.Checked = false;
            checkBoxAchieve早昼.Checked = false;
            textBoxAchieve可動率.Text = "";
            if (!DBAccessor.GetKD8020(ref aparam)) return;
            checkBoxAchieveお昼稼働.Checked = aparam.昼稼働;
            checkBoxAchieve休憩稼働.Checked = aparam.休憩稼働;
            checkBoxAchieveピカピカ.Checked = aparam.ピカピカ;
            checkBoxAchieve早昼.Checked = aparam.早昼;
            textBoxAchieve可動率.Text = (aparam.可動率 != 0) ? aparam.可動率.ToString("F0") : "";
            achieveDt.Rows.Clear(); // ここから明細
            if (!DBAccessor.GetKD8030(ref achieveDt, aparam)) return;
            dataGridViewAchieve.DataSource = achieveDt;

        }

        // データバインド後の調整
        private void DataGridViewPlan_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridViewPlan.ColumnHeadersDefaultCellStyle.Font = new Font(
                    dataGridViewPlan.ColumnHeadersDefaultCellStyle.Font.FontFamily, 7.0f);      // 小さいサイズ

            string[] colNames1Plan = ["No", "CT", "本数", "可動率"];
            foreach (string colName in colNames1Plan)
            {
                var col = dataGridViewPlan.Columns[colName];
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;     // ヘッダー中央寄せ
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;      // セル右寄せ
            }
            dataGridViewPlan.Columns["CT"].DefaultCellStyle.Format = "N1";                      // CTは小数点以下1桁
            dataGridViewPlan.Columns["可動率"].DefaultCellStyle.Format = "N0";                  // 小数点以下0桁

            string[] colNames2Plan = ["開始時刻", "終了時刻", "休憩時間"];
            foreach (string colName in colNames2Plan)
            {
                var col = dataGridViewPlan.Columns[colName];
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;     // ヘッダー中央寄せ
                col.DefaultCellStyle.Alignment = (colName != "休憩時間") ?
                    DataGridViewContentAlignment.MiddleCenter :
                    DataGridViewContentAlignment.MiddleRight;
                col.ReadOnly = true;
            }

            dataGridViewPlan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            // No列は固定幅
            var colNo = dataGridViewPlan.Columns["No"];
            colNo.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colNo.Width = 30;
        }

        // データバインド後の調整
        private void DataGridViewAchieve_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridViewAchieve.ColumnHeadersDefaultCellStyle.Font = new Font(
                    dataGridViewAchieve.ColumnHeadersDefaultCellStyle.Font.FontFamily, 7.0f);  // 小さいサイズ

            string[] colNames1Plan = ["No", "CT", "本数", "可動率"];
            foreach (string colName in colNames1Plan)
            {
                var col = dataGridViewAchieve.Columns[colName];
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;    // ヘッダー中央寄せ
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     // セル右寄せ
            }
            dataGridViewAchieve.Columns["CT"].DefaultCellStyle.Format = "N1";                  // CTは小数点以下1桁
            dataGridViewAchieve.Columns["可動率"].DefaultCellStyle.Format = "N0";              // 小数点以下0桁

            string[] colNames2Plan = ["開始時刻", "終了時刻", "休憩時間"];
            foreach (string colName in colNames2Plan)
            {
                var col = dataGridViewAchieve.Columns[colName];
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;     // ヘッダー中央寄せ
                col.DefaultCellStyle.Alignment = (colName != "休憩時間") ?
                    DataGridViewContentAlignment.MiddleCenter :
                    DataGridViewContentAlignment.MiddleRight;
                col.ReadOnly = true;
            }

            dataGridViewAchieve.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            // No列は固定幅
            var colNo = dataGridViewAchieve.Columns["No"];
            colNo.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colNo.Width = 30;
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
            int buttonCount = grouped.Count;
            int buttonHeight = (this.Height > buttonCount * 80) ? 80 : 40;
            for (int i = 0; i < buttonCount; i++)
            {
                var btn = CreateTileButton(grouped[i].ODCD, grouped[i].ODRNM, buttonHeight);
                splitContainerMain.Panel1.Controls.Add(btn);
                // スペーサーを挟む
                var spacer = new Panel
                {
                    Height = 10,
                    Dock = DockStyle.Top
                };
                splitContainerMain.Panel1.Controls.Add(spacer);
            }
        }
        // 左パネルにボタンを作成（実態）
        private Button CreateTileButton(string odcd, string odrnm, int buttonHeight)
        {
            var btn = new Button
            {
                Name = odcd,
                Text = odcd + "\n" + odrnm,
                Height = buttonHeight,
                Dock = DockStyle.Top,
                BackColor = (odcd == selectedOdCd) ? Color.LightGreen : Color.LightGray,
                ForeColor = Color.Black
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Margin = new Padding(5);
            btn.Click += (s, e) =>
            {
                selectedOdCd = odcd;
                OdCdSetting = DataStore.OdCdSettings.FirstOrDefault(s => s.OdCd == selectedOdCd) ?? new OdCdSetting();
                foreach (Control ctrl in splitContainerMain.Panel1.Controls)
                {
                    if (ctrl is Button)
                        ctrl.BackColor = (ctrl.Name == selectedOdCd) ? Color.LightBlue : Color.LightGray;
                }
                RefreshPlanProduction();
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
            if (e.KeyCode == Keys.F5)
            {
                RefreshPlanProduction();
            }
        }

        // アプリケーションの終了
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 「設定画面」をモーダルで呼び出す
        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            FormSrttings fm = new();
            fm.ShowDialog();
            // アプリケーション設定が変更されているかを判定
            if (!DataTableEquals_MultiKey(DataStore.originalKM5010kai, DataStore.dtKM5010kai, "ODCD", "WKGRCD"))
            {
                OdCdSetting = DataStore.OdCdSettings.FirstOrDefault(s => s.OdCd == selectedOdCd) ?? new OdCdSetting();
                DataStore.originalKM5010kai = DataStore.dtKM5010kai.Copy();
                ReCreateODCDButtons();
            }
            // アプリケーション設定が変更されているかを
            // 判定
            if (DataStore.originalDefaultOdCd != DataStore.DefaultOdCd)
            {
                selectedOdCd = DataStore.DefaultOdCd;
                DataStore.originalDefaultOdCd = selectedOdCd;
                ReCreateODCDButtons();
            }
        }

        // アプリケーション設定が変更されているかを判定
        private static bool DataTableEquals_MultiKey(DataTable dt1, DataTable dt2, string key1, string key2)
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

        // 「手配一覧」だけをモードレスで呼び出す
        private void ButtonOrderList_Click(object sender, EventArgs e)
        {
            var frm = new FormOrderList(OdCdSetting, null);
            frm.Show();
        }

        // 「計画入力」と「手配一覧（コールバック付き）」をモードレスで呼び出す
        private void ButtonPlanEntry_Click(object sender, EventArgs e)
        {
            var frm = new FormPlanEntry(PlanDate, OdCdSetting);
            // イベント登録
            frm.IsUpdated += CallBackPlanEntry;
            frm.Show();
        }
        // コールバック関数（サブフォームからの戻り値を受け取る）
        private void CallBackPlanEntry(bool isupdated)
        {
            if (isupdated) RefreshPlanProduction();
        }
        // カレンダー選択
        private void MonthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            PlanDate = monthCalendar1.SelectionStart;
            RefreshPlanProduction();
        }


    }// FormPlanProduction

}

