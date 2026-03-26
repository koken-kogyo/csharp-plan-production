using DecryptPassword;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PlanProduction
{
    /// <summary>
	/// データベース アクセス クラス (EM テーブル)
    /// </summary>
    public partial class DBAccessor
    {
        private static OracleConnection oraCnn;     // Oracle データベース接続 (ODAC)

        public static bool IsConnectedOraSchema()
        {
            return oraCnn != null;
        }

        /// <summary>
        /// Oracle データベース接続
        /// </summary>
        /// <param name="dbConf">データベース設定</param>
        /// <param name="oraCnn">EM データベースへの接続クラス</param>
        /// <returns>結果 (false: 失敗, true: 成功)</returns>
        public static bool OpenOraSchema()
        {
            bool ret;

            string user = Common.DbConfig[Common.DB_CONFIG_EM].User; // ユーザー

            // パスワード復号化
            var dpc = new DecryptPasswordClass();
            dpc.DecryptPassword(Common.DbConfig[Common.DB_CONFIG_EM].EncPasswd, out string decPasswd);

            // データソース
            string ds = "(DESCRIPTION="
                        + "(ADDRESS="
                          + "(PROTOCOL=" + Common.DbConfig[Common.DB_CONFIG_EM].Protocol + ")"
                          + "(HOST=" + Common.DbConfig[Common.DB_CONFIG_EM].Host + ")"
                          + "(PORT=" + Common.DbConfig[Common.DB_CONFIG_EM].Port + ")"
                        + ")"
                        + "(CONNECT_DATA="
                          + "(SERVICE_NAME=" + Common.DbConfig[Common.DB_CONFIG_EM].ServiceName + ")"
                        + ")"
                      + ")";  // Oracle Client を使用せず直接接続する

            // Oracle 接続文字列を組み立てる
            string connectString = "User Id=" + user + "; "
                                 + "Password=" + decPasswd + "; "
                                 + "Data Source=" + ds;
            try
            {
                oraCnn = new OracleConnection(connectString);

                // Oracle へのコネクションの確立
                oraCnn.Open();
                ret = true;
            }
            catch
            {
                // 接続を閉じる
                DBAccessor.CloseOraSchema();
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Oracle データベース スキーマからの切断
        /// </summary>
        /// <param name="oraCnn">Oracle データベースへの接続クラス</param>
        public static void CloseOraSchema()
        {
            oraCnn?.Close();
        }
        
        /// <summary>
        /// EM 利用権限確認 (M0010 担当者マスター)
        /// </summary>
        /// <param name="isPasswdFree">パスワード不要か</param>
        /// <returns>権限あり</returns>
        public static bool IsAuthrizedEMUser(bool isPasswdFree)
        {
            bool ret = false;

            // DB接続
            if (oraCnn is null) OpenOraSchema();

            try
            {
                string sql;
                if (isPasswdFree)
                {
                    sql = "SELECT "
                        + "TANCD, "
                        + "TANNM, "
                        + "PASSWD, "
                        + "ATGCD "
                        + "FROM "
                        + Common.DbConfig[Common.DB_CONFIG_EM].Schema + ".M0010 "
                        + "WHERE "
                        + "TANCD = '" + Common.UserId + "'"
                        ;
                }
                else
                {
                    sql = "SELECT "
                        + "TANCD, "
                        + "TANNM, "
                        + "PASSWD, "
                        + "ATGCD "
                        + "FROM "
                        + Common.DbConfig[Common.DB_CONFIG_EM].Schema + ".M0010 "
                        + "WHERE "
                        + "TANCD = '" + Common.UserId + "' AND "
                        + "PASSWD = '" + Common.Passwd + "'"
                        ;
                }
                using (OracleCommand myCmd = new OracleCommand(sql, oraCnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        var myDt = new DataTable();
                        // 結果取得
                        myDa.Fill(myDt);
                        foreach (DataRow dr in myDt.Rows)
                        {
                            Common.UserName = dr[1].ToString();
                            Common.AtgCd = dr[3].ToString();
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
            DBAccessor.CloseOraSchema();
            return ret;
        }

        /// <summary>
        /// EM マスタ読み込みデータストアへセット (KM5010 作業グループマスタ)
        /// </summary>
        /// <returns>権限あり</returns>
        public static bool ReadKM5010()
        {
            bool ret = false;
            try
            {
                if (oraCnn is null) OpenOraSchema();
                string sql= "SELECT "
                    + "a.ODCD"
                    + ", a.WKGRCD"
                    + ", a.WKGRNM"
                    + ", m.ODRNM"
                    + " FROM "
                    + Common.DbConfig[Common.DB_CONFIG_EM].Schema + ".KM5010 a, "
                    + Common.DbConfig[Common.DB_CONFIG_EM].Schema + ".M0300 m "
                    + "WHERE a.ODCD=m.ODCD "
                    + "ORDER BY a.ODCD"
                    ;
                using (OracleCommand myCmd = new OracleCommand(sql, oraCnn))
                {
                    using (OracleDataAdapter myDa = new OracleDataAdapter(myCmd))
                    {
                        DataStore.dtKM5010kai = new DataTable();
                        myDa.Fill(DataStore.dtKM5010kai);
                        // 列の追加
                        DataStore.dtKM5010kai.Columns.Add("CHECKED", typeof(bool));
                        DataStore.dtKM5010kai.Columns.Add("SORTORDER", typeof(string));
                        DataStore.dtKM5010kai.Columns.Add("TANNAME", typeof(string));
                        DataStore.dtKM5010kai.Columns.Add("AVA", typeof(string));
                        // 主キーを設定して検索に使用
                        DataStore.dtKM5010kai.PrimaryKey = new DataColumn[]
                        {
                            DataStore.dtKM5010kai.Columns["ODCD"],
                            DataStore.dtKM5010kai.Columns["WKGRCD"]
                        };
                        // 追加した列に対して初期化を設定
                        foreach (DataRow row in DataStore.dtKM5010kai.Rows)
                        {
                            row["CHECKED"] = false;
                            row["TANNAME"] = string.Empty;
                            row["AVA"] = string.Empty;
                        }
                        ret = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // エラー
                string msg = "Exception Source = " + ex.Source + ", Message = " + ex.Message;
                MessageBox.Show(msg);
            }
            return ret;
        }




    }
}