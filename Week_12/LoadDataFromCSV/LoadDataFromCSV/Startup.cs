using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoadDataFromCSV.Startup))]
namespace LoadDataFromCSV
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
