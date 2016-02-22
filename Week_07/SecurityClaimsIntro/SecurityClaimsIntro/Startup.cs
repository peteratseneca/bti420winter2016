using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SecurityClaimsIntro.Startup))]
namespace SecurityClaimsIntro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
