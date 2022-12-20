using FTBusiness.BaseRepository;
using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Helpers;
using FTData.Context;
using FTData.Model.Entities;
using FTDTO.ApiResponse;
using FTDTO.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTBusiness.ServiceManager.ServiceAction
{
    public class ProductRepo : Repository<Product>, IProductRepo
    {
        SLCContext currentContext;
        AdoRepository adoRepo;
        public ProductRepo(SLCContext dbContext, AdoRepository repo) : base(dbContext)
        {
            currentContext = dbContext;
            adoRepo = repo;
        }


        public dynamic GetProductDetailByProductId(int product_ID, string userEmail)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Product_ID", product_ID),
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Product_Detail", spParams);
        }

        public dynamic GetProductsHistory(int productId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Product_ID", productId)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Product_History", spParams);
        }

        public dynamic GetProductDetailByCode(string code, string userEmail)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Code", code),
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Product_Detail", spParams);
        }

        public dynamic GetProductList(string userEmail)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Master_Product_List", parameters);
        }

        public dynamic UpdateProduct(string userEmail, int itemID, string code, string description, double price)
        {
            SqlParameter[] spParams = new SqlParameter[]
           {
                new SqlParameter("@Product_ID", itemID),
                new SqlParameter("@Code", code.Trim()),
                new SqlParameter("@Description", description.Trim()),
                new SqlParameter("@Price", price),
                new SqlParameter("@Creation_Date", DateTime.UtcNow),
                new SqlParameter("@Last_Updated_Date", DateTime.UtcNow),
                new SqlParameter("@UserEmail", userEmail)
           };
            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Update_Product", spParams);
        }


        public dynamic GetProducts(int jobId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Job_ID", jobId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Job_Product_List", spParams);
        }
    }
}
