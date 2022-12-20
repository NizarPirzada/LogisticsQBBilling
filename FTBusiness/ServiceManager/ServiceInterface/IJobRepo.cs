using FTDTO.ApiResponse;
using FTDTO.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FTBusiness.ServiceManager.ServiceInterface
{
    public interface IJobRepo
    {
        dynamic GetJobs(int customerId);
        dynamic GetJobProfit(int jobId);
        dynamic GetJobProductDetails(int jobProductId);
        dynamic GetJobListWithComplete1(int customerId);
        dynamic GetJobDetails(int jobId);
        dynamic GetJobDetails(string code, int customerId);
        dynamic GetJobByCode(string code, string userEmail);
        dynamic GetJobsHistory(int jobId);
        dynamic UpdateJobProduct(string userEmail, int itemId, int jobId, int productId, string code, string description, double price, double driverPrice);
        dynamic UpdateJob(string userEmail, int jobId, int customerId, string code, string description, string poNumber, bool isComplete, string awardedDate, string location);
    }
}