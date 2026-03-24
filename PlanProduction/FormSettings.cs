using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace PlanProduction
{
    public partial class FormSrttings : Form
    {
        public FormSrttings()
        {
            InitializeComponent();

            this.KeyPreview = true;                     // フォームでキーイベントを受け取る設定

            dataGridView1.Rows.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = DataStore.dtKM5010kai;
        }


        /*
         * 
        // 保存して終了
        private void ButtonExit_Click(object sender, System.EventArgs e)
        {
            // 設定ファイル保存（実行ファイルの場所）
            string fileName = Common.CONFIG_FILE_LS;
            var config = new AppSettings
            {
                OdCd = textBoxODCD.Text,
                KtCd = textBoxKTCD.Text,
                Ava = textBox可動率.Text,
            };
            Common.OdCd = textBoxODCD.Text;
            Common.KtCd = textBoxKTCD.Text;
            Common.Ava = textBox可動率.Text;
            string jsonString = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, jsonString);
            Console.WriteLine($"JSONファイルを書き込みました:\n{jsonString}");
            Close();
        }

        private void FormSrttings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
                e.Handled = true;
            }
        }

        private void textBoxODCD_KeyDown(object sender, KeyEventArgs e)
        {
            // Enterキーが押されたか判定
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");     // Tabキーが押されたのと同じ効果を発生させる
                e.SuppressKeyPress = true;  // Enterキー入力時の「ポン」という音を消す
            }
        }

        private void textBoxKTCD_KeyDown(object sender, KeyEventArgs e)
        {
            // Enterキーが押されたか判定
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");     // Tabキーが押されたのと同じ効果を発生させる
                e.SuppressKeyPress = true;  // Enterキー入力時の「ポン」という音を消す
            }
        }

        private void textBox可動率_KeyDown(object sender, KeyEventArgs e)
        {
            // Enterキーが押されたか判定
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");     // Tabキーが押されたのと同じ効果を発生させる
                e.SuppressKeyPress = true;  // Enterキー入力時の「ポン」という音を消す
            }
        }
         * 
         */

        // 保存して終了
        private void ButtonExit_Click(object sender, System.EventArgs e)
        {
            // 設定ファイル保存（実行ファイルの場所）
            string fileName = Common.CONFIG_FILE_LS;
            //var config = new AppSetting
            //{
            //OdCd = textBoxODCD.Text,
            //KtCd = textBoxKTCD.Text,
            //Ava = textBox可動率.Text,
            //};
            //Common.OdCd = textBoxODCD.Text;
            //Common.KtCd = textBoxKTCD.Text;
            //Common.Ava = textBox可動率.Text;
            //string jsonString = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            //File.WriteAllText(fileName, jsonString);
            //Console.WriteLine($"JSONファイルを書き込みました:\n{jsonString}");
            Close();
        }

        // 設定ファイル読み込み（実行ファイルの場所）
        private void button1_Click(object sender, System.EventArgs e)
        {
            string fileName = Common.CONFIG_FILE_LS;
            //var config = new AppSetting();

            //if (File.Exists(fileName))
            //{
            //    string jsonString = File.ReadAllText(@fileName);
            //    config = JsonSerializer.Deserialize<AppSetting>(jsonString);
            //    //Common.OdCd = config.OdCd;
            //    //Common.KtCd = config.KtCd;
            //    //Common.Ava = config.Ava;
            //    Console.WriteLine($"JSONファイルを読み込みました:\n{jsonString}");
            //}
        }

        private void FormSrttings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
                e.Handled = true;
            }
        }

        private void textBoxODCD_KeyDown(object sender, KeyEventArgs e)
        {
            // Enterキーが押されたか判定
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");     // Tabキーが押されたのと同じ効果を発生させる
                e.SuppressKeyPress = true;  // Enterキー入力時の「ポン」という音を消す
            }
        }

        private void textBoxKTCD_KeyDown(object sender, KeyEventArgs e)
        {
            // Enterキーが押されたか判定
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");     // Tabキーが押されたのと同じ効果を発生させる
                e.SuppressKeyPress = true;  // Enterキー入力時の「ポン」という音を消す
            }
        }

        private void textBox可動率_KeyDown(object sender, KeyEventArgs e)
        {
            // Enterキーが押されたか判定
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");     // Tabキーが押されたのと同じ効果を発生させる
                e.SuppressKeyPress = true;  // Enterキー入力時の「ポン」という音を消す
            }
        }

        // 設定されなく画面を閉じた場合はアプリケーションを終了する
        private void FormSrttings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!File.Exists(@Common.CONFIG_FILE_LS)) Application.Exit();
        }

        // 閉じるボタン
        private void buttonCancelClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // 検索ボックス
        private void textBoxSearchOdCd_TextChanged(object sender, EventArgs e)
        {
            string filter = (textBoxSearchOdCd.TextLength == 0) ? string.Empty :
                $"ODCD LIKE '{textBoxSearchOdCd.Text}*'";
            DataStore.dtKM5010kai.DefaultView.RowFilter = filter;
        }

        // 検索条件クリア
        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxSearchOdCd.Text = string.Empty;
        }

        // 全てをチェック
        private void checkBoxHeader_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBoxHeader.Checked;

            // 表示されている行だけを処理
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
        }

        // 保存して閉じる
        private void buttonSaveClose_Click(object sender, EventArgs e)
        {
            Common.SerializeAppSettings(ref this.dataGridView1);
            Close();
        }
    }
}
