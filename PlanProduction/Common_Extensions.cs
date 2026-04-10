using System;
using System.Text;
using System.Text.RegularExpressions;

namespace PlanProduction
{
    public static class ParseExtensions
    {
        // double
        public static double ToDoubleOrDefault(this object value, double def = 0.0)
        {
            if (value == null) return def;
            return double.TryParse(value.ToString(), out double v) ? v : def;
        }

        // int
        public static int ToIntOrDefault(this object value, int def = 0)
        {
            if (value == null) return def;
            return int.TryParse(value.ToString(), out int v) ? v : def;
        }

        // string（null → "" にしたい場合）
        public static string ToSafeString(this object value, string def = "")
        {
            if (value == null) return def;
            return value.ToString();
        }

        // string（null や空白をまとめて null にしたい場合）（MySQL に NULL を入れたい時に最適）
        public static string ToNullIfEmpty(this object value)
        {
            if (value == null) return null;

            string s = value.ToString();
            return string.IsNullOrWhiteSpace(s) ? null : s;
        }

        // datetime
        public static DateTime ToDateTimeOrDefaultToday(this object value)
        {
            if (value == null) return DateTime.Today;
            return DateTime.TryParse(value.ToString(), out DateTime d) ? d : DateTime.Today;
        }

        /// <summary>
        /// 半角カタカナを全角カタカナに変換する。
        /// ただし「①〜⑳」などの丸数字はそのまま残す。
        /// </summary>
        public static string ToZenkakuKanaKeepMaruNumber(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            // 丸数字（①〜⑳）を保護しつつ、それ以外を Normalize(FormKC) で全角化
            return Regex.Replace(value, @"[①-⑳]|[^①-⑳]+", m =>
            {
                // 丸数字はそのまま返す
                if (Regex.IsMatch(m.Value, @"[①-⑳]"))
                    return m.Value;

                // それ以外 → 半角カナを含む文字を全角化
                return m.Value.Normalize(NormalizationForm.FormKC);
            });
        }
    }
}
