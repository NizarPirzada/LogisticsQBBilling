using System.Collections.Generic;

namespace FTDTO.User
{
    public class AddUsersToCompanyDto
    {
        public int CompanyID { get; set; }
        public IEnumerable<int> UserIDs { get; set; }
    }

    public class RemoveUserFromCompanyDto
    {
        public int CompanyID { get; set; }
        public int UserID { get; set; }
    }
}
