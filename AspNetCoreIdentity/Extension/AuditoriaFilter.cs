﻿using KissLog;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;


namespace AspNetCoreIdentity.Extension
{
    public class AuditoriaFilter : IActionFilter
    {
        private readonly KissLog.IKLogger _logger;

        public AuditoriaFilter(KissLog.IKLogger logger)
        {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var message = context.HttpContext.User.Identity.Name + " Acessou " +
                    context.HttpContext.Request.GetDisplayUrl();

                _logger.Log(LogLevel.Information, message);
            }
        }


    }
}
