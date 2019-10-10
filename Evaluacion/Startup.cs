using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Evaluacion.Startup))]
namespace Evaluacion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
