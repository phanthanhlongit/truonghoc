using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KidsSchool.Startup))]
namespace KidsSchool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
