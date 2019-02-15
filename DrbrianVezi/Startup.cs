using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DrbrianVezi.Startup))]
namespace DrbrianVezi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
