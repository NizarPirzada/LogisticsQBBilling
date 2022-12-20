using FTBusiness.Logger;
using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Utils;
using FTDTO.ApiResponse;
using FTDTO.Payment;
using FTEnum.ResponseMessageEnum;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace FineToonAPI.Controllers
{
    public class PaymentController : BaseController
    {
        private ServiceContext ServiceContext;
        private readonly ICompanyRepo companyRepo;
        private readonly IPaymentRepo paymentRepo;
        private readonly IReportRepo reportRepo;
        private readonly ILoggerService loggerService;
        private readonly IConfiguration configuration;
        private readonly string QbBaseUrl;

        public PaymentController(ICompanyRepo companyRepo, IPaymentRepo paymentRepo, IReportRepo reportRepo, ILoggerService loggerService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.companyRepo = companyRepo;
            this.paymentRepo = paymentRepo;
            this.reportRepo = reportRepo;
            this.loggerService = loggerService;
            this.configuration = configuration;
            QbBaseUrl = this.configuration.GetValue<string>("QuickBooks_Sandbox:base_url");
        }

        [HttpGet]
        [Route("ViewPaymentBeforeCreate")]
        public ApiResponseDto ViewPaymentBeforeCreate(PaymentDriverDTO payment)
        {
            return ApiOkResult(paymentRepo.ViewPaymentBeforeCreate(payment.DriverId, payment.StartDate, payment.EndDate), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("CreatePayment")]
        public ApiResponseDto CreatePayment(PaymentDriverTypeDTO payment)
        {
            return ApiOkResult(paymentRepo.CreatePayment(payment.DriverType, payment.DriverId, payment.StartDate, payment.EndDate), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("CreateBill")]
        public ApiResponseDto CreateBill(PaymentDriverTypeDTO payment)
        {
            var level = "INFORMATION";
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            var statusCode = HttpStatusCode.OK;
            string path = HttpContext.Request.Path;
            string message = $"CreateBill action invoked.";
            loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

            try
            {
                //var data = paymentRepo.GetDriverDetailForBillPayment(payment.DriverType, payment.DriverId, payment.StartDate, payment.EndDate);

                message = $"Creating bill/payment in DB. DriverID: {payment.DriverId}, StartDate: {payment.StartDate:yyyy-MM-dd}, EndDate: {payment.EndDate:yyyy-MM-dd}";
                loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                int paymentID = paymentRepo.CreatePayment(payment.DriverType, payment.DriverId, payment.StartDate, payment.EndDate);
                if (paymentID == 0)
                {
                    throw new Exception("Bill/Payment creation failed. An error occurred in SP.");
                }

                message = $"Bill/Payment hass been created in DB. PaymentID: {paymentID}";
                loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                var data = reportRepo.GetEmployeePayrollGenerated(userEmail, paymentID);
                if (data != null && data.Count != 0)
                {
                    SetServiceContext(userEmail);
                    DataService dataService = new DataService(ServiceContext);

                    message = $"Getting vendor details from QuickBooks. Vendor: {Convert.ToString(data[0].DriverName)}";
                    loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                    List<Vendor> vendors = GetVendors(Convert.ToString(data[0].DriverName));
                    Vendor vendor = vendors.FirstOrDefault();
                    if (vendor == null)
                    {
                        List<Customer> customers = GetCustomers(Convert.ToString(data[0].DriverName));
                        Customer customer = customers.FirstOrDefault();
                        if (customer == null)
                        {
                            // Create new vendor in Quickbooks ...
                            message = $"Vendor: {Convert.ToString(data[0].DriverName)} not found in QuickBooks. Creating new vendor.";
                            loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                            vendor = new Vendor
                            {
                                DisplayName = data[0].DriverName,
                                CompanyName = data[0].DriverName
                            };
                            vendor = dataService.Add<Vendor>(vendor);
                        }
                        else
                        {
                            message = $"A customer with same name already exists. Checking for vendor: {Convert.ToString(data[0].DriverName)} (V).";
                            loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                            string name = $"{Convert.ToString(data[0].DriverName)} (V)";
                            vendors = GetVendors(name);
                            vendor = vendors.FirstOrDefault();
                            if (vendor == null)
                            {
                                // Create a "customer as a vendor" in Quickbooks ...
                                message = $"Vendor: {Convert.ToString(data[0].DriverName)} (V) not found in QuickBooks. Creating new 'customer as a vendor'.";
                                loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                                vendor = new Vendor
                                {
                                    DisplayName = name,
                                    CompanyName = name
                                };
                                vendor = dataService.Add<Vendor>(vendor);
                            }
                        }
                    }

                    List<Line> lineItems = new List<Line>();
                    foreach (var line in data)
                    {
                        lineItems.Add(new Line
                        {
                            Description = $"Ticket # {line.TicketNumber}\nTicket Creation Date: {Convert.ToDateTime(line.Date):MM-dd-yyyy}\nContractor: {line.CustomerDescription}\nDriver: {line.DriverName}\nDriver Rate: {line.Percentage}%\nTons/Hrs: {line.Quantity.ToString("0.00")}\nPrice Per Unit: ${line.Price.ToString("0.00")}",
                            Amount = Convert.ToDecimal(Convert.ToDecimal(line.Quantity) * Convert.ToDecimal(line.Price) * (Convert.ToDecimal(line.Percentage) / Convert.ToDecimal(100.0))),
                            AmountSpecified = true,
                            DetailType = LineDetailTypeEnum.ItemBasedExpenseLineDetail,
                            DetailTypeSpecified = true,
                            AnyIntuitObject = new ItemBasedExpenseLineDetail
                            {
                                ItemRef = new ReferenceType
                                {
                                    Value = "354",
                                }
                            }
                        });
                    }

                    message = $"Creating bill in QuickBooks.";
                    loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                    var bill = new Bill
                    {
                        DocNumber = Convert.ToString(paymentID),
                        Line = lineItems.ToArray(),
                        VendorRef = new ReferenceType
                        {
                            Value = vendor.Id,
                            name = vendor.CompanyName
                        }
                    };

                    bill = dataService.Add<Bill>(bill);

                    message = $"Created bill in QuickBooks. QB Bill ID: {bill.Id}, DocNumber: {bill.DocNumber}. API call successful.";
                    loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                    return ApiOkResult(paymentID, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
                }

                // Execution should not reach this point ...
                throw new Exception($"An error accurred while retrieving payroll data from DB. PaymentID: {paymentID}");
            }
            catch (Exception e)
            {
                level = "ERROR";
                statusCode = HttpStatusCode.InternalServerError;
                loggerService.InsertLog((int)statusCode, e.Message, e.StackTrace, e.InnerException?.Message, e.InnerException?.StackTrace, path, level, userEmail, DateTimeOffset.UtcNow);

                return ApiOkResult(null, (int)HttpStatusCode.InternalServerError, false, ResponseMessagesEnum.Error.GetEnumDescription());
            }
        }

        [HttpPost]
        [Route("FindPayments")]
        public ApiResponseDto FindPayments(PaymentDriverDTO payment)
        {
            return ApiOkResult(paymentRepo.FindPayments(payment.DriverId, payment.StartDate, payment.EndDate), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("GetPayableTickets")]
        public ApiResponseDto GetPayableTickets(PaymentDriverTypeDTO payment)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            return ApiOkResult(paymentRepo.GetPayableTickets(payment.DriverType, payment.StartDate, payment.EndDate, userEmail), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("GetPaymentDetail")]
        public ApiResponseDto GetPaymentDetail(int paymentID)
        {
            return ApiOkResult(paymentRepo.GetPaymentDetail(paymentID), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        private List<Account> GetAccounts(string accountName)
        {
            string qbQuery = string.Format("SELECT * FROM Account WHERE Name = '{0}'", accountName);
            QueryService<Account> queryService = new QueryService<Account>(ServiceContext);
            return queryService.ExecuteIdsQuery(qbQuery).ToList();
        }

        private List<Vendor> GetVendors(string vendorName)
        {
            string qbQuery = string.Format("SELECT * FROM Vendor WHERE DisplayName = '{0}'", vendorName);
            QueryService<Vendor> queryService = new QueryService<Vendor>(ServiceContext);
            return queryService.ExecuteIdsQuery(qbQuery).ToList();
        }

        private List<Customer> GetCustomers(string customerName)
        {
            string qbQuery = string.Format("SELECT * FROM Customer WHERE DisplayName = '{0}'", customerName);
            QueryService<Customer> queryService = new QueryService<Customer>(ServiceContext);
            return queryService.ExecuteIdsQuery(qbQuery).ToList();
        }

        private void SetServiceContext(string userEmail)
        {
            OAuth2RequestValidator oAuthValidator;
            var qbInfo = companyRepo.GetQuickBookDetails(userEmail);
            if (qbInfo.Count != 0 && qbInfo[0] != null)
            {
                oAuthValidator = new OAuth2RequestValidator(qbInfo[0].AccessToken);
                ServiceContext = new ServiceContext(qbInfo[0].RealmID, IntuitServicesType.QBO, oAuthValidator);
                ServiceContext.IppConfiguration.BaseUrl.Qbo = QbBaseUrl;
                //ServiceContext.IppConfiguration.MinorVersion.Qbo = "23";
            }
        }
    }
}
