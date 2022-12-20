using System;

namespace FTBusiness.ServiceManager.ServiceInterface
{
    public interface IReportRepo
    {
        dynamic GetEmployeePayrollGenerated(string userEmail, int paymentID);
        dynamic GetEmployeePayroll(string userEmail, int driverID, DateTime startDate, DateTime endDate);
        dynamic GetTruckHireReport(string userEmail, int invoiceID);
        dynamic GetTruckHireReports(string userEmail, DateTime startDate, DateTime endDate, int? invoiceID = null);
        dynamic GetMoneyOutReport(string userEmail, DateTime startDate, DateTime endDate);
    }
}
