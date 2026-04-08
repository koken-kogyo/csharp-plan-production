using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;                       // Process.Start
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;           // Marshal.ReleaseComObject
using System.Text.Json;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace PlanProduction
{
    public static partial class Common
    {

        /// <summary>
        /// 一定時間後に自動で閉じるメッセージボックスクラス
        /// </summary>
        public static class MessageBox2
        {
            public static void Show(string text, string caption, int timeout, MessageBoxIcon icon)
            {
                var timer = new System.Windows.Forms.Timer
                {
                    Interval = timeout
                };
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    IntPtr mbWnd = FindWindow(null, caption);
                    if (mbWnd != IntPtr.Zero)
                    {
                        int v = SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                    }
                };
                timer.Start();

                MessageBox.Show(text, caption, MessageBoxButtons.OK, icon);
            }

            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            private static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

            private const uint WM_CLOSE = 0x0010;
        }


        /// <summary>
        /// データベース設定ファイルの逆シリアライズ
        /// </summary>
        /// <returns>データベース設定データ配列</returns>
        public static DBConfigData[] ReserializeDBConfigFile()
        {
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




        // 文字列を検索し行番号を返却
        private static int GetRowNo(ref Excel.Worksheet sheet, string findstring, int column)
        {
            Excel.Range foundCell = sheet.Columns[column].Find(
                What: findstring.Replace("'", ""),
                LookIn: Excel.XlFindLookIn.xlValues,
                LookAt: Excel.XlLookAt.xlPart,      // 部分一致
                SearchOrder: Excel.XlSearchOrder.xlByRows,
                MatchCase: false                    // 大文字小文字を区別しない
            );
            return foundCell == null 
                ? throw new Exception($"文字列[{findstring}]が列{column}で見つかりませんでした.") 
                : foundCell.Row;
        }

        private static string GetExcelColumnName(int columnNumber)
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

        // dgv行ヘッダーのマッピングを作成
        private static Dictionary<string, int> GetDgvColumnMap(ref DataGridView dgv, ref Excel.Worksheet sheet, int baserow)
        {
            Dictionary<string, int> map = [];

            int lastColumnIndex = sheet.Cells[baserow, 1].End(Excel.XlDirection.xlToRight).Column;

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                string header = column.HeaderText;
                for (int col = 1; col <= lastColumnIndex; col++)
                {
                    object value = sheet.Cells[baserow, col].Value;
                    if (value != null && value.ToString().Contains(header))
                    {
                        map[header] = col;
                        break;
                    }
                }
            }
            return map;
        }

        // excelの行ヘッダーマッピングを作成
        private static List<int> GetFormulaColumnList(ref Excel.Worksheet sheet, int baserow)
        {
            List<int> ary = [];

            int lastColumnIndex = sheet.Cells[baserow, 1].End(Excel.XlDirection.xlToRight).Column;

            for (int col = 1; col <= lastColumnIndex; col++)
            {
                if (sheet.Cells[baserow + 1, col]?.HasFormula)
                {
                    ary.Add(col);
                }
            }
            return ary;
        }

        // 保存ファイル名の存在チェックと作成
        public static bool CreateSaveFullPath(string odcd, DateTime plandt, out string saveFullPath)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFullPath = @$"{desktop}\可動率_計画と実績_{odcd}_{plandt:yyyyMMdd}.xlsx";
            return !File.Exists(saveFullPath);
        }

        /// <summary>
        /// 計画を印刷
        /// 　雛形ファイルを開いて、計画を貼り付け
        /// 　デスクトップに名前を付けて保存し終了
        /// </summary>
        /// <param name="dgv">(参照型)DataGridView</param>
        /// <param name="odcd">手配先コード</param>
        /// <param name="plandt">計画日</param>
        /// <param name="hinapath">雛形ExcelのFullPath</param>
        /// <param name="hinapath">出力ファイルのFullPath</param>
        public static bool PrintPlan(ref DataGridView dgv, string odcd, DateTime plandt, string タイトル可動率
            , string hinapath, string savefullpath)
        {
            bool ret = false;
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            try
            {
                // Excel本体と雛形ファイルを開く
                excelApp = new()
                {
                    Visible = true
                };
                workbook = excelApp.Workbooks.Open(hinapath);
                worksheet = (Excel.Worksheet)workbook.ActiveSheet;
                worksheet.Name = plandt.ToString("MMdd");

                // タイトルのチェックと作成（Excelの名前の管理に存在する事）
                List<string> names = [];
                foreach (Excel.Name n in workbook.Names) names.Add(n.Name);
                if (!names.Contains("タイトル") || !names.Contains("タイトル日付") || !names.Contains("可動率"))
                {
                    savefullpath = hinapath; // finally で雛形ファイルを開かせる
                    excelApp.Visible = false;
                    throw new Exception("雛形Excelを修正してください．\n\n数式 ＞ 名前の管理を作成してください．\n「タイトル:A1」「タイトル日付:C2」「可動率:O2」");
                }
                worksheet.Range["タイトル"].Value = DataStore.M0300Map[odcd];
                worksheet.Range["タイトル日付"].Value = plandt.ToString("yyyy/MM/dd");
                worksheet.Range["可動率"].Value = タイトル可動率;

                // タイトルの行番号とタイトルの最終列番号を取得
                int baserow = GetRowNo(ref worksheet, "No", 1);
                int endCol = worksheet.Cells[baserow, 1].End(Excel.XlDirection.xlToRight).Column;
                int startRow = baserow + 1;

                // dgv行ヘッダーとExcelのマッピングを作成
                var dgvMap = GetDgvColumnMap(ref dgv, ref worksheet, baserow);
                var excelFormulas = GetFormulaColumnList(ref worksheet, baserow);

                // Excel「No.」の最終行を取得（※文字列「最終行（マクロで使用消さないで）」が入っている事が前提）
                int endFrameRow = startRow;
                object cellValue = null;
                do
                {
                    endFrameRow++;
                    cellValue = worksheet.Cells[endFrameRow, 1].Value;
                    if (endFrameRow > 1000) // セーフティー
                        throw new Exception("Excelの最終行が見つかりませんでした。雛形ファイルの「No.」列に「最終行（マクロで使用消さないで）」という文字列が入っているか確認してください。");
                } while (string.IsNullOrEmpty(cellValue?.ToString()) || int.TryParse(cellValue.ToString(), out _));
                endFrameRow--;

                // 雛形Excelの行が足りない場合、行追加を行う
                if ((dgv.Rows.Count - 1) > (endFrameRow - startRow + 1))
                {
                    int rowsToAdd = (dgv.Rows.Count - 1) - (endFrameRow - startRow + 1);
                    Excel.Range insertRange = worksheet.Rows[endFrameRow];
                    insertRange.Resize[rowsToAdd].Insert(Excel.XlInsertShiftDirection.xlShiftDown);
                    // endRowを更新
                    endFrameRow += rowsToAdd;
                    // 連番を振り直す
                    int i = 1;
                    for (int noRow = startRow; noRow <= endFrameRow; noRow++)
                    {
                        worksheet.Cells[noRow, 1] = i;
                        i++;
                    }
                    //// 外枠内枠の引き直し
                    //Excel.Range rng = worksheet.Range[worksheet.Cells[startRow, 1], worksheet.Cells[endFrameRow, endCol]];
                    //rng.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                }

                // DataGridView のデータを Excel に書き込む（NewRowを除く）
                for (int r = 0; r < dgv.Rows.Count - 1; r++)
                {
                    int excelRow = r + startRow;

                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        string dgvHeader = col.HeaderText;

                        if (dgvMap.TryGetValue(dgvHeader, out int excelCol))
                        {
                            if (!excelFormulas.Contains(excelCol))
                            {
                                var value = dgv.Rows[r].Cells[col.Index].Value;
                                worksheet.Cells[excelRow, excelCol].Value = value;
                            }
                        }
                    }
                }

                // 式のコピー
                int endDataRow = worksheet.Cells[baserow, 2].End(Excel.XlDirection.xlDown).Row;
                foreach (int col in excelFormulas)
                {
                    worksheet.Cells[startRow, col].Copy();
                    worksheet.Range[worksheet.Cells[startRow + 1, col], worksheet.Cells[endDataRow, col]]
                        .PasteSpecial(Excel.XlPasteType.xlPasteFormulas);
                }
                for (int i = endDataRow + 1; i <= endFrameRow; i++)
                {
                    worksheet.Cells[i, 1].Value = "";
                }
                // 別名で保存（Desktopに作成）
                workbook.SaveAs(savefullpath);
                ret = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー: " + ex.Message, "計画印刷", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // ブックを閉じる
                if (workbook != null)
                {
                    workbook.Close(false);
                    Marshal.ReleaseComObject(workbook);
                    workbook = null;
                }

                // Excelアプリケーション終了
                if (excelApp != null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                    excelApp = null;
                }

                // ガベージコレクションを2回強制実行してCOM参照を完全解放
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                // 関連付けられたアプリでExcelを開く
                if (File.Exists(@savefullpath))
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = @savefullpath,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
            }
            return ret;
        }






    }
}

