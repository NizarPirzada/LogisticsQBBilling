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
    public class JobRepo : Repository<Job>, IJobRepo
    {
        SLCContext currentContext;
        AdoRepository adoRepo;
        public JobRepo(SLCContext dbContext, AdoRepository repo) : base(dbContext)
        {
            currentContext = dbContext;
            adoRepo = repo;
        }

        public dynamic GetJobs(int customerId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Customer_ID", customerId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Job_List", spParams);
        }

        public dynamic GetJobProfit(int jobId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Job_ID", jobId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Job_Profit_Report", spParams);
        }

        public dynamic GetJobProductDetails(int jobProductId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Job_Product_ID", jobProductId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Job_Product_Detail", spParams);
        }

        public dynamic GetJobListWithComplete1(int customerId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Customer_ID", customerId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Job_List_Including_Complete", spParams);
        }

        public dynamic GetJobDetails(int jobId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Job_ID", jobId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Job_Detail", spParams);
        }

        public dynamic GetJobDetails(string code, int customerId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Code", code),
                new SqlParameter("@Customer_ID", customerId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Job_Detail", spParams);
        }

        public dynamic GetJobByCode(string code, string userEmail)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Code", code),
                new SqlParameter("@UserEmail", userEmail)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Job_By_Code", parameters);
        }

        public dynamic GetJobsHistory(int jobId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Job_ID", jobId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Job_History", spParams);
        }


        public dynamic UpdateJobProduct(string userEmail, int itemId, int jobId, int productId, string code, string description, double price, double driverPrice)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Job_Product_ID", itemId),
                new SqlParameter("@Job_ID", jobId),
                new SqlParameter("@Product_ID", productId),
                new SqlParameter("@Code", code),
                new SqlParameter("@Description", description),
                new SqlParameter("@Price", price),
                new SqlParameter("@Driver_Price", driverPrice),
                new SqlParameter("@Creation_Date", DateTime.UtcNow),
                new SqlParameter("@Last_Updated_Date", DateTime.UtcNow),
                new SqlParameter("@UserEmail", userEmail)
            };

            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Update_Job_Product", spParams);
        }


        public dynamic UpdateJob(string userEmail, int jobId, int customerId, string code, string description, string poNumber, bool isComplete, string awardedDate, string location)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Job_ID", jobId),
                new SqlParameter("@Customer_ID", customerId),
                new SqlParameter("@Code", code),
                new SqlParameter("@Description", description),
                new SqlParameter("@PO_Number", poNumber),
                new SqlParameter("@Is_Complete", isComplete),
                new SqlParameter("@Awarded_Date", awardedDate),
                new SqlParameter("@Location", location),
                new SqlParameter("@Creation_Date", DateTime.UtcNow),
                new SqlParameter("@Last_Updated_Date", DateTime.UtcNow),
                new SqlParameter("@UserEmail", userEmail)
            };

            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Update_Job", spParams);
        }


    }
}
