using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MongnDB_WEB.Startup))]
namespace MongnDB_WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
