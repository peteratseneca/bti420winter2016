using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AjaxItemSelect.Startup))]
namespace AjaxItemSelect
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
