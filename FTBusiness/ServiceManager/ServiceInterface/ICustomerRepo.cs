using FTDTO.ApiResponse;
using FTDTO.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FTBusiness.ServiceManager.ServiceInterface
{
    public interface ICustomerRepo
    {
        dynamic GetCustomers(string userEmail);
        dynamic GetCustomerListWithInactive(string uerEmail);
        dynamic GetCustomerDetails(int customerId);
        dynamic GetCustomerDetails(string code);
        dynamic GetCustomersHistory(int customerId);
        dynamic GetCustomerJobPricings();
        dynamic UpdateCustomer(string userEmail, int customerId, string code, string description, string address1, string address2, string city, string state, string zipCode, string phone, string fax, string PaymentTerms, bool IsInActive);
    }
}