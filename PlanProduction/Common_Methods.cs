using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace PlanProduction
{
    public static partial class Common
    {



        /// <summary>
        /// データベース設定ファイルの逆シリアライズ
        /// </summary>
        /// <returns>データベース設定データ配列</returns>
        public static DBConfigData[] ReserializeDBConfigFile()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 設定ファイル名
            string fileName = Common.CONFIG_FILE_DB;

            // XmlSerializerオブジェクトを作成
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(DBConfigData[]));

            // 読み込むファイルを開く
            System.IO.StreamReader sr = new System.IO.StreamReader(
                fileName, new System.Text.UTF8Encoding(false));

            // XMLファイルから読み込み、逆シリアル化する
            DBConfigData[] loadAry = (DBConfigData[])serializer.Deserialize(sr);

            // ファイルを閉じる
            sr.Close();

            return loadAry;
        }

        /// <summary>
        /// ファイル システム設定ファイルの逆シリアライズ
        /// </summary>
        /// <returns>ファイル システム設定データ配列</returns>
        public static FSConfigData[] ReserializeFSConfigFile()
        {
            Debug.WriteLine("[MethodName] " + MethodBase.GetCurrentMethod().Name);

            // 設定ファイル名
            string fileName = Common.CONFIG_FILE_FS;

            // XmlSerializerオブジェクトを作成
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(FSConfigData[]));

            // 読み込むファイルを開く
            System.IO.StreamReader sr = new System.IO.StreamReader(
                fileName, new System.Text.UTF8Encoding(false));

            // XMLファイルから読み込み、逆シリアル化する
            FSConfigData[] loadAry = (FSConfigData[])serializer.Deserialize(sr);

            // ファイルを閉じる
            sr.Close();

            return loadAry;
        }



        // アプリケーション設定ファイルの読み込み（実行ファイルと同じ階層）
        //
        // ※DataStore.dtKM5010kaiが読み込まれている事が前提
        //
        public static void DeserializeAppSettings()
        {
            string fileName = Common.CONFIG_FILE_AS;
            
            string jsonString = File.ReadAllText(@fileName);
            // JSONデシリアライザー
            var config = JsonSerializer.Deserialize<AppConfig>(jsonString);
            // 読み込んだデータを表示
            foreach (var odcd in config.OdCdSettings)
            {
                // データテーブルにチェック状態を設定
                DataRow findRow = DataStore.dtKM5010kai.Rows.Find(new object[]
                        { odcd.OdCd , odcd.KtCd });
                if (findRow == null) continue;
                findRow["CHECKED"] = true;
                if (sortOrderMap.ContainsKey(odcd.SortOrder))
                {
                    findRow["SORTORDER"] = sortOrderMap[odcd.SortOrder];
                }
                else
                {
                    findRow["SORTORDER"] = sortOrderMap[1];
                }
                findRow["TANNAME"] = odcd.TanName;
                findRow["AVA"] = odcd.Ava;
                // データテーブルの起動時のオリジナルを保存
                DataStore.originalKM5010kai = DataStore.dtKM5010kai.Copy();
            }
            DataStore.DefaultOdCd = config.DefaultOdCd;
            DataStore.originalDefaultOdCd = config.DefaultOdCd;
        }

        // アプリケーション設定ファイルへの書き込み
        //
        // DataStore の dtKM5010kaiのチェックレコードと DefaultOdCd を書き込む
        //
        public static void SerializeAppSettings()
        {
            List<Common.OdCdSetting> records = new List<Common.OdCdSetting>();
            string fileName = Common.CONFIG_FILE_AS;

            foreach (DataRow row in DataStore.dtKM5010kai.Rows)
            {
                bool isChecked = (string.IsNullOrEmpty(row["CHECKED"].ToString())) ? false : Convert.ToBoolean(row["CHECKED"]);
                if (!isChecked) continue;

                // 選択バリューからキーを検索
                string val = row[5].ToString();
                var key = sortOrderMap.FirstOrDefault(x => x.Value == val).Key;
                if (key == 0) key = 1; // Default値

                var config = new Common.OdCdSetting
                {
                    OdCd = row[0]?.ToString(),
                    KtCd = row[1]?.ToString(),
                    SortOrder = key,
                    TanName = row[6]?.ToString(),
                    Ava = row[7]?.ToString()
                };
                records.Add(config);
            }
            var root = new AppConfig
            {
                OdCdSettings = records,
                DefaultOdCd = DataStore.DefaultOdCd
            };
            var json = JsonSerializer.Serialize(root, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(fileName, json);
        }



        //
        // 画面設定ファイルの読み込み
        //
        public static FormConfig FormSettingsLoad()
        {
            if (!File.Exists(Common.CONFIG_FILE_WS))
                return new FormConfig();

            string json = File.ReadAllText(Common.CONFIG_FILE_WS);
            return JsonSerializer.Deserialize<FormConfig>(json) ?? new FormConfig();
        }

        //
        // 画面設定ファイルへの書き込み
        //
        public static void FormSettingsSave(FormConfig settings)
        {
            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(Common.CONFIG_FILE_WS, json);
        }



        /// <summary>
        /// 列名が日付（例: "3/17", "3/18"）の列を縦持ち化し、
        /// 「数量 × CT」を日付ごとに合計します。（可動率 (Availability)も考慮）
        /// 戻り値の列は [Date(string), Total(double)]。
        /// </summary>
        public static DataTable SumQtyTimesCT_ByDate(ref DataTable src, int 可動率)
        {
            ArgumentNullException.ThrowIfNull(src);

            string[] dateFormats = ["M/d", "M/d/yy", "M/d/yyyy", "MM/dd", "MM/dd/yy", "MM/dd/yyyy"];

            // 対象となる「日付列」を抽出（HMCD と CT は除外）
            var dateCols = src.Columns.Cast<DataColumn>()
                .Where(c => c.ColumnName != "HMCD" && c.ColumnName != "CT"
                            && DateTime.TryParseExact(c.ColumnName, dateFormats,
                                CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                .ToList();

            if (dateCols.Count == 0)
                throw new InvalidOperationException("日付列が見つかりませんでした。列名が '3/17' などの日付になっているか確認してください。");

            // 縦持ち＋集計：各行の CT を取り出し、各日付列の数量と掛け算して合計
            double ava = 1 / Convert.ToDouble(可動率) * 100;
            var grouped = src.AsEnumerable()
                .SelectMany(row =>
                {
                    double ct = SafeToDouble(row["CT"]); // 行ごとの CT
                    return dateCols.Select(dc => new
                    {
                        Date = ParseDate(dc.ColumnName, dateFormats),
                        // 数量 × CT(s) × 可動率 ÷ 3,600 
                        Work = Math.Round(SafeToDouble(row[dc]) * ct * ava / 3600, 2)
                    });
                })
                .GroupBy(x => x.Date.Date)
                .Select(g => new { Date = g.Key, Total = g.Sum(v => v.Work) })
                .OrderBy(x => x.Date);

            // 結果 DataTable（表示は "M/d"）
            var result = new DataTable();
            result.Columns.Add("Date", typeof(string));
            result.Columns.Add("Total", typeof(double));

            foreach (var x in grouped)
                result.Rows.Add(x.Date.ToString("M/d", CultureInfo.InvariantCulture), x.Total);

            return result;
        }

        private static DateTime ParseDate(string s, string[] fmts)
        {
            if (DateTime.TryParseExact(s, fmts, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                return dt.Date;
            throw new FormatException($"列名 '{s}' を日付として解釈できませんでした。");
        }

        private static double SafeToDouble(object v)
        {
            if (v == null || v == DBNull.Value) return 0.0;
            if (v is double d) return d;
            if (v is float f) return f;
            if (v is int i) return i;
            if (v is long l) return l;
            if (v is decimal m) return (double)m;

            var s = Convert.ToString(v, CultureInfo.InvariantCulture);
            if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var x))
                return x;

            return 0.0;
        }





    }
}

