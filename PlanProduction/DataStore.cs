using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace PlanProduction
{
    public static class DataStore
    {
        // データベース値
        public static DataTable dtKM5010kai;
        public static DataTable dtD0410;

        public static void KM5010AddColumns()
        {
            if (dtKM5010kai.Columns.Count <= 3)
            {
                dtKM5010kai.Columns.Add("CHECKED", typeof(bool));
                dtKM5010kai.Columns.Add("SORTORDER", typeof(string));
                dtKM5010kai.Columns.Add("TANNAME" , typeof(string));
                dtKM5010kai.Columns.Add("AVA", typeof(string));
                // 主キーを設定して検索に使用
                dtKM5010kai.PrimaryKey = new DataColumn[]
                {
                    dtKM5010kai.Columns["ODCD"],
                    dtKM5010kai.Columns["WKGRCD"]
                };
            }
        }
    }
}
