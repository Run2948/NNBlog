using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNBlog.Model
{
    public class Blog
    {
        /// <summary>
        /// 自增标识
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>

        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// MarkDown
        /// </summary>
        public string Body_md { get; set; }
        /// <summary>
        /// 访问量
        /// </summary>
        public int VisitNo { get; set; }
 
        /// <summary>
        /// 分类名称
        /// </summary>

        public string CateName { get; set; }
        /// <summary>
        /// 分类编号
        /// </summary>

        public string CateNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>

        public string Remark { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; }
    }
}
