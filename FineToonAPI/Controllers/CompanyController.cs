using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Utils;
using FTDTO.ApiResponse;
using FTDTO.Company;
using FTEnum.ResponseMessageEnum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace FineToonAPI.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly ICompanyRepo companyRepo;
        public CompanyController(ICompanyRepo customerRepo, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.companyRepo = customerRepo;
        }

        [HttpGet]
        [Route("GetAllCompanies")]
        public ApiResponseDto GetAllCompanies()
        {
            var data = companyRepo.GetAllCompanies();
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetCompanyInformation")]
        public ApiResponseDto GetCompanyInformation()
        {
            string userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var data = companyRepo.GetCompanyDetails(userEmail);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpPost]
        [Route("UpdateCompanyInformation")]
        public ApiResponseDto UpdateCompanyInformation(CompanyDTO model)
        {
            var response = companyRepo.UpdateCompany(Convert.ToInt32(model.Id), model.CompanyId, model.CompanyName, model.Address_1, model.Address_2, model.City, model.State, model.Zip_Code, model.Phone, model.Fax, model.Url);
            return ApiOkResult(response, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Update.GetEnumDescription());
        }

        [HttpPost]
        [Route("SetCompanyAsActive")]
        public ApiResponseDto SetCompanyAsActive(int companyID)
        {
            string userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            dynamic response = companyRepo.SetCompanyAsActive(companyID, userEmail);
            return ApiOkResult(response, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Update.GetEnumDescription());
        }
    }
}
