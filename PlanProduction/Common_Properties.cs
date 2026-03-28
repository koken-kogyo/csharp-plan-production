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
        public class OdCdSetting
        {
            public string OdCd { get; set; } = "";                      // 手配先コード
            public string KtCd { get; set; } = "";                      // 工程コード
            public int SortOrder { get; set; } = 1;                     // ソート順（1:品番順、2:手配日の古い順）Dic<sortOrderMap>
            public string TanName { get; set; } = "";                   // 初期表示担当者名
            public string Ava { get; set; } = "70";                     // 可動率 Equipment availability rate
        }
        /// <summary>
        /// アプリケーション設定ファイル
        /// </summary>
        public class AppConfig
        {
            public string DefaultOdCd { get; set; }                     // アプリケーション初期表示設定
            public List<OdCdSetting> OdCdSettings { get; set; }         // アプリケーション設定
        }


        /// <summary>
        /// 画面設定情報
        /// </summary>
        public class FormSettings
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }
            public int SplitterMainDistance { get; set; }
            public int SplitterSubVerticalDistance { get; set; }
            public int SplitterSubHorizontalDistance { get; set; }
        }
        /// <summary>
        // 画面設定ファイル
        /// </summary>
        public class FormConfig
        {
            public Dictionary<string, FormSettings> Forms { get; set; } = [];
        }

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