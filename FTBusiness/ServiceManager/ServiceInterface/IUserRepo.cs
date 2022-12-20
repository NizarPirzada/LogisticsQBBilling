using System.Collections.Generic;

namespace FTBusiness.ServiceManager.ServiceInterface
{
    public interface IUserRepo
    {
        dynamic StoreActiveUser(string email, string fullname);
        dynamic AddUsersToCompany(int companyID, IEnumerable<int> userIDs);
        dynamic RemoveUserFromCompany(int companyID, int userID);
        dynamic GetUsersByCompany(int companyID);
        dynamic GetUsersNotInCompany(int companyID);
        dynamic GetUserRole(string email);
    }
}
