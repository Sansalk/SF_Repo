using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FlightFinder.Startup))]
namespace FlightFinder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
