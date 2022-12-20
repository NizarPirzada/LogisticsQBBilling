using FTDTO.ApiResponse;
using FTDTO.Estimate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FTBusiness.ServiceManager.ServiceInterface
{
    public interface IEstimateRepo
    {
        public dynamic GetEstimates();
        public dynamic GetEstimateDetails(int estimateId);
        public dynamic GetEstimateDetails(string code);
        public dynamic UpdateEstimate(int estimateId, int customerId, string code, string phone, string location, string description, double total, string expirationDate, string userEmail);
        public dynamic DeleteEstimate(int estimateId);
    }
}