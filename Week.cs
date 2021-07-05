using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace asp_scheduler
{
    public class Week
    {
        public Week(string day, string format)
        {
            _day = day;
            _format = format;
        }

        string _day;
        string _format;

        public string GetCurWeek()
        {
            //get prev monday
            var dt = DateTime.ParseExact(_day, _format, CultureInfo.InvariantCulture);
            dt = dt.AddDays(-DayOfWeek2Int(dt.DayOfWeek));
            return dt.ToString(_format);
        }

        public string GetFullWeek()
        {
            //get interval: prev monday - sunday
            var dt = DateTime.ParseExact(_day, _format, CultureInfo.InvariantCulture);
            dt = dt.AddDays(-DayOfWeek2Int(dt.DayOfWeek));
            var dt1 = DateTime.ParseExact(_day, _format, CultureInfo.InvariantCulture);
            dt1 = dt1.AddDays(-DayOfWeek2Int(dt1.DayOfWeek));
            dt1 = dt1.AddDays(6);
            return dt.ToString(_format) + "-" + dt1.ToString(_format);
        }

        public int DayOfWeek2Int(DayOfWeek dw)
        {
            switch (dw)
            {
                case DayOfWeek.Monday: return 0;
                case DayOfWeek.Tuesday: return 1;
                case DayOfWeek.Wednesday: return 2;
                case DayOfWeek.Thursday: return 3;
                case DayOfWeek.Friday: return 4;
                case DayOfWeek.Saturday: return 5;
                case DayOfWeek.Sunday: return 6;
            }
            return 0;
    }
    }
}
