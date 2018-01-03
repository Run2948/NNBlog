using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NNBlog.Utility;
using System.IO;

namespace NNBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private IHostingEnvironment hostingEnv;
        private DAL.BlogDAL blogdal;
        private DAL.CategoryDAL catedal;

        public BlogController(DAL.BlogDAL blogdal, DAL.CategoryDAL catedal, IHostingEnvironment hostingEnv)
        {
            this.blogdal = blogdal;
            this.catedal = catedal;
            this.hostingEnv = hostingEnv;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.calist = catedal.GetList("ParentCate='01' order by CateName");
            return View();
        }



        /// <summary>
        /// 分页取数据，返回JSON
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(string key, string startdate, string enddate, string cabh, int pagesize = 12, int pageindex = 1)
        {
            string cond = "1=1";
            if (!string.IsNullOrEmpty(key))
            {
                key = CommonTools.GetSafeSQL(key);
                cond += $" and Title like '%{key}%'";
            }
            if (!string.IsNullOrEmpty(startdate))
            {
                startdate = CommonTools.GetSafeSQL(startdate);
                cond += $" and replace(CONVERT(char(10),CreateDate,111),'/','-')>='{startdate}'";
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                enddate = CommonTools.GetSafeSQL(enddate);
                cond += $" and replace(CONVERT(char(10),CreateDate,111),'/','-')<='{enddate}'";
            }
            if (!string.IsNullOrEmpty(cabh) && cabh != "0")
            {
                cabh = CommonTools.GetSafeSQL(cabh);
                cond += $" and CateNo='{cabh}'";
            }
            int total = blogdal.CalcCount(cond);
            int pagecount = total % pagesize == 0 ? total / pagesize : total / pagesize + 1;//总页数
            List<Model.Blog> list = blogdal.GetList("id desc", pagesize, pageindex, cond);
            string str = CommonTools.ToJsonString(new { totalrecord = total, pagecount = pagecount, list = list });
            return Content(str);
        }

        /// <summary>
        /// 新增编辑博客页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Add(int? id)
        {
            ViewBag.calist = catedal.GetList("ParentCate='01' ORDER BY CateName");
            Model.Blog b = new Model.Blog() { Id = 0, };
            if (id != null)
            {
                b = blogdal.GetModel(id.Value);
            }
            return View(b);
        }


        /// <summary>
        /// layui在线编辑器里的上传图片功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadImage()
        {
            #region 文件上传
            var imgFile = Request.Form.Files[0];
            if (imgFile != null && !string.IsNullOrEmpty(imgFile.FileName))
            {
                var filename = ContentDispositionHeaderValue
                    .Parse(imgFile.ContentDisposition)
                    .FileName
                    .Trim('"');
                var extname = filename.Substring(filename.LastIndexOf(".", StringComparison.Ordinal), filename.Length - filename.LastIndexOf(".", StringComparison.Ordinal));
                var filename1 = System.Guid.NewGuid().ToString().Substring(0, 6) + extname;
                var path = hostingEnv.WebRootPath;
                string dir = DateTime.Now.ToString("yyyyMMdd");
                if (!System.IO.Directory.Exists(hostingEnv.WebRootPath + $@"\upload\{dir}"))
                {
                    System.IO.Directory.CreateDirectory(hostingEnv.WebRootPath + $@"\upload\{dir}");
                }
                filename = hostingEnv.WebRootPath + $@"\upload\{dir}\{filename1}";
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    imgFile.CopyTo(fs);
                    fs.Flush();
                }

                return Json(new { code = 0, msg = "上传成功", data = new { src = $"/upload/{dir}/{filename1}", title = "图片标题" } });

            }
            return Json(new { code = 1, msg = "上传失败", });
            #endregion
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Add(Model.Blog blog)
        {
            blog.CreateDate = DateTime.Now;
            if (blog.CateNo == "0")
            {
                //新分类，先增加 
                string cateNo = catedal.GetCateNo("01", 2);
                catedal.Insert(new Model.Category() { CateName = blog.CateName, CateNo = cateNo, CreateDate = DateTime.Now, ParentCate = "01", Remark = "" });
                blog.CateNo = cateNo;
            }
            if (blog.Id == 0)
            {
                blogdal.Insert(blog);
                return Json(new { status = "y", info = "博客新增成功！" });
            }
            else
            {
                blogdal.Update(blog);
                return Json(new { status = "y", info = "博客编辑成功！" });
            }
        }

        /// <summary>
        /// 删除博客 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(string ids)
        {
            string[] ss = ids.Split(',');
            int count = 0;
            foreach (var item in ss)
            {
                int x;
                if (int.TryParse(item, out x))
                {
                    blogdal.Delete(x);
                    count++;
                }
            }

            return Json(new { status = "y", info = $"成功删除{count}条博客！" });
        }
    }
}