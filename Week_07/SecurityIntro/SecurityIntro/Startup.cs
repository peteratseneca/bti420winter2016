using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SecurityIntro.Startup))]
namespace SecurityIntro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
