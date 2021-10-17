using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OlwandleHotel.Startup))]
namespace OlwandleHotel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
