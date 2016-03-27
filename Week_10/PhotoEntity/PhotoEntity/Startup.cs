using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhotoEntity.Startup))]
namespace PhotoEntity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
