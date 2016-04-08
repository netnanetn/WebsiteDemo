using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebSiteBanHangNoiThat.Startup))]
namespace WebSiteBanHangNoiThat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
