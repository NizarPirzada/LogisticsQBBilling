using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Utils;
using FTDTO.ApiResponse;
using FTDTO.Job;
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
    public class JobController : BaseController
    {
        private readonly IJobRepo jobRepo;
        public JobController(IJobRepo jobRepo, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.jobRepo = jobRepo;
        }


        [HttpGet]
        [Route("GetJobList")]
        public ApiResponseDto GetJobList(int customerId)
        {
            var data = jobRepo.GetJobs(customerId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpGet]
        [Route("GetJobProfitability")]
        public ApiResponseDto GetJobProfitability(int jobId)
        {
            var data = jobRepo.GetJobProfit(jobId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpGet]
        [Route("GetJobProductDetail")]
        public ApiResponseDto GetJobProductDetail(int jobProductId)
        {
            var data = jobRepo.GetJobProductDetails(jobProductId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpGet]
        [Route("GetJobListWithComplete")]
        public ApiResponseDto GetJobListWithComplete(int customerId)
        {
            var data = jobRepo.GetJobListWithComplete1(customerId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpGet]
        [Route("GetJobDetailByJobId")]
        public ApiResponseDto GetJobDetailByJobId(int jobId)
        {
            var data = jobRepo.GetJobDetails(jobId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }
        
        [HttpGet]
        [Route("GetJobDetailByCode")]
        public ApiResponseDto GetJobDetailByCode(string code, int customerId)
        {
            var data = jobRepo.GetJobDetails(code, customerId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetJobByCode")]
        public ApiResponseDto GetJobByCode(string code)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var data = jobRepo.GetJobByCode(code, userEmail);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetJobHistory")]
        public ApiResponseDto GetJobHistory(int jobId)
        {
            var data = jobRepo.GetJobsHistory(jobId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpPost]
        [Route("UpdateJobProduct")]
        public ApiResponseDto UpdateJobProduct(JobProductDto model)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var response = jobRepo.UpdateJobProduct(userEmail, model.ItemId, model.JobId, model.ProductId, model.Code, model.Description, model.Price, model.DriverPrice);

            return ApiOkResult(response, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpPost]
        [Route("UpdateJob")]
        public ApiResponseDto UpdateJob(JobDto model)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var response = jobRepo.UpdateJob(userEmail, model.JobId, model.CustomerId, model.Code, model.Description, model.PoNumber, model.IsComplete, model.AwardDate, model.Location);

            return ApiOkResult(response, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Update.GetEnumDescription());
        }

    }
}
