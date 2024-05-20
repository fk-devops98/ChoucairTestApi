using ChoucairTest.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ChoucairTest.Api.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class AppExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<AppExceptionFilterAttribute> _Logger;

        public AppExceptionFilterAttribute(ILogger<AppExceptionFilterAttribute> logger)
        {
            _Logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            if (context != null)
            {
                context.HttpContext.Response.StatusCode = context.Exception switch
                {
                    EstadoNotRegisteredException => (int)HttpStatusCode.NotFound,
                    TareaNotFoundException => (int)HttpStatusCode.NotFound,
                    UserUnregisteredException => (int)HttpStatusCode.NotFound,
                    AppException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                _Logger.LogError(context.Exception, context.Exception.Message, new[] { context.Exception.StackTrace });

                if (context.Exception.InnerException != null)
                {
                    var msg = new
                    {
                        context.Exception.InnerException.Message
                    };
                    context.Result = new ObjectResult(msg);
                }
                else
                {
                    var msg = new
                    {
                        context.Exception.Message
                    };
                    context.Result = new ObjectResult(msg);
                }
            }
        }
    }
}