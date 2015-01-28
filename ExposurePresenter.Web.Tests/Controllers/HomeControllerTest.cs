using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ExposurePresenter.DataInput;
using ExposurePresenter.Web.DataContexts;
using ExposurePresenter.Web.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExposurePresenter.Web;
using ExposurePresenter.Web.Controllers;

namespace ExposurePresenter.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {

        [TestMethod]
        public void t33()
        {
            var months = new List<string>()
            {
                "Styczeń",
                "Luty",
                "Marzec",
             
                "Sierpień",
                "Wrzesień",
                "Październik",
                "Listopad",
                "Grudzień",
                   "Kwiecień",
                "Maj",
                "Czerwiec",
                "Lipiec",
            };

            var a = months.OrderByMonth(false);
        }
    

    [TestMethod]
        public void t()
        {
            var reader = new ExcelFileReader();
            var itmes=    reader.ReadDistinctBrands(
                @"D:\dev\C#\ExposurePresenter\XLSInputFiles\PLK Grzegorz branże.xlsx").ToArray();
//            var db = new ExposureDb();
//            foreach (var item in itmes)
//            {
//                db.ExposureRecords.Add(item);
//            }
        }


        [TestMethod]
        public void t2()
        {
            var reader = new ExcelFileReader();
            var itmes = reader.ReadDistinctDisciplines(
                @"D:\dev\C#\ExposurePresenter\XLSInputFiles\PLK Grzegorz branże.xlsx").ToArray();
//            var db = new ExposureDb();
//            foreach (var item in itmes)
//            {
//                db.ExposureRecords.Add(item);
//            }
        }

//
//        [TestMethod]
//        public void Index()
//        {
//            // Arrange
//            HomeController controller = new HomeController();
//
//            // Act
//            ViewResult result = controller.Index() as ViewResult;
//
//            // Assert
//            Assert.IsNotNull(result);
//        }
//
//        [TestMethod]
//        public void About()
//        {
//            // Arrange
//            HomeController controller = new HomeController();
//
//            // Act
//            ViewResult result = controller.About() as ViewResult;
//
//            // Assert
//            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
//        }
//
//        [TestMethod]
//        public void Contact()
//        {
//        
//        }
    }
}
