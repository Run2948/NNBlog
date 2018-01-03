using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NNBlog.Web.Controllers
{
    public class TestController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var str = "";
            var cateDal = new DAL.CategoryDAL();

            str = "新增的分类返回的值："+ cateDal.Insert(new Model.Category() { CateName = "newcatename"}).ToString() + "\r\n";

            str = "删除Id==7返回的值：" + cateDal.Delete(7) + "\r\n";

            Model.Category cate = cateDal.GetModel(8);
            if (cate != null)
            {
                cate.CateName = "新修改的名称" + DateTime.Now;
                str +="修改Id==8返回的值：" + cateDal.Update(cate) + "\r\n";
            }

            List<Model.Category> list = cateDal.GetList("");
            foreach (var item in list)
            {
                str += $"分类ID：{item.Id},分类名称：{item.CateName}\r\n";
            }
            return Content(str);
        }
    }
}
