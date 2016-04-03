using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoadDataFromXLSX.Startup))]
namespace LoadDataFromXLSX
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
