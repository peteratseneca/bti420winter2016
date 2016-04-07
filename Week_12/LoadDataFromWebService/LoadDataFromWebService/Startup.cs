using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoadDataFromWebService.Startup))]
namespace LoadDataFromWebService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
