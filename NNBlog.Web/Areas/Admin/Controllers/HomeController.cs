using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NNBlog.Web.Filter;

namespace NNBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminLoginFilter]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Left()
        {
            return View();
        }

        public IActionResult Top()
        {
            ViewBag.UserName = HttpContext.Session.GetString("blog_admin");
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.SetString("blog_admin", "");
            return Redirect("/Admin/Login/Index");
        }
    }
}
