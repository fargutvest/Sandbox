using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ViyarParser
{
    public class HtmlHelper
    {
        private double requiredSquare = 0.687;

        public string Build(List<KeyValuePair<int, List<EntryModel>>> sorted)
        {
            var resultDoc = new HtmlDocument();
            foreach (var item in sorted)
            {
                Console.Write('"');
                resultDoc.DocumentNode.AppendChild(HtmlNode.CreateNode($@"<li>
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <img src = 'Assets/target.png' width='300'>
                                                                                    </td>
                                                                                    <td>
                                                                                        <img src = '{item.Value.First().ImageUrl}' width='300'>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div><b>{item.Key}</b> - {item.Value.First().Nomenklature}</div>
                                                                                        {string.Join("", item.Value.Select(Select).ToList())}
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                          </li>"));
            }

            return resultDoc.DocumentNode.OuterHtml;
        }

        private string Select(EntryModel _)
        {

            var cost = _.Cost;
            var characteristic = $"{_.Characteristic.ToString().Replace(",", "х")}";
            var count = $"({_.Count})";

            var bold = "<b>{0}</b>";
            if (_.Count > requiredSquare)
            {
                cost = string.Format(bold, cost);
            }

            var result = $"<div>{characteristic} {count} - {cost} ({_.Price})</div>";
            return result;
        }
    }
}
