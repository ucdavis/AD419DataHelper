using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AD_419_DataHelperWebApp.Startup))]
namespace AD_419_DataHelperWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
