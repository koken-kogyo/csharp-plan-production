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
using Excel = Microsoft.Office.Interop.Excel;

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
            System.Xml.Serialization.XmlSerializer serializer = new(typeof(DBConfigData[]));

            // 読み込むファイルを開く
            System.IO.StreamReader sr = new(fileName, new System.Text.UTF8Encoding(false));

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
            System.Xml.Serialization.XmlSerializer serializer = new(typeof(FSConfigData[]));

            // 読み込むファイルを開く
            System.IO.StreamReader sr = new(fileName, new System.Text.UTF8Encoding(false));

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
                // データテーブルにチェック状態を設定（主キーで検索PrimaryKey）
                DataRow findRow = DataStore.dtKM5010kai.Rows.Find([odcd.OdCd , odcd.KtCd]);
                if (findRow == null) continue;
                findRow["CHECKED"] = true;
                if (sortOrderMap.TryGetValue(odcd.SortOrder, out string value))
                {
                    findRow["SORTORDER"] = value;
                }
                else
                {
                    findRow["SORTORDER"] = sortOrderMap[1];
                }
                findRow["TANNAME"] = odcd.TanName;
                findRow["AVA"] = odcd.Ava;
                findRow["STARTTIME"] = odcd.StartTime;
                findRow["EXCELNAME"] = odcd.ExcelName;
                findRow["FULLPATH"] = odcd.FullPath;
            }
            // データストアに退避保存
            DataStore.DefaultOdCd = config.DefaultOdCd;
            DataStore.OdCdSettings = config.OdCdSettings;
            DataStore.originalDefaultOdCd = config.DefaultOdCd;
            DataStore.originalKM5010kai = DataStore.dtKM5010kai.Copy();
        }

        // アプリケーション設定ファイルへの書き込み
        //
        // DataStore の dtKM5010kaiのチェックレコードと DefaultOdCd を書き込む
        //
        public static void SerializeAppSettings()
        {
            List<OdCdSetting> records = [];
            string fileName = Common.CONFIG_FILE_AS;

            foreach (DataRow row in DataStore.dtKM5010kai.Rows)
            {
                bool v = bool.TryParse(row["CHECKED"]?.ToString(), out bool isChecked);
                if (!isChecked) continue;

                // 選択バリューからキーを検索
                string val = row[5].ToString();
                var key = sortOrderMap.FirstOrDefault(x => x.Value == val).Key;
                if (key == 0) key = 1; // Default値

                var config = new OdCdSetting
                {
                    OdCd = row[0]?.ToString(),
                    KtCd = row[1]?.ToString(),
                    SortOrder = key,
                    TanName = row[6]?.ToString(),
                    Ava = row[7]?.ToString(),
                    StartTime = row[8]?.ToString(),
                    ExcelName = row[9]?.ToString(),
                    FullPath = row[10]?.ToString()
                };
                records.Add(config);
            }
            var root = new AppConfig
            {
                OdCdSettings = records,
                DefaultOdCd = DataStore.DefaultOdCd
            };
            string json = JsonSerializer.Serialize(root, options: JsonWriteOptions);
            DataStore.OdCdSettings = records;
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
            string json = JsonSerializer.Serialize(settings, options: JsonWriteOptions);
            File.WriteAllText(Common.CONFIG_FILE_WS, json);
        }

        private static readonly JsonSerializerOptions JsonWriteOptions = new()
        {
            WriteIndented = true
        };

        private static readonly JsonSerializerOptions JsonReadOptions = new()
        {
            AllowTrailingCommas = true
        };


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

        /// <summary>
        /// 雛形ファイルを開いてデスクトップに名前を付けて保存
        /// </summary>
        /// <param name="fullPath"></param>
        public static string SaveExcelToDesktop(ref DataGridView dgv, string fullPath)
        {
            // Excelアプリ起動
            var app = new Excel.Application();
            Excel.Workbook book = null;

            try
            {
                // 既存ファイルを開く
                book = app.Workbooks.Open(fullPath);

                // 保存ファイル名の作成
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string baseName = Path.GetFileNameWithoutExtension(fullPath);// 元ファイル名（拡張子なし）
                baseName = baseName.Replace("雛形_", "");
                string ext = Path.GetExtension(fullPath);// 拡張子
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string saveName = $"{baseName}_{timestamp}{ext}";
                string savePath = Path.Combine(desktop, saveName);

                // 別名で保存
                book.SaveAs(savePath);

                return savePath;

            }
            finally
            {
                book?.Close();
                app.Quit();
            }
        }

        public static string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";
            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }
            return columnName;
        }

        public static Dictionary<string, string> GetExcelColumnMap(Excel.Worksheet sheet)
        {
            var map = new Dictionary<string, string>();

            Excel.Range used = sheet.UsedRange;
            int lastRow = used.Rows.Count;
            int lastCol = used.Columns.Count;

            // ★ 3行目以降を検索
            for (int row = 3; row <= lastRow; row++)
            {
                for (int col = 1; col <= lastCol; col++)
                {
                    Excel.Range cell = (Excel.Range)sheet.Cells[row, col];
                    string header = cell.Value?.ToString();

                    if (!string.IsNullOrEmpty(header))
                    {
                        string excelColName = GetExcelColumnName(col);

                        // 同じヘッダー名が複数あっても最初のものを採用
                        if (!map.ContainsKey(header))
                        {
                            map[header] = excelColName;
                        }
                    }
                }
            }

            return map;
        }


        public static void ExportByHeaderMatch(ref DataGridView dgv, string excelPath)
        {
            var app = new Excel.Application();
            app.Visible = true;

            Excel.Workbook book = app.Workbooks.Open(excelPath);
            Excel.Worksheet sheet = (Excel.Worksheet)book.ActiveSheet;

            // ★ Excel の 3行目以降からマッピングを作成
            var excelMap = GetExcelColumnMap(sheet);

            // ★ DataGridView のデータを Excel の 4行目以降に書き込む
            for (int r = 0; r < dgv.Rows.Count; r++)
            {
                int excelRow = r + 5; // 5行目から書き込む

                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    string dgvHeader = col.HeaderText;

                    if (excelMap.ContainsKey(dgvHeader))
                    {
                        string excelCol = excelMap[dgvHeader];
                        var value = dgv.Rows[r].Cells[col.Index].Value;

                        sheet.Range[$"{excelCol}{excelRow}"].Value = value;
                    }
                }
            }
        }






    }
}

