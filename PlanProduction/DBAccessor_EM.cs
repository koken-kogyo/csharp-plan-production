using DecryptPassword;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

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
            string connectString = "User Id=" + user + ";"
                                 + "Password=" + decPasswd + ";"
                                 + "Data Source=" + ds + ";"
                                 + "Pooling=true";
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
            if (oraCnn?.State == System.Data.ConnectionState.Open)
            {
                oraCnn.Close();
                oraCnn.Dispose();
            }
            oraCnn = null;
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
                using OracleCommand myCmd = new(sql, oraCnn);
                using OracleDataAdapter myDa = new(myCmd);
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
            catch (Exception)
            {
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
                    + "ORDER BY a.ODCD, a.WKGRNM"
                    ;
                using OracleCommand myCmd = new(sql, oraCnn);
                using OracleDataAdapter myDa = new(myCmd);
                DataStore.dtKM5010kai = new DataTable();
                myDa.Fill(DataStore.dtKM5010kai);
                // 列の追加
                DataStore.dtKM5010kai.Columns.Add("CHECKED", typeof(bool));
                DataStore.dtKM5010kai.Columns.Add("SORTORDER", typeof(string));
                DataStore.dtKM5010kai.Columns.Add("TANNAME", typeof(string));
                DataStore.dtKM5010kai.Columns.Add("AVA", typeof(string));
                DataStore.dtKM5010kai.Columns.Add("STARTTIME", typeof(string));
                DataStore.dtKM5010kai.Columns.Add("EXCELNAME", typeof(string));
                DataStore.dtKM5010kai.Columns.Add("FULLPATH", typeof(string));
                // 主キーを設定して検索に使用
                DataStore.dtKM5010kai.PrimaryKey =
                [
                    DataStore.dtKM5010kai.Columns["ODCD"],
                    DataStore.dtKM5010kai.Columns["WKGRCD"]
                ];
                // 追加した列に対して初期化を設定
                foreach (DataRow row in DataStore.dtKM5010kai.Rows)
                {
                    row["CHECKED"] = false;
                    row["TANNAME"] = string.Empty;
                    row["AVA"] = string.Empty;
                    row["STARTTIME"] = string.Empty;
                    row["EXCELNAME"] = string.Empty;
                    row["FULLPATH"] = string.Empty;
                }
                ret = true;
            }
            catch (Exception ex)
            {
                // エラー
                string msg = "Exception Source = " + ex.Source + ", Message = " + ex.Message;
                MessageBox.Show(msg);
            }
            return ret;
        }

        /// <summary>
        /// EM マスタ読み込み (KM5030 標準作業時間マスタ)
        /// </summary>
        public static bool ReadKM5030(ref DataTable dt, string condition)
        {
            bool ret = false;
            try
            {
                if (oraCnn is null) OpenOraSchema();
                string sql = "SELECT * FROM "
                    + Common.DbConfig[Common.DB_CONFIG_EM].Schema + ".KM5030 "
                    + "WHERE ODCD||WKGRCD in " + condition + " "
                    + "ORDER BY HMCD";
                using OracleCommand myCmd = new(sql, oraCnn);
                using OracleDataAdapter myDa = new(myCmd);
                myDa.Fill(dt);
                ret = true;
            }
            catch (Exception ex)
            {
                // エラー
                string msg = "Exception Source = " + ex.Source + ", Message = " + ex.Message;
                MessageBox.Show(msg);
            }
            return ret;
        }

        /// <summary>
        /// EM マスタ更新 (KM5030 標準作業時間マスタ)
        /// </summary>
        public static bool SaveKM5030(ref DataTable dt)
        {
            bool ret = false;
            using OracleTransaction txn = oraCnn.BeginTransaction();
            try
            {
                DataTable dtUpdate = new();
                using (OracleDataAdapter adapter = new())
                {
                    string sql = "SELECT * FROM "
                        + Common.DbConfig[Common.DB_CONFIG_EM].Schema + ".KM5030" + " "
                        + "WHERE ROWNUM < 1";
                    adapter.SelectCommand = new OracleCommand(sql, oraCnn);
                    using var buider = new OracleCommandBuilder(adapter);
                    // 全件読み込み
                    adapter.Fill(dtUpdate);
                    adapter.Update(dt);
                }
                txn.Commit();
                dt.AcceptChanges();
                ret = true;
            }
            catch (Exception　ex)
            {
                txn.Rollback();
                MessageBox.Show("更新に失敗しました．\n" + ex.Message);
            }
            return ret;
        }

        /// <summary>
        /// EM マスタ読み込みデータストアへセット (M300 手配先マスタ)
        /// </summary>
        public static bool ReadM300()
        {
            bool ret = false;
            try
            {
                if (oraCnn is null) OpenOraSchema();
                string sql = "SELECT ODCD, ODRNM FROM "
                    + Common.DbConfig[Common.DB_CONFIG_EM].Schema + ".M0300 a "
                    + "WHERE exists (select * from "
                        + Common.DbConfig[Common.DB_CONFIG_EM].Schema + ".KM5010 b "
                        + "where a.ODCD=b.ODCD) "
                    + "ORDER BY a.ODCD";
                using OracleCommand myCmd = new(sql, oraCnn);
                using OracleDataAdapter myDa = new(myCmd);
                DataTable dt = new();
                myDa.Fill(dt);
                var map = dt.AsEnumerable().ToDictionary(row => row[0].ToString(), row => row[1].ToString());
                DataStore.M0300Map = map;

                ret = true;
            }
            catch (Exception ex)
            {
                // エラー
                string msg = "Exception Source = " + ex.Source + ", Message = " + ex.Message;
                MessageBox.Show(msg);
            }
            return ret;
        }

        /// <summary>
        /// EM 手配ファイルを読み込み作業標準マスタにない品番を返却
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool ReadD0410ConvertToMaster(ref DataTable dt, string condition)
        {
            bool ret = false;
            string sql = string.Empty;
            try
            {
                string s = DateTime.Now.AddMonths(-3).ToString("yyMM");
                sql = "select HMCD from "
                    + Common.DbConfig[Common.DB_CONFIG_EM].Schema + ".D0410 a "
                    + $"WHERE ODRNO > '{s}000000' and ODCD||KTCD in {condition} "
                    + $"and not exists(select * from KM5030 m where ODCD||KTCD in {condition} and m.HMCD=a.HMCD) "
                    + "group by HMCD order by HMCD";
                using (OracleCommand myCmd = new(sql, oraCnn))
                {
                    using OracleDataAdapter myDa = new(myCmd);
                    myDa.Fill(dt);
                }
                ret = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(sql + "\n" + ex.Message);
            }
            return ret;
        }

        /// <summary>
        /// EM 手配ファイルを読み込みPivotテーブルを作成し返却
        /// </summary>
        /// <returns>DataTable</returns>
        public static bool ReadD0410Pivot(ref DataTable dt, string condition)
        {
            bool ret = false;
            string sql = string.Empty;
            try
            {
                if (oraCnn is null) OpenOraSchema();
                // trunc(sysdate)前後の7営業日を取得して FROM ～ TO を決める
                DataTable fromtoDt = new();
                sql = "with plusrec as ( "
                        + "select YMD from s0820 where caltyp='00001' and wkkbn='1' "
                        + "and ymd between trunc(sysdate) and sysdate + 30 order by YMD asc "
                    + "), "
                    + "minusrec as ( "
                        + "select YMD from s0820 where caltyp='00001' and wkkbn='1' "
                        + "and ymd between sysdate - 30 and trunc(sysdate) order by YMD desc "
                    + ") "
                    + "select to_char(min(YMD),'YYYY-MM-DD') as FROMDT "
                    + ", to_char(max(YMD),'YYYY-MM-DD') as TODT from "
                    + "( "
                        + "select YMD from minusrec where rownum <= 8 "
                        + "union  "
                        + "select YMD from plusrec where rownum <= 8 "
                    + ")";
                using (OracleCommand myCmd = new(sql, oraCnn))
                {
                    using OracleDataAdapter myDa = new(myCmd);
                    myDa.Fill(fromtoDt);
                }
                string from = fromtoDt.Rows[0]["FROMDT"].ToString();
                string to = fromtoDt.Rows[0]["TODT"].ToString();

                // Pivotテーブル用の列ヘッダーを取得
                // 「行番号、YMD(YYYY-MM-DD)、MD(MM/DD)」
                DataTable headerDt = new();
                sql = "select ROW_NUMBER() OVER(ORDER BY EDDT) AS 行番号 "
                    + ", to_char(eddt, 'yyyy-MM-dd') as YMD, to_char(eddt, 'mm/dd') as MD "
                    + "from ( "
                        + "select EDDT from "
                        + Common.DbConfig[Common.DB_CONFIG_EM].Schema + ".D0410 "
                        + $"where EDDT between '{from}' and '{to}' "
                        + "and ODCD || KTCD in " + condition + " and ODRSTS in ('2', '3') "
                        + "and ODRNO > to_char(sysdate - 90, 'YYMM') || '000000' "
                    + "group by EDDT)";
                using (OracleCommand myCmd = new(sql, oraCnn))
                {
                    using OracleDataAdapter myDa = new(myCmd);
                    myDa.Fill(headerDt);
                }


                // Pivot IN 句の作成
                // 例）DATE '2026-03-18' AS '3/18' 
                var conditions = new List<string>();
                foreach (DataRow row in headerDt.Rows)
                {
                    string cond = $"DATE '{row["YMD"]}' as \"{row["MD"]}\"";
                    conditions.Add(cond);
                }
                string pivotList = string.Join(",", conditions);

                // Case When 句の作成
                // 例）when '3/18' is not null then 1
                conditions = [];
                foreach (DataRow row in headerDt.Rows)
                {
                    string cond = $"when \"{row["MD"]}\" is not null then {row["行番号"]}";
                    conditions.Add(cond);
                }
                string casewhenList = string.Join(" ", conditions) + " ";

                // 実際の取得
                sql =
                    "WITH pivot as ( "
                        + "SELECT * "
                        + "FROM ( "
                            + "SELECT "
                                + "a.HMCD, a.ODCD, a.KTCD, m50.HMRNM, m51.WKNOTE, TRUNC(a.EDDT) AS 手配日, a.ODRQTY "
                            + "FROM "
                                + Common.DbConfig[Common.DB_CONFIG_EM].Schema + ".D0410 a, "
                                + Common.DbConfig[Common.DB_CONFIG_EM].Schema + ".M0500 m50, "
                                + Common.DbConfig[Common.DB_CONFIG_EM].Schema + ".M0510 m51 "
                            + "WHERE  "
                                + "m50.HMCD = a.HMCD "
                                + "and m51.HMCD = a.HMCD "
                                + "and m51.ODCD = a.ODCD and m51.KTCD = a.KTCD and m51.VALDTF = "
                                    + "(select max(VALDTF) from M0510 where HMCD=a.HMCD and ODCD=a.ODCD and KTCD=a.KTCD) "
                                + "and a.EDDT between sysdate - 30 and sysdate + 30 "
                                + "and a.ODCD || a.KTCD in " + condition + " and a.ODRSTS in ('2', '3') "
                                + "and a.ODRNO > to_char(sysdate - 90, 'YYMM') || '000000' "
                        + ") src "
                        + "PIVOT ( "
                            + "SUM(ODRQTY) "
                            + "FOR 手配日 IN ( "
                                + pivotList
                            + ") "
                        + ") "
                    + ") "
                    + "select "
                        + "case "
                        + casewhenList
                        + "else 9 end as 優先度 "
                        + ", pivot.* "
                    + "from pivot "
                    + "ORDER BY 優先度, pivot.HMCD";
                using (OracleCommand myCmd = new(sql, oraCnn))
                {
                    using OracleDataAdapter myDa = new(myCmd);
                    myDa.Fill(dt);
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                // エラー
                string msg = "Exception Source = " + ex.Source + ", Message = " + ex.Message;
                MessageBox.Show(msg + "\n" + sql);
            }
            return ret;
        }




    }
}