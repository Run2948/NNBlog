using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNBlog.Model
{
    public class Admin
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
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 备注
        /// </summary>

        public string Remark { get; set; }
    }
}
