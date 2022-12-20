using FTBusiness.BaseRepository;
using FTDTO.QuickBook;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
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

namespace QBServiceConsole
{
    public class QuickBook
    {
        private readonly AdoRepository AdoRepo;
        private readonly string conString;

        private ServiceContext ServiceContext;

        public QbInfoDTO QbInfo;

        public QuickBook()
        {
            AdoRepo = new AdoRepository();
            conString = ConfigurationManager.AppSettings["connectionstring"];

            /*
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Offset", offset)
            };
            var dbResponse = AdoRepo.GetResults(conString, "sproc_Get_QuickBook_Info", parameters);
            //QbInfo = dbResponse.Count != 0 ? JsonConvert.DeserializeObject<QbInfoDTO>(JsonConvert.SerializeObject(dbResponse[0])) : null;
            if (dbResponse[0].Count == 0)
            {
                QbInfo = null;
                return;
            }
            int totalCompanies = Convert.ToInt32(dbResponse[1][0].TotalCompanies);
            if (offset >= totalCompanies)
            {
                QbInfo = null;
            }
            QbInfo = JsonConvert.DeserializeObject<QbInfoDTO>(JsonConvert.SerializeObject(dbResponse[0][0]));
            //bool isNull = QbInfo.GetType().GetProperties().Where(pi => pi.PropertyType == typeof(string)).Select(pi => (string)pi.GetValue(QbInfo)).Any(value => string.IsNullOrEmpty(value));
            if (QbInfo == null || string.IsNullOrEmpty(QbInfo.RefreshToken) || string.IsNullOrEmpty(QbInfo.RealmID))
            {
                return;
            }
            */
        }

        public IEnumerable<QbInfoDTO> GetCompaniesQuickbookDetails()
        {
            var data = AdoRepo.GetResult(conString, "sproc_Get_Companies_Quickbook_Details", null);
            IEnumerable<QbInfoDTO> companiesQuickbookDetails = JsonConvert.DeserializeObject<IEnumerable<QbInfoDTO>>(JsonConvert.SerializeObject(data));
            return companiesQuickbookDetails;
        }

        public dynamic GetServiceExecutionDateFromDB()
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", QbInfo.CompanyID)
            };
            dynamic data = AdoRepo.GetResult(conString, "sproc_Get_Service_Execution_Date", parameters);
            return data[0];
        }

        public DateTime? GetInvoiceUpdateDateFromDB()
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", QbInfo.CompanyID)
            };
            dynamic data = AdoRepo.GetResult(conString, "sproc_Get_Service_Update_Date", parameters);
            if (data.Count > 0)
            {
                return Convert.ToDateTime(data[0].UpdatedAt);
            }
            return null;
        }

        public bool SetInvoiceUpdateDateInDB(DateTime updatedAt)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", QbInfo.CompanyID),
                new SqlParameter("@UpdatedAt", updatedAt)
            };
            dynamic data = AdoRepo.GetResult(conString, "sproc_Set_Service_Invoice_Update_Date", parameters);
            return (Convert.ToString(data[0].Status) == "1");
        }

        public bool SetBillUpdateDateInDB(DateTime updatedAt)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", QbInfo.CompanyID),
                new SqlParameter("@UpdatedAt", updatedAt)
            };
            dynamic data = AdoRepo.GetResult(conString, "sproc_Set_Service_Bill_Update_Date", parameters);
            return (Convert.ToString(data[0].Status) == "1");
        }

        public bool MarkInvoiceAsPaidInDB(int invoiceNumber, int qbInvoiceID, decimal amount)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@InvoiceNumber", invoiceNumber),
                new SqlParameter("@QbInvoiceID", qbInvoiceID),
                new SqlParameter("@Amount", amount)
            };
            dynamic data = AdoRepo.GetResult(conString, "sproc_Mark_Invoice_As_Paid", parameters);
            return (Convert.ToString(data[0].Status) == "1");
        }

        public bool MarkBillAsPaidInDB(int billID, int qbBillID, decimal amount)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@BillID", billID),
                new SqlParameter("@QbBillID", qbBillID),
                new SqlParameter("@Amount", amount)
            };
            dynamic data = AdoRepo.GetResult(conString, "sproc_Mark_Bill_As_Paid", parameters);
            return (Convert.ToString(data[0].Status) == "1");
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

        public IEnumerable<Invoice> GetInvoices(DateTime? lastUpdatedAt)
        {
            // "SELECT * FROM Invoice WHERE Balance = '0' and MetaData.LastUpdatedTime > '2022-01-13' ORDERBY MetaData.LastUpdatedTime"
            string qbInvoiceQuery;
            if (lastUpdatedAt == null)
            {
                qbInvoiceQuery = "SELECT * FROM Invoice WHERE Balance = '0' ORDERBY MetaData.LastUpdatedTime";
            }
            else
            {
                string lastUpdatedAtISOString = string.Format("{0:yyyy}-{0:MM}-{0:dd}T{0:HH}:{0:mm}:{0:ss}{0:zzz}", lastUpdatedAt.Value);
                qbInvoiceQuery = $"SELECT * FROM Invoice WHERE Balance = '0' and MetaData.LastUpdatedTime > '{lastUpdatedAtISOString}' ORDERBY MetaData.LastUpdatedTime";
            }

            return ExecuteQuery<Invoice>(qbInvoiceQuery);
        }

        public IEnumerable<Bill> GetBills(DateTime? lastUpdatedAt)
        {
            string qbBillQuery;
            if (lastUpdatedAt == null)
            {
                qbBillQuery = "SELECT * FROM Bill WHERE Balance = '0' ORDERBY MetaData.LastUpdatedTime";
            }
            else
            {
                string lastUpdatedAtISOString = string.Format("{0:yyyy}-{0:MM}-{0:dd}T{0:HH}:{0:mm}:{0:ss}{0:zzz}", lastUpdatedAt.Value);
                qbBillQuery = $"SELECT * FROM Bill WHERE Balance = '0' and MetaData.LastUpdatedTime > '{lastUpdatedAtISOString}' ORDERBY MetaData.LastUpdatedTime";
            }

            return ExecuteQuery<Bill>(qbBillQuery);
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
        }

        public void GetAccessToken()
        {
            string clientID = Convert.ToString(ConfigurationManager.AppSettings["QuickBooks_Sandbox_client_id"]);
            string clientSecret = Convert.ToString(ConfigurationManager.AppSettings["QuickBooks_Sandbox_client_secret"]);
            string redirectURI = Convert.ToString(ConfigurationManager.AppSettings["QuickBooks_Sandbox_redirect_url"]);
            string environment = Convert.ToString(ConfigurationManager.AppSettings["QuickBooks_Sandbox_environment"]);

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
