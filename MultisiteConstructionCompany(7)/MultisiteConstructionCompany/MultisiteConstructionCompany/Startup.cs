using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MultisiteConstructionCompany.Startup))]
namespace MultisiteConstructionCompany
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
