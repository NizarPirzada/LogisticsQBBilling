using FTBusiness.AuthenticationHandler;
using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Utils;
using FTDTO.ApiResponse;
using FTDTO.Auth;
using FTDTO.Invoice;
using FTDTO.QuickBook;
using FTEnum.ResponseMessageEnum;
using Intuit.Ipp.Data;
using Intuit.Ipp.OAuth2PlatformClient;
using Intuit.Ipp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FineToonAPI.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IMemoryCache cache;
        private readonly IConfiguration configuration;
        private readonly OAuth2Client oauth2Client;
        private readonly ICompanyRepo companyRepo;

        public AuthController(IHttpContextAccessor httpContextAccessor, ICompanyRepo customerRepo, IMemoryCache cache, IConfiguration conf) : base(httpContextAccessor)
        {
            this.companyRepo = customerRepo;
            this.cache = cache;
            this.configuration = conf;
            oauth2Client = new OAuth2Client(
               configuration.GetValue<string>("QuickBooks_Sandbox:client_id"),
               configuration.GetValue<string>("QuickBooks_Sandbox:client_secret"),
               configuration.GetValue<string>("QuickBooks_Sandbox:redirect_url"),
               configuration.GetValue<string>("QuickBooks_Sandbox:environment")
            );
        }

        [HttpPost]
        [Route("qbauth")]
        public async Task<ApiResponseDto> qbauth()
        {
            dynamic jsonObj = new { authUrl = QBOAuth() };

            string userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            dynamic dbResponse = companyRepo.GetQuickBookDetails(userEmail);
            QbInfoDTO qbInfo = dbResponse.Count != 0 ? JsonConvert.DeserializeObject<QbInfoDTO>(JsonConvert.SerializeObject(dbResponse[0])) : null;
            if (qbInfo == null || string.IsNullOrEmpty(qbInfo.RealmID) || string.IsNullOrEmpty(qbInfo.AccessToken) || string.IsNullOrEmpty(qbInfo.RefreshToken))
            {
                // init login ...
                return ApiOkResult(jsonObj, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
            }
            else
            {
                DateTime refreshTokenExpireTime = qbInfo.TokenUpdateDate.Value.AddSeconds(Convert.ToDouble(qbInfo.RefreshTokenExpiresIn)).AddMinutes(5);
                DateTime accessTokenExpireTime = qbInfo.TokenUpdateDate.Value.AddSeconds(Convert.ToDouble(qbInfo.AccessTokenExpiresIn)).AddMinutes(5);
                if (accessTokenExpireTime < DateTime.UtcNow)
                {
                    // fetch access token using refresh token ...
                    // Refresh token endpoint
                    var qbResponse = await oauth2Client.RefreshTokenAsync(qbInfo.RefreshToken);
                    if (qbResponse.HttpStatusCode != HttpStatusCode.OK)
                    {
                        // init login, fetch access token failed
                        return ApiOkResult(jsonObj, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
                    }

                    qbInfo.AccessToken = qbResponse.AccessToken;
                    qbInfo.RefreshToken = qbResponse.RefreshToken;
                    qbInfo.AccessTokenExpiresIn = qbResponse.AccessTokenExpiresIn;
                    qbInfo.RefreshTokenExpiresIn = qbResponse.RefreshTokenExpiresIn;
                    qbInfo.TokenUpdateDate = DateTime.UtcNow;
                    companyRepo.UpdateCompanyPartial(userEmail, qbInfo);
                    return ApiOkResult(qbInfo, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
                }
                else if (refreshTokenExpireTime < DateTime.UtcNow)
                {
                    // init login, refresh token expired ...
                    return ApiOkResult(jsonObj, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
                }
                else
                {
                    // return QbInfo DTO ...
                    return ApiOkResult(qbInfo, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
                }
            }

            //var a = configuration.GetValue<string>("QuickBooks_Sandbox:client_id");
            // var task = QBInit(Convert.ToString(invoiceData[0].customer_name), Convert.ToDecimal(invoiceData[0].invoice_total), Convert.ToString(invoiceData[0].po_number), Convert.ToString(invoiceData[0].invoice_number));
            //System.Threading.Tasks.Task.WaitAll(task);


            //var a = new AuthorizeResponse(authUrl?.Raw);
            //return ApiOkResult(invoiceRepo.CreateInvoice(invoice.JobID, invoice.JobProductID, invoice.StartDate, invoice.EndDate), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("qbauthwithcode")]
        public async Task<ApiResponseDto> qbauthwithcode(InvoiceQB code)
        {
            //OAuth2Client oauth2Client = new OAuth2Client("ABx5m6tcmQdht7rATp1UjbexWyQAe133TwUpXuzDIau94b6nMx", "T3iOY1x9d38oakKFKOGlfOewr2XuQjGvrC61SYc1", "http://localhost:4200/qbauth1", "sandbox");
            var tokenResp = await oauth2Client.GetBearerTokenAsync(code.code);
            if (tokenResp?.AccessToken != null && tokenResp.AccessToken != string.Empty)
            {
                try
                {
                    var data = await oauth2Client.GetUserInfoAsync(tokenResp?.AccessToken);
                    //iden(userId, claims);
                    #region test company

                    //var tok = await oauth2Client.RefreshTokenAsync(tokenResp?.RefreshToken);
                    //OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator("eyJlbmMiOiJBMTI4Q0JDLUhTMjU2IiwiYWxnIjoiZGlyIn0..ArUjFtCHbz2OqSefXDH77w.NRv2wHWwsUb9TZeCZ8ieQOF8HQOerWmUezp9IciH3pFUjt4OnI3cgH4R1MoNWvvNAdbEK2DX-6Ke1C5H9UF4bionYxtYifj66cLk9es0TB-Shjfg0yJqF9WiKH-B49mP_xm_NLQ0b00lGRIzihSGGgapBWHnADo-jd_PEGYfQ3P3wD0tU1-IpczaDacsNkQO0L3vU-LbfLcDjg-4MTuHrutXNM-e_jfHttpNI-mmZF1j80zidJQhn12IzHWC3-HFZnDPuQK_hkwStKEZO8EGfRfOBZxUqghIfvgXzHh2LDCznyzk5nVvWJtZUwFcZZb6y0s0UAnuD8Zb8-rqPAlJubdK7yxmCgawLUV3AufDzsTllpywAifW7Vp5oV7U7llFNidBFA-4x50PUO6oxRbw1ZjKKh8yr4HhXnHc8NnyQzjcX0EtW7vvBo48sNDE77GU_t8iBRmnS2439gPF3i6lFurWo9JG3rn7UWDNWww076XnEoygidD56m_3SPotkPgbSQUiRgrYXt0nSOALTSaN-QudLjPD6KEK6sC84Q2UQgUeQrwzUMFkeMj74H2t5NSnEI7jEX9aPSCi5qVEo5xpC6STpuhmI5VbTkJfsZefR_sv0bZ51v3saVXZs1eVrFuum2Yfxd-95xF3pPyIui2zK5mHkrsC4wpRvyNKmh-In8vcxyz9bcPLmglzlrD75Wwz3B0uakF5BxRooH3jjDNreCQ_wJtfEwBBUZonWWK11hrCd3EM5AM9JAkneKuVu3yf.Fe5TnadDpdZnozSzXf47eQ");
                    OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(tokenResp?.AccessToken);



                    //ServiceContext serviceContext = new ServiceContext("4620816365214064010", IntuitServicesType.QBO, oauthValidator);
                    ////serviceContext.IppConfiguration.MinorVersion.Qbo = "61";
                    //serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
                    //DataService dataService = new DataService(serviceContext);
                    //CompanyInfo cinfo1 = new CompanyInfo();
                    //cinfo1.Id = "4620816365214064010";

                    //var cdata1 = dataService.FindById(cinfo1);



                    //ServiceContext serviceContext2 = new ServiceContext("4620816365181786250", IntuitServicesType.QBO, oauthValidator);
                    ////serviceContext2.IppConfiguration.MinorVersion.Qbo = "61";
                    //serviceContext2.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
                    //DataService dataService2 = new DataService(serviceContext2);

                    //Account cinfo2 = new CompanyInfo();
                    //cinfo2.Id = "4620816365181786250";
                    //var cdata2 = dataService2.Find(cinfo2);


                    //CompanyInfo cinfo2 = new CompanyInfo();                    
                    //cinfo2.Id = "4620816365181786250";
                    //var cdata2 = dataService2.FindById(cinfo2);


                    //Invoice inv = new Invoice();
                    //inv.Id = "4620816365181786250";
                    //var invdata = dataService2.FindById(inv);

                    #endregion

                    string userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
                    var qbInfo = new QbInfoDTO()
                    {
                        AccessToken = tokenResp?.AccessToken,
                        RefreshToken = tokenResp?.RefreshToken,
                        AccessTokenExpiresIn = tokenResp?.AccessTokenExpiresIn,
                        RefreshTokenExpiresIn = tokenResp?.RefreshTokenExpiresIn,
                        TokenUpdateDate = DateTime.UtcNow,
                        RealmID = code.realmId
                    };
                    companyRepo.UpdateCompanyPartial(userEmail, qbInfo);

                    //var userdata = await authService.RegisterUserQB(new UserRegisterDto() { UserName = "adeel", Password = "Adeel123!^", 
                    //    Email = "adeel@test.com",QbAccessToken= tokenResp?.AccessToken,QbRefreshToken= tokenResp?.RefreshToken,
                    //    ReelmId = code.realmId
                    //});
                    //if (userdata != null)
                    //{

                    //}

                    //HttpContext.Session.SetString("accesstoken", tokenResp.AccessToken);
                    //HttpContext.Session.SetString("refreshtoken", tokenResp.RefreshToken);
                }
                catch (Exception ex)
                {

                }
            }
            return ApiOkResult(tokenResp, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        private string QBOAuth()
        {

            //OAuth2Client oauth2Client = new OAuth2Client("ABx5m6tcmQdht7rATp1UjbexWyQAe133TwUpXuzDIau94b6nMx", "T3iOY1x9d38oakKFKOGlfOewr2XuQjGvrC61SYc1", "http://localhost:4200/qbauth1", "sandbox");
            List<OidcScopes> scopes = new List<OidcScopes>();
            scopes.Add(OidcScopes.OpenId);
            scopes.Add(OidcScopes.Phone);
            scopes.Add(OidcScopes.Profile);
            scopes.Add(OidcScopes.Address);
            scopes.Add(OidcScopes.Email);
            scopes.Add(OidcScopes.Accounting);
            scopes.Add(OidcScopes.Payment);

            return oauth2Client.GetAuthorizationURL(scopes);

        }

        #region QuickBook Test
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

            item.Name = "Fine Toon Ticket Item-" + poNumber;
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
        #endregion

        #region create customer
        /// <summary>
        /// This API creates customer 
        /// </summary>
        /// <returns></returns>
        private Customer CreateCustomer()
        {
            Random random = new Random();
            Customer customer = new Customer();

            customer.GivenName = "Bob" + random.Next();
            customer.FamilyName = "Serling";
            customer.DisplayName = customer.CompanyName;
            return customer;
        }


        /// <summary>
        /// This API creates an Invoice
        /// </summary>
        private Invoice CreateInvoice(string realmId, Customer customer, Item item, string invoiceNumber)
        {


            // Step 1: Initialize OAuth2RequestValidator and ServiceContext
            //ServiceContext serviceContext = IntializeContext(realmId);

            // Step 2: Initialize an Invoice object
            Invoice invoice = new Invoice();
            // invoice.Deposit = new Decimal(0.00);
            //invoice.DepositSpecified = true;


            // Step 3: Invoice is always created for a customer so lets retrieve reference to a customer and set it in Invoice
            /*QueryService<Customer> querySvc = new QueryService<Customer>(serviceContext);
            Customer customer = querySvc.ExecuteIdsQuery("SELECT * FROM Customer WHERE CompanyName like 'Amy%'").FirstOrDefault();*/
            invoice.CustomerRef = new ReferenceType()
            {
                Value = customer.Id
            };


            // Step 4: Invoice is always created for an item so lets retrieve reference to an item and a Line item to the invoice
            /* QueryService<Item> querySvcItem = new QueryService<Item>(serviceContext);
            Item item = querySvcItem.ExecuteIdsQuery("SELECT * FROM Item WHERE Name = 'Lighting'").FirstOrDefault();*/
            List<Line> lineList = new List<Line>();
            Line line = new Line();
            line.Description = "Fine Toon Invoice:" + invoiceNumber;
            line.Amount = item.UnitPrice;
            line.AmountSpecified = true;

            SalesItemLineDetail salesItemLineDetail = new SalesItemLineDetail();
            salesItemLineDetail.Qty = new Decimal(1.0);
            salesItemLineDetail.ItemRef = new ReferenceType()
            {
                Value = item.Id
            };
            line.AnyIntuitObject = salesItemLineDetail;

            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;

            lineList.Add(line);
            invoice.Line = lineList.ToArray();

            // Step 5: Set other properties such as Total Amount, Due Date, Email status and Transaction Date
            invoice.DueDate = DateTime.UtcNow.Date;
            invoice.DueDateSpecified = true;


            invoice.TotalAmt = item.UnitPrice;
            invoice.TotalAmtSpecified = true;

            invoice.EmailStatus = EmailStatusEnum.NotSet;
            invoice.EmailStatusSpecified = true;

            invoice.Balance = item.UnitPrice;
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




        /// <summary>
        /// This end point will be used to get the access token on successfull authentication 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost(template: "token")]
        public ApiResponseDto Token([FromBody] UserLoginDto model)
        {
            //var response = await authService.ValidateUserAsync(model);
            //if (response.Status == 1)
            //    return ApiOkResult(response.Data, (int)HttpStatusCode.OK, true, response.Message);
            //else
            return ApiOkResult(null, (int)HttpStatusCode.BadRequest, false, "");
        }

        /*
        [HttpGet("GetCurrentUserDetails")]
        public async Task<ApiResponseDto> GetCurrentUserDetails()
        {
            //var data = await authService.GetCurrentUserDetails();
            //if (data != null)
            //    return ApiOkResult(data, (int)HttpStatusCode.OK, true);
            //else
            return ApiOkResult(null, (int)HttpStatusCode.BadRequest, false);
        }

        [HttpGet("RegisterUser")]
        public async Task<ApiResponseDto> RegisterUser()
        {
            var data = await authService.RegisterUser(new UserRegisterDto() { UserName = "adeel", Password = "Adeel123!^", Email = "adeel@test.com" });
            if (data != null)
                return ApiOkResult(data, (int)HttpStatusCode.OK, true);
            else
                return ApiOkResult(data, (int)HttpStatusCode.BadRequest, false);
        }

        [HttpGet("GetUser")]
        public async Task<ApiResponseDto> GetUser()
        {

            var data = await authService.CheckUserName("adeel");
            if (data != null)
                return ApiOkResult(data, (int)HttpStatusCode.OK, true);
            else
                return ApiOkResult(data, (int)HttpStatusCode.BadRequest, false);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("CheckEmail")]
        public async Task<ApiResponseDto> CheckEmail(string email)
        {
            var response = await authService.CheckEmail(email);
            if (response.IsAuthenticated)
                return ApiOkResult(response, (int)HttpStatusCode.BadRequest, false);
            else
                return ApiOkResult(response, (int)HttpStatusCode.OK, true);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("CheckUserName")]
        public async Task<ApiResponseDto> CheckUserName(string username)
        {
            var response = await authService.CheckUserName(username);
            if (response.IsAuthenticated)
                return ApiOkResult(response, (int)HttpStatusCode.BadRequest, false);
            else
                return ApiOkResult(response, (int)HttpStatusCode.OK, true);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("verifyCode")]
        public async Task<ApiResponseDto> CodeVerification(string username, string email)
        {
            var response = await authService.CheckUserName(username);
            if (response.IsAuthenticated)
                return ApiOkResult(response, (int)HttpStatusCode.BadRequest, false);
            else
                return ApiOkResult(response, (int)HttpStatusCode.OK, true);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPasswordWithToken")]
        public async Task<ApiResponseDto> ResetPasswordWithToken(ResetPasswordDTO model)
        {
            //var response = await authService.ResetPasswordWithToken(model);
            //if (response.Item1)
            //    return ApiOkResult(response.Item2, (int)HttpStatusCode.BadRequest, true);
            //else
            return ApiOkResult(null, (int)HttpStatusCode.OK, false);
        }
        */
    }
}
