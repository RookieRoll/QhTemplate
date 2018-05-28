using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QhTemplate.FontWeb.Filer
{
    public class MyAuthenticationsAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.Claims.Any())
            {
                context.Result = new RedirectResult("http://localhost:54791/");
                return;
            }
        }
    }
}