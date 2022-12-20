using FTBusiness.BaseRepository;
using FTBusiness.ServiceManager.ServiceInterface;
using FTData.Context;
using FTData.Model.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace FTBusiness.ServiceManager.ServiceAction
{

    public class ReportRepo : Repository<Payment>, IReportRepo
    {
        readonly SLCContext currentContext;
        readonly AdoRepository adoRepo;
        public ReportRepo(SLCContext dbContext, AdoRepository repo) : base(dbContext)
        {
            currentContext = dbContext;
            adoRepo = repo;
        }

        public dynamic GetEmployeePayrollGenerated(string userEmail, int paymentID)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@UserEmail", userEmail),
                new SqlParameter("@PaymentID", paymentID)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Employee_Payroll_Generated", spParams);
        }

        public dynamic GetEmployeePayroll(string userEmail, int driverID, DateTime startDate, DateTime endDate)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@UserEmail", userEmail),
                new SqlParameter("@DriverID", driverID),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Employee_Payroll", spParams);
        }

        public dynamic GetTruckHireReport(string userEmail, int invoiceID)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@UserEmail", userEmail),
                new SqlParameter("@InvoiceID", invoiceID)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Payment_Detail", spParams);
        }

        public dynamic GetTruckHireReports(string userEmail, DateTime startDate, DateTime endDate, int? invoiceID = null)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserEmail", userEmail),
                new SqlParameter("@InvoiceID", invoiceID),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Truck_Hire_Reports", parameters);
        }

        public dynamic GetMoneyOutReport(string userEmail, DateTime startDate, DateTime endDate)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserEmail", userEmail),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            return adoRepo.GetResults(currentContext.Database.GetConnectionString(), "sproc_Get_Money_Out_Report", parameters);
        }
    }
}
