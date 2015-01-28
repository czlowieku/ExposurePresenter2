using ExposurePresenter.DataInput;
using ExposurePresenter.Web.Models;
using Microsoft.Ajax.Utilities;

namespace ExposurePresenter.Web.DataContexts.ExposureMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ExposurePresenter.Web.DataContexts.ExposureDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContexts\ExposureMigrations";

        }

        protected override void Seed(ExposurePresenter.Web.DataContexts.ExposureDb context)
        {

            var ctx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter) context).ObjectContext;
            ctx.ExecuteStoreCommand("TRUNCATE TABLE ExposureRecords");


            PopulateData(@"D:\dev\C#\ExposurePresenter\XLSInputFiles\PLK Grzegorz bran¿e.xlsx", context);
            PopulateData(@"D:\dev\C#\ExposurePresenter\XLSInputFiles\AllMonths.xlsx", context);


//            var reader = new ExcelFileReader();
//            var itmes = reader.ReadExposureRecords(
//                @"D:\dev\C#\ExposurePresenter\XLSInputFiles\PLK Grzegorz bran¿e.xlsx").ToArray();
//            foreach (var exposureRecord in itmes)
//            {
//                try
//                {
//                    context.ExposureRecords.Add(exposureRecord);
//
//                }
//                catch (Exception)
//                {
//                    Console.WriteLine(exposureRecord.Brand + " " + exposureRecord.Month);
//                    throw;
//                }
//            }

        }

        public void PopulateData(string filePath, ExposureDb db)
        {
            var fileReader = new ExcelFileReader();

            db.Branches.AddOrUpdate(branch => branch.Name, fileReader.ReadDistinctBrnaches(filePath).ToArray());
            db.Brands.AddOrUpdate(brand => brand.Name, fileReader.ReadDistinctBrands(filePath).ToArray());
            db.Disciplines.AddOrUpdate(discipline => discipline.Name,
                fileReader.ReadDistinctDisciplines(filePath).ToArray());

            var records = fileReader.ReadExposureRecords(filePath).ToArray();
            foreach (var exposureRecord in records)
            {
                db.ExposureRecords.AddOrUpdate(record => new { record.Brand, record.Month }, exposureRecord);
                db.SaveChanges();
            }
          

        }
    }
}
