using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(August6thExercise.Startup))]
namespace August6thExercise
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
