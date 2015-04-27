using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoskolaWeb.Startup))]
namespace AutoskolaWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
