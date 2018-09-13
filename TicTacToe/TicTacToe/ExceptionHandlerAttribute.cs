
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        Log log = new Log();
        Repository addLog = new Repository();

        public override void OnException(ExceptionContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is Exception)
            {
                log.Request = actionExecutedContext.RouteData.Values["action"].ToString() + "/" + actionExecutedContext.RouteData.Values["action"].ToString();
                log.Exception = actionExecutedContext.Exception.ToString();
                var index = log.Exception.IndexOf("\r");
                log.Exception = log.Exception.Substring(0, index);
                log.Response = "Failure";
                log.Time = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                addLog.AddLog(log);
            }
        }
    }
}
