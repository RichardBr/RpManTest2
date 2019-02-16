using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RpMan.Application.Exceptions;

namespace RpMan.WebApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

            var calledException = context.Exception.GetType();

            if (context.Exception is ValidationException)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                // var resultObject = ((ValidationException)context.Exception).Failures;
                var resultObject = new
                {
                    Category = calledException.Name,
                    context.Exception.Message,
                    ((ValidationException)context.Exception).Failures,
                };
                context.Result = new JsonResult(resultObject);

                return;
            }

            if (context.Exception is UserLoginException)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                var resultObject = new
                {
                    Category = calledException.Name,
                    context.Exception.Message,
                    ((GenericException)context.Exception).Failures,
                };
                context.Result = new JsonResult(resultObject);

                return;
            }

            //if (context.Exception is IdentityErrorException)
            //{
            //    context.HttpContext.Response.ContentType = "application/json";
            //    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            //    var resultObject = new
            //    {
            //        Category = calledException.Name,
            //        context.Exception.Message,
            //        ((IdentityErrorException)context.Exception).Failures,
            //    };

            //    context.Result = new JsonResult(resultObject);
            //    return;
            //}

            if (calledException.IsSubclassOf(typeof(GenericException)))
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var resultObject = new
                {
                    Category = calledException.Name,
                    context.Exception.Message,
                    ((GenericException)context.Exception).Failures,
                };

                context.Result = new JsonResult(resultObject);
                return;
            }

            var code = HttpStatusCode.InternalServerError;

            if (context.Exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message },
                stackTrace = context.Exception.StackTrace
            });
        }
    }
}
