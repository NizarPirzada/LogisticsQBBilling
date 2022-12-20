using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Utils;
using FTDTO.ApiResponse;
using FTDTO.Customer;
using FTEnum.ResponseMessageEnum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FineToonAPI.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerRepo customerRepo;
        public CustomerController(ICustomerRepo customerRepo, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.customerRepo = customerRepo;
        }


        [HttpGet]
        [Route("GetCustomerList")]
        public ApiResponseDto GetCustomerList()
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var data = customerRepo.GetCustomers(userEmail);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpGet]
        [Route("GetCustomerListWithInactive")]
        public ApiResponseDto GetCustomerListWithInactive()
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var data = customerRepo.GetCustomerListWithInactive(userEmail);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpGet]
        [Route("GetCustomerDetail")]
        public ApiResponseDto GetCustomerDetail(int customerId)
        {
            var data = customerRepo.GetCustomerDetails(customerId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }
        
        [HttpGet]
        [Route("GetCustomerHistory")]
        public ApiResponseDto GetCustomerHistory(int customerId)
        {
            var data = customerRepo.GetCustomersHistory(customerId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpGet]
        [Route("GetCustomerDetailByCode")]
        public ApiResponseDto GetCustomerDetailByCode(string code)
        {
            var data = customerRepo.GetCustomerDetails(code);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpGet]
        [Route("GetCustomerJobPricing")]
        public ApiResponseDto GetCustomerJobPricing()
        {
            var data = customerRepo.GetCustomerJobPricings();
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpPost]
        [Route("UpdateCustomer")]
        public ApiResponseDto UpdateCustomer(CustomerDTO model)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var response = customerRepo.UpdateCustomer(userEmail, model.CustomerId, model.Code, model.Description, model.Address_1, model.Address_2, model.City, model.State, model.Zip_Code, model.Phone, model.Fax, model.PaymentTerms, model.IsInActive);

            return ApiOkResult(response, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Update.GetEnumDescription());
        }


        //[HttpPost]
        //[Route("SaveUpdateCustomer")]
        //public async Task<ApiResponseDto> SaveCustomer(CustomerDTO model)
        //{
        //    model.UserId = SessionUser.LicenseUserId;
        //    var response = await customerRepo.SaveUpdateCustomer(model);
        //    if (response.Status is 1)
        //        return ApiOkResult(response.Data, (int)HttpStatusCode.OK, true, response.Message);
        //    else
        //        return ApiOkResult(response.Data, (int)HttpStatusCode.BadRequest, false, response.Message);
        //}

        //[HttpGet]
        //[Route("GetCustomerById/{id}")]
        //public async Task<ApiResponseDto> GetCustomerById(long id)
        //{
        //    return ApiOkResult(await customerRepo.GetCustomerById(id), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        //}

        //[HttpGet]
        //[Route("GetCustomerList")]
        //public async Task<ApiResponseDto> GetCustomerList()
        //{
        //    return ApiOkResult(await customerRepo.GetCustomers(), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        //}
        //[HttpDelete("RemoveCustomer/{id}")]
        //public async Task<ApiResponseDto> Remove(long id)
        //{
        //    var res = await customerRepo.RemoveCustomer(id, SessionUser.LicenseUserId);
        //    if (res)
        //        return ApiOkResult(res, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Deleted.GetEnumDescription());
        //    return ApiOkResult(null, (int)HttpStatusCode.BadRequest, false, ResponseMessagesEnum.Error.GetEnumDescription());
        //}

    }
}
