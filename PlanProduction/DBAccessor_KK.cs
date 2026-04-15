using DecryptPassword;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                ret = true;
            }
            catch
            {
                MessageBox.Show("データベースに接続できません．", "MySQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                Debug.WriteLine($"{active}{authLv}");

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
                using MySqlCommand myCmd = new(sql, kkCnn);
                using MySqlDataAdapter myDa = new(myCmd);
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
            catch (Exception)
            {
                // エラー
                //string msg = "Exception Source = " + ex.Source + ", Message = " + ex.Message;
                ret = false;
            }
            // 接続を閉じる
            DBAccessor.CloseMySqlSchema();
            return ret;
        }

        public static bool SaveDataGridView(ref DataGridView dgv, SaveOptions opt)
        {
            if (!IsConnectMySqlSchema()) return false;

            // トランザクション開始
            MySqlTransaction txn = kkCnn.BeginTransaction();
            try
            {

                // 登録
                string sql = @"
                    INSERT INTO KD8020
                        (ODCD, PLANDT, TYPE, STARTTIME, ENDTIME, HIRUKADO, KYUKEIKADO, PIKAPIKA, HAYAHIRU, NOTE, 
                         TTLQTY, CTHOUR, OPEHOUR, AVA)
                    VALUES
                        (@手配先コード, @計画日, @計画種別, @開始時刻, @終了時刻, @昼稼働, @休憩稼働, @ピカピカ, @早昼, @所感, 
                         @合計本数, @CT合計時間, @合計稼働時間, @可動率)
                    ON DUPLICATE KEY UPDATE
                        ODCD = VALUES(ODCD),
                        PLANDT = VALUES(PLANDT),
                        TYPE = VALUES(TYPE),
                        STARTTIME = VALUES(STARTTIME),
                        ENDTIME = VALUES(ENDTIME),
                        HIRUKADO = VALUES(HIRUKADO),
                        KYUKEIKADO = VALUES(KYUKEIKADO),
                        PIKAPIKA = VALUES(PIKAPIKA),
                        HAYAHIRU = VALUES(HAYAHIRU),
                        NOTE = VALUES(NOTE),
                        TTLQTY = VALUES(TTLQTY),
                        CTHOUR = VALUES(CTHOUR),
                        OPEHOUR = VALUES(OPEHOUR),
                        AVA = VALUES(AVA);
                ";
                using (var cmd = new MySqlCommand(sql, kkCnn))
                {
                    // 時刻の変換
                    DateTime startDateTime = TimeSpan.TryParse(opt.開始時刻, out TimeSpan st) ? opt.PlanDate.Date + st : opt.PlanDate.Date;
                    DateTime endDateTime = TimeSpan.TryParse(opt.終了時刻, out TimeSpan et) ? opt.PlanDate.Date + et : opt.PlanDate.Date;
                    if (endDateTime < startDateTime && startDateTime != opt.PlanDate.Date && endDateTime != opt.PlanDate.Date)
                    {
                        endDateTime = endDateTime.AddDays(1); // 終了日時が開始日時より前の場合は翌日にする
                    }

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@手配先コード", opt.OdCd);
                    cmd.Parameters.AddWithValue("@計画日", opt.PlanDate.Date);
                    cmd.Parameters.AddWithValue("@計画種別", opt.Type);
                    cmd.Parameters.AddWithValue("@開始時刻", (startDateTime == opt.PlanDate.Date) ? null : startDateTime);
                    cmd.Parameters.AddWithValue("@終了時刻", (endDateTime == opt.PlanDate.Date) ? null : endDateTime);

                    cmd.Parameters.AddWithValue("@昼稼働", (opt.昼稼働) ? "1" : "0");
                    cmd.Parameters.AddWithValue("@休憩稼働", (opt.休憩稼働) ? "1" : "0");
                    cmd.Parameters.AddWithValue("@ピカピカ", (opt.ピカピカ) ? "1" : "0");
                    cmd.Parameters.AddWithValue("@早昼", (opt.早昼) ? "1" : "0");

                    cmd.Parameters.AddWithValue("@所感", opt.所感);
                    cmd.Parameters.AddWithValue("@合計本数", opt.合計本数);
                    cmd.Parameters.AddWithValue("@CT合計時間", opt.CT合計時間);
                    cmd.Parameters.AddWithValue("@合計稼働時間", opt.合計稼働時間);
                    cmd.Parameters.AddWithValue("@可動率", opt.可動率);

                    int insupdCount = cmd.ExecuteNonQuery();
                }

                // 明細を一旦削除
                sql = $"DELETE FROM KD8030 WHERE ODCD='{opt.OdCd}' and PLANDT='{opt.PlanDate.Date}' and TYPE='{opt.Type}'";
                using (var cmd = new MySqlCommand(sql, kkCnn))
                {
                    int deleteCount = cmd.ExecuteNonQuery();
                }

                // 明細の登録
                sql = @"
                    INSERT INTO KD8030
                        (ODCD, PLANDT, TYPE, NO, HMCD, CT, QTY, STARTTIME, ENDTIME, BREAKTIME, AVA, TANNM, NOTE)
                    VALUES
                        (@手配先コード, @計画日, @計画種別, @行番号, @品番, @CT, @本数, @開始時刻, @終了時刻, @休憩時間, @可動率, @作業者, @備考);
                ";
                using (var cmd = new MySqlCommand(sql, kkCnn))
                {
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        // 新規行（※の行）はスキップ
                        if (row.IsNewRow) continue;

                        cmd.Parameters.Clear();

                        // 行ヘッダーの No を取得
                        int no = row.HeaderCell.Value == null
                            ? row.Index + 1
                            : int.Parse(row.HeaderCell.Value.ToString());

                        // 時刻の変換
                        DateTime startDateTime = TimeSpan.TryParse(row.Cells[3].Value?.ToString(), out TimeSpan st) ? opt.PlanDate.Date + st : opt.PlanDate.Date;
                        DateTime endDateTime = TimeSpan.TryParse(row.Cells[4].Value?.ToString(), out TimeSpan et) ? opt.PlanDate.Date + et : opt.PlanDate.Date;
                        if (endDateTime < startDateTime && startDateTime != opt.PlanDate.Date && endDateTime != opt.PlanDate.Date)
                        {
                            endDateTime = endDateTime.AddDays(1); // 終了日時が開始日時より前の場合は翌日にする
                        }

                        cmd.Parameters.AddWithValue("@手配先コード", opt.OdCd);
                        cmd.Parameters.AddWithValue("@計画日", opt.PlanDate.Date);
                        cmd.Parameters.AddWithValue("@計画種別", opt.Type);
                        cmd.Parameters.AddWithValue("@行番号", no);
                        cmd.Parameters.AddWithValue("@品番", row.Cells[0].Value);
                        cmd.Parameters.AddWithValue("@CT", row.Cells[1].Value);
                        cmd.Parameters.AddWithValue("@本数", row.Cells[2].Value);
                        cmd.Parameters.AddWithValue("@開始時刻", (startDateTime == opt.PlanDate.Date) ? null : startDateTime);
                        cmd.Parameters.AddWithValue("@終了時刻", (endDateTime == opt.PlanDate.Date) ? null : endDateTime);
                        cmd.Parameters.AddWithValue("@休憩時間", row.Cells[5].Value);
                        cmd.Parameters.AddWithValue("@可動率", (opt.Type == "P") ? opt.可動率 : row.Cells[6].Value);
                        cmd.Parameters.AddWithValue("@作業者", row.Cells[7].Value);
                        cmd.Parameters.AddWithValue("@備考", row.Cells[8].Value);

                        cmd.ExecuteNonQuery();
                    }
                }
                txn.Commit();
                CloseMySqlSchema();
                return true;
            }
            catch (Exception ex)
            {
                txn.Rollback();
                MessageBox.Show("データの保存に失敗しました。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // カレンダーに表示する登録済み計画日一覧を取得
        public static List<DateTime> GetRegistDates(string odcd)
        {
            if (!IsConnectMySqlSchema()) return null;
            try
            {
                List<DateTime> planDates = [];
                string sql = $"SELECT PLANDT FROM KD8020 WHERE ODCD='{odcd}' GROUP BY PLANDT";
                using (var cmd = new MySqlCommand(sql, kkCnn))
                {
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        planDates.Add(reader.GetDateTime("PLANDT"));
                    }
                }
                CloseMySqlSchema();
                return planDates;
            }
            catch (Exception ex)
            {
                MessageBox.Show("異常が発生しました\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static bool GetKD8020(ref SaveOptions opt)
        {
            if (!IsConnectMySqlSchema()) return false;
            try
            {
                string sql = @"
                    SELECT * FROM KD8020 WHERE
                        ODCD = @手配先コード and
                        PLANDT = @計画日 and 
                        TYPE = @計画種別
                ";
                using (var cmd = new MySqlCommand(sql, kkCnn))
                {
                    cmd.Parameters.AddWithValue("@手配先コード", opt.OdCd);
                    cmd.Parameters.AddWithValue("@計画日", opt.PlanDate.Date);
                    cmd.Parameters.AddWithValue("@計画種別", opt.Type);
                    using MySqlDataAdapter adapter = new(cmd);
                    DataTable dt = new();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        opt.開始時刻 = DateTime.TryParse(dt.Rows[0]["STARTTIME"].ToString(), out DateTime sd) ? sd.ToString("HH:mm") : "";
                        opt.終了時刻 = DateTime.TryParse(dt.Rows[0]["ENDTIME"].ToString(), out DateTime ed) ? ed.ToString("HH:mm") : "";
                        opt.昼稼働 = (dt.Rows[0]["HIRUKADO"].ToString() == "1");
                        opt.休憩稼働 = (dt.Rows[0]["KYUKEIKADO"].ToString() == "1");
                        opt.ピカピカ = (dt.Rows[0]["PIKAPIKA"].ToString() == "1");
                        opt.早昼 = (dt.Rows[0]["HAYAHIRU"].ToString() == "1");
                        opt.所感 = dt.Rows[0]["NOTE"].ToString();
                        opt.合計本数 = dt.Rows[0]["TTLQTY"].ToIntOrDefault();
                        opt.CT合計時間 = dt.Rows[0]["CTHOUR"].ToDoubleOrDefault();
                        opt.合計稼働時間 = dt.Rows[0]["OPEHOUR"].ToDoubleOrDefault();
                        opt.可動率 = dt.Rows[0]["AVA"].ToDoubleOrDefault();
                    }
                }
                CloseMySqlSchema();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("異常が発生しました\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        // チャート用データを過去20日分取得
        public static bool GetKD8020Chart(ref DataTable dt, string odcd)
        {
            if (!IsConnectMySqlSchema()) return false;
            try
            {
                string sql = "SELECT * FROM (SELECT PLANDT, TTLQTY, AVA FROM KD8020 "
                    + $"WHERE ODCD='{odcd}' and TYPE = 'J' and "
                    + "PLANDT <= SYSDATE() ORDER BY PLANDT desc limit 20"
                    +") AS t ORDER BY PLANDT asc";
                using (var cmd = new MySqlCommand(sql, kkCnn))
                {
                    using MySqlDataAdapter adapter = new(cmd);
                    adapter.Fill(dt);
                }
                CloseMySqlSchema();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("異常が発生しました\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        // 製造部計画表
        public static bool GetKD8030(ref DataTable dt, SaveOptions opt)
        {
            if (!IsConnectMySqlSchema()) return false;
            try
            {
                string sql = @"
                    SELECT NO as No, HMCD as 品番, CT, QTY as 本数
                        , DATE_FORMAT(STARTTIME,'%H:%i') as 開始時刻 
                        , DATE_FORMAT(ENDTIME,'%H:%i') as 終了時刻
                        , BREAKTIME as 休憩時間
                        , TANNM as 作業者, NOTE as 備考, AVA as 可動率
                    FROM 
                        KD8030 
                    WHERE
                        ODCD = @手配先コード and
                        PLANDT = @計画日 and 
                        TYPE = @計画種別
                    ORDER BY NO
                ";
                using (var cmd = new MySqlCommand(sql, kkCnn))
                {
                    cmd.Parameters.AddWithValue("@手配先コード", opt.OdCd);
                    cmd.Parameters.AddWithValue("@計画日", opt.PlanDate.Date);
                    cmd.Parameters.AddWithValue("@計画種別", opt.Type);
                    using MySqlDataAdapter adapter = new(cmd);
                    adapter.Fill(dt);
                }
                CloseMySqlSchema();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("異常が発生しました\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }




    }
}
