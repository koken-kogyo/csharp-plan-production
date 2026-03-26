using System.Data;

namespace PlanProduction
{
    public static class DataStore
    {
        // [原価管理] KM5010:作業グループマスタ JOIN アプリケーション設定ファイルの手配コード群(JSON)
        public static DataTable dtKM5010kai;
        /* DBAccessor にて 主キーを設定
        DataStore.dtKM5010kai.PrimaryKey = new DataColumn[]
        {
            DataStore.dtKM5010kai.Columns["ODCD"],
            DataStore.dtKM5010kai.Columns["WKGRCD"]
        };
        */
        // DataTable.Copy() を実行すると、主キー（PrimaryKey）もコピーされます。
        public static DataTable originalKM5010kai;  

        // アプリケーション設定ファイルのデフォルト手配先コード
        public static string DefaultOdCd;
        public static string originalDefaultOdCd;

        //
        public static DataTable dtD0410;

    }
}
