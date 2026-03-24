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
