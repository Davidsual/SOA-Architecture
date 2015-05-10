using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartupAttribute(typeof(DavideTrotta.Wcf.ClientWeb.Startup))]
namespace DavideTrotta.Wcf.ClientWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            ConfigureAuth(app);
            //app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
