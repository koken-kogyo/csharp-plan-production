/*
 * 
 *     /// CA2211:非定数フィールドは表示されるべきではありません
 *     https://learn.microsoft.com/ja-jp/dotnet/fundamentals/code-analysis/quality-rules/ca2211
 *     
 *     → このアプリケーションはスレッドセーフではないため慎重に制御してください
 *     （ 自分訳：あるフォームで設定したここの変数を、他のフォームに反映されられない！）
 *     → .editorconfig で CA2211 を無効にしてあります！
 */
using System.Collections.Generic;

namespace PlanProduction
{
    /// <summary>
    /// 共通クラス
    /// </summary>
    public partial class Common
    {
        public static readonly int DB_CONFIG_EM = 0;                        // DB 接続定義 (OracleEM)
        public static readonly int DB_CONFIG_KK = 1;                        // DB 接続定義 (MySQL内製プログラム)
        public static readonly string CONFIG_FILE_DB = "ConfigDB.xml";      // データベース設定ファイル
        public static readonly string CONFIG_FILE_FS = "ConfigFS.xml";      // ファイル システム設定ファイル
        public static readonly string CONFIG_FILE_AS = "AppSettings.json";  // アプリケーション設定ファイル
        public static readonly string CONFIG_FILE_WS = "FormSettings.json"; // 画面設定ファイル
        public static readonly string 雛形フォルダ = @"Applications\雛形ファイル"; // FSConfig共有フォルダからのパス
        public static readonly string 設定フォルダ = @"Applications\設定ファイル"; // FSConfig共有フォルダからのパス


        /// <summary>
        /// 設定ファイル
        /// </summary>
        public static DBConfigData[] DbConfig { get; set; }             // データベース設定データ
        public static FSConfigData[] FsConfig { get; set; }             // ファイル システム設定データ

        /// <summary>
        /// アプリケーション設定情報
        /// </summary>
        public static Dictionary<int, string> sortOrderMap = new()
        {
            { 1, "品番優先" },
            { 2, "手配日優先" }
        };

        public static readonly int DEFAULT_WKSEQ = 46;

        /// <summary>
        /// 利用者情報
        /// </summary>
        public static string UserId;                                    // ユーザー ID
        public static string Passwd;                                    // パスワード
        public static string UserName;                                  // ユーザー名称
        public static string AtgCd;                                     // EM 権限グループ コード
        public static string Active;                                    // 切削生産計画システム有効フラグ
        public static string AuthLv;                                    // 切削生産計画システム権限レベル
        public static bool MemAuthInfo;                                 // 認証情報記憶



    }
}