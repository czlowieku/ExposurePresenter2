using System.Data.Entity;
using System.Linq;
using ExposurePresenter.Domain;
using ExposurePresenter.Web.Models;

namespace ExposurePresenter.Web.DataContexts
{
    public interface IExposureDataSource
    {
        IQueryable<ExposureRecord> ExposureRecords { get;  }
        IQueryable<Branch> Branches { get; }
        IQueryable<Discipline> Disciplines { get; }
        IQueryable<Brand> Brands { get; }

    }

    public class ExposureDb : DbContext, IExposureDataSource
    {
        public ExposureDb()
            : base("DefaultConnection")
        {
            
        }
      
        public DbSet<ExposureRecord> ExposureRecords { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Brand> Brands { get; set; }

        IQueryable<ExposureRecord> IExposureDataSource.ExposureRecords
        {
            get { return ExposureRecords; }
        }

        IQueryable<Branch> IExposureDataSource.Branches
        {
            get { return Branches; }
        }

        IQueryable<Discipline> IExposureDataSource.Disciplines
        {
            get { return Disciplines; }
        }

        IQueryable<Brand> IExposureDataSource.Brands
        {
            get { return Brands; }
        }
    }
}