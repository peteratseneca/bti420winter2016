using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NotesApp.Startup))]
namespace NotesApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
