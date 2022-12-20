using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Utils;
using FTDTO.ApiResponse;
using FTDTO.User;
using FTEnum.ResponseMessageEnum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FineToonAPI.Controllers
{
    public class ApplicationUserController : BaseController
    {
        private readonly IApplicationUserRepo applicationUserRepo;
        public ApplicationUserController(IApplicationUserRepo applicationUserRepo, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.applicationUserRepo = applicationUserRepo;
        }


        [HttpGet]
        [Route("GetUserList")]
        public ApiResponseDto GetUserList()
        {
            var data = applicationUserRepo.GetUsers();
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpGet]
        [Route("GetUserDetailById")]
        public ApiResponseDto GetUserDetailById(int userId)
        {
            var data = applicationUserRepo.GetUserDetails(userId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }
        

        [HttpGet]
        [Route("GetUserDetailByCode")]
        public ApiResponseDto GetUserDetailByCode(string code)
        {
            var data = applicationUserRepo.GetUserDetails(code);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpGet]
        [Route("GetApplicationSetting")]
        public ApiResponseDto GetApplicationSetting(string applicationSetting)
        {
            var data = applicationUserRepo.GetApplicationSettings(applicationSetting);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpPost]
        [Route("UpdateUser")]
        public ApiResponseDto UpdateUser(UserUpdateDto model)
        {
            var response = applicationUserRepo.UpdateUser(model.UserId, model.FullName, model.LoginId, model.Email, model.IsSystemAdmin);

            return ApiOkResult(response, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Update.GetEnumDescription());
        }

    }
}
