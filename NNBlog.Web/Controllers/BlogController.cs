using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NNBlog.Utility;

namespace NNBlog.Web.Controllers
{
    public class BlogController : Controller
    {
        public DAL.BlogDAL blogdal;
        private readonly DAL.CategoryDAL catedal;

        public BlogController(DAL.CategoryDAL catedal, DAL.BlogDAL blogdal)
        {
            this.blogdal = blogdal;
            this.catedal = catedal;
        }

        public IActionResult Index(string cate, string month,string key)
        {
            ViewBag.blogdal = blogdal;
            ViewBag.CateNo = cate;
            ViewBag.Month = month;
            ViewBag.Key = key;
            ViewBag.blogmonth = blogdal.GetBlogMonth();
            ViewBag.calist = catedal.GetList("");
            return View();
        }

        public IActionResult List(string key, string month, string cate, int pagesize = 12, int pageindex = 1)
        {
            string cond = "1=1";
            if (!string.IsNullOrEmpty(key))
            {
                key = CommonTools.GetSafeSQL(key);
                cond += $" and Title like '%{key}%'";
            }
            if (!string.IsNullOrEmpty(month))
            {
                month = CommonTools.GetSafeSQL(month);
                DateTime d = DateTime.Parse(month + "-01");
                cond += $"and CreateDate >= '{d:yyyy-MM-dd}' and CreateDate < '{d.AddMonths(1):yyyy-MM-dd}'";
            }
            if (!string.IsNullOrEmpty(cate) && cate != "0")
            {
                cate = CommonTools.GetSafeSQL(cate);
                cond += $" and CateNo='{cate}'";
            }
            int total = blogdal.CalcCount(cond);
            int pagecount = total % pagesize == 0 ? total / pagesize : total / pagesize + 1;//总页数
            List<Model.Blog> list = blogdal.GetList("id desc", pagesize, pageindex, cond);
            string str = CommonTools.ToJsonString(new { totalrecord = total, pagecount = pagecount, list = list });
            return Content(str);
        }
        

        public IActionResult Show(int id)
        {
            Model.Blog blog = blogdal.GetModel(id);
//            if (blog == null)
//            {
//                return Content("找不到该博客！");
//            }
            ViewBag.blogdal = blogdal;
            ViewBag.calist = catedal.GetList("");
            ViewBag.blogmonth = blogdal.GetBlogMonth();
            return View(blog);
        }
    }
}
