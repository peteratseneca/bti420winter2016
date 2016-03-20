using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhotoProperty.Startup))]
namespace PhotoProperty
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
