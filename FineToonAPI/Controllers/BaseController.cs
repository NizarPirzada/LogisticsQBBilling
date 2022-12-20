using FTCommon.Helpers;
using FTCommon.Utils;
using FTDTO.ApiResponse;
using FTDTO.User;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FineToonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("alloworigin")]
    public class BaseController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        [NonAction]
        public string GetDomainEndPoint()
        {
            var apiendpoint = httpContextAccessor.HttpContext.Request.Path;
            return $"{httpContextAccessor.HttpContext.Request.Scheme + "://" + httpContextAccessor.HttpContext.Request.Host}{apiendpoint}";
        }
        public UserSessionDto SessionUser
        {
            get
            {
                using var serviceScope = ServiceActivator.GetScope();
                return serviceScope.ServiceProvider.GetRequiredService<UserSessionProfileService>().GetUserModel();
            }

        }
        [NonAction]
        public ApiResponseDto ApiOkResult(dynamic _data, int status, bool isSuccessfull, string message = "")
        {
            return new ApiResponseDto
            {
                ResponseData = _data,
                Status = status,
                IsSuccessful = isSuccessfull,
                Message = message
            };

        }
    }
}
