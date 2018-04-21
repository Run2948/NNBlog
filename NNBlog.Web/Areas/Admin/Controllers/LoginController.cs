using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NNBlog.Utility;

namespace NNBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        // GET

        private DAL.AdminDAL dal;

        public LoginController(DAL.AdminDAL dal)
        {
            this.dal = dal;
        }

        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("blog_admin");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult doLogin(string username, string password)
        {
            username = CommonTools.GetSafeSQL(username);
            password = CommonTools.MD5Hash(password);
            Model.Admin model = dal.GetModel(username, password);
            if (model == null)
            {
                return Json(new { status = "n", info = "   用户名或者密码错误！   " });
            }
            HttpContext.Session.SetString("blog_admin", model.UserName);
            return Json(new { status = "y", info = "    登录成功！正在前往管理中心....    " });
        }
    }
}