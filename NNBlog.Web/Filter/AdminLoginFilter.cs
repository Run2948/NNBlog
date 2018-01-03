using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NNBlog.Web.Filter
{
    public class AdminLoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //base.OnActionExecuting(context);
            //HttpContext.Current.Response.Write("OnActionExecuting:正要准备执行Action的时候但还未执行时执行<br />");
            string str = context.HttpContext.Session.GetString("nnblog_admin");
            if (string.IsNullOrEmpty(str))
            {
                context.Result = new ContentResult { Content = "parent.location.href='/Admin/Login'" };
            }
        }


        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            //HttpContext.Current.Response.Write("OnActionExecuted:Action执行时但还未返回结果时执行<br />");  
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
            // HttpContext.Current.Response.Write("OnResultExecuting:OnResultExecuting也和OnActionExecuted一样，但前者是在后者执行完后才执行<br />");  
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
            // HttpContext.Current.Response.Write("OnResultExecuted:是Action执行完后将要返回ActionResult的时候执行<br />");  
        }
    }
}
