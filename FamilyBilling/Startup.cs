using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FamilyBilling.Startup))]
namespace FamilyBilling
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
