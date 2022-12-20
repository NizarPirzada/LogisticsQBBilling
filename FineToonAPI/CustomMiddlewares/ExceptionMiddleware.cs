using FTCommon.Utils;
using FTDTO.ApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FineToonAPI.CustomMiddlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
               
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            using var serviceScope = ServiceActivator.GetScope();
            var Logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<ExceptionMiddleware>>();
            Logger.LogError(exception, exception.Message);
            int statusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ApiResponseDto
            {
                ResponseData = null,
                Status = (int)HttpStatusCode.InternalServerError,
                Error = "Something went wrong. Please contact your system administrator." // exception.Message
            }));
        }
    }
}
