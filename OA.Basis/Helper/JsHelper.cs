using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace OA.Basis.Helper
{
    /// <summary>
    /// 客户端脚本输出
    /// </summary>
    public class JsHelper
    {
        /// <summary>
        /// 弹出信息，并跳转到指定页面
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="toURL"></param>
        public static void AlertAndRedirect(string Message,string toURL)
        {
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, Message, toURL));
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 弹出信息，并返回历史页面
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="toURL"></param>
        public static void AlertAndGoHistory(string Message, int value)
        {
            string js = "<script language=javascript>alert('{0}');history.go('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, Message, value));
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 弹出信息，并指定到父窗口
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="toURL"></param>
        public static void AlertAndParentUrl(string Message, string toURL)
        {
            string js = "<script language=javascript>alert('{0}');window.top.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, Message, toURL));
        }
        /// <summary>
        /// 直接跳转到指定页面
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="toURL"></param>
        public static void Redirect(string Message, string toURL)
        {
            string js = "<script language=javascript>window.location.replace('{0}')</script>";
            HttpContext.Current.Response.Write(string.Format(js,toURL));
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 返回到父窗口
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="toURL"></param>
        public static void ParentRedirect(string toURL)
        {
            string js = "<script language=javascript>window.top.location.replace('{0}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, toURL));
        }
        /// <summary>
        /// 返回历史页面
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="toURL"></param>
        public static void BackHistory(string Message, int value)
        {
            string js = "<script language=javascript>history.go('{0}')</script>";
            HttpContext.Current.Response.Write(string.Format(js,  value));
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 弹出信息
        /// </summary>
        public static void Alert(string message)
        {
            string js = "<script language=javascript>alert('{0}');</script>";
            HttpContext.Current.Response.Write(string.Format(js, message));
        }
        public static void RegisterScriptBlock(System.Web.UI.Page page,string _ScriptString)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "scriptblock", "<script type='text/javascript'>" + _ScriptString + "</script>");
        }
    }
}