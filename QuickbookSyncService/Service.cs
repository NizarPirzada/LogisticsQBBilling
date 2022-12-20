using FTBusiness.BaseRepository;
using FTDTO.QuickBook;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.Exception;
using Intuit.Ipp.OAuth2PlatformClient;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace QuickbookSyncService
{
    public class Service
    {
        private readonly AdoRepository AdoRepo;
        private readonly string conString;

        private ServiceContext ServiceContext;

        public QbInfoDTO QbInfo;

        public Service(int offset = 0)
        {
            AdoRepo = new AdoRepository();
            conString = ConfigurationManager.AppSettings["ConnectionString"];

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Offset", offset)
            };
            var dbResponse = AdoRepo.GetResult(conString, "sproc_Get_QuickBook_Info", parameters);
            QbInfo = dbResponse.Count != 0 ? JsonConvert.DeserializeObject<QbInfoDTO>(JsonConvert.SerializeObject(dbResponse[0])) : null;
            if (QbInfo == null || string.IsNullOrEmpty(QbInfo.RefreshToken) || string.IsNullOrEmpty(QbInfo.RealmID))
            {
                return;
            }

            SetServiceContext();
        }

        public dynamic GetCustomersFromDb()
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", QbInfo.CompanyID)
            };
            var response = AdoRepo.GetResult(conString, "sproc_Get_Customers", parameters);
            return response;
        }

        public void UpdateQuickBookInfoInDB()
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@CompanyID", QbInfo.CompanyID),
                    new SqlParameter("@AccessToken", QbInfo.AccessToken),
                    new SqlParameter("@RefreshToken", QbInfo.RefreshToken),
                    new SqlParameter("@AccessTokenExpiresIn", QbInfo.AccessTokenExpiresIn),
                    new SqlParameter("@RefreshTokenExpiresIn", QbInfo.RefreshTokenExpiresIn),
                    new SqlParameter("@TokenUpdateDate", QbInfo.TokenUpdateDate)
            };
            AdoRepo.GetResult(conString, "sproc_Update_QuickBook_Info", parameters);
        }

        public IEnumerable<T> ExecuteQuery<T>(string query)
        {
            try
            {
                var queryService = new QueryService<T>(ServiceContext);
                return queryService.ExecuteIdsQuery(query).ToList();
            }
            catch (InvalidTokenException)
            {
                GetAccessToken();
                SetServiceContext();
                try
                {
                    QueryService<T> queryService = new QueryService<T>(ServiceContext);
                    return queryService.ExecuteIdsQuery(query).ToList();
                }
                catch (InvalidTokenException)
                {
                    // retrun empty list if quickbook authentication fails...
                    return new List<T>();
                }
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public T ExecuteInsertQuery<T>(T entity) where T : IEntity
        {
            try
            {
                var dataService = new DataService(ServiceContext);
                entity = dataService.Add<T>(entity);
                return entity;
            }
            catch (InvalidTokenException)
            {
                GetAccessToken();
                SetServiceContext();
                try
                {
                    var dataService = new DataService(ServiceContext);
                    entity = dataService.Add<T>(entity);
                    return entity;
                }
                catch (InvalidTokenException)
                {
                    return default;
                }
            }
        }

        public IEnumerable<Customer> GetCustomersByName(string name)
        {
            var qbCustomerQuery = $"SELECT * FROM Customer WHERE DisplayName = '{name}' ORDERBY MetaData.LastUpdatedTime";
            return ExecuteQuery<Customer>(qbCustomerQuery);
        }

        public Customer CreateCustomerInQuickbooks(string name)
        {
            var existingCustomers = GetCustomersByName(name);
            var customer = existingCustomers.FirstOrDefault();
            if (customer == null)
            {
                customer = new Customer
                {
                    GivenName = name,
                    DisplayName = name
                };
                customer = ExecuteInsertQuery<Customer>(customer);
            }
            return customer;
        }

        public void GetAccessToken()
        {
            string clientID = Convert.ToString(ConfigurationManager.AppSettings["ClientId"]);
            string clientSecret = Convert.ToString(ConfigurationManager.AppSettings["ClientSecret"]);
            string redirectURI = Convert.ToString(ConfigurationManager.AppSettings["RedirectUrl"]);
            string environment = Convert.ToString(ConfigurationManager.AppSettings["Environment"]);

            OAuth2Client client = new OAuth2Client(clientID, clientSecret, redirectURI, environment);

            // Refresh token endpoint
            var qbResponse = client.RefreshTokenAsync(QbInfo.RefreshToken).Result;
            if (qbResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                QbInfo.AccessToken = qbResponse.AccessToken;
                QbInfo.RefreshToken = qbResponse.RefreshToken;
                QbInfo.AccessTokenExpiresIn = qbResponse.AccessTokenExpiresIn;
                QbInfo.RefreshTokenExpiresIn = qbResponse.RefreshTokenExpiresIn;
                QbInfo.TokenUpdateDate = DateTime.UtcNow;
                UpdateQuickBookInfoInDB();
            }
        }

        public void SetServiceContext()
        {
            OAuth2RequestValidator oAuthValidator;
            try
            {
                oAuthValidator = new OAuth2RequestValidator(QbInfo.AccessToken);
            }
            catch (InvalidTokenException)
            {
                oAuthValidator = new OAuth2RequestValidator("refresh");
            }

            ServiceContext = new ServiceContext(QbInfo.RealmID, IntuitServicesType.QBO, oAuthValidator);
            //ServiceContext.IppConfiguration.MinorVersion.Qbo = "23";
            ServiceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
        }
    }
}
