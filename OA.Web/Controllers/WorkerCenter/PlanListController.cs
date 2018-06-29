using OA.Basis.Extentions;
using OA.Interfaces;
using OA.Models;
using OA.Models.Filters;
using OA.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OA.Web.Controllers.WorkerCenter
{
    public class PlanListController : BaseMsgController
    {
        private readonly IPlanService _planService;
        public PlanListController(IPlanService planService)
        {
            _planService = planService;
        }
        #region View
        // GET: DeptInfo
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Add(string id)
        {
            var model = await _planService.FindAsync(id);
            if (model == null)
                return View(new PlanDto());
            return View(model);
        }
        #endregion

        #region GetInfo

        public async Task<JsonResult> GetAllPlanInfo(PlanFilter filter)
        {
            if(filter.CurrStatus!=-1)
            {
                var userID = User.Identity.GetLoginUserID();
                filter.Operator = userID;
            }
            var result = await _planService.SearchAsync(filter);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetDetailInfo(string id)
        {
            var result = await _planService.FindAsync(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region OperateResult
        [ValidateInput(false)]
        public async Task<JsonResult> Save(PlanDto dto)
        {
            var result = string.Empty;
            var success = false;
            var userID = User.Identity.GetLoginUserID();
            dto.Operator = userID;
            //dto.Remark= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (ModelState.IsValid)
            {
                if(string.IsNullOrEmpty(dto.PlanID)||dto.PlanID=="")
                {
                    result =await  _planService.AddASync(dto);
                    if (result.IsNotBlank())
                        return OkOperate(result);
                    return FailOperate("添加失败");
                }
                else
                {
                    success = await _planService.UpdateAsync(dto);
                    return Json(success);
                }
            }
            else
            {
                return FailOperate("验证失败");
            }
        }

        public async Task<JsonResult> Del(IList<string> ids)
        {
            var success = new JsonResultModel<bool>();
            if(ids.AnyOne())
            {
                success.flag = await _planService.DeleteAsync(ids);
            }
            return Json(success, JsonRequestBehavior.AllowGet);
        }

        //public  JsonResult Upload()
        //{
        //    var files = HttpContext.Request.Files;
        //    HttpPostedFile file = files[0];
        //    var success = new JsonResultModel<string>();
        //    string _filePath = HttpContext.Server.MapPath(@"upload");
        //    string resFileName = string.Empty;
        //    string result = string.Empty;
        //    if (Directory.Exists(_filePath+"\\"+DateTime.Now.ToString("yyyyMMdd")))
        //    {
        //        Directory.CreateDirectory(_filePath + "\\" + DateTime.Now.ToString("yyyyMMdd"));
        //    }
        //    if(file.ContentLength>0)
        //    {
        //        string fileName = FileExtension.GetFileName(file);
        //        /* 保存图片 */
        //        file.SaveAs(_filePath + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + fileName);
        //        resFileName = "/upload/" + DateTime.Now.ToString("yyyyMMdd") + "\\" + fileName;
        //        string imgUrl = _filePath + "/" + DateTime.Now.ToString("yyyyMMdd") + "/" + fileName;
        //        /* 地址存入数据库 */
        //        result= "{\"error\":0,\"data\":[" + imgUrl + "]}";
               
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        #endregion
    }
}