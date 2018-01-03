using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNBlog.Model
{
    /// <summary>
    /// 用于layui的tree控件显示 
    /// </summary>
    public class TreeJson_LayUI
    {
        public string name { set; get; }
        public bool spread { set; get; }
        public string href { set; get; }
        public List<TreeJson_LayUI> children { set; get; }
        public int caid { set; get; }
    }
}
