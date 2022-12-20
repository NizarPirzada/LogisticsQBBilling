using AutoMapper;
using FTBusiness.Logger;
using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Utils;
using FTDTO.ApiResponse;
using FTDTO.Invoice;
using FTDTO.Ticket;
using FTEnum.ResponseMessageEnum;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.OAuth2PlatformClient;
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
    public class InvoiceController : BaseController
    {
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly ICompanyRepo companyRepo;
        private readonly IInvoiceRepo invoiceRepo;
        private readonly ITicketRepo ticketRepo;
        private readonly ILoggerService loggerService;
        private readonly OAuth2Client oauth2Client;
        private readonly string QbBaseUrl;

        public InvoiceController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IMapper mapper, ICompanyRepo companyRepo, IInvoiceRepo invoiceRepo, ITicketRepo ticketRepo, ILoggerService loggerService) : base(httpContextAccessor)
        {
            this.configuration = configuration;
            this.mapper = mapper;
            this.companyRepo = companyRepo;
            this.invoiceRepo = invoiceRepo;
            this.ticketRepo = ticketRepo;
            this.loggerService = loggerService;
            oauth2Client = new OAuth2Client(
                this.configuration.GetValue<string>("QuickBooks_Sandbox:client_id"),
                this.configuration.GetValue<string>("QuickBooks_Sandbox:client_secret"),
                this.configuration.GetValue<string>("QuickBooks_Sandbox:redirect_url"),
                this.configuration.GetValue<string>("QuickBooks_Sandbox:environment"));
            QbBaseUrl = this.configuration.GetValue<string>("QuickBooks_Sandbox:base_url");
        }

        [HttpPost]
        [Route("GetInvoices")]
        public ApiResponseDto GetInvoices([FromBody] InvoiceParameterDTO param)
        {
            dynamic data = invoiceRepo.GetInvoices(param);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("GetInvoicesNotMarkedForPayment")]
        public ApiResponseDto GetInvoicesNotMarkedForPayment(InvoiceDateDTO invoiceDate)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            return ApiOkResult(invoiceRepo.GetInvoicesNotMarkedForPayment(invoiceDate.StartDate, invoiceDate.EndDate, userEmail), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("GetUnpaidInvoices")]
        public ApiResponseDto GetUnpaidInvoices(int customerId)
        {
            return ApiOkResult(invoiceRepo.GetUnpaidInvoiceList(customerId), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetInvoicesReadyForPayment")]
        public ApiResponseDto GetInvoicesReadyForPayment()
        {
            return ApiOkResult(invoiceRepo.GetReadyForPayment(), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetInvoicesByStatus")]
        public ApiResponseDto GetInvoicesByStatus(int stauts)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            return ApiOkResult(invoiceRepo.GetInvoiceListByStatus(stauts, userEmail), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("MarkInvoiceReadyForPayment")]
        public ApiResponseDto MarkInvoiceReadyForPayment(int invoiceID)
        {
            return ApiOkResult(invoiceRepo.MarkInvoiceReadyForPayment(invoiceID), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("MarkInvoiceStatus")]
        public ApiResponseDto MarkInvoiceStatus(InvoiceStatusDTO invoiceStatus)
        {
            return ApiOkResult(invoiceRepo.MarkInvoiceReadyForPayment(0), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetInvoiceDetailByNumber")]
        public ApiResponseDto GetInvoiceDetailByNumber(string InvoiceNumber)
        {
            return ApiOkResult(invoiceRepo.GetInvoiceDetail(InvoiceNumber), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetInvoiceDetailById")]
        public ApiResponseDto GetInvoiceDetailById(int invoiceID)
        {
            return ApiOkResult(invoiceRepo.GetInvoiceDetail(invoiceID), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetInvoiceHistory")]
        public ApiResponseDto GetInvoiceHistory(int invoiceId)
        {
            return ApiOkResult(invoiceRepo.GetInvoicesHistory(invoiceId), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }


        [HttpPost]
        [Route("MarkInvoiceAsPaid")]
        public ApiResponseDto MarkInvoiceAsPaid(InvoiceCheckDTO invoiceCheck)
        {
            return ApiOkResult(invoiceRepo.MarkInvoiceAsPaid(invoiceCheck.CheckNumber, invoiceCheck.InvoiceID), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("MarkInvoiceAsFunded")]
        public ApiResponseDto MarkInvoiceAsFunded(int invoiceID)
        {
            string userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            return ApiOkResult(invoiceRepo.MarkInvoiceAsFunded(invoiceID, userEmail), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Update.GetEnumDescription());
        }

        [HttpPost]
        [Route("SetIsReadyForPayment")]
        public ApiResponseDto SetIsReadyForPayment(int invoiceID)
        {
            string userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            return ApiOkResult(invoiceRepo.SetIsReadyForPayment(invoiceID, userEmail), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Update.GetEnumDescription());
        }

        [HttpPost]
        [Route("CreateInvoice")]
        public ApiResponseDto CreateInvoice(InvoiceJobDTO invoice)
        {
            var level = "INFORMATION";
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            var statusCode = HttpStatusCode.OK;
            string path = HttpContext.Request.Path;
            string message = $"CreateInvoice action invoked. JobID: {invoice.JobID}, JobProductID: {invoice.JobProductID}, StartDate: {invoice.StartDate:yyyy-MM-dd}, EndDate: {invoice.EndDate:yyyy-MM-dd}";
            loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

            try
            {
                string qbInvoiceID = null;
                var invoiceData = invoiceRepo.GetInvoiceDetailsQB(invoice.JobID, invoice.JobProductID, invoice.StartDate, invoice.EndDate);
                string tickets = Convert.ToString(invoiceData[0]?.Tickets);
                List<TicketLineItemDto> ticketItemsList = new List<TicketLineItemDto>();
                if (!string.IsNullOrEmpty(tickets))
                {
                    string[] ticketList = tickets.Split(',');
                    ticketList.ToList().ForEach(ticket =>
                    {
                        var ticketLineItems = ticketRepo.GetTicketLineItems1(Convert.ToInt32(ticket));
                        List<TicketLineItemDto> mappedData = mapper.Map<List<TicketLineItemDto>>(ticketLineItems);
                        ticketItemsList.AddRange(mappedData);
                    });

                    if (invoiceData != null)
                    {
                        var company = companyRepo.GetActiveCompany(userEmail);
                        if (company == null || company.Count == 0)
                        {
                            return ApiOkResult(null, 0, false, ResponseMessagesEnum.Error.ToString());
                        }

                        var serviceContext = GetServiceContext(userEmail);
                        var dataService = new DataService(serviceContext);

                        string customerName = Convert.ToString(invoiceData[0].customer_name);
                        customerName = customerName.Replace(":", string.Empty);
                        customerName = customerName.Replace("\t", string.Empty);
                        customerName = customerName.Replace("\n", string.Empty);
                        customerName = customerName.Replace("'", "\\'");
                        customerName = customerName.Trim();

                        message = $"Getting customer details form QuickBooks. Customer: {customerName}";
                        loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                        IEnumerable<Customer> customers = GetCustomers(customerName, serviceContext);
                        Customer customer = customers.FirstOrDefault();
                        if (customer == null)
                        {
                            IEnumerable<Vendor> vendors = GetVendors(customerName, serviceContext);
                            Vendor vendor = vendors.FirstOrDefault();
                            if (vendor == null)
                            {
                                // Create new customer in Quickbooks ...
                                message = $"Customer: {customerName} not found in QuickBooks. Creating new customer.";
                                loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                                customer = new Customer
                                {
                                    DisplayName = customerName,
                                    CompanyName = customerName,
                                };
                                customer = dataService.Add<Customer>(customer);
                            }
                            else
                            {
                                message = $"A vendor with same name already exists. Checking for customer: {customerName} (C).";
                                loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                                string name = $"{customerName} (C)";
                                customers = GetCustomers(name, serviceContext);
                                customer = customers.FirstOrDefault();
                                if (customer == null)
                                {
                                    // Create a "vendor as a customer" in Quickbooks ...
                                    message = $"Customer: {customerName} (C) not found in QuickBooks. Creating new 'vendor as a customer'.";
                                    loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                                    customer = new Customer
                                    {
                                        DisplayName = name,
                                        CompanyName = name,
                                    };
                                    customer = dataService.Add<Customer>(customer);
                                }
                            }
                        }

                        if (customer.BillAddr == null || customer.ShipAddr == null)
                        {
                            var address = new PhysicalAddress()
                            {
                                City = invoiceData[0].customer_city,
                                Line1 = invoiceData[0].customer_address,
                                PostalCode = invoiceData[0].customer_postal_code,
                            };
                            customer.BillAddr = address;
                            customer.ShipAddr = address;
                            customer = dataService.Update<Customer>(customer);
                        }

                        message = $"Customer: {customerName} has been created in QuickBooks.";
                        loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                        // Add Invoice
                        message = $"CreateInvoice private method invoked.";
                        loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);
                        Invoice objInvoice = CreateInvoice(customer, ticketItemsList,
                                                           Convert.ToDecimal(invoiceData[0].invoice_total),
                                                           Convert.ToString(invoiceData[0].invoice_number),
                                                           Convert.ToString(invoiceData[0].po_number),
                                                           Convert.ToString(invoiceData[0].job_description));
                        objInvoice = dataService.Add(objInvoice);
                        qbInvoiceID = objInvoice.Id;

                        message = $"Created invoice in QuickBooks. QB Invoice ID: {objInvoice.Id}, DocNumber: {objInvoice.DocNumber}";
                        loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                        int ftInvoiceID = invoiceRepo.CreateInvoice(invoice.JobID, invoice.JobProductID, invoice.StartDate, invoice.EndDate, qbInvoiceID, userEmail);

                        message = $"Created invoice in FineToon. FT Invoice ID: {ftInvoiceID}. API call successful.";
                        loggerService.InsertLog((int)statusCode, message, null, null, null, path, level, userEmail, DateTimeOffset.UtcNow);

                        return ApiOkResult(ftInvoiceID, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
                    }

                    throw new Exception("Error occurred while creating invoice. Could not retrieve invoice data from DB.");
                }

                throw new Exception("Error occurred while creating invoice. Could not retrieve invoice data from DB.");
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
        [Route("GetInvoiceList")]
        public ApiResponseDto GetInvoiceList(InvoiceDateDTO invoiceDate)
        {
            return ApiOkResult(invoiceRepo.GetInvoiceList(invoiceDate.StartDate, invoiceDate.EndDate), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("GetJobListForInvoice")]
        public ApiResponseDto GetJobListForInvoice(DateTime StartDate, DateTime EndDate)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            return ApiOkResult(invoiceRepo.GetJobListForInvoice(StartDate, EndDate, userEmail), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("GetTentativeInvoice")]
        public ApiResponseDto GetTentativeInvoice(InvoiceJobDTO invoice)
        {
            return ApiOkResult(invoiceRepo.GetTentativeInvoice(invoice.JobID, invoice.JobProductID, invoice.StartDate, invoice.EndDate), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetInvoiceDetails")]
        public ApiResponseDto GetInvoiceDetails(int invoiceNumber)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var data = invoiceRepo.GetInvoiceDetails(invoiceNumber, userEmail);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GenerateInvoiceDetails")]
        public ApiResponseDto GenerateInvoiceDetails(int jobID, int jobProductID, int invoiceID, DateTime? startDate, DateTime? endDate)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var data = invoiceRepo.GenerateInvoiceDetails(jobID, jobProductID, invoiceID, userEmail, startDate, endDate);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("UpdateInvoice")]
        public ApiResponseDto UpdateInvoice(InvoiceDriverDTO invoice)
        {
            return ApiOkResult(invoiceRepo.UpdateInvoice(Convert.ToInt32(invoice.Id), invoice.TruckId, invoice.DefaultDriverId, invoice.Code, invoice.Description), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("UpdateVoidStatus")]
        public ApiResponseDto UpdateVoidStatus(InvoiceStatusDTO param)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var invoiceInDb = invoiceRepo.GetInvoice(param.InvoiceID);
            if (invoiceInDb == null || invoiceInDb.Count == 0)
            {
                // Invoice not found in Database...
                return ApiOkResult(null, (int)HttpStatusCode.InternalServerError, false, ResponseMessagesEnum.Error.GetEnumDescription());
            }

            // Get Invoice from Quickbooks
            var serviceContext = GetServiceContext(userEmail);
            if (serviceContext == null)
            {
                // Quickbook authentication failed...
                return ApiOkResult(null, (int)HttpStatusCode.InternalServerError, false, ResponseMessagesEnum.Error.GetEnumDescription());
            }

            var queryService = new QueryService<Invoice>(serviceContext);
            string qbInvoiceQuery = $"SELECT * FROM Invoice ORDER BY MetaData.LastUpdatedTime DESC";
            var invoices = queryService.ExecuteIdsQuery(qbInvoiceQuery);
            var invoice = invoices.FirstOrDefault(i => Convert.ToInt32(i.CustomField[0].AnyIntuitObject) == Convert.ToInt32(invoiceInDb[0].InvoiceNumber));
            if (invoice == null)
            {
                // Invoice not found on Quickbooks...
                return ApiOkResult(null, (int)HttpStatusCode.InternalServerError, true, ResponseMessagesEnum.Error.GetEnumDescription());
            }

            var dataService = new DataService(serviceContext);
            invoice = dataService.Void(invoice);

            dynamic data = invoiceRepo.UpdateVoidStatus(param.InvoiceID, param.Status, userEmail);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Update.GetEnumDescription());
        }

        [HttpPost]
        [Route("UpdateFundedStatus")]
        public ApiResponseDto UpdateFundedStatus(InvoiceStatusDTO param)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            dynamic data = invoiceRepo.UpdateFundedStatus(param.InvoiceID, param.Status, userEmail);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Update.GetEnumDescription());
        }

        private static List<Account> GetAccounts(string accountName, ServiceContext context)
        {
            string qbQuery = string.Format("SELECT * FROM Account WHERE Name = '{0}'", accountName);
            QueryService<Account> queryService = new QueryService<Account>(context);
            return queryService.ExecuteIdsQuery(qbQuery).ToList();
        }

        private static List<Customer> GetCustomers(string customerName, ServiceContext context)
        {
            string qbQuery = string.Format("SELECT * FROM Customer WHERE DisplayName = '{0}'", customerName);
            QueryService<Customer> queryService = new QueryService<Customer>(context);
            return queryService.ExecuteIdsQuery(qbQuery).ToList();
        }

        private static List<Vendor> GetVendors(string vendorName, ServiceContext context)
        {
            string qbQuery = string.Format("SELECT * FROM Vendor WHERE DisplayName = '{0}'", vendorName);
            QueryService<Vendor> queryService = new QueryService<Vendor>(context);
            return queryService.ExecuteIdsQuery(qbQuery).ToList();
        }

        private ServiceContext GetServiceContext(string userEmail)
        {
            OAuth2RequestValidator oAuthValidator;
            var qbInfo = companyRepo.GetQuickBookDetails(userEmail);
            if (qbInfo[0] != null && qbInfo.Count != 0)
            {
                oAuthValidator = new OAuth2RequestValidator(qbInfo[0].AccessToken);
                var serviceContext = new ServiceContext(qbInfo[0].RealmID, IntuitServicesType.QBO, oAuthValidator);
                serviceContext.IppConfiguration.BaseUrl.Qbo = QbBaseUrl;
                return serviceContext;
            }
            return null;
        }

        #region Quick Books
        private Account CreateAccount()
        {

            Random randomNum = new Random();
            Account account = new Account();


            account.Name = "Name_" + randomNum.Next();

            account.FullyQualifiedName = account.Name;

            account.Classification = AccountClassificationEnum.Revenue;
            account.ClassificationSpecified = true;
            account.AccountType = AccountTypeEnum.Bank;
            account.AccountTypeSpecified = true;

            account.CurrencyRef = new ReferenceType()
            {
                name = "United States Dollar",
                Value = "USD"
            };

            return account;
        }


        #region create item
        /// <summary>
        /// This API creates invoice item 
        /// </summary>
        /// <returns></returns>
        private Item CreateItem(Account incomeAccount, string poNumber, string invoiceNumber, decimal totalDue)
        {

            Item item = new Item();

            Random randomNum = new Random();

            item.Name = "Fine Toon Ticket Item-" + poNumber + randomNum.Next();
            item.Description = "PO Number: " + poNumber + ", InvoiceNumber: " + invoiceNumber;
            item.Type = ItemTypeEnum.NonInventory;
            item.TypeSpecified = true;

            item.Active = true;
            item.ActiveSpecified = true;

            item.Taxable = false;
            item.TaxableSpecified = true;

            item.UnitPrice = totalDue;
            item.UnitPriceSpecified = true;

            item.TrackQtyOnHand = false;
            item.TrackQtyOnHandSpecified = true;

            item.IncomeAccountRef = new ReferenceType()
            {
                name = incomeAccount.Name,
                Value = incomeAccount.Id
            };

            item.ExpenseAccountRef = new ReferenceType()
            {
                name = incomeAccount.Name,
                Value = incomeAccount.Id
            };

            //For inventory item, assetacocunref is required
            return item;

        }

        private Item CreateLineItem(decimal pricePerUnit, decimal quantity, string truckDescription, int ticketId, int ticketLineItemId, DateTime creationDate)
        {
            Item item = new Item();
            Random randomNum = new Random();
            //item.Id = ticketLineItemId.ToString() + randomNum.Next();
            //item.
            item.Name = truckDescription + "_" + randomNum.Next();
            item.Description = truckDescription;
            item.Type = ItemTypeEnum.NonInventory;
            item.TypeSpecified = true;

            item.Active = true;
            item.ActiveSpecified = true;

            item.Taxable = false;
            item.TaxableSpecified = true;
            //item.price
            item.UnitPrice = pricePerUnit;
            item.UnitPriceSpecified = true;
            //item.QtyOnSalesOrder = quantity;

            //item.PrintGroupedItems = true;
            item.TrackQtyOnHand = false;
            item.TrackQtyOnHandSpecified = true;

            item.IncomeAccountRef = new ReferenceType()
            {
                name = "truck revenue",
                Value = "42"
            };

            //item.ExpenseAccountRef = new ReferenceType()
            //{
            //    name = "truck revenue",
            //    Value = "42"
            //};

            //item.IncomeAccountRef = new ReferenceType()
            //{
            //    name = incomeAccount.Name,
            //    Value = incomeAccount.Id
            //};
            //item.ExpenseAccountRef = new ReferenceType()
            //{
            //    name = incomeAccount.Name,
            //    Value = incomeAccount.Id
            //};

            //For inventory item, assetacocunref is required
            return item;

        }
        #endregion

        #region create customer
        /// <summary>
        /// This API creates customer 
        /// </summary>
        /// <returns></returns>
        private Customer CreateCustomer(string customerName)
        {
            //Random random = new Random();
            Customer customer = new Customer();

            customer.GivenName = customerName;
            //customer.FamilyName = "Serling";
            customer.DisplayName = customerName;
            return customer;
        }


        /// <summary>
        /// This API creates an Invoice
        /// </summary>
        private Invoice CreateInvoice(Customer customer, List<TicketLineItemDto> items, decimal invoiceAmount, string invoiceNumber, string poNumber, string jobDescription)
        {
            Random random = new Random();
            // Step 1: Initialize OAuth2RequestValidator and ServiceContext
            //ServiceContext serviceContext = IntializeContext(realmId);

            // Step 2: Initialize an Invoice object
            Invoice invoice = new Invoice();
            // invoice.Deposit = new Decimal(0.00);
            //invoice.DepositSpecified = true;


            // Step 3: Invoice is always created for a customer so lets retrieve reference to a customer and set it in Invoice
            /*QueryService<Customer> querySvc = new QueryService<Customer>(serviceContext);
            Customer customer = querySvc.ExecuteIdsQuery("SELECT * FROM Customer WHERE CompanyName like 'Amy%'").FirstOrDefault();*/
            //invoice.Id = invoiceNumber;
            invoice.CustomerRef = new ReferenceType()
            {
                Value = customer.Id
            };


            // Step 4: Invoice is always created for an item so lets retrieve reference to an item and a Line item to the invoice
            /* QueryService<Item> querySvcItem = new QueryService<Item>(serviceContext);
            Item item = querySvcItem.ExecuteIdsQuery("SELECT * FROM Item WHERE Name = 'Lighting'").FirstOrDefault();*/
            List<Line> lineList = new List<Line>();

            items?.ForEach(item =>
            {

                Line line = new Line();
                //line.Id = item.TicketLineItemId.ToString();
                line.Description = $"Ticket # {item.Code}\n{item.Truck_Description}";
                line.Amount = item.Price_Per_Unit * item.Quantity;
                line.AmountSpecified = true;
                SalesItemLineDetail salesItemLineDetail = new SalesItemLineDetail();
                salesItemLineDetail.Qty = item.Quantity;
                salesItemLineDetail.QtySpecified = true;
                salesItemLineDetail.DiscountRate = item.Price_Per_Unit;
                salesItemLineDetail.DiscountRateSpecified = true;
                salesItemLineDetail.ServiceDate = item.Creation_Date;
                salesItemLineDetail.ServiceDateSpecified = true;
                salesItemLineDetail.ItemRef = new ReferenceType()
                {
                    Value = "350"
                };
                salesItemLineDetail.AnyIntuitObject = item.Price_Per_Unit;
                salesItemLineDetail.ItemElementName = ItemChoiceType.UnitPrice;
                line.AnyIntuitObject = salesItemLineDetail;
                line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
                line.DetailTypeSpecified = true;

                lineList.Add(line);
            });

            invoice.Line = lineList.ToArray();
            int invoiceNumberLength = invoiceNumber.Length > 31 ? 31 : invoiceNumber.Length;
            int poNumberLength = poNumber.Length > 31 ? 31 : poNumber.Length;
            int jobDescriptionLength = jobDescription.Length > 31 ? 31 : jobDescription.Length;

            invoice.CustomField = new CustomField[]
            {
                new CustomField() { Name = "FT Invoice", Type = CustomFieldTypeEnum.StringType, AnyIntuitObject = invoiceNumber.Substring(0, invoiceNumberLength), DefinitionId = "1" },
                new CustomField() { Name = "PO Number", Type = CustomFieldTypeEnum.StringType, AnyIntuitObject = poNumber.Substring(0, poNumberLength), DefinitionId = "2" },
                new CustomField() { Name = "Job Name", Type = CustomFieldTypeEnum.StringType, AnyIntuitObject = jobDescription.Substring(0, jobDescriptionLength), DefinitionId = "3" }
            };

            // Step 5: Set other properties such as Total Amount, Due Date, Email status and Transaction Date
            //invoice.ShipDate = DateTime.Now;

            invoice.DueDate = DateTime.UtcNow.Date;
            invoice.DueDateSpecified = false;

            invoice.TotalAmt = invoiceAmount;
            invoice.TotalAmtSpecified = true;

            invoice.EmailStatus = EmailStatusEnum.NotSet;
            invoice.EmailStatusSpecified = true;

            invoice.Balance = invoiceAmount;
            invoice.BalanceSpecified = true;

            invoice.TxnDate = DateTime.UtcNow.Date;
            invoice.TxnDateSpecified = true;
            invoice.TxnTaxDetail = new TxnTaxDetail()
            {
                TotalTax = 0,
                TotalTaxSpecified = true,
            };
            return invoice;
        }
        #endregion

        #endregion

    }
}
