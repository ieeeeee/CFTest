using OA.Basis;
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
        #endregion
        #region GetInfo
        public async Task<JsonResult> GetTableStructInfo(TableStructFilter filters)
        {
            var result = await _tableStructService.SearchAsync(filters);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region OperateResult
        #endregion
        

    }
}