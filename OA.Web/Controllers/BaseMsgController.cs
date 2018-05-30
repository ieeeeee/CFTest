using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.Web.Controllers
{
    public class BaseMsgController : Controller
    {
        //操作成功
        public JsonResult OkOperate(object data=null)
        {
            return Json(new { flag = true, data,msg="操作成功" }, JsonRequestBehavior.AllowGet);
        }
        //操作成功 原样返回data的值
        public JsonResult JsonOKOperate(object data)
        {
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        //操作失败
        public JsonResult FailOperate(string msg="操作失败")
        {
            return Json(new { flag = false, msg }, JsonRequestBehavior.AllowGet);

        }
        //是否操作成功
        public JsonResult Json(bool success)
        {
            var msg = success ? "操作成功" : "操作失败";
            return Json(new { flag = success, msg }, JsonRequestBehavior.AllowGet);
        }
    }
}