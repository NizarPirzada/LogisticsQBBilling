using FTBusiness.BaseRepository;
using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Helpers;
using FTData.Context;
using FTData.Model.Entities;
using FTDTO.ApiResponse;
using FTDTO.Customer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTDTO.QuickBook;

namespace FTBusiness.ServiceManager.ServiceAction
{
    public class CompanyRepo : Repository<CompanyInformation>, ICompanyRepo
    {
        SLCContext currentContext;
        AdoRepository adoRepo;
        public CompanyRepo(SLCContext dbContext, AdoRepository repo) : base(dbContext)
        {
            currentContext = dbContext;
            adoRepo = repo;
        }

        public dynamic GetAllCompanies()
        {
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_All_Companies", null);
        }

        public dynamic GetCompanyDetails(string userEmail)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Company_Information", parameters);
        }

        public dynamic GetActiveCompany(string userEmail)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Active_Company", parameters);
        }

        public dynamic GetQuickBookDetails(string userEmail = null)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_QuickBook_Info", parameters);
        }

        public dynamic UpdateCompany(int userId, int companyId, string companyName, string address1, string address2, string city, string state, string zipCode, string phone, string fax, string url)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Company_Information_ID", companyId),
                new SqlParameter("@Company_Name", companyName),
                new SqlParameter("@Address_1", address1),
                new SqlParameter("@Address_2", address2),
                new SqlParameter("@City", city),
                new SqlParameter("@State", state),
                new SqlParameter("@Zip_Code", zipCode),
                new SqlParameter("@Phone", phone),
                new SqlParameter("@Fax", fax),
                new SqlParameter("@URL", url),
                new SqlParameter("@Created_By", userId),
                new SqlParameter("@Creation_Date", DateTime.UtcNow),
                new SqlParameter("@Updated_By", userId),
                new SqlParameter("@Last_Updated_Date", DateTime.UtcNow)
            };

            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Update_Company_Information", spParams);
        }

        public dynamic UpdateCompanyPartial(string userEmail, QbInfoDTO qbInfo)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@UserEmail", userEmail),
                new SqlParameter("@QbAccessToken", qbInfo.AccessToken),
                new SqlParameter("@QbRefreshToken", qbInfo.RefreshToken),
                new SqlParameter("@QbAccessTokenExpiresIn", qbInfo.AccessTokenExpiresIn),
                new SqlParameter("@QbRefreshTokenExpiresIn", qbInfo.RefreshTokenExpiresIn),
                new SqlParameter("@QbTokenUpdateDate", qbInfo.TokenUpdateDate),
                new SqlParameter("@RealmID", qbInfo.RealmID),
            };

            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Update_Company_Information_Partial", spParams);
        }

        public dynamic SetCompanyAsActive(int companyID, string userEmail)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", companyID),
                new SqlParameter("@UserEmail", userEmail)
            };

            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Set_Company_As_Active", parameters);
        }
    }
}
