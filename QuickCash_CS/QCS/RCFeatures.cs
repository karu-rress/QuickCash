//
// RCFeatrues.cs
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Osl;
using DAT = RC.RollingData;

namespace RC
{
    public static class RCFeatures
    {
        public static void Save()
        {
            if (!MainWindow.TextChanged)
                return;

			// Read Data File
            using (FileStream fs = new(DAT.DataFilePath, FileMode.Create))
            {
                StreamWriter sw = new(fs, Encoding.UTF8);
                StringBuilder sb = new(30);
                var Keys =
                    from data in DAT.Memo
                    where data.Value != null 
                    orderby data.Key ascending
                    select new { Clock = data.Key, Memos = data.Value.Select((elem) => elem) };

                foreach (var item in Keys)
                {
                    foreach (var inner in item.Memos)
                    {
                        if (inner.price.HasValue)
                            sb.Append($"{inner.chore}={inner.price.Value}|");
                        else
                            sb.Append($"{inner.chore}|");
                    }
                    sw.WriteLine($"{item.Clock}\t{sb}".Trim('|'));
                    sb.Clear();
                }
                sw.Flush();
            } 

            using FileStream fs = new(DAT.MapFilePath, FileMode.Create);     
            StreamWriter sw = new(fs, Encoding.UTF8);
            foreach (var item in DAT.ChoreMap)
            {
                sw.WriteLine("{0}={1}", item.Key, item.Value);
            }
            sw.Flush();
        }

        public static Task SaveAsync() => Task.Factory.StartNew(() => { Save(); });
        public static async Task DoAsyncSave()
        {
            var task1 = SaveAsync();
            await Task.WhenAll(task1);
        }

        public static void Read()
        {
            if ((new FileInfo(DAT.DataFilePath)).Exists)
            {
                using StreamReader sr = new (DAT.DataFilePath, Encoding.UTF8);
                
                string sb, date; 
                string[][] buf = new string[2][];
                (string chore, int? price) vdata;

                while ((sb = sr.ReadLine()).IsNotNullOrEmpty())
                {
                    // ignore comment
                    if (sb[0] is '#') continue;

                    buf[0] = sb.Split('\t');
                    if (!DAT.DateRegex.IsMatch(buf[0][0]))
                        throw new Exception("Regex Don't Match");
                    

                    date = buf[0][0];
                    buf[1] = buf[0][1].Split('|');

                    int? price = 0;
                    foreach (string item in buf[1])
                    {
                        buf[0] = item.Split('=');
                        if (buf[0].Length is 2) price = Convert.ToInt32(buf[0][1]);
                        else if (buf[0].Length is 1) price = null;
                        vdata = (buf[0][0], price);

                        if (DAT.Memo.ContainsKey(date))
                        {
                            DAT.Memo[date].Add(vdata);
                            continue;
                        }
                        DAT.Memo[date] = new List<(string, int?)> { vdata };
                    }
                }
            }

            if ((new FileInfo(DAT.MapFilePath).Exists))
            {
                using StreamReader sr = new(DAT.MapFilePath, Encoding.UTF8);
                
                string buf;
                string[] ary;

                while ((buf = sr.ReadLine()).IsNotNullOrEmpty())
                {
                    ary = buf.Split('=');
                    DAT.ChoreMap[ary[0]] = Convert.ToInt32(ary[1]);
                }
            }
        }

        public static Task ReadAsync() => Task.Factory.StartNew(() => { Read(); });
        public static async Task DoAsyncRead()
        {
            var task1 = ReadAsync();
            await Task.WhenAll(task1);
        }
    } // class RCFeatures
}
