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

namespace FTBusiness.ServiceManager.ServiceAction
{
    public class CustomerRepo : Repository<Customer>, ICustomerRepo
    {
        SLCContext currentContext;
        AdoRepository adoRepo;
        public CustomerRepo(SLCContext dbContext, AdoRepository repo) : base(dbContext)
        {
            currentContext = dbContext;
            adoRepo = repo;
        }

        public dynamic GetCustomers(string userEmail)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Customer_List", parameters);
        }

        public dynamic GetCustomerListWithInactive(string userEmail)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Customer_List_With_Inactive", parameters);
        }

        public dynamic GetCustomerDetails(int customerId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
               new SqlParameter("@Customer_ID", customerId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Customer_Detail", spParams);
        }

        public dynamic GetCustomerDetails(string code)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
               new SqlParameter("@Code", code)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Customer_Detail", spParams);
        }

        public dynamic GetCustomersHistory(int customerId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
               new SqlParameter("@Customer_ID", customerId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Customer_History", spParams);
        }

        public dynamic GetCustomerJobPricings()
        {
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Report_Customer_Job_Pricing", null);
        }

        public dynamic UpdateCustomer(string userEmail, int customerId, string code, string description, string address1, string address2, string city, string state, string zipCode, string phone, string fax, string PaymentTerms, bool IsInActive)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Customer_ID", customerId),
                new SqlParameter("@Code", code),
                new SqlParameter("@Description", description),
                new SqlParameter("@Address_1", address1),
                new SqlParameter("@Address_2", address2),
                new SqlParameter("@City", city),
                new SqlParameter("@State", state),
                new SqlParameter("@Zip_Code", zipCode),
                new SqlParameter("@Phone", phone),
                new SqlParameter("@Fax", fax),                
                new SqlParameter("@Is_InActive", IsInActive),
                new SqlParameter("@Creation_Date", DateTime.UtcNow),
                new SqlParameter("@Last_Updated_Date", DateTime.UtcNow),
                new SqlParameter("@UserEmail", userEmail)
            };

            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Update_Customer", spParams);
        }
    }
}
