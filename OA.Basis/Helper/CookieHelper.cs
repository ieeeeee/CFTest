using Newtonsoft.Json.Linq;
using OA.Basis.Extentions;
using System;
using System.Web;

namespace OA.Basis.Helper
{
    public  class CookieHelper
    {
        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        public static void ClearCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookiename"></param>
        /// <returns></returns>
        public static string GetCookieValue(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            string str = string.Empty;
            if(cookie!=null)
            {
                str = cookie.Value;
            }
            return str;
        }

        /// <summary>
        /// 添加一个Cookie(24小时过期)
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="cookieValue"></param>
        public static void SetCookie(string cookieName, string cookieValue)
        {
            SetCookie(cookieName, cookieValue, 1);
        }

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="cookieValue"></param>
        /// <param name="expires"></param>
        public static void SetCookie(string cookieName,string cookieValue,int time)
        {
            if(time>0)
            {
                HttpCookie myCookie = HttpContext.Current.Request.Cookies[cookieName];
                //HttpCookie myCookie = HttpContext.Response.Cookies[saveKey];
                if (myCookie==null||string.IsNullOrWhiteSpace(myCookie.Value))
                {
                    myCookie = new HttpCookie(cookieName);
                }
                JObject json = JObject.FromObject(cookieValue);
                string desCode = EnDecryption.DESEncrypt(json.ToString());
                myCookie.Value = HttpUtility.UrlEncode(desCode);
                myCookie.Expires = DateTime.Now.AddDays(time);
                HttpContext.Current.Response.Cookies.Set(myCookie);
            }  
        }

    }
}
