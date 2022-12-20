using FTBusiness.Logger;
using FTCommon.Utils;
using FTDTO.ApiResponse;
using FTEnum.ResponseMessageEnum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace FineToonAPI.Controllers
{
    [AllowAnonymous]
    public class ErrorController : BaseController
    {
        private readonly ILoggerService _loggerService;

        public ErrorController(IHttpContextAccessor httpContextAccessor, ILoggerService loggerService) : base(httpContextAccessor)
        {
            _loggerService = loggerService;
        }

        [HttpGet]
        [Route("Error")]
        public ApiResponseDto Error()
        {
            var level = "ERROR";
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            var timestamp = DateTimeOffset.UtcNow;

            ExceptionHandlerFeature exception = (HttpContext.Features.Get<IExceptionHandlerFeature>()) as ExceptionHandlerFeature;
            string path = exception.Path;
            var statusCode = exception.Error.GetType().Name switch
            {
                "ArgumentException" => HttpStatusCode.BadRequest,
                "NotFound" => HttpStatusCode.NotFound,
                "Unauthorized" => HttpStatusCode.Unauthorized,
                "InternalServerError" => HttpStatusCode.InternalServerError,
                _ => HttpStatusCode.ServiceUnavailable
            };

            //return Problem(detail: exception.Error.Message, statusCode: (int)statusCode);
            var response = _loggerService.InsertLog((int)statusCode, exception.Error.Message, exception.Error.StackTrace, exception.Error.InnerException?.Message, exception.Error.InnerException?.StackTrace, path, level, userEmail, timestamp);
            return ApiOkResult(response, (int)statusCode, false, ResponseMessagesEnum.Error.GetEnumDescription());
        }
    }
}
