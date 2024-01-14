//
// RollingExtension.cs
//

using System;
using Osl.Clock;
using Gtk;
using System.Collections.Generic;
using MemoType = System.Collections.Generic.Dictionary<(Osl.Clock.Year, Osl.Clock.Month, Osl.Clock.Date), (string chore, int? price)>;

namespace Osl
{
    public static class RollingExtension
    {
        static readonly TextTag tag = new TextTag(null);
        static TextIter iter;

        public static (Year, Month, Date) ToValueTuple(this DateTime var) => (var.Year.ToYear(), var.Month.ToMonth(), var.Day.ToDate());

        public static (string, int?) Find(this MemoType m, DateType dt) => m.ContainsKey(dt) ? (m[dt].chore, (m[dt].price)) : default;

        public static DateType ToDateType(this Calendar calendar) => new DateType(calendar.ToValueTuple());

        public static (Year, Month, Date) ToValueTuple(this Calendar c) => (c.Year.ToYear(), (c.Month + 1).ToMonth(), c.Day.ToDate());

        public static string ToDayString(this Calendar calendar) => DateType.GetDay(calendar.ToValueTuple());

        public static bool IsOdd(this int var) => (var & 1) is 1;

        public static void PrintToLabel(this Calendar calendar, ref Label date_label, ref Label day_label)
        {
            date_label.Print(25, $"{calendar.Year}-{calendar.Month + 1:00}-{calendar.Day:00}");
            day_label.Print(25, $"{calendar.ToDayString()}");
        }

        public static void Print(this Label label, int fontsize = 25, string msg = default)
        => label.Markup = $"<span size='{fontsize * 1000}'>{msg}</span>";

        public static void Println(this TextView txt, int line = 0, int fontsize = 25, params string[] msg)
        {
            txt.Buffer.TagTable.Add(tag);
            tag.Size = (int)Pango.Scale.PangoScale * fontsize;
            iter = txt.Buffer.GetIterAtLine(line);

            foreach (string item in msg)
            {
                txt.Buffer.InsertWithTags(ref iter, $"{item}\n", tag);
            }
        }

        public static List<T> Connect<T>(this List<T> list, List<T> other)
        {
            List<T> result = new(list);
            result.AddRange(other);
            return result;
        }

        public static Year ToYear(this int var) => new Year(var);
        public static Month ToMonth(this int var) => new Month(var);
        public static Date ToDate(this int var) => new Date(var);

        public static bool Exists(this Dictionary<string, List<(string, int?)>> d, string date, string chore, int? price)
        {
            if (d.TryGetValue(date, out var value))
            {
                foreach ((string c, int? p) in value)
                {
                    if (c == chore && p == price)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }

        public static bool IsNotNullOrEmpty(this string s)
        => !string.IsNullOrEmpty(s);

        public static bool IsNotNullOrWhiteSpace(this string s)
        => !string.IsNullOrWhiteSpace(s);
            
        
    }
}
