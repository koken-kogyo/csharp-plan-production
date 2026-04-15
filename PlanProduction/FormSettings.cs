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
        private FormConfig settings;
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
            if (settings.Forms.TryGetValue(key, out FormSettings s))
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(s.X, s.Y);
                this.Size = new Size(s.Width, s.Height);
            }
            else
            {
                settings.Forms[key] = new FormSettings();
            }
            dataGridView1.Rows.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Font = new Font("Yu Gothic UI", 12);
            dataGridView1.Columns[1].ReadOnly = true; // 1列目
            dataGridView1.Columns[2].ReadOnly = true; // 2列目
            dataGridView1.Columns[3].ReadOnly = true; // 3列目
            dataGridView1.Columns["ColumnExcel"].ReadOnly = true;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridView1.DataSource = DataStore.dtKM5010kai;
            ReCreateComboBox();
        }
        // 初回起動表示
        private void FormSrttings_Shown(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            dataGridView1.ClearSelection();
            if (!string.IsNullOrEmpty(DataStore.DefaultOdCd))
                checkBoxSelected.Checked = true;
            textBoxSearchOdCd.Focus();
        }
        // 画面閉じる前
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
        // ショートカットキー
        private void FormSrttings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                ButtonSaveClose_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (textBoxSearchOdCd.Text != string.Empty)
                {
                    ButtonClear_Click(sender, e);
                }
                else
                {
                    ButtonCancelClose_Click(sender, e);
                    e.Handled = true;
                }
            }
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

        // 手配先コード検索ボックス
        private void TextBoxSearchOdCd_TextChanged(object sender, EventArgs e)
        {
            checkBoxSelected.Checked = false;
            string filter = (textBoxSearchOdCd.TextLength == 0) ? string.Empty :
                $"ODCD LIKE '{textBoxSearchOdCd.Text}*'";
            DataStore.dtKM5010kai.DefaultView.RowFilter = filter;
            dataGridView1.ClearSelection();
        }

        // 検索条件クリア
        private void ButtonClear_Click(object sender, EventArgs e)
        {
            textBoxSearchOdCd.Text = string.Empty;
        }

        // 初期値で埋める
        private void ButtonInitialValue_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0) return;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    string exeDir = Application.StartupPath;

                    if (string.IsNullOrEmpty(row.Cells["ColumnListOrder"].Value.ToString())) 
                        row.Cells["ColumnListOrder"].Value = Common.sortOrderMap[2];
                    if (string.IsNullOrEmpty(row.Cells["ColumnTanName"].Value.ToString()))
                        row.Cells["ColumnTanName"].Value = "鈴木";
                    if (string.IsNullOrEmpty(row.Cells["Column可動率"].Value.ToString()))
                        row.Cells["Column可動率"].Value = "80";
                    if (string.IsNullOrEmpty(row.Cells["Column開始時刻"].Value.ToString()))
                        row.Cells["Column開始時刻"].Value = "08:15";
                    if (string.IsNullOrEmpty(row.Cells["ColumnExcel"].Value?.ToString()))
                        row.Cells["ColumnExcel"].Value = "雛形_Default.xlsx";
                    if (string.IsNullOrEmpty(row.Cells["ColumnFullPath"].Value?.ToString()))
                        row.Cells["ColumnFullPath"].Value = exeDir + @"\雛形_Default.xlsx";
                }
            }
        }

        // 保存して閉じる
        private void ButtonSaveClose_Click(object sender, EventArgs e)
        {
            string errmsg = string.Empty;
            dataGridView1.ClearSelection();
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("手配コードを選択して下さい．", "入力チェック",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                comboBox1.Focus();
                return;
            }
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
                    if (!TimeSpan.TryParse(row.Cells["Column開始時刻"].Value.ToString(), out _))
                    {
                        row.Cells["Column開始時刻"].Selected = true;
                        errmsg = "時刻を入力してください．";
                    }
                    if (string.IsNullOrEmpty(row.Cells["ColumnExcel"].Value?.ToString()))
                    {
                        errmsg = "雛形ファイルを選択してください．";
                    }
                }
                else
                {
                    // データテーブルに残っている値を初期化
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
                    if (!string.IsNullOrEmpty(row.Cells["Column開始時刻"].Value.ToString()))
                    {
                        row.Cells["Column開始時刻"].Value = "";
                    }
                    if (!string.IsNullOrEmpty(row.Cells["ColumnExcel"].Value?.ToString()))
                    {
                        row.Cells["ColumnExcel"].Value = "";
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
        private void ButtonCancelClose_Click(object sender, EventArgs e)
        {
            DataStore.dtKM5010kai = DataStore.originalKM5010kai?.Copy();
            DataStore.DefaultOdCd = DataStore.originalDefaultOdCd;
            Close();
        }

        // 選択されたものだけ表示
        private void CheckBoxSelected_CheckedChanged(object sender, EventArgs e)
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
        private void CheckBoxAllCheck_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBoxAllCheck.Checked;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Visible)
                {
                    // データテーブルにチェック状態を設定
                    DataRow findRow = DataStore.dtKM5010kai.Rows.Find([row.Cells["ColumnOdCd"].Value, row.Cells["ColumnKtCd"].Value]);
                    if (findRow != null) findRow["CHECKED"] = true;
                    row.Cells[0].Value = isChecked;
                }
            }
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
            ReCreateComboBox();
        }

        // セルクリック時にドロップダウンを即開く
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
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
            if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnExcel")
            {
                string settingFullPath = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString();
                string initialPath = Path.GetDirectoryName(settingFullPath);
                using (var dlg = new OpenFileDialog())
                {
                    dlg.Filter = "Excel ファイル (*.xlsx)|*.xlsx";
                    dlg.Title = "Excel ファイルを選択してください";
                    dlg.InitialDirectory = (initialPath == "") ? Application.StartupPath : initialPath;
                    dlg.RestoreDirectory = true;

                    // 上の階層へ移動するボタンを無効化
                    dlg.AddExtension = true;
                    dlg.CheckFileExists = true;
                    dlg.CheckPathExists = true;

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        string fullPath = dlg.FileName;
                        string fileName = Path.GetFileName(fullPath);
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = fileName;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value =fullPath;
                    }
                }
            }

        }

        // クリックした瞬間にイベント
        private void DataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null) return;
            if (dataGridView1.CurrentCell is DataGridViewCheckBoxCell)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        // チェックされた瞬間にコンボボックスを再作成
        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
            {
                ReCreateComboBox();
            }
        }

        // 「担当者名」を編集する際は日本語入力とする
        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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

        // 「CTマスタメンテ」呼出
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnButton") // ボタン列のクリックかどうかを判定
            {
                if (e.RowIndex < 0) return;
                OdCdSetting setting = new()
                {
                    OdCd = dataGridView1.Rows[e.RowIndex].Cells["ColumnOdCd"].Value.ToString(),
                    KtCd = dataGridView1.Rows[e.RowIndex].Cells["ColumnKtCd"].Value.ToString()
                };
                FormCTMaster frm = new(setting);
                frm.ShowDialog();
            }
        }

    }
}
