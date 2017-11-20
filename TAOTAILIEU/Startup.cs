using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TAOTAILIEU.Startup))]
namespace TAOTAILIEU
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
