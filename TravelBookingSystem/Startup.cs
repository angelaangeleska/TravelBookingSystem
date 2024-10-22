using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TravelBookingSystem.Startup))]
namespace TravelBookingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
