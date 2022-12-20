using System;

namespace FTBusiness.ServiceManager.ServiceInterface
{
    public interface IPaymentRepo
    {
        dynamic CreatePayment(int driverType, int driverId, DateTime startDate, DateTime endDate);
        dynamic FindPayments(int driverId, DateTime startDate, DateTime endDate);
        dynamic GetDriverDetailForBillPayment(int driverType, int driverId, DateTime startDate, DateTime endDate);
        dynamic GetPayableTickets(int driverType, DateTime startDate, DateTime endDate, string userName);
        dynamic GetPaymentDetail(int payment_ID);
        dynamic ViewPaymentBeforeCreate(int driverId, DateTime startDate, DateTime endDate);
    }
}
