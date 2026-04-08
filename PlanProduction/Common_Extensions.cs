using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
