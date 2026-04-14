using System;
using System.IO;
using System.Windows.Forms;

namespace PlanProduction
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // データベース設定ファイルの読込
            if (!File.Exists(@Common.CONFIG_FILE_DB))
            {
                MessageBox.Show("データベース設定ファイルが見つかりません！\nアプリケーションを中断します．"
                    , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            Common.DbConfig = Common.ReserializeDBConfigFile();

            // ファイルシステム設定ファイルの読込
            if (!File.Exists(@Common.CONFIG_FILE_FS))
            {
                MessageBox.Show("ファイルシステム設定ファイルが見つかりません！\nアプリケーションを中断します．"
                    , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            Common.FsConfig = Common.ReserializeFSConfigFile();

            // サーバーの共有フォルダが存在するか確認
            if (!Directory.Exists(Common.FsConfig[0].ShareName))
            {
                MessageBox.Show("サーバーの共有フォルダにアクセス出来ません！\nアプリケーションを中断します．"
                    , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // EMデータベースへの接続確認（コネクションプール接続）
            if (!DBAccessor.OpenOraSchema())
            {
                MessageBox.Show("データベースへの接続に失敗しました！\n定義ファイルを見直してください\nアプリケーションを中断します．"
                    , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // ログイン画面起動
            Application.Run(new FormLogin());
        }
    }
}
