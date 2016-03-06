using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewDataModelSecuredOnAzure.Startup))]
namespace NewDataModelSecuredOnAzure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
