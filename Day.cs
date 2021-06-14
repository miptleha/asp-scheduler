using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace asp_scheduler
{
    public class Day
    {
        public Day(string day, string format)
        {
            if (string.IsNullOrEmpty(day))
                throw new Exception("Day not set");

            _day = day;
            _format = format;
        }

        string _day;
        string _format;

        public string Want { get; set; }
        public string Done { get; set; }

        public void Save()
        {
            XElement xW = new XElement("want");
            xW.Value = Want;
            XElement xD = new XElement("done");
            xD.Value = Done;
            XElement xA = new XElement("day");
            xA.Add(xW);
            xA.Add(xD);

            XDocument doc = new XDocument();
            doc.Add(xA);
            string path = Path.Combine(StoragePath, GetDayFile(_day));
            doc.Save(path);
        }

        public void Load()
        {
            string path = Path.Combine(StoragePath, GetDayFile(_day));
            if (File.Exists(path))
            {
                XDocument doc = XDocument.Load(path);
                XElement xA = doc.Element("day");
                XElement xW = xA.Element("want");
                XElement xD = xA.Element("done");

                Want = xW.Value;
                Done = xD.Value;
            }
        }

        private string StoragePath
        {
            get
            {
                string appPath = AppContext.BaseDirectory;
                string path = Path.Combine(appPath, "schedule");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }

        private string GetDayFile(string day)
        {
            if (_format != "dd.MM.yyyy")
                throw new Exception("Change GetDayFile() for new format");

            if (day == null || day.Length != 10)
                return day;
            string dd = day.Substring(0, 2);
            string mm = day.Substring(3, 2);
            string yyyy = day.Substring(6, 4);
            return yyyy + mm + dd + ".txt";
        }
    }
}
