using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PlanProduction
{
    public partial class FormPlanEntry : Form
    {
        // フォーム起動時の値
        private FormConfig settings;

        // モードレスなのでインスタンス保持する
        private readonly FormOrderList formOrderList;

        // 現在表示中の手配先コードを格納する変数
        private readonly OdCdSetting OdCdSetting;
        private double 可動率;

        // DataTable を保持するフィールドを作る
        private readonly DataTable dt = new();

        public FormPlanEntry(OdCdSetting OdCdSetting)
        {
            InitializeComponent();

            this.KeyPreview = true;                     // フォームでキーイベントを受け取る設定

            // 手配一覧を開く
            formOrderList = new FormOrderList(OdCdSetting, OnSelectedList);  // ← コールバック渡す
            formOrderList.Show();                       // モードレス

            // 表示対象の手配先コードをこのフォームに保持
            this.OdCdSetting = OdCdSetting;

            // イベント登録
            dataGridViewPlan.RowPostPaint += DataGridViewPlan_RowPostPaint;
            dataGridViewPlan.CellEndEdit += DataGridViewPlan_CellEndEdit;
            dataGridViewPlan.CellValueChanged += DataGridViewPlan_CellValueChanged;
        }

        // 手配一覧から選択されたアイテムを受け取るコールバック関数
        private void OnSelectedList(string mode, List<SelectedItem> list)
        {
            DataGridView dgv = (mode == "Plan") ? dataGridViewPlan : dataGridViewAchieve;
            foreach (var item in list)
            {
                dgv.Rows.Add(item.HmCd, item.CT, item.SumQty);
            }
            if (mode == "Plan") CalculatePlan(); // 計画リストが更新されたら計画終了時刻を再計算
        }


        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormPlanEntry_Load(object sender, EventArgs e)
        {
            // フォームの状態を復元
            settings = Common.FormSettingsLoad();
            string key = this.Name;
            if (settings.Forms.TryGetValue(key, out FormSettings s))
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(s.X, s.Y);
                this.Size = new Size(s.Width, s.Height);
                splitContainerMain.SplitterDistance = s.SplitterMainDistance;
            }

            // 初期表示
            labelTitleOdCd.Text = $"【{OdCdSetting.OdCd}】 {DataStore.M0300Map[OdCdSetting.OdCd]}";
            textBoxPlan可動率.Text = (string.IsNullOrEmpty(OdCdSetting.Ava)) ? "70" : OdCdSetting.Ava;
            可動率 = double.TryParse(OdCdSetting.Ava, out double result) ? 1 / result * 100 : 1.4286; // デフォルトは70%で1.4286倍
            textBoxPlanStartTime.Text = (string.IsNullOrEmpty(OdCdSetting.StartTime)) ? "08:15" : OdCdSetting.StartTime;
            textBoxPlanQty.Text = "0";
            textBoxPlanCT.Text = "0.0";
            textBoxPlanKdo.Text = "0.0";
            //checkBoxPlanピカピカ.Checked 金曜日なら

            // 計画リストの初期設定
            dataGridViewPlan.EnableHeadersVisualStyles = false;
            dataGridViewPlan.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            var ctP = dataGridViewPlan.Columns["PlanCT"];
            ctP.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;         // ヘッダー右寄せ
            ctP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;          // セル右寄せ
            ctP.DefaultCellStyle.Format = "N1";                                                 // CTは小数点以下1桁
            var qtyP = dataGridViewPlan.Columns["Plan本数"];
            qtyP.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;        // ヘッダー右寄せ
            qtyP.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;         // セル右寄せ
            string[] colNamesPlan = ["Plan開始時刻", "Plan終了時刻", "Plan休憩時間"];
            foreach (string colName in colNamesPlan)
            {
                var col = dataGridViewPlan.Columns[colName];
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;     // ヘッダー中央寄せ
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;     // セル中央寄せ
                col.HeaderCell.Style.Font = new Font(
                    dataGridViewPlan.ColumnHeadersDefaultCellStyle.Font.FontFamily, 7.0f);      // 小さいサイズ
            }
            dataGridViewPlan.RowTemplate.Height = 30;
            dataGridViewPlan.Rows[dataGridViewPlan.NewRowIndex].Height = 30;
            //dataGridViewPlan.Rows.Add("T1855-70743-000", 60, "08:15", "10:10", "", "鈴木", "");

            // 実績リストの初期設定
            dataGridViewAchieve.EnableHeadersVisualStyles = false;
            dataGridViewAchieve.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            var ctA = dataGridViewAchieve.Columns["AchieveCT"];
            ctA.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;         // ヘッダー右寄せ
            ctA.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;          // セル右寄せ
            ctA.DefaultCellStyle.Format = "N1";                                                 // CTは小数点以下1桁
            var qtyA = dataGridViewAchieve.Columns["Achieve本数"];
            qtyA.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;        // ヘッダー右寄せ
            qtyA.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;         // セル右寄せ
            string[] colNamesAchieve = ["Achieve開始時刻", "Achieve終了時刻", "Achieve休憩時間"];
            foreach (string colName in colNamesAchieve)
            {
                var col = dataGridViewAchieve.Columns[colName];
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;     // ヘッダー中央寄せ
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;     // セル中央寄せ
                col.HeaderCell.Style.Font = new Font(
                    dataGridViewAchieve.ColumnHeadersDefaultCellStyle.Font.FontFamily, 7.0f);   // 小さいサイズ
            }
            dataGridViewAchieve.RowTemplate.Height = 30;
            dataGridViewAchieve.Rows[dataGridViewAchieve.NewRowIndex].Height = 30;
        }

        // 行番号を付ける（1 から始める）
        private void DataGridViewPlan_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (e.RowIndex == dgv.NewRowIndex) return; // ★ 新規行は行番号を描画しない、「※」は描画

            // ★ 通常行は矢印を上書きして消す
            Rectangle rect = new(
                e.RowBounds.Left + 1,
                e.RowBounds.Top + 1,
                dgv.RowHeadersWidth - 2,
                e.RowBounds.Height - 2);
            e.Graphics.FillRectangle(SystemBrushes.Control, rect);

            // 行番号を描画
            string rowNumber = (e.RowIndex + 1).ToString();
            var headerBounds = new Rectangle(
                e.RowBounds.Left,
                e.RowBounds.Top,
                dataGridViewPlan.RowHeadersWidth,
                e.RowBounds.Height);
            TextRenderer.DrawText(
                e.Graphics,
                rowNumber,
                dataGridViewPlan.Font,
                headerBounds,
                Color.Black,
                TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
        }

        private void DataGridViewPlan_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value != null && e.ColumnIndex == 0) cell.Value = cell.Value.ToString().ToUpper();// 品番のみ小文字を大文字に変換（入力中は小文字）
            dataGridViewPlan.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void DataGridViewPlan_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // 列2または列3が変更された場合に処理
            if (e.ColumnIndex == 1 || e.ColumnIndex == 2) CalculatePlan();
        }

        private void FormPlanEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            formOrderList?.Close(); // 手配一覧も閉じる

            settings = Common.FormSettingsLoad(); // 他のフォームで変更された可能性があるので、最新の状態を読み込む
            string key = this.Name;
            if (!settings.Forms.ContainsKey(key)) settings.Forms[key] = new FormSettings();
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

        private void ButtonPlanClear_Click(object sender, EventArgs e)
        {
            dataGridViewPlan.Rows.Clear();
        }

        private void ButtonAchieveClear_Click(object sender, EventArgs e)
        {
            dataGridViewAchieve.Rows.Clear();
        }

        private void ButtonSaveClose_Click(object sender, EventArgs e)
        {

        }


        private void TextBoxPlan可動率_Enter(object sender, EventArgs e)
        {
            textBoxPlan可動率.SelectAll();
        }
        private void TextBoxPlan可動率_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (double.TryParse(textBoxPlan可動率.Text, out double result))
                {
                    可動率 = 1 / result * 100;
                }
                else
                {
                    可動率 = 1.4286; // デフォルトは70%で1.4286倍
                    textBoxPlan可動率.Text = "70";
                }
                CalculatePlan();
                textBoxPlan可動率.SelectAll();
                e.Handled = true;
            }
        }
        private void TextBoxPlan可動率_MouseDown(object sender, MouseEventArgs e)
        {
            textBoxPlan可動率.SelectAll();
        }

        private void TextBoxPlanStartTime_Enter(object sender, EventArgs e)
        {
            textBoxPlanStartTime.SelectAll();
        }
        private void TextBoxPlanStartTime_MouseDown(object sender, MouseEventArgs e)
        {
            textBoxPlanStartTime.SelectAll();
        }
        private void TextBoxPlanStartTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (DateTime.TryParse(textBoxPlanStartTime.Text, out _)) CalculatePlan();
                textBoxPlanStartTime.SelectAll();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                if (DateTime.TryParse(textBoxPlanStartTime.Text, out DateTime dt))
                {
                    int minutes = (e.KeyCode == Keys.Up) ? 1 :
                                  (e.KeyCode == Keys.Down) ? -1 :
                                  (e.KeyCode == Keys.Left) ? 10 :
                                  (e.KeyCode == Keys.Right) ? -10 : 0;
                    textBoxPlanStartTime.Text = dt.AddMinutes(minutes).ToString("HH:mm");
                    CalculatePlan();
                }
                textBoxPlanStartTime.SelectAll();
                e.Handled = true;
            }
        }
        private void TextBoxPlanStartTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (e.KeyChar == '\b') return;  // バックスペースはOK
            if (!char.IsDigit(e.KeyChar))   // 数字以外は入力させない
            {
                e.Handled = true;
                return;
            }
            // 2文字入力した時点で自動的に「:」を挿入
            if (tb.Text.Length == 1)
            {
                tb.Text += e.KeyChar + ":";
                tb.SelectionStart = tb.Text.Length;   // カーソルを末尾へ移動
                e.Handled = true;                    // 入力済みなので処理完了
            }
            // 2文字入力した時点で自動的に「:」を挿入
            if (tb.Text.Length == 2 && e.KeyChar != ':')
            {
                tb.Text += ":" + e.KeyChar;
                tb.SelectionStart = tb.Text.Length;   // カーソルを末尾へ移動
                e.Handled = true;                    // 入力済みなので処理完了
            }
        }



        private void CalculatePlan()
        {
            // 計画開始時刻と計画CTから計画終了時刻を計算して、DataGridView に反映するロジックをここに実装
            // 例: 各行の計画開始時刻 + (計画CT * 可動率) を計算して、計画終了時刻のセルにセットする
            double 合計本数 = 0.0;
            double CT稼働時間 = 0.0;
            double 計画稼働時間 = 0.0;
            for (int i = 0; i < dataGridViewPlan.Rows.Count; i++)
            {
                var row = dataGridViewPlan.Rows[i]; // ここで通常処理

                if (row.IsNewRow) continue;         // 新規行はスキップ

                // 計画開始時刻を取得
                row.Cells["Plan開始時刻"].Value = (i == 0) ?
                    textBoxPlanStartTime.Text :
                    dataGridViewPlan.Rows[i - 1].Cells["Plan終了時刻"].Value?.ToString();
                string startTimeStr = row.Cells["Plan開始時刻"].Value?.ToString();
                if (!DateTime.TryParse(startTimeStr, out DateTime startTime))
                {
                    row.Cells["Plan終了時刻"].Value = startTimeStr; // 無効な開始時刻の場合
                    continue;
                }
                // 計画CTを取得
                string ctStr = row.Cells["PlanCT"].Value?.ToString();
                if (!double.TryParse(ctStr, out double ct))
                {
                    row.Cells["Plan終了時刻"].Value = startTimeStr; // 無効なCTの場合
                    continue;
                }
                // 計画終了時刻を計算
                double 本数 = (row.Cells["Plan本数"].Value != null) ? Convert.ToDouble(row.Cells["Plan本数"].Value) : 0.0;
                double adjustedCt = ct * 本数 * 可動率; // 可動率を考慮してCTを調整
                DateTime endTime = startTime.AddSeconds(adjustedCt);
                // 計画終了時刻をセルにセット
                row.Cells["Plan終了時刻"].Value = endTime.ToString("HH:mm");
                // 作業者をセット
                row.Cells["Plan作業者"].Value = OdCdSetting.TanName;
                // サマリー
                合計本数 += 本数;
                CT稼働時間 += ct * 本数;
                計画稼働時間 += adjustedCt;
            }
            textBoxPlanQty.Text = 合計本数.ToString("#,0");
            textBoxPlanCT.Text = (CT稼働時間 / 3600).ToString("N1");
            textBoxPlanKdo.Text = (計画稼働時間 / 3600).ToString("N1");
            textBoxPlanEndTime.Text = (dataGridViewPlan.Rows.Count > 1) ?
                dataGridViewPlan.Rows[^2].Cells["Plan終了時刻"].Value?.ToString() :
                textBoxPlanStartTime.Text;
        }

    }
}
