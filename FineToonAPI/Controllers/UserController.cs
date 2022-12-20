using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Utils;
using FTDTO.ApiResponse;
using FTDTO.User;
using FTEnum.ResponseMessageEnum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace FineToonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserRepo _userRepo;
        public UserController(IHttpContextAccessor httpContextAccessor, IUserRepo userRepo) : base(httpContextAccessor)
        {
            _userRepo = userRepo;
        }

        [HttpPost]
        [Route("StoreActiveUser")]
        public ApiResponseDto StoreActiveUser()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var fullname = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            dynamic model = _userRepo.StoreActiveUser(email, fullname);

            string message = Convert.ToString(model[0].Message);
            bool hasStored = Convert.ToBoolean(model[0].Status);
            if (!hasStored)
            {
                //return ApiOkResult(model, (int)HttpStatusCode.OK, false, message);
                return ApiOkResult(model, (int)HttpStatusCode.OK, false, ResponseMessagesEnum.Exists.GetEnumDescription());
            }

            //return ApiOkResult(model, (int)HttpStatusCode.OK, true, message);
            return ApiOkResult(model, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Saved.GetEnumDescription());
        }

        [HttpPost]
        [Route("AddUsersToCompany")]
        public ApiResponseDto AddUsersToCompany([FromBody] AddUsersToCompanyDto viewModel)
        {
            var response = _userRepo.AddUsersToCompany(viewModel.CompanyID, viewModel.UserIDs);
            return ApiOkResult(response, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Saved.GetEnumDescription());
        }

        [HttpPost]
        [Route("RemoveUserFromCompany")]
        public ApiResponseDto RemoveUserFromCompany([FromBody] RemoveUserFromCompanyDto viewModel)
        {
            var response = _userRepo.RemoveUserFromCompany(viewModel.CompanyID, viewModel.UserID);
            return ApiOkResult(response, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Deleted.GetEnumDescription());
        }

        [HttpPost]
        [Route("GetUsersByCompany")]
        public ApiResponseDto GetUsersByCompany(int companyID)
        {
            dynamic model = _userRepo.GetUsersByCompany(companyID);
            return ApiOkResult(model, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("GetUsersNotInCompany")]
        public ApiResponseDto GetUsersNotInCompany(int companyID)
        {
            dynamic model = _userRepo.GetUsersNotInCompany(companyID);
            return ApiOkResult(model, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("GetUserRole")]
        public ApiResponseDto GetUserRole()
        {
            string email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            if (string.IsNullOrEmpty(email))
            {
                return ApiOkResult(null, (int)HttpStatusCode.Unauthorized, false, "Unauthorized user!");
            }

            dynamic model = _userRepo.GetUserRole(email);
            return ApiOkResult(model, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }
    }
}
