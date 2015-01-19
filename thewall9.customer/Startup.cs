using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(thewall9.customer.Startup))]
namespace thewall9.customer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
