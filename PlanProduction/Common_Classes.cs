using System.Collections.Generic;

namespace PlanProduction
{
    /// <summary>
    /// データベース設定データ クラス
    /// </summary>
    public class DBConfigData
    {
        // プロパティ
        public string User { get; set; }        // ユーザー ID
        public string EncPasswd { get; set; }   // 暗号化パスワード ([KCM002SF] パスワード暗号化アプリ で暗号化した文字列)
        public string Protocol { get; set; }    // 通信プロトコル
        public string Host { get; set; }        // ホスト名または IPv4 アドレス
        public int Port { get; set; }           // ポート番号
        public string ServiceName { get; set; } // サービス名
        public string Schema { get; set; }      // スキーマ名
        public string CharSet { get; set; }     // 文字セット
    }

    /// <summary>
    /// ファイル システム設定データ クラス
    /// </summary>
    public class FSConfigData
    {
        // プロパティ
        public string HostName { get; set; }    // ホスト名
        public string IpAddr { get; set; }      // IPv4 アドレス
        public string UserId { get; set; }      // ＜ホスト名＞\＜ローカル アカウント名＞
        public string EncPasswd { get; set; }   // [KCM002SF] パスワード暗号化アプリで暗号化したパスワード
        public string ShareName { get; set; }   // ファイル保存先の直近共有フォルダー
        public string RootPath { get; set; }    // ファイル保存先へのフルパス
        public string FileName { get; set; }    // ファイル名
        public bool VisibleExcel { get; set; }  // Excel 可視 (出力専用) ("": 未設定, false: いいえ, true: はい (動作遅い))
        public bool CreateSubDir { get; set; }  // サブ ディレクトリ生成 (出力専用) ("": 未設定, false: いいえ, true: はい)
    }

    /// <summary>
    /// アプリケーション設定クラス
    /// </summary>

    public class OdCdSetting
    {
        public string OdCd { get; set; } = "";                      // 手配先コード
        public string KtCd { get; set; } = "";                      // 工程コード
        public int SortOrder { get; set; } = 1;                     // ソート順（1:品番順、2:手配日の古い順）Dic<sortOrderMap>
        public string TanName { get; set; } = "";                   // 初期表示担当者名
        public string Ava { get; set; } = "70";                     // 可動率 Equipment availability rate
        public string StartTime { get; set; } = "08:15";            // 開始時刻
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
    /// 画面情報設定クラス
    /// </summary>
    /// <values>
    /// フラグ１～５(int型)、0:false、1:true 
    /// </values>
    public class FormSettings
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int SplitterMainDistance { get; set; }
        public int SplitterSubVerticalDistance { get; set; }
        public int SplitterSubHorizontalDistance { get; set; }
        public int Flg1 { get; set; }
        public int Flg2 { get; set; }
        public int Flg3 { get; set; }
        public int Flg4 { get; set; }
        public int Flg5 { get; set; }
    }
    /// <summary>
    // 画面情報設定ファイル
    /// </summary>
    public class FormConfig
    {
        public Dictionary<string, FormSettings> Forms { get; set; } = [];
    }

    /// <summary>
    /// フォーム間データ連携クラス
    /// </summary>
    public class SelectedItem
    {
        public string HmCd { get; set; }
        public int SumQty { get; set; }
        public double CT { get; set; }
    }

    // 元に戻すアクション種類
    public enum UndoType
    {
        RowMove,
        CellSwap,
        CellEdit,
        RowDelete
    }

    // 元に戻すアクション
    public class UndoAction
    {
        public UndoType Type;

        // 行移動用
        public int SourceRow;
        public int TargetRow;

        // セル入れ替え用
        public int Row1, Col1;
        public object Value1;
        public int Row2, Col2;
        public object Value2;

        // セル編集用
        public int EditRow;
        public int EditCol;
        public object OldValue;
        public object NewValue;

        // 行削除用
        public int DeletedRowIndex;
        public string DeletedRowHeader;
        public object[] DeletedRowValues;
    }


}
