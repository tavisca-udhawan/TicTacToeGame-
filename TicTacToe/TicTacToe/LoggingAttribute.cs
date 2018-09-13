using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class LoggingAttribute : ResultFilterAttribute, IActionFilter
    {
        Log log = new Log();
        Repository addLog = new Repository();
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                log.Request = context.RouteData.Values["action"].ToString() + "/" + context.RouteData.Values["controller"].ToString();
                log.Response = "Success";
                log.Exception = "NULL";
                bool result = addLog.AddLog(log);

            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            log.Request = context.RouteData.Values["action"].ToString() + "/" + context.RouteData.Values["controller"].ToString();
            log.Response = "NULL";
            log.Exception = "NULL";
            log.Time = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            bool result = addLog.AddLog(log);

        }
    }
}
