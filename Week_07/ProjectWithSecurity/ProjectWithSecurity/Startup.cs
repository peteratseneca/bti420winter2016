using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectWithSecurity.Startup))]
namespace ProjectWithSecurity
{
    // Attention - 01 - Initial loading and configuration of security
    // When the app loads, this class is instantiated, and runs the Configuration method

    // This is a "partial" class, meaning that its code is in two separate source code files
    // The other source code file is in App_Start/Startup.Auth.cs

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
