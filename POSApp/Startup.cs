using Microsoft.Owin;
using Owin;
using POSApp;

[assembly: OwinStartup(typeof(Startup))]
namespace POSApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
       
    }
}
