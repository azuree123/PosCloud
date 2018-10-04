using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using PoSCloudApp.Core;
using PoSCloudApp.Persistence;

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
