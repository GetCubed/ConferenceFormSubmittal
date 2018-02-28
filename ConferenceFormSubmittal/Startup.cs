using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConferenceFormSubmittal.Startup))]
namespace ConferenceFormSubmittal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
