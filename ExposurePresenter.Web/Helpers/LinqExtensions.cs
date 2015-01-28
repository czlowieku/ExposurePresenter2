using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExposurePresenter.Web.Helpers
{
    public static class LinqExtensions
    {
       
        public static IEnumerable<string> OrderByMonth(this IEnumerable<string> source, bool ascending=true)
        {
            var months = new List<string>()
            {
                "Styczeń",
                "styczeń",
                "Luty",
                "luty",
                "Marzec",
                "marzec",
                "Kwiecień",
                "kwiecień",
                "Maj",
                "maj",
                "Czerwiec",
                "czerwiec",
                "Lipiec",
                "lipiec",
                "Sierpień",
                "sierpień",
                "Wrzesień",
                "wrzesień",
                "Październik",
                "październik",
                "Listopad",
                "listopad",
                "Grudzień",
                "grudzień",
            };
           var orderDict = months.Select((c, i) => new { month = c, Order = i })
                    .ToDictionary(o => o.month, o => o.Order);
            if (ascending)
            {
                return source.OrderBy(s => orderDict[s]);
            }
            return source.OrderByDescending(s => orderDict[s]);

        }
    }
}