using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExposurePresenter.Web.Startup))]
namespace ExposurePresenter.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
