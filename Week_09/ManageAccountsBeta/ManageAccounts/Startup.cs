using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManageAccounts.Startup))]
namespace ManageAccounts
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
