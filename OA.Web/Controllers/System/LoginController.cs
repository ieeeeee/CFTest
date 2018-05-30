using OA.Interfaces;
using OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using OA.Basis.Extentions;
using SimpleInjector.Lifestyles;

namespace OA.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService; // OA.Interfaces;

        public LoginController(IUserService userSvr) //会报没有为该对象定义无参数的构造函数时，引用SimpleInjector 并在App_start中配置，global.aspx中也要进行配置
        {
            _userService = userSvr;
        }
        // GET: Login
        public ActionResult Index()
        {
            //根据cookie
            var model = new LoginDto
            {
                ReturnUrl = Request.QueryString["ReturnUrl"]

            };
            if(User.Identity.IsAuthenticated)
            {
                if (model.ReturnUrl.IsNotBlank())
                    return Redirect(model.ReturnUrl);
                return RedirectToAction("../Home/Index");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public  async Task<ActionResult> Login(LoginDto model)
        { 
            //获取登录模型，基于声明的认证，添加错误信息，返回结果
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            model.LoginIP = HttpContext.Request.UserHostAddress;
            var loginDto = await _userService.Login(model);
            if (loginDto.LoginSuccess)
            {
                //基于声明的认证
                //获取当前请求上可用的身份验证中间件功能。 需要在App_Start中进行配置
                var authenticationManager = HttpContext.GetOwinContext().Authentication; //安装NuGet包 Microsoft.Owin.Host.SystemWeb 

                var claims = new List<Claim>
                {
                    new Claim("LoginUserID",loginDto.User.UserID),
                    new Claim(ClaimTypes.Name,model.LoginName)
                };
                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);//安装NuGet包  Microsoft.AspNet.Identity;
                var pro = new AuthenticationProperties() //using Microsoft.Owin.Security;
                {
                    IsPersistent = true
                };
                //生成cookie
                authenticationManager.SignIn(pro, identity);
                if (model.ReturnUrl.IsBlank())
                    return RedirectToAction("../Home/Index");
                return Redirect(model.ReturnUrl);
            }
            ModelState.AddModelError(loginDto.Result == OA.Models.Enum.LoginResult.AccountNotExists ? "LoginName" : "Password", loginDto.Message);
            return View(model);
        }

    }
}