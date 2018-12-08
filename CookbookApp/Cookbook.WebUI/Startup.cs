using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cookbook.WebUI.Startup))]
namespace Cookbook.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
