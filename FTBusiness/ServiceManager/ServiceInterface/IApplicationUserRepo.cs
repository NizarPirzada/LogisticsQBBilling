using FTDTO.ApiResponse;
using FTDTO.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FTBusiness.ServiceManager.ServiceInterface
{
    public interface IApplicationUserRepo
    {
        dynamic GetUsers();
        dynamic GetUserDetails(int userId);
        dynamic GetUserDetails(string code);
        dynamic GetApplicationSettings(string applicationSetting);
        dynamic UpdateUser(int userId, string fullName, string logonId, string email, bool isSystemAdministrator);
    }
}