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

            // EMデータベースへの接続確認
            if (!DBAccessor.OpenOraSchema())
            {
                MessageBox.Show("データベースへの接続に失敗しました！\n定義ファイルを見直してください\nアプリケーションを中断します．"
                    , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            // 作業グループマスタを読み込んでおく
            DBAccessor.ReadKM5010();
            DataStore.KM5010AddColumns();

            // 初回起動の場合は設定画面からスタート
            if (!File.Exists(@Common.CONFIG_FILE_LS))
            {
                MessageBox.Show("設定画面を起動します。\n\n初回設定を行ってください．"
                    , "[生産計画]", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Run(new FormSrttings());
            }
            else
            {
                // アプリケーション設定ファイルの読込
                Common.DeserializeAppSettings();
            }




            // メイン画面起動
            Application.Run(new FormPlanProduction());
        }
    }
}
