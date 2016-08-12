using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(labweb.Startup))]
namespace labweb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
