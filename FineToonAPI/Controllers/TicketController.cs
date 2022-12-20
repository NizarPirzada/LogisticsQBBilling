using AutoMapper;
using FTBusiness.Logger;
using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Utils;
using FTDTO.ApiResponse;
using FTDTO.Ticket;
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
    public class TicketController : BaseController
    {
        private readonly ITicketRepo ticketRepo;
        private readonly IInvoiceRepo invoiceRepo;
        private readonly ICompanyRepo companyRepo;
        private readonly ILoggerService loggerService;
        private readonly IConfiguration configuration;
        private readonly IMapper _mapper;
        private ServiceContext ServiceContext;

        public TicketController(ITicketRepo ticketRepo, IInvoiceRepo invoiceRepo, ICompanyRepo companyRepo, ILoggerService loggerService, IConfiguration configuration, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.ticketRepo = ticketRepo;
            this.invoiceRepo = invoiceRepo;
            this.companyRepo = companyRepo;
            this.loggerService = loggerService;
            this.configuration = configuration;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetTickets")]
        public ApiResponseDto GetTickets(int ticketCode)
        {
            var data = ticketRepo.GetTickets(ticketCode);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetAllTicket")]
        public ApiResponseDto GetAllTicket(int jobId)
        {
            var data = ticketRepo.GetAllTickets(jobId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTicketsNotInvoiced")]
        public ApiResponseDto GetTicketsNotInvoiced(DateTime endDate, int offset, int limit)
        {
            var data = ticketRepo.GetTicketsNotInvoiced1(endDate);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTicketsNotInvoicedPaging")]
        public ApiResponseDto GetTicketsNotInvoicedPaging(DateTime endDate, int offset, int limit, bool invoiced = false)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var data = ticketRepo.GetTicketsNotInvoicedPaging(userEmail, endDate, offset, limit, invoiced);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTicketsNotPaid")]
        public ApiResponseDto GetTicketsNotPaid(DateTime endDate)
        {
            var data = ticketRepo.GetTicketsNotPaid1(endDate);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTicketsNotPaidByJob")]
        public ApiResponseDto GetTicketsNotPaidByJob(int jobId)
        {
            var data = ticketRepo.GetTicketsNotInvoicedByJob(jobId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTicketsNotPaidByTicketNumber")]
        public ApiResponseDto GetTicketsNotPaidByTicketNumber(string ticketNumber)
        {
            var data = ticketRepo.GetTicketsNotInvoicedByTicketNumber(ticketNumber);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }



        [HttpGet]
        [Route("GetTicketPaymentCount")]
        public ApiResponseDto GetTicketPaymentCount(int ticketid)
        {
            var data = ticketRepo.GetTicketPaymentCount1(ticketid);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTicketLineItems")]
        public ApiResponseDto GetTicketLineItems(int itemId)
        {
            var data = ticketRepo.GetTicketLineItems1(itemId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTicketLineItemDetail")]
        public ApiResponseDto GetTicketLineItemDetail(int itemId)
        {
            var data = ticketRepo.GetTicketLineItemDetails(itemId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTicketDetailByTicket")]
        public ApiResponseDto GetTicketDetailByTicket(int ticketId)
        {
            var data = ticketRepo.GetTicketDetails(ticketId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTicketDetail")]
        public ApiResponseDto GetTicketDetail(string code, int jobId)
        {
            var data = ticketRepo.GetTicketDetails(code, jobId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTicketHistory")]
        public ApiResponseDto GetTicketHistory(int ticketId)
        {
            var data = ticketRepo.GetTicketsHistory(ticketId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("UpdateTicket")]
        public ApiResponseDto UpdateTicket(TicketUpdateDto model)
        {
            var level = "INFORMATION";
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            var statusCode = HttpStatusCode.OK;
            string path = HttpContext.Request.Path;
            string message = $"UpdateTicket action invoked. TicketID: {model.TicketId}, TicketCode: {model.Code}, JobID: {model.JobId}, JobProductID: {model.JobProductId}, Dated: {model.CreationDate:yyyy-MM-dd}.";
            loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

            var response = ticketRepo.UpdateTicket(userEmail, model.TicketId, model.JobId, model.JobProductId, model.Code, model.Description, model.CreationDate);
            if (model.TicketId == 0)
            {
                message = $"New ticket created. TicketID: {response}, TicketCode: {model.Code}.";
                loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                return ApiOkResult(response, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Saved.GetEnumDescription());
            }

            message = $"Ticket updated. TicketID: {response}, TicketCode: {model.Code}. Checking if invoice for this ticket is already created.";
            loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

            var invoiceInDb = invoiceRepo.UpdateInvoice(model.TicketId, userEmail);
            if (invoiceInDb == null || invoiceInDb.Count == 0 || invoiceInDb[0].InvoiceNumber == 0 || string.IsNullOrEmpty(Convert.ToString(invoiceInDb[0].QbInvoiceID)))
            {
                message = $"Ticket not invoiced yet. Good to go.";
                loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                return ApiOkResult(response, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Update.GetEnumDescription());
            }

            message = $"Invoice already created. QB Invoice ID: {Convert.ToString(invoiceInDb[0].QbInvoiceID)}. Updating invoice...";
            loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

            SetServiceContext(userEmail);

            var data = ticketRepo.GetTicketById(model.TicketId);
            Customer customer = GetCustomer(Convert.ToString(data[0].CustomerName), Convert.ToString(data[0].CustomerCity), Convert.ToString(data[0].CustomerAddress), Convert.ToString(data[0].CustomerPostalCode));

            DataService dataService = new DataService(ServiceContext);
            Invoice invoice = new Invoice
            {
                Id = Convert.ToString(invoiceInDb[0].QbInvoiceID)
            };
            invoice = dataService.FindById<Invoice>(invoice);
            invoice.CustomerRef = new ReferenceType
            {
                Value = customer.Id
            };

            string invoiceNumber = Convert.ToString(data[0].InvoiceNumber);
            string poNumber = Convert.ToString(data[0].PoNumber);
            string jobDescription = Convert.ToString(data[0].JobDescription);

            int invoiceNumberLength = invoiceNumber.Length > 31 ? 31 : invoiceNumber.Length;
            int poNumberLength = poNumber.Length > 31 ? 31 : poNumber.Length;
            int jobDescriptionLength = jobDescription.Length > 31 ? 31 : jobDescription.Length;

            invoice.CustomField = new CustomField[]
            {
                new CustomField() { Name = "FT Invoice", Type = CustomFieldTypeEnum.StringType, AnyIntuitObject = invoiceNumber.Substring(0, invoiceNumberLength), DefinitionId = "1" },
                new CustomField() { Name = "PO Number", Type = CustomFieldTypeEnum.StringType, AnyIntuitObject = poNumber.Substring(0, poNumberLength), DefinitionId = "2" },
                new CustomField() { Name = "Job Name", Type = CustomFieldTypeEnum.StringType, AnyIntuitObject = jobDescription.Substring(0, jobDescriptionLength), DefinitionId = "3" }
            };

            invoice = dataService.Update<Invoice>(invoice);

            message = $"QuickBooks Invoice updated successfully. QB Invoice ID: {invoice.Id}, DocNumber: {invoice.DocNumber}. API call successful.";
            loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

            return ApiOkResult(response, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Update.GetEnumDescription());
        }

        [HttpPost]
        [Route("UpdateTicketLineItem")]
        public ApiResponseDto UpdateTicketLineItem(TicketLineUpdateDto model)
        {
            var level = "INFORMATION";
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            var statusCode = HttpStatusCode.OK;
            string path = HttpContext.Request.Path;
            string message = $"UpdateTicketLineItem action invoked. TicketID: {model.TicketId}, TicketLineItemID: {model.TicketLineItemId}.";
            loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

            var response = ticketRepo.UpdateTicketLineItem(userEmail, model.TicketLineItemId, model.TicketId, model.DriverId, model.TruckId, model.Quantity, model.PricePerUnit, model.DriverPrice);
            if (response == 0)
            {
                level = "ERROR";
                message = $"An error occurred in SP. Failed to insert/update ticket line item";
                loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                return ApiOkResult(response, (int)HttpStatusCode.InternalServerError, false, ResponseMessagesEnum.Error.GetEnumDescription());
            }

            message = $"Ticket Line Item updated. Checking if invoice for selected ticket is already created. TicketID: {model.TicketId}.";
            loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

            var invoiceInDb = invoiceRepo.UpdateInvoice(model.TicketId, userEmail);
            if (invoiceInDb == null || invoiceInDb.Count == 0 || invoiceInDb[0].InvoiceNumber == 0 || string.IsNullOrEmpty(Convert.ToString(invoiceInDb[0].QbInvoiceID)))
            {
                message = $"Ticket not invoiced yet. Good to go.";
                loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                return ApiOkResult(response, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Update.GetEnumDescription());
            }

            SetServiceContext(userEmail);

            message = $"Invoice found. Updating on QuickBooks";
            loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

            var data = ticketRepo.GetTicketLineItems1(model.TicketId);
            IEnumerable<TicketLineItemDto> ticketLineItems = _mapper.Map<IEnumerable<TicketLineItemDto>>(data);

            List<Line> lines = new List<Line>();
            foreach (var lineItem in ticketLineItems)
            {
                lines.Add(new Line
                {
                    Description = $"Ticket # {lineItem.Code}\n{lineItem.Truck_Description}",
                    Amount = lineItem.Price_Per_Unit * lineItem.Quantity,
                    AmountSpecified = true,
                    DetailType = LineDetailTypeEnum.SalesItemLineDetail,
                    DetailTypeSpecified = true,
                    AnyIntuitObject = new SalesItemLineDetail
                    {
                        Qty = lineItem.Quantity,
                        QtySpecified = true,
                        DiscountRate = lineItem.Price_Per_Unit,
                        DiscountRateSpecified = true,
                        ServiceDate = lineItem.Creation_Date,
                        ServiceDateSpecified = true,
                        ItemRef = new ReferenceType()
                        {
                            Value = "350"
                        },
                        AnyIntuitObject = lineItem.Price_Per_Unit,
                        ItemElementName = ItemChoiceType.UnitPrice
                    }
                });
            }

            DataService dataService = new DataService(ServiceContext);
            Invoice invoice = new Invoice
            {
                Id = Convert.ToString(invoiceInDb[0].QbInvoiceID)
            };
            invoice = dataService.FindById<Invoice>(invoice);
            invoice.Line = lines.ToArray();
            invoice = dataService.Update<Invoice>(invoice);

            message = $"Invoice updated on QuickBooks. QB InvoiceID: {invoice.Id}, DocNumber: {invoice.DocNumber}. API call successful.";
            loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

            return ApiOkResult(response, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Update.GetEnumDescription());
        }

        [HttpDelete]
        [Route("RemoveTicketLineItemDetail")]
        public ApiResponseDto RemoveTicketLineItemDetail(int ticketLineItemId)
        {
            var response = ticketRepo.DeleteTicketLineItemDetail(ticketLineItemId);
            return ApiOkResult(response, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Deleted.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTicketsSummary")]
        public ApiResponseDto GetTicketsSummary()
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var data = ticketRepo.GetTicketsSummary(userEmail);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("GetPayableTickets")]
        public ApiResponseDto GetPayableTickets([FromBody] PayableTicketFilterDTO filters)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            if (filters.StartDate > filters.EndDate)
            {
                filters.StartDate = filters.EndDate;
            }
            dynamic data = ticketRepo.GetPayableTickets(filters, userEmail);

            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("GetPayableTicketItems")]
        public ApiResponseDto GetPayableTicketItems([FromBody] PayableTicketFilterDTO filters)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            if (filters.StartDate > filters.EndDate)
            {
                filters.StartDate = filters.EndDate;
            }
            dynamic data = ticketRepo.GetPayableTicketItems(filters, userEmail);

            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpDelete]
        [Route("DeleteTicket")]
        public ApiResponseDto DeleteTicket(int id)
        {
            var response = ticketRepo.DeleteTicket(id);
            return ApiOkResult(response, (int)HttpStatusCode.NoContent, true, ResponseMessagesEnum.Deleted.GetEnumDescription());
        }

        private void SetServiceContext(string userEmail)
        {
            OAuth2RequestValidator oAuthValidator;
            var qbInfo = companyRepo.GetQuickBookDetails(userEmail);
            if (qbInfo.Count != 0 && qbInfo[0] != null)
            {
                oAuthValidator = new OAuth2RequestValidator(qbInfo[0].AccessToken);
                ServiceContext = new ServiceContext(qbInfo[0].RealmID, IntuitServicesType.QBO, oAuthValidator);
                ServiceContext.IppConfiguration.BaseUrl.Qbo = this.configuration.GetValue<string>("QuickBooks_Sandbox:base_url");
                //ServiceContext.IppConfiguration.MinorVersion.Qbo = "23";
            }
        }

        private Item GetItem(string name, decimal unitPrice)
        {
            var random = new Random();
            name = name + "_" + random.Next();

            string qbQuery = $"SELECT * FROM Item WHERE Name = '{name}'";
            QueryService<Item> queryService = new QueryService<Item>(ServiceContext);
            List<Item> items = queryService.ExecuteIdsQuery(qbQuery).ToList();
            Item item = items.FirstOrDefault();
            if (item == null)
            {
                item = new Item
                {
                    Name = name,
                    Description = name,
                    UnitPrice = unitPrice,
                    UnitPriceSpecified = true,
                    Type = ItemTypeEnum.NonInventory,
                    TypeSpecified = true,
                    Active = true,
                    ActiveSpecified = true,
                    Taxable = false,
                    TaxableSpecified = true,
                    TrackQtyOnHand = false,
                    TrackQtyOnHandSpecified = true,
                    IncomeAccountRef = new ReferenceType()
                    {
                        name = "truck revenue",
                        Value = "42"
                    }
                };

                DataService dataService = new DataService(ServiceContext);
                item = dataService.Add<Item>(item);
            }

            return item;
        }

        private Account GetAccount(string name, string type)
        {
            string accountName = $"{name} - {type}";
            string qbQuery = $"SELECT * FROM Account WHERE Name = '{accountName}'";
            QueryService<Account> queryService = new QueryService<Account>(ServiceContext);
            List<Account> accounts = queryService.ExecuteIdsQuery(qbQuery).ToList();
            Account account = accounts.FirstOrDefault();
            if (account != null)
            {
                return account;
            }

            if (type == "Income")
            {
                account = new Account
                {
                    Name = accountName,
                    AccountType = AccountTypeEnum.Income,
                    AccountSubType = AccountSubTypeEnum.Auto.ToString()
                };
            }
            else
            {
                account = new Account
                {
                    Name = accountName,
                    AccountType = AccountTypeEnum.Expense,
                    AccountSubType = AccountSubTypeEnum.Auto.ToString()
                };
            }

            DataService dataService = new DataService(ServiceContext);
            account = dataService.Add<Account>(account);
            return account;
        }

        private Customer GetCustomer(string name, string city, string address, string postalCode)
        {
            name = name.Replace(":", string.Empty);
            name = name.Replace("\t", string.Empty);
            name = name.Replace("\n", string.Empty);
            name = name.Replace("'", "\\'");

            string qbQuery = $"SELECT * FROM Customer WHERE DisplayName = '{name}'";
            QueryService<Customer> queryService = new QueryService<Customer>(ServiceContext);
            List<Customer> customers = queryService.ExecuteIdsQuery(qbQuery).ToList();
            Customer customer = customers.FirstOrDefault();
            if (customer != null)
            {
                return customer;
            }

            customer = new Customer
            {
                DisplayName = name,
                CompanyName = name,
            };

            DataService dataService = new DataService(ServiceContext);
            customer = dataService.Add<Customer>(customer);

            if (customer.BillAddr == null || customer.ShipAddr == null)
            {
                var physicalAddress = new PhysicalAddress()
                {
                    City = city,
                    Line1 = address,
                    PostalCode = postalCode,
                };
                customer.BillAddr = physicalAddress;
                customer.ShipAddr = physicalAddress;
                customer = dataService.Update<Customer>(customer);
            }
            return customer;
        }
    }
}
