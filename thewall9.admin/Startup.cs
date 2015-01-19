using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(thewall9.admin.Startup))]
namespace thewall9.admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
