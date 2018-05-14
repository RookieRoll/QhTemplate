using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QhTemplate.FontWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QhTemplate.FontWeb.Filer
{
    public class MyExceptionFilter : IExceptionFilter, IFilterMetadata
    {
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                string msg = context.Exception.Message;

                if (context.HttpContext.Request.IsAjaxRequest())
                {
                    context.Result = new JsonResult(msg);
                }
                else
                {
                    context.Result = new RedirectToActionResult("Error", "Home", new ErrorViewModel() { });

                }
            }

            context.ExceptionHandled = true; //异常已处理了
        }


    }
}