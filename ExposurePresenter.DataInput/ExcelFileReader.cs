using System.Collections.Generic;
using System.Linq;
using ExposurePresenter.Domain;
using LinqToExcel;
using LinqToExcel.Query;

namespace ExposurePresenter.DataInput
{
    public class ExcelFileReader
    {
        private string _worksheetName;

        public ExcelFileReader()
        {
            _worksheetName = "Telewizja";
        }

        public IEnumerable<Brand> ReadDistinctBrands(string fileName)
        {
            var book = new LinqToExcel.ExcelQueryFactory(fileName);
            var a = book.Worksheet(_worksheetName);
            return Enumerable.TakeWhile(a, row => !string.IsNullOrEmpty(row["Marka"].Cast<string>())).Select(row => new Brand()
            {
                Name = row["Marka"].Cast<string>()
            }).GroupBy(brand => brand.Name).Select(brands => brands.First());
        }

        public IEnumerable<Discipline> ReadDistinctDisciplines(string fileName)
        {
            var book = new LinqToExcel.ExcelQueryFactory(fileName);
            ExcelQueryable<Row> a = book.Worksheet(_worksheetName);
            return Enumerable.TakeWhile(a, row => !string.IsNullOrEmpty(row["Dyscyplina"].Cast<string>())).Select(row => new Discipline()
            {
                Name = row["Dyscyplina"].Cast<string>()
            }).GroupBy(brand => brand.Name).Select(brands => brands.First());
        }

        public IEnumerable<Branch> ReadDistinctBrnaches(string fileName)
        {
            var book = new LinqToExcel.ExcelQueryFactory(fileName);
            ExcelQueryable<Row> a = book.Worksheet(_worksheetName);
            return Enumerable.TakeWhile(a, row => !string.IsNullOrEmpty(row["Branża"].Cast<string>())).Select(row => new Branch()
            {
                Name = row["Branża"].Cast<string>()
            }).GroupBy(brand => brand.Name).Select(brands => brands.First());
        }

        public IEnumerable<ExposureRecord> ReadExposureRecords(string fileName)
        {
            var book = new LinqToExcel.ExcelQueryFactory(fileName);
            var a = book.Worksheet(_worksheetName);
            foreach (var row in a)
            {
                if(string.IsNullOrEmpty(row["Dyscyplina"].Cast<string>()))
                    break;
                var item = new ExposureRecord()
                {
                    Branch = row["Branża"].Cast<string>(),
                    Brand = row["Marka"].Cast<string>(),
                    Discipline = row["Dyscyplina"].Cast<string>(),
                    Month = row["Miesiąc"].Cast<string>(),
                    Quantity = row["Liczba ekspozycji"].Cast<long>(),
                    Value = row["Ekwiwalent reklamowy"].Cast<decimal>()
                };
                yield return item;
            }
        }
    }
}

