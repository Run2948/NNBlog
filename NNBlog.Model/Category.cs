using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNBlog.Model
{
    /// <summary>
    /// 分类表
    /// </summary>
    public class Category
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
        /// 分类名称
        /// </summary>

        public string CateName { get; set; }
        /// <summary>
        /// 分类编号
        /// </summary>

        public string CateNo { get; set; }
        /// <summary>
        /// 父分类
        /// </summary>

        public string ParentCate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>

        public string Remark { get; set; }
    }
}
