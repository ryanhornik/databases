using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SiliconShores.Startup))]
namespace SiliconShores
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
