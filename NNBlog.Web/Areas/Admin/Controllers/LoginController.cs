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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string username, string password)
        {
            username = CommonTools.GetSafeSQL(username);
            password = CommonTools.MD5Hash(password);
            Model.Admin a = dal.GetModel(username, password);
            if (a == null)
            {
                return Json(new { status = "n", info = "用户名或者密码错误！" });
            }
            HttpContext.Session.SetString("nnblog_admin", a.UserName);
            return Json(new { status = "y", info = "登录成功！" });
        }
    }
}