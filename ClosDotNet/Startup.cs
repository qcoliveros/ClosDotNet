using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClosDotNet.Startup))]
namespace ClosDotNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
