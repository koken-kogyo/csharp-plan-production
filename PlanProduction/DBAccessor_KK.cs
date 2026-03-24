using DecryptPassword;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace PlanProduction
{
    /// <summary>
	/// データベース アクセス クラス (切削工程所管テーブル)
    /// </summary>
    public partial class DBAccessor
    {
        private static MySqlConnection kkCnn;    // MySQL データベース接続 (ODAC)

        /// <summary>
        /// MySQL データベース スキーマ接続成否
        /// </summary>
        /// <param name="mpCnn">MySql データベースへの接続クラス</param>
        /// <returns>結果 (false: 失敗, true: 成功)</returns>
        public static bool IsConnectMySqlSchema()
        {
            bool ret;

            try
            {
                // パスワード復号化
                var dpc = new DecryptPasswordClass();
                dpc.DecryptPassword(Common.DbConfig[Common.DB_CONFIG_KK].EncPasswd, out string decPasswd);

                // MySQL への接続情報
                string server = Common.DbConfig[Common.DB_CONFIG_KK].Host;
                string database = Common.DbConfig[Common.DB_CONFIG_KK].Schema;
                int port = Common.DbConfig[Common.DB_CONFIG_KK].Port;
                string uid = Common.DbConfig[Common.DB_CONFIG_KK].User;
                string pwd = decPasswd;
                string charset = Common.DbConfig[Common.DB_CONFIG_KK].CharSet;

                string connectionString = string.Format("Server={0};Database={1};Port={2};Uid={3};Pwd={4};Charset={5}", server, database, port, uid, pwd, charset);

                // MySQL への接続
                kkCnn = new MySqlConnection(connectionString);
                kkCnn.Open();	// 接続
                Trace.WriteLine("MySQL に接続しました。");
                Trace.WriteLine(kkCnn.ServerVersion);
                ret = true;
            }
            catch
            {
                kkCnn?.Close();	// 接続の解除
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// MySQL データベース スキーマからの切断
        /// </summary>
        /// <param name="mpCnn">MySQL データベースへの接続クラス</param>
        public static void CloseMySqlSchema()
        {
            kkCnn?.Close();
        }


        /// <summary>
        /// 原価管理システム利用権限確認 (km5000 原価管理システム利用者マスター)★
        /// </summary>
        /// <param name="active">有効フラグ</param>
        /// <param name="authLv">権限レベル</param>
        /// <returns>権限あり</returns>
        public static bool IsAuthrizedKKUser(ref string active, ref string authLv)
        {
            bool ret = false;
            try
            {
                // 切削生産計画システム データベースへ接続
                DBAccessor.IsConnectMySqlSchema();

                string sql;
                sql = "SELECT "
                    + "USERID, "
                    + "ACTIVE, "
                    + "AUTHLV "
                    + "FROM "
                    + Common.DbConfig[Common.DB_CONFIG_KK].Schema + ".KM5000 "
                    + "WHERE "
                    + "USERID = '" + Common.UserId + "' "
                    ;
                using (MySqlCommand myCmd = new MySqlCommand(sql, kkCnn))
                {
                    using (MySqlDataAdapter myDa = new MySqlDataAdapter(myCmd))
                    {
                        var myDt = new DataTable();
                        myDa.Fill(myDt);
                        foreach (DataRow dr in myDt.Rows)
                        {
                            Common.Active = dr[1].ToString();
                            Common.AuthLv = dr[2].ToString();
                            ret = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // エラー
                string msg = "Exception Source = " + ex.Source + ", Message = " + ex.Message;
                ret = false;
            }
            // 接続を閉じる
            DBAccessor.CloseMySqlSchema();
            return ret;
        }




    }
}
