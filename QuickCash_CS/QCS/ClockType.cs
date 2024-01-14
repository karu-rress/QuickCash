//
// ClockType.cs
//

using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using RollingAttribute;

namespace Osl.Clock
{
    [RollingAbout("Sunwoo Na"), Version(1.0), Language(Language.None)]
    [Comment("An Enumerator for classes drived DateType.")]
    public class DateTypeEnumerator : IEnumerator<char>
    {
        int pos = -1;
        readonly char[] list;

        public DateTypeEnumerator(char[] ary) => list = ary;
        public char Current { get => list[pos]; }
        object IEnumerator.Current { get => list[pos]; }
        public void Reset() => pos = -1;
        public void Dispose() { }

        public bool MoveNext()
        {
            if (pos < list.Length - 1)
            {
                pos += 1;
                return true;
            }
            return false;
        }
    }

    [RollingAbout("Sunwoo Na"), Version(1.0), Language(Language.None)]
    [Comment("An Interface which Osl.Clock.* drives.")]
    public interface IDateType : IComparable, IComparable<IDateType>, IEnumerable
    {
        int this[int index] { get; }
    }

    [RollingAbout("Sunwoo Na"), Version(1.0), Language(Language.None)]
    public struct Year : IDateType
    {
        short year;

        public Year(int y) : this((short)y) {  }
        public Year(short y) => year = y;

        public IEnumerator GetEnumerator()
        => new DateTypeEnumerator(year.ToString().ToCharArray());

        public int this[int index] { get => year.ToString()[index]; }

        public static explicit operator int(Year y) => y.year;

        public static bool operator >(Year y1, Year y2) => y1.year > y2.year;
        public static bool operator <(Year y1, Year y2) => y1.year < y2.year;
        public static bool operator ==(Year y, int n) => y.year == n;
        public static bool operator ==(Year y1, Year y2) => y1.year == y2.year;
        public static bool operator !=(Year y, int n) => y.year != n;
        public static bool operator !=(Year y1, Year y2) => y1.year != y2.year;
        public static bool operator <=(Year y, int n) => y.year <= n;
        public static bool operator >=(Year y, int n) => y.year >= n;
        public static Year operator -(Year y, int n) => (y.year - n).ToYear();

        public void Set(int n) => year = (short)n;

        int IComparable.CompareTo(object obj) => CompareTo((IDateType)obj);
        public int CompareTo(IDateType idt)
        {
            switch ((Year)idt)
            {
                case var y when y.year != year:
                    return (year > y.year) ? 1 : -1;
                default: return 0;
            }
        }

        public override string ToString() => $"{year:0000}";
        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj) => base.Equals(obj);
    }

    [RollingAbout("Sunwoo Na"), Version(1.0), Language(Language.None)]
    public struct Month : IDateType
    {
        byte month;

        public Month(int m) : this((byte)m) { }
        public Month(byte m) => month = m;

        public IEnumerator GetEnumerator() => new DateTypeEnumerator(month.ToString().ToCharArray());

        public int this[int index] {
            get => month.ToString()[index];
        }

        public static bool operator >(Month m1, Month m2) => m1.month > m2.month;
        public static bool operator <(Month m1, Month m2) => m1.month < m2.month;
        public static bool operator ==(Month m1, Month m2) => m1.month == m2.month;
        public static bool operator ==(Month m, int n) => m.month == n;
        public static bool operator !=(Month m1, Month m2) => m1.month != m2.month;
        public static bool operator !=(Month m, int n) => m.month != n;
        public static bool operator >=(Month m, int n) => m.month >= n;
        public static bool operator <=(Month m, int n) => m.month <= n;
        public static Month operator -(Month m1, Month m2) => (m1.month - m2.month).ToMonth();
        public static Month operator -(Month m, int n) => (m.month - n).ToMonth();
        public static Month operator +(Month m, int n) => (m.month + n).ToMonth();

        public static explicit operator int(Month m) => m.month;

        public void Set(int n) => month = (n > 0 && n <= 12) ? (byte)n
            : throw new ArgumentOutOfRangeException(nameof(n), $"Month: Out of Range. {n}: Expected 1 - 12");

        int IComparable.CompareTo(object obj) => CompareTo((IDateType)obj);

        public int CompareTo(IDateType idt)
        {
            switch ((Month)idt)
            {
                case var m when m.month != month: return (month > m.month) ? 1 : -1;
                default: return 0;
            }
        }

        public override string ToString() => $"{month:00}";
        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj) => base.Equals(obj);
    }

    [RollingAbout("Sunwoo Na"), Version(1.0), Language(Language.None)]
    public struct Date : IDateType
    {
        byte date;

        public Date(int d) : this((byte)d) { }
        public Date(byte d) => date = d;

        public IEnumerator GetEnumerator() => new DateTypeEnumerator(date.ToString().ToCharArray());

        public int this[int index] { get => date.ToString()[index]; }

        public static bool operator >(Date d1, Date d2) => d1.date > d2.date;
        public static bool operator <(Date d1, Date d2) => d1.date < d2.date;
        public static bool operator ==(Date d1, Date d2) => d1.date == d2.date;
        public static bool operator ==(Date d, int n) => d.date == n;
        public static bool operator !=(Date d1, Date d2) => d1.date != d2.date;
        public static bool operator !=(Date d, int n) => d.date != n;
        public static bool operator >=(Date d, int n) => d.date >= n;
        public static bool operator <=(Date d, int n) => d.date <= n;
        public static Date operator -(Date d1, Date d2) => (d1.date - d2.date).ToDate();
        public static Date operator -(Date d, int n) => (d.date - n).ToDate();

        public static explicit operator int(Date d) => d.date;

        public void Set(int n) => date = (n > 0 && n <= 31) ? (byte)n
            : throw new ArgumentOutOfRangeException(nameof(n), $"Date: Out of Range. {n}: Expected 1 - 31");

        int IComparable.CompareTo(object obj) => CompareTo((IDateType)obj);

        public int CompareTo(IDateType idt)
        {
            switch ((Date)idt)
            {
                case var d when d.date != date : return (date > d.date) ? 1 : -1;
                default: return 0;
            }
        }

        public override string ToString() => $"{date:00}";
        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj) => base.Equals(obj);
    }

    public struct DateType : IDateType
    {
        public static string[] Days { get; } = { "월요일", "화요일", "수요일", "목요일", "금요일", "토요일", "일요일" };

        Year year;
        Month month;
        Date date;


        public Year Year { get => year; set => year = value; }
        public Month Month { get => month; set => month = value; }
        public Date Date { get => date; set => date = value; }

        public DateType(DateTime dt) : this(dt.ToValueTuple())
		{
		}

        public DateType((Year, Month, Date) tuple)
		{
			(year, month, date) = tuple;
		}

        public DateType(int y, int m, int d)
			=> (year, month, date) = (y.ToYear(), m.ToMonth(), d.ToDate());
        public DateType(Year y, Month m, Date d) : this((y, m, d)) {  }
        public DateType(string token)
        {
            (year, month, date) = (int.Parse(s[0]).ToYear(), int.Parse(s[1]).ToMonth(), int.Parse(s[2]).ToDate());
        }

        override public string ToString() => $"{year}-{month:00}-{date:00}";
        override public bool Equals(object obj) => obj != null && CompareTo((IDateType)obj) is 0;
        override public int GetHashCode() => DateTime.Now.Millisecond ^ 129 << 7;

        public int this[int index] { get => $"{year}{month:00}{date:00}"[index]; }
        public IEnumerator GetEnumerator() => new DateTypeEnumerator($"{year}{month:00}{date:00}".ToCharArray());

        int IComparable.CompareTo(object obj) => CompareTo((DateType)obj);
        public int CompareTo(IDateType obj)
        {
            switch ((DateType)obj)
            {
                case DateType dt when year != dt.year:
                    return (year > dt.year) ? 1 : -1;
                case DateType dt when month != dt.month:
                    return (month > dt.month) ? 1 : -1;
                case DateType dt when date != dt.date:
                    return (date > dt.date) ? 1 : -1;
                default:
                    return 0;
            }
        }

        public static implicit operator (Year year, Month month, Date df) (DateType date) => (date.year, date.month, date.date);
        public static bool operator < (DateType dt1, DateType dt2)
        {
            return (dt1 is DateType item && (item.year < dt2.year || item.month < dt2.month || item.date < dt2.date));
        }

        public static bool operator >(DateType dt1, DateType dt2)
        => (dt1.year > dt2.year) || (dt1.year == dt2.year && dt1.month > dt2.month) || (dt1.month == dt2.month && dt1.date > dt2.date);

        public void Deconstruct(out Year year, out Month month, out Date date)
        {
            year = this.year;
            month = this.month;
            date = this.date;
        }

        public string GetDay() => GetDay((year, month, date));
        public static string GetDay((Year y, Month m, Date d) clock)
        {
            CalcDay(out int result, clock.y, clock.m, clock.d);
            return Days[result];
        }

        public static (Year year, Month month, Date date) Now() => DateTime.Now.ToValueTuple();

        public static void CalcDay(out int day, Year year, Month month, Date date)
        {
            int year_quater = (int)year >> 2;
            int year_divided_100 = year_quater / 25;
            int trim;
            switch ((int)month)
            {
                case var i when i is 1 || i is 2:
                    year -= 1;
                    month += 12;
                    trim = i.IsOdd() ? -5 : -4;
                    break;

                case var i when i is 4 || i is 9:
                    trim = -4;
                    break;

                case var i when (i is 3) || (i >= 5 && i <= 8) || (i >= 10 && i <= 12):
                    trim = -5;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(month), "ClockType.GetDay(): month is upper than 12 or lower than 1.");
            }
            day = (((int)year + year_quater - year_divided_100 + (year_divided_100 >> 2) + ((13 * (int)month - 8) / 5) + (int)date) % 7) + trim;
            TrimDay(ref day);
        }

        static void TrimDay(ref int day)
        {
            switch (day)
            {
                case var i when i >= 7:
                    day -= 7;
                    break;
                case var i when i >= 14:
                    day -= 14;
                    break;
                case var i when i < 0:
                    day += 7;
                    break;
            }
            TrimCheck(day);
        }

        [Conditional("DEBUG")]
        static void TrimCheck(int day)
        {
            if (day < 0 || day >= 7)
            {
                throw new ArgumentOutOfRangeException($"ClockType: Argument trim failed. The result is: {day}. (Expected 0 ~ 6.)");
            }
        }
    }

}
