using FTBusiness.BaseRepository;
using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Helpers;
using FTData.Context;
using FTData.Model.Entities;
using FTDTO.ApiResponse;
using FTDTO.Estimate;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTBusiness.ServiceManager.ServiceAction
{
    public class EstimateRepo : Repository<Estimate>, IEstimateRepo
    {
        SLCContext currentContext;
        AdoRepository adoRepo;
        public EstimateRepo(SLCContext dbContext, AdoRepository repo) : base(dbContext)
        {
            currentContext = dbContext;
            adoRepo = repo;
        }


        public dynamic GetEstimates()
        {
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Estimate_List", null);
        }


        public dynamic GetEstimateDetails(int estimateId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
               new SqlParameter("@Estimate_ID", estimateId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Customer_Detail", spParams);
        }

        public dynamic GetEstimateDetails(string code)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
               new SqlParameter("@Code", code)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Customer_Detail", spParams);
        }


        public dynamic UpdateEstimate(int estimateId, int customerId, string code, string phone, string location, string description, double total, string expirationDate, string userEmail)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Estimate_ID", estimateId),
                new SqlParameter("@Customer_ID", customerId),
                new SqlParameter("@Code", code),
                new SqlParameter("@Phone", phone),
                new SqlParameter("@Location", location),
                new SqlParameter("@Description", description),
                new SqlParameter("@Total", total),
                new SqlParameter("@Expiration_Date", expirationDate),
                new SqlParameter("@Creation_Date", DateTime.UtcNow),
                new SqlParameter("@Last_Updated_Date", DateTime.UtcNow),
                new SqlParameter("@UserEmail", userEmail)
            };

            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Update_Estimate", spParams);
        }

        public dynamic DeleteEstimate(int estimateId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Estimate_ID", estimateId)
            };

            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Remove_Estimate", spParams);
        }
    }
}
