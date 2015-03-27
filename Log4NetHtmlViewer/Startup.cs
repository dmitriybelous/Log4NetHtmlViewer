using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Log4NetHtmlViewer.Startup))]
namespace Log4NetHtmlViewer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
