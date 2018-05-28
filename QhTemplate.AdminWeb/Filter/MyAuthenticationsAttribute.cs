using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace QhTemplate.AdminWeb.Filter
{
    public class MyAuthenticationsAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var user= context.HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid));
            if (user==null&&!context.HttpContext.Request.Path.StartsWithSegments("/Articles/DoUeditor"))
            {
                context.Result =new RedirectResult("http://localhost:54791/");
                return;
            }
        }

        //public void OnResultExecuted(ResultExecutedContext context)
        //{
        //}

        //public void OnResultExecuting(ResultExecutingContext context)
        //{
        //    if (!context.HttpContext.User.Identities.Any())
        //    {
        //        context.Result = new RedirectToActionResult("SignIn", "Account", null);
        //        return;
        //    }
        //}
    }
}
