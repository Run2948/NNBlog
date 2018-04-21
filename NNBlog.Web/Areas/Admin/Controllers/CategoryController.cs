using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NNBlog.Web.Filter;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NNBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminLoginFilter]
    public class CategoryController : Controller
    {
        private DAL.CategoryDAL cadal;
        private DAL.BlogDAL blogdal;
        public CategoryController(DAL.CategoryDAL cadal, DAL.BlogDAL blogdal)
        {
            this.cadal = cadal;
            this.blogdal = blogdal;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Model.Category> list = cadal.GetList("ParentCate='0'");
            List<Model.TreeJson_LayUI> list_tree = new List<Model.TreeJson_LayUI>();
            foreach (var item in list)
            {
                List<Model.TreeJson_LayUI> list_sub = new List<Model.TreeJson_LayUI>();
                List<Model.Category> list_casub = cadal.GetList($"ParentCate='{item.CateNo}'");
                foreach (var item_sub in list_casub)
                {
                    list_sub.Add(new Model.TreeJson_LayUI() { name = item_sub.CateName, caid = item_sub.Id });
                }
                list_tree.Add(new Model.TreeJson_LayUI() { name = item.CateName, href = "", spread = true, children = list_sub, caid = item.Id });
            }
            ViewBag.treejson = Newtonsoft.Json.JsonConvert.SerializeObject(list_tree);
            return View();
        }

        /// <summary>
        /// 新增编辑 分类
        /// </summary>
        /// <param name="ca"></param>
        /// <returns></returns>
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Add(Model.Category ca)
        {
            if (ca.Id == 0)
            {
                ca.CreateDate = DateTime.Now;
                ca.ParentCate = "01";
                ca.CateNo = cadal.GetCateNo("01", 2);
                cadal.Insert(ca);
                return Json(new { status = "y", info = "新增分类成功！" });
            }
            else
            {
                //只编辑分类名称
                Model.Category ca_indb = cadal.GetModel(ca.Id);
                ca_indb.CateName = ca.CateName;
                cadal.Update(ca_indb);
                return Json(new { status = "y", info = "编辑分类成功！" });
            }

        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(int id)
        {
            Model.Category ca = cadal.GetModel(id);
            if (ca == null)
            {
                return Json(new { status = "n", info = $"删除失败，没有对应分类！" });
            }
            if (blogdal.CalcCount($"cabh='{ca.CateNo}'") > 0)
            {
                return Json(new { status = "n", info = $"删除失败，该分类下还有博客！" });
            }
            int res = cadal.Delete(id);
            return Json(new { status = "y", info = $"成功删除{res}个分类！" });
        }
    }
}
