using Microsoft.Owin;
using OA.Basis.Extentions;
using Owin;
using System.Configuration;

[assembly:OwinStartupAttribute(typeof(OA.Web.Startup))]
namespace OA.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           ConfigureAuth(app);
            var enableHangfire = ConfigurationManager.AppSettings["EnableHangfire"].IsEqual("true");
            if (enableHangfire)
            {
                //ConfigureHangfire(app);
            }
        }

    }
}