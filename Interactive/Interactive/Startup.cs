using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Interactive.Startup))]
namespace Interactive
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
