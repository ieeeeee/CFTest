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
        // GET: EntInfo
        public ActionResult Index()
        {
            return View();
        }

        //获取企业信息
        public Task<JsonResult> GetEntInfo(int pageSize,int pageIndex)
        {
            var result=await _entService
        }
    }
}