using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StorageUnit.Startup))]
namespace StorageUnit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
