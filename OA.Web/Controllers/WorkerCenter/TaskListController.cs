using OA.Basis.Extentions;
using OA.Interfaces;
using OA.Models;
using OA.Models.Filters;
using OA.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OA.Web.Controllers.WorkerCenter
{
    public class TaskListController : BaseMsgController
    {
        private readonly ITaskService _taskService;
        public TaskListController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        #region View
        // GET: DeptInfo
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Add(string id)
        {
            var model = await _taskService.FindAsync(id);
            if (model == null)
                return View(new TaskDto());
            return View(model);
        }
        #endregion

        #region GetInfo

        public async Task<JsonResult> GetAllTaskInfo(TaskFilter filter)
        {
            var result = await _taskService.SearchAsync(filter);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetDetailInfo(string id)
        {
            var result = await _taskService.FindAsync(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region OperateResult

        public async Task<JsonResult> Save(TaskDto dto)
        {
            var result = string.Empty;
            bool success = false;
            if(ModelState.IsValid)
            {
                if(dto.TaskID.IsBlank())
                {
                    result = await _taskService.AddAsync(dto);
                    if (result.IsNotBlank())
                        return OkOperate(result);
                    return FailOperate("添加失败");

                }
                else
                {
                    success = await _taskService.UpdateAsync(dto);
                    return Json(success);
                }
            }
            else
            {
                string error = string.Empty;
                foreach(var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if(state.Errors.Any())
                    {
                        error = state.Errors.First().ErrorMessage;
                        if(String.IsNullOrEmpty(error))
                        {
                            error = "请求参数缺失或错误";
                        }
                        break;
                    }
                }
                return FailOperate("验证失败：" + error);
            }
        }

        public async Task<JsonResult> Del(IList<string> ids)
        {
            var result = new JsonResultModel<bool>();
            if(ids.AnyOne())
            {
                result.flag = await _taskService.DeleteAsync(ids);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}