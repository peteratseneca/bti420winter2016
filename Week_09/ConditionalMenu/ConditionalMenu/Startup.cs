using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConditionalMenu.Startup))]
namespace ConditionalMenu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
