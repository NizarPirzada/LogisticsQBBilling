using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Utils;
using FTDTO.ApiResponse;
using FTDTO.Estimate;
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
    public class EstimateController : BaseController
    {
        private readonly IEstimateRepo estimateRepo;
        public EstimateController(IEstimateRepo estimateRepo, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.estimateRepo = estimateRepo;
        }


        [HttpGet]
        [Route("GetEstimateList")]
        public async Task<ApiResponseDto> GetEstimateList()
        {
            return ApiOkResult(await estimateRepo.GetEstimates(), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpGet]
        [Route("GetEstimateDetail")]
        public ApiResponseDto GetEstimateDetail(int estimateId)
        {
            var data = estimateRepo.GetEstimateDetails(estimateId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpGet]
        [Route("GetEstimateDetailByCode")]
        public ApiResponseDto GetEstimateDetailByCode(string code)
        {
            var data = estimateRepo.GetEstimateDetails(code);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpPost]
        [Route("UpdateEstimate")]
        public ApiResponseDto UpdateEstimate(EstimateDTO model)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var response = estimateRepo.UpdateEstimate(model.EstimateId,model.CustomerId, model.Code, model.Phone, model.Location, model.Description, model.Total, model.ExpirationDate, userEmail);

            return ApiOkResult(response, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Update.GetEnumDescription());
        }


        [HttpDelete]
        [Route("RemoveEstimate")]
        public ApiResponseDto RemoveEstimate(int estimateId)
        {
            var response = estimateRepo.DeleteEstimate(estimateId);

            return ApiOkResult(response, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Deleted.GetEnumDescription());
        }

        //[HttpPost]
        //[Route("SaveUpdateEstimate")]
        //public async Task<ApiResponseDto> SaveEstimate(EstimateDTO model)
        //{
        //    model.UserId = SessionUser.LicenseUserId;
        //    var response = await estimateRepo.SaveUpdateEstimate(model);
        //    if (response.Status is 1)
        //        return ApiOkResult(response.Data, (int)HttpStatusCode.OK, true, response.Message);
        //    else
        //        return ApiOkResult(response.Data, (int)HttpStatusCode.BadRequest, false, response.Message);
        //}

        //[HttpGet]
        //[Route("GetEstimateById/{id}")]
        //public async Task<ApiResponseDto> GetEstimateById(long id)
        //{
        //    return ApiOkResult(await estimateRepo.GetEstimateById(id), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        //}


        //[HttpDelete("RemoveEstimate/{id}")]
        //public async Task<ApiResponseDto> Remove(long id)
        //{
        //    var res = await estimateRepo.RemoveEstimate(id, SessionUser.LicenseUserId);
        //    if (res)
        //        return ApiOkResult(res, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Deleted.GetEnumDescription());
        //    return ApiOkResult(null, (int)HttpStatusCode.BadRequest, false, ResponseMessagesEnum.Error.GetEnumDescription());
        //}

    }
}
