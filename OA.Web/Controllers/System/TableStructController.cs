using OA.Basis;
using OA.Basis.Extentions;
using OA.Interfaces;
using OA.Models;
using OA.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OA.Web.Controllers.System
{
    public class TableStructController : BaseMsgController
    {
        private readonly ITableStructService _tableStructService;
        public TableStructController(ITableStructService tableStructService)
        {
            _tableStructService = tableStructService;
        }
        #region View
        // GET: TableStruct
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Add(int id)
        {
            var result = await _tableStructService.FindAsync(id);
            return View(result);
        }
        #endregion
        #region GetInfo
        public async Task<JsonResult> GetTableStructInfo(TableStructFilter filters)
        {
            var result = await _tableStructService.SearchAsync(filters);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetDetailInfo(int id)
        {
            var result = await _tableStructService.FindAsync(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region OperateResult

        public async Task<JsonResult> Save(TableStructDto dto)
        {
            var result = string.Empty;
            var success = false;
            if (ModelState.IsValid)
            {
                if(dto.TStructID==0)
                {
                    result = await _tableStructService.AddAsync(dto);
                    if (result.IsNotBlank())
                    {
                        return OkOperate(result);
                    }
                    else
                        return FailOperate("添加失败");
                }
                else
                {
                    success = await  _tableStructService.UpdateAsync(dto);
                    return Json(success);
                }
            }
            return FailOperate("验证失败");

        }
        #endregion


    }
}