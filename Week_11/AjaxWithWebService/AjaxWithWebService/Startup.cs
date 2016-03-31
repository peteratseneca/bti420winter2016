using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AjaxWithWebService.Startup))]
namespace AjaxWithWebService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
