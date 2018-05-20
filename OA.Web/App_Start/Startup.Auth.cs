using Microsoft.AspNet.Identity;
using Owin;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Helpers;

namespace OA.Web
{
    public partial class Startup
    {
        // 有关配置身份验证的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name; //using System.Security.Claims;
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType=DefaultAuthenticationTypes.ApplicationCookie,//using Microsoft.AspNet.Identity;
                LoginPath =new Microsoft.Owin.PathString("/Login/Index")
            });
        }
    }
    //IIdentity扩展
    public static class IdentityExtention
    {
        // 获取登录的用户ID
        public static string GetLoginUserID(this IIdentity identity)
        {
            if(identity!=null)
            {
                var claim = (identity as ClaimsIdentity).FindFirst("LoginUserID");
                if (claim != null)
                    return claim.Value;
            }
            return string.Empty;
        }
    }
}