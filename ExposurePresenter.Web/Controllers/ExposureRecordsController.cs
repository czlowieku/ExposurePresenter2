using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ExposurePresenter.Domain;
using ExposurePresenter.Web.DataContexts;
using ExposurePresenter.Web.Helpers;
using ExposurePresenter.Web.Models;

namespace ExposurePresenter.Web.Controllers
{
    [Authorize]
    public class ExposureRecordsController : Controller
    {
        private ExposureDb db;
        private ExposurePresenterViewModel model;

        public ExposureRecordsController()
        {
            db = new ExposureDb();
            model = new ExposurePresenterViewModel();
            model.Brands = db.Brands.Select(brand => brand.Name).ToList();
            model.Brands.Sort(String.Compare);
            model.Disciplines = db.Disciplines.Select(discipline => discipline.Name).ToList();
            model.Disciplines.Sort(String.Compare);

            model.Branches = db.Branches.Select(branch => branch.Name).ToList();
            model.Branches.Sort(String.Compare);

            Palete = new List<Color>()
            {
                Color.Gray,
                Color.LightSalmon,
                Color.LightPink,
                Color.PaleGreen,
                Color.LightSeaGreen,
                Color.IndianRed,
                Color.Yellow

            };

        }

        public ActionResult Index(IEnumerable<string> brands, IEnumerable<string> disciplines,
            IEnumerable<string> branches,
            IEnumerable<string> months)
        {
            model.ExposureRecords = FilterExposure(brands, disciplines, branches, months);

            if (Request.IsAjaxRequest())
                return PartialView("_GraphAndTable", model);
            return View(model);
        }



        [HttpPost]
        public ActionResult FilterRecords(IEnumerable<string> brands, IEnumerable<string> disciplines,
            IEnumerable<string> branches,
            IEnumerable<string> months)
        {
            model.ExposureRecords = FilterExposure(brands, disciplines, branches, months);

            if (Request.IsAjaxRequest())
                return PartialView("_GraphAndTable", model);
            return View(model);
        }


//  labels: monthLabels,
//     datasets: [
//         {
//             label: "My First dataset",
//             fillColor: "rgba(220,220,220,0.2)",
//             strokeColor: "rgba(220,220,220,1)",
//             pointColor: "rgba(220,220,220,1)",
//             pointStrokeColor: "#fff",
//             pointHighlightFill: "#fff",
//             pointHighlightStroke: "rgba(220,220,220,1)",
//             data: [randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor()]
//         },
//         {
//             label: "My Second dataset",
//             fillColor: "rgba(151,187,205,0.2)",
//             strokeColor: "rgba(151,187,205,1)",
//             pointColor: "rgba(151,187,205,1)",
//             pointStrokeColor: "#fff",
//             pointHighlightFill: "#fff",
//             pointHighlightStroke: "rgba(151,187,205,1)",
//             data: [randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor()]
//         }

        private List<Color> Palete;
        private int _maxGraphLines = 15;

        [HttpPost]
        public JsonResult GetGraphJason(IEnumerable<string> selectedBrands, IEnumerable<string> selectedDisciplines)
        {

            var graphLines = new List<dynamic>();
            int i = 0;
            foreach (var selectedBrand in selectedBrands.Take(15))
            {
                var color = Palete[i];
                if (i == Palete.Count - 1) i = -1;
                i++;
                graphLines.Add(new
                {
                    label = selectedBrand,
                    fillColor = string.Format("rgba({0},{1},{2},0.2)", color.R, color.G, color.B),
                    strokeColor = string.Format("rgba({0},{1},{2},1)", color.R, color.G, color.B),
                    pointColor = string.Format("rgba({0},{1},{2},1)", color.R, color.G, color.B),
                    pointStrokeColor = "#fff",
                    pointHighlightFill = "#fff",
                    pointHighlightStroke = string.Format("rgba({0},{1},{2},1)", color.R, color.G, color.B),
                    data = GetGraphDataForBrand(selectedBrand).ToArray()
                });
            }

            return Json(new {labels = model.Months, datasets = graphLines}, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<long> GetGraphDataForBrand(string selectedBrand)
        {
            var ret = new List<long>();
            var records = db.ExposureRecords.Where(record => record.Brand == selectedBrand);
            foreach (var month in model.Months)
            {
                var recFromMonth = records.FirstOrDefault(record => record.Month == month);
                ret.Add(recFromMonth != null ? recFromMonth.Quantity : 0);
            }
            return ret;
        }

        private IEnumerable<ExposureRecord> FilterExposure(IEnumerable<string> brands, IEnumerable<string> disciplines,
            IEnumerable<string> branches, IEnumerable<string> months)
        {
            bool filerbyBrand = false, filerbyDiscipline = false, filterByMonth = false, filterbyBrnach = false;
            var bandsFilter = new List<string>();
            if (brands != null)
            {
                filerbyBrand = true;
                bandsFilter.AddRange(brands);
            }

            var disciplinesFilter = new List<string>();
            if (disciplines != null)
            {
                filerbyDiscipline = true;
                disciplinesFilter.AddRange(disciplines);
            }


            var branchFilter = new List<string>();
            if (branches != null)
            {
                filterbyBrnach = true;
                branchFilter.AddRange(branches);
            }

            var monthsFilter = new List<string>();
            if (months != null)
            {
                filterByMonth = true;
                monthsFilter.AddRange(months);
            }
            return
                db.ExposureRecords
                    .Where(record => !filerbyBrand || bandsFilter.Contains(record.Brand))
                    .Where(record => !filerbyDiscipline || disciplinesFilter.Contains(record.Discipline))
                    .Where(record => !filterbyBrnach || branchFilter.Contains(record.Branch))
                    .Where(record => !filterByMonth || monthsFilter.Contains(record.Month))
                    .ToList();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
