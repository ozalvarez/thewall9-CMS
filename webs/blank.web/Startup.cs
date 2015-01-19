using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(thewall9.web.Startup))]
namespace thewall9.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
