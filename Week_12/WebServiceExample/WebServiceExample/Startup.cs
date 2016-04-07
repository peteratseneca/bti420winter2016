using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebServiceExample.Startup))]
namespace WebServiceExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
