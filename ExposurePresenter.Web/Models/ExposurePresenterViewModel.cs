using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExposurePresenter.Domain;

namespace ExposurePresenter.Web.Models
{
    public class ExposurePresenterViewModel
    {
        public ExposurePresenterViewModel()
        {
            Months = new List<string>()
            {
                "Styczeń",
                "Luty",
                "Marzec",
                "Kwiecień",
                "Maj",
                "Czerwiec",
                "Lipiec",
                "Sierpień",
                "Wrzesień",
                "Październik",
                "Listopad",
                "Grudzień"
            };

        }
        public List<string> Brands { get; set; }
        public List<string> Disciplines { get; set; }
        public List<string> Months { get; private set; }
        public List<string> Branches { get; set; }
        public IEnumerable<ExposureRecord> ExposureRecords { get; set; }
    }
}