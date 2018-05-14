using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace QhTemplate.AdminWeb.Filter
{
    public class MyAuthenticationsAttribute : Attribute, IResultFilter
    {
        //public void OnActionExecuted(ActionExecutedContext context)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    if (!context.HttpContext.User.Claims.Any())
        //    {
        //        context.Result = new RedirectToActionResult("SignIn", "Account", null);
        //        return;
        //    }
        //}

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.HttpContext.User.Claims.Any())
            {
                context.Result = new RedirectToActionResult("SignIn", "Account", null);
                return;
            }
        }
    }
}
