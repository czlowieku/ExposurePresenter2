using ExposurePresenter.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ExposurePresenter.Web.DataContexts
{
    public class IdentityDb : IdentityDbContext<ApplicationUser>
    {
        public IdentityDb()
            : base("DefaultConnection")
        {
        }

        public static IdentityDb Create()
        {
            return new IdentityDb();
        }
    }
}