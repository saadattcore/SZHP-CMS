using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SZHPCMS.Startup))]
namespace SZHPCMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
