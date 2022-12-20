using FTBusiness.BaseRepository;
using FTBusiness.ServiceManager.ServiceInterface;
using FTData.Context;
using FTData.Model.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace FTBusiness.ServiceManager.ServiceAction
{
    public class PaymentRepo : Repository<Payment>, IPaymentRepo
    {
        readonly SLCContext currentContext;
        readonly AdoRepository adoRepo;
        public PaymentRepo(SLCContext dbContext, AdoRepository repo) : base(dbContext)
        {
            currentContext = dbContext;
            adoRepo = repo;
        }

        public dynamic ViewPaymentBeforeCreate(int driverId, DateTime startDate, DateTime endDate)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Driver_ID", driverId),
                new SqlParameter("@Start_Date", startDate),
                new SqlParameter("@End_Date", endDate)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Payment_Before_Create", spParams);
        }

        public dynamic CreatePayment(int driverType, int driverId, DateTime startDate, DateTime endDate)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@DriverType", driverType),
                new SqlParameter("@Driver_ID", driverId),
                new SqlParameter("@Start_Date", startDate),
                new SqlParameter("@End_Date", endDate)
            };
            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Create_Payment", spParams);
        }

        public dynamic FindPayments(int driverId, DateTime startDate, DateTime endDate)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Driver_ID", driverId),
                new SqlParameter("@Start_Date", startDate),
                new SqlParameter("@End_Date", endDate)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Search_Payments", spParams);
        }

        public dynamic GetDriverDetailForBillPayment(int driverType, int driverId, DateTime startDate, DateTime endDate)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Driver_ID", driverId),
                new SqlParameter("@Start_Date", startDate),
                new SqlParameter("@End_Date", endDate),
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Driver_Detail_For_Bill_Payment", parameters);
        }

        public dynamic GetPayableTickets(int driverType, DateTime startDate, DateTime endDate, string userEmail)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Start_Date", startDate),
                new SqlParameter("@End_Date", endDate),
                new SqlParameter("@UserEmail", userEmail)
            };
            if (driverType == 1)
            {
                return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Employee_Available_Payments", spParams);
            }
            else
                return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Truck_Hire_Available_Payments", spParams);
        }

        public dynamic GetPaymentDetail(int payment_ID)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Payment_ID", payment_ID)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Payment_Detail", spParams);
        }
    }
}
