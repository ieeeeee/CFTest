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

namespace OA.Web.Controllers
{
    public class EntInfoController : Controller
    {
        private readonly IEntService _entService;

        public EntInfoController(IEntService entService)
        {
            _entService = entService;
        }
        // GET: EntInfo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View(new EntDto());
        }

        public async Task<ActionResult> Edit(int entID)
        {
            var model = await _entService.FindAsync(entID);
            return View(model);
        }


        //获取企业信息
        public async Task<JsonResult> GetEntInfo(EntFilter entFilter)
        {
            var result = await _entService.SearchAsync(entFilter);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //添加企业信息
        [HttpPost]
        public async Task<ActionResult> Add(EntDto dto)
        {
            if(ModelState.IsValid)
            {
                var result = await _entService.AddAsync(dto);
                if(result.IsNotBlank())
                {
                    return RedirectToAction("Index");
                }
               
            }
            return View(dto);
        }
    }
}