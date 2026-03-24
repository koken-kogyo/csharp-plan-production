using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Windows.Forms;
using static Org.BouncyCastle.Math.EC.ECCurve;

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



        // 設定ファイル読み込み（実行ファイルと同じ階層）
        public static void DeserializeAppSettings()
        {
            string fileName = Common.CONFIG_FILE_LS;
            
            string jsonString = File.ReadAllText(@fileName);
            // JSONデシリアライザー
            var config = JsonSerializer.Deserialize<AppConfig>(jsonString);
            // 読み込んだデータを表示
            foreach (var odcd in config.OdCdSettings)
            {
                Console.WriteLine($"ODCD: {odcd.OdCd}");
                Console.WriteLine($"KTCD: {odcd.KtCd}");
                Console.WriteLine($"SortOrder: {odcd.SortOrder}");
                Console.WriteLine($"ODCD: {odcd.TanName}");
                Console.WriteLine($"KTCD: {odcd.Ava}");
            }
            Console.WriteLine($"JSONファイルを読み込みました:\n{jsonString}");
        }

        // 設定ファイル書き込み
        public static void SerializeAppSettings(ref DataGridView dgv)
        {
            List<Common.OdCdSetting> records = new List<Common.OdCdSetting>();
            string fileName = Common.CONFIG_FILE_LS;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                bool isChecked = Convert.ToBoolean(row.Cells[0].Value);
                if (!isChecked) continue;

                var config = new Common.OdCdSetting
                {
                    OdCd = row.Cells[1].Value?.ToString(),
                    KtCd = row.Cells[2].Value?.ToString(),
                    SortOrder = Convert.ToInt32(row.Cells[4].Value),
                    TanName = row.Cells[5].Value?.ToString(),
                    Ava = row.Cells[6].Value?.ToString()
                };

                records.Add(config);
            }

            // JSON書き込み
            var json = JsonSerializer.Serialize(records, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(fileName, json);
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

