using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PlanProduction
{
    public partial class FormSrttings : Form
    {
        private Common.FormConfig settings;
        public FormSrttings()
        {
            InitializeComponent();

            this.KeyPreview = true;                     // フォームでキーイベントを受け取る設定

        }

        private void FormSrttings_Load(object sender, EventArgs e)
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
            else
            {
                settings.Forms[key] = new Common.FormSettings();
            }
            dataGridView1.Rows.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = DataStore.dtKM5010kai;
            dataGridView1.Font = new Font("Yu Gothic UI", 12);
            dataGridView1.Columns[1].ReadOnly = true; // 1列目
            dataGridView1.Columns[2].ReadOnly = true; // 2列目
            dataGridView1.Columns[3].ReadOnly = true; // 3列目
            dataGridView1.ClearSelection();
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;

            if (!string.IsNullOrEmpty(DataStore.DefaultOdCd))
                checkBoxSelected.Checked = true; //_CheckedChanged(sender, e);

            ReCreateComboBox();
        }

        private void FormSrttings_FormClosing(object sender, FormClosingEventArgs e)
        {
            string key = this.Name;
            var s = settings.Forms[key];
            s.X = this.Location.X;
            s.Y = this.Location.Y;
            s.Width = this.Width;
            s.Height = this.Height;
            Common.FormSettingsSave(settings);
        }

        // コンボボックスの再作成
        private void ReCreateComboBox()
        {
            // 初期表示を行う手配先コードを設定
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
                .OrderBy(x => x.ODCD)
                .ToList();

            if (grouped.Count > 0)
            {
                // ▼ ComboBox に設定
                comboBox1.DataSource = grouped;
                comboBox1.DisplayMember = "DisplayName";
                comboBox1.ValueMember = "ODCD";

                // アプリケーション設定ファイルのデフォルト値を指定
                if (string.IsNullOrEmpty(DataStore.DefaultOdCd))
                {
                    comboBox1.SelectedIndex = -1;
                }
                else
                {
                    comboBox1.SelectedValue = DataStore.DefaultOdCd;
                }
            }
        }

        // ショートカットキー
        private void FormSrttings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                buttonSaveClose_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (textBoxSearchOdCd.Text != string.Empty)
                {
                    buttonClear_Click(sender, e);
                }
                else
                {
                    DataStore.dtKM5010kai = DataStore.originalKM5010kai.Copy();
                    DataStore.DefaultOdCd = DataStore.originalDefaultOdCd;
                    Close();
                    e.Handled = true;
                }
            }
        }

        // 設定ファイルなしの状態で画面を閉じた場合はアプリケーションを終了する
        private void FormSrttings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!File.Exists(@Common.CONFIG_FILE_AS)) Application.Exit();
        }

        // 手配先コード検索ボックス
        private void textBoxSearchOdCd_TextChanged(object sender, EventArgs e)
        {
            checkBoxSelected.Checked = false;
            string filter = (textBoxSearchOdCd.TextLength == 0) ? string.Empty :
                $"ODCD LIKE '{textBoxSearchOdCd.Text}*'";
            DataStore.dtKM5010kai.DefaultView.RowFilter = filter;
            dataGridView1.ClearSelection();
        }

        // 検索条件クリア
        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxSearchOdCd.Text = string.Empty;
        }

        // 保存して閉じる
        private void buttonSaveClose_Click(object sender, EventArgs e)
        {
            string errmsg = string.Empty;
            dataGridView1.ClearSelection();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    //
                    // 項目チェック
                    //
                    if (string.IsNullOrEmpty(row.Cells["ColumnListOrder"].Value.ToString()))
                    {
                        errmsg = "手配リスト順番を選択して下さい．";
                    }
                    if (string.IsNullOrEmpty(row.Cells["Column可動率"].Value.ToString()))
                    {
                        row.Cells["Column可動率"].Selected = true;
                        errmsg = "可動率を入力して下さい．";
                    }
                    if (!int.TryParse(row.Cells["Column可動率"].Value.ToString(), out int ava))
                    {
                        row.Cells["Column可動率"].Selected = true;
                        errmsg = "数値(1～100)を入力して下さい．";
                    }
                    if (ava < 1 || ava > 100)
                    {
                        row.Cells["Column可動率"].Selected = true;
                        errmsg = "数値(1～100)を入力して下さい．";
                    }
                }
                else
                {
                    // チェック状態から外された場合はデータテーブルの値をクリアしておく
                    if (!string.IsNullOrEmpty(row.Cells["ColumnListOrder"].Value.ToString()))
                    {
                        row.Cells["ColumnListOrder"].Value = "";
                    }
                    if (!string.IsNullOrEmpty(row.Cells["ColumnTanName"].Value.ToString()))
                    {
                        row.Cells["ColumnTanName"].Value = "";
                    }
                    if (!string.IsNullOrEmpty(row.Cells["Column可動率"].Value.ToString()))
                    {
                        row.Cells["Column可動率"].Value = "";
                    }
                }
                if (errmsg != string.Empty)
                {
                    MessageBox.Show(errmsg, "入力チェック", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridView1.Focus();
                    return;
                }
            }

            // 初期表示設定
            DataStore.DefaultOdCd = (comboBox1.SelectedIndex < 0) ? "" : comboBox1.SelectedValue.ToString();

            // アプリケーション設定ファイルへの書き込み
            Common.SerializeAppSettings();
            Close();
        }

        // 閉じるボタン
        private void buttonCancelClose_Click(object sender, EventArgs e)
        {
            DataStore.dtKM5010kai = DataStore.originalKM5010kai.Copy();
            DataStore.DefaultOdCd = DataStore.originalDefaultOdCd;
            Close();
        }

        // 選択されたものだけ表示
        private void checkBoxSelected_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBoxSelected.Checked;
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!isChecked) row.Visible = true;
                if (isChecked) row.Visible = (Convert.ToBoolean(row.Cells["ColumnSelected"].Value) == true);
            }
        }

        // 表示されている行に対して全てをチェック
        private void checkBoxAllCheck_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBoxAllCheck.Checked;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Visible)
                {
                    // データテーブルにチェック状態を設定
                    DataRow findRow = DataStore.dtKM5010kai.Rows.Find(new object[]
                        { row.Cells["ColumnOdCd"].Value, row.Cells["ColumnKtCd"].Value });
                    if (findRow != null) findRow["CHECKED"] = true;
                    row.Cells[0].Value = isChecked;
                }
            }
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
            ReCreateComboBox();
        }

        // セルクリック時にドロップダウンを即開く
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 4) return;

            // 対象セルが ComboBoxCell か判定
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
            {
                dataGridView1.BeginEdit(true);
                if (dataGridView1.EditingControl is ComboBox combo)
                {
                    combo.DroppedDown = true;
                }
            }
        }

        // クリックした瞬間にイベント
        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null) return;
            if (dataGridView1.CurrentCell is DataGridViewCheckBoxCell)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        // チェックされた瞬間にコンボボックスを再作成
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
            {
                bool isChecked = (bool)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                ReCreateComboBox();
            }
        }

        // 「担当者名」を編集する際は日本語入力とする
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox tb)
            {
                int col = dataGridView1.CurrentCell.ColumnIndex;
                if (col == 5)
                    tb.ImeMode = ImeMode.Hiragana;   // 日本語入力
                else
                    tb.ImeMode = ImeMode.Off;        // それ以外は英数字
            }
        }

    }
}
