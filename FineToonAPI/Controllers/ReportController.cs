using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Utils;
using FTDTO.ApiResponse;
using FTDTO.Payment;
using FTDTO.Report;
using FTEnum.ResponseMessageEnum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net;

namespace FineToonAPI.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IMemoryCache cache;
        private readonly IConfiguration configuration;
        private readonly IReportRepo reportRepo;

        public ReportController(IReportRepo reportRepo, IHttpContextAccessor httpContextAccessor, IMemoryCache cache, IConfiguration conf) : base(httpContextAccessor)
        {
            this.cache = cache;
            this.configuration = conf;
            this.reportRepo = reportRepo;
        }

        [HttpPost]
        [Route("EmployeePayrollGenerated")]
        public ApiResponseDto EmployeePayrollGenerated(int paymentID)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var data = reportRepo.GetEmployeePayrollGenerated(userEmail, paymentID);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("EmployeePayroll")]
        public ApiResponseDto EmployeePayroll([FromBody] EmployeePayrollDTO employeePayroll)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var data = reportRepo.GetEmployeePayroll(userEmail, employeePayroll.DriverID, employeePayroll.StartDate, employeePayroll.EndDate);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("TruckHireReport")]
        public ApiResponseDto TruckHireReport(int invoiceID)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var data = reportRepo.GetTruckHireReport(userEmail, invoiceID);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("TruckHireReports")]
        public ApiResponseDto TruckHireReports([FromBody] TruckHireReportsFilterDTO filter)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            if (filter == null)
            {
                filter = new()
                {
                    InvoiceID = null,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(-1)
                };
            }

            if (filter.EndDate == null)
            {
                filter.EndDate = DateTime.Today;
            }

            if (filter.StartDate == null)
            {
                filter.StartDate = filter.EndDate.Value.AddMonths(-1);
            }

            var data = reportRepo.GetTruckHireReports(userEmail, Convert.ToDateTime(filter.StartDate), Convert.ToDateTime(filter.EndDate), filter.InvoiceID);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("MoneyOutReport")]
        public ApiResponseDto MoneyOutReport([FromBody] MoneyOutReportFilterDTO filter)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            if (filter == null)
            {
                filter = new()
                {
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(1)
                };
            }

            if (filter.EndDate == null)
            {
                filter.EndDate = DateTime.Today.AddDays(1);
            }

            if (filter.StartDate == null)
            {
                filter.StartDate = filter.EndDate.Value.AddDays(-1);
            }

            var data = reportRepo.GetMoneyOutReport(userEmail, Convert.ToDateTime(filter.StartDate), Convert.ToDateTime(filter.EndDate));
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }
    }
}
