using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PoSCloudApp.Startup))]
namespace PoSCloudApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
