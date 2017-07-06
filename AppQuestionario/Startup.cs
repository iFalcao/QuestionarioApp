using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppQuestionario.Startup))]
namespace AppQuestionario
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
