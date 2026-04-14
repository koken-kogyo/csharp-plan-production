using System;
using System.IO;
using System.Windows.Forms;

namespace PlanProduction
{
    public partial class FormLogin : Form
    {
        bool MemUser = false;
        string UserID = string.Empty;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            // デザイナーの設定ページを読み込み
            var memuser = Properties.Settings.Default.MemUser;
            var userid = Properties.Settings.Default.UserID;
            checkBoxMem.Checked = memuser;
            textBoxID.Text = (checkBoxMem.Checked) ? userid : "";
            this.MemUser = memuser;
            this.UserID = textBoxID.Text;
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 設定に保存
            if (MemUser != checkBoxMem.Checked || UserID != textBoxID.Text)
            {
                Properties.Settings.Default.MemUser = checkBoxMem.Checked;
                Properties.Settings.Default.UserID = (checkBoxMem.Checked) ? textBoxID.Text : "";
                Properties.Settings.Default.Save();
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            // 従業員番号セット
            this.UserID = textBoxID.Text;

            // 従業員番号チェック
            if (!DBAccessor.IsAuthrizedEMPUser(this.UserID))
            {
                textBoxID.SelectionStart = 0;
                textBoxID.SelectionLength = textBoxID.Text.Length;
                textBoxID.Focus();
                return;
            }
            else
            {
                Common.UserId = textBoxID.Text;
            }

            // 作業グループマスタと手配先マスタを読み込んでおく
            DBAccessor.ReadKM5010();
            DBAccessor.ReadM300();

            // 一旦Oracleコネクションを削除（コネクションプールなしで細かな制御をしたい場合に必要）
            //DBAccessor.CloseOraSchema();

            // 初回起動の場合は設定画面からスタート
            string fullPath = Common.AppSettingsFullPath();
            if (!File.Exists(@fullPath))
            {
                MessageBox.Show("設定画面を起動します。\n\n初回設定を行ってください．"
                    , "[生産計画]", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormSrttings frmSetting = new();
                frmSetting.ShowDialog();

                // 初回起動のがキャンセルされたら終了
                if (!File.Exists(@fullPath)) return;
            }
            else
            {
                // アプリケーション設定ファイルの読込
                Common.DeserializeAppSettings();
            }

            // メイン画面起動
            this.Hide();
            FormPlanProduction frm = new();
            frm.ShowDialog();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

    }
}
