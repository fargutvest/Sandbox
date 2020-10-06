using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace ViyarParser
{
    public class App
    {
        public void Start()
        {
            var preferedColors = new int[] { 23163, 30753, 26529, 26534, 43097, 43101, 63118 };

            var report = new Report();
            var viyarHelper = new ViyarByHelper(report);
            var catalogDsp = viyarHelper.GrabCatalogDsp();

            var fileName = "stebeneva8.xls";
            viyarHelper.DownloadRemainsList(fileName);

            var remainsCells = new ExcelHelper(report).ReadAllNotEmptyLines($"{Environment.CurrentDirectory}/{fileName}", new int[] { 2, 3, 4, 5, 6 });
            Array.Copy(remainsCells, 1, remainsCells, 0, remainsCells.Length - 1);
            var remainsList = remainsCells.Select(_ => Select(_, catalogDsp)).ToList();

            var grouped = remainsList.GroupBy(_ => _.Code).ToDictionary(_ => _.Key, _ => _.OrderBy(x => x.Count).ToList()).ToList();
            var prefered = grouped.FindAll(_ => preferedColors.Contains(_.Key));
            var rest = grouped.FindAll(_ => preferedColors.Contains(_.Key) == false);

            var sorted = new List<KeyValuePair<int, List<EntryModel>>>();
            sorted.AddRange(prefered);
            sorted.AddRange(rest);


            var resultFilePath = "result.html";
            if (File.Exists(resultFilePath))
            {
                File.Delete(resultFilePath);
            }

            using (StreamWriter file = new StreamWriter(resultFilePath, true))
            {
                file.WriteLine(new HtmlHelper().Build(sorted));
            }
        }

        private EntryModel Select(string[] _, Dictionary<int, string[]> catalogDsp)
        {
            var code = int.Parse(_[0]);
            var count = double.Parse(_[4]);
            var imageUrl = catalogDsp.ContainsKey(code) ? catalogDsp[code][0] : "";
            var price = catalogDsp.ContainsKey(code) ? catalogDsp[code][1] : "";
            return new EntryModel()
            {
                Code = code,
                Nomenklature = _[1],
                Characteristic = Size.Parse(_[2].Replace("х", ",")),
                Thikness = _[3],
                Count = count,
                ImageUrl = imageUrl,
                Price = price,
                Cost = string.IsNullOrEmpty(price) == false ? (count / (2070 * 2800 / (double)1000000) * double.Parse(price.Replace(" руб/лист", ""))).ToString("0.##") + "руб" : null
            };
        }
    }
}
