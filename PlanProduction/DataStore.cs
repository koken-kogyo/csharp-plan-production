using System.Collections.Generic;
using System.Data;
using System.Linq;

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

        // 手配先マスタ
        public static Dictionary<string, string> M0300Map;

        // アプリケーション設定ファイルのデフォルト手配先コード
        public static string DefaultOdCd;
        public static string originalDefaultOdCd;

        // アプリケーション設定
        public static List<OdCdSetting> OdCdSettings { get; set; }

        /// <summary>
        /// 抽出条件を作成
        /// </summary>
        /// <returns>ODCD+KTCD In 句（例）('0631ABETP01',,,'0631ABETP11')</returns>
        public static string ExtracConditions(string odcd)
        {
            var row = dtKM5010kai.AsEnumerable()
                .Where(r => r.Field<bool>("CHECKED") == true && r.Field<string>("ODCD") == odcd)
                .Select(s => "'" + s.Field<string>("ODCD") + s.Field<string>("WKGRCD") + "'")
                .ToList();
            string s = "(" + string.Join(",", row) + ")";
            return s;
        }


    }
}
