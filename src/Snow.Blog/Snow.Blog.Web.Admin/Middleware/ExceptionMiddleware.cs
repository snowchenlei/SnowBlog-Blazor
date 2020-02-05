using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Snow.Blog.Common.Exceptions;
using Snow.Blog.Web.Admin.Middleware.Extensions;
using Snow.Blog.Web.Admin.Middleware.Wrappers;

namespace Snow.Blog.Web.Admin.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UserFriendlyException ue)
            {
                await HandleExceptionAsync(context, ue);

            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            //_logger.LogError("Exception:", exception);

            ApiError apiError = null;
            ApiResponse apiResponse = null;
            int code = 0;

            if(exception is UserFriendlyException)
            {
                var ex = exception as UserFriendlyException;
                apiError = new ApiError(ex.Message);
                httpContext.Response.StatusCode =  code = (int)HttpStatusCode.BadRequest;
            }
            else if (exception is UnauthorizedAccessException)
            {
                apiError = new ApiError("Unauthorized Access");
                httpContext.Response.StatusCode = code = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
#if !DEBUG
                var msg = "An unhandled error occurred.";
                string stack = null;
#else
                var msg = exception.GetBaseException().Message;
                string stack = exception.StackTrace;
#endif

                apiError = new ApiError(msg)
                {
                    Details = stack
                };
                code = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.StatusCode = code;
            }

#if DEBUG
            // 这里先设置响应均为200，避免HttpRequestException
            httpContext.Response.StatusCode = code = 200;
#endif

            httpContext.Response.ContentType = "application/json";

            apiResponse = new ApiResponse(code, ResponseMessageEnum.Exception.GetDescription(), null, apiError);

            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse));
        }
    }
}
