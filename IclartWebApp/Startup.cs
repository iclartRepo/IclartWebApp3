using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IclartWebApp.Startup))]
namespace IclartWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
