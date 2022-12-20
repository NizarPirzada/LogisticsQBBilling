using FTBusiness.BaseRepository;
using FTBusiness.ServiceManager.ServiceInterface;
using FTData.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;

namespace FTBusiness.ServiceManager.ServiceAction
{
    public class UserRepo : IUserRepo
    {
        private readonly SLCContext _ctx;
        private readonly AdoRepository _adoRepo;

        public UserRepo(SLCContext ctx, AdoRepository adoRepo)
        {
            _ctx = ctx;
            _adoRepo = adoRepo;
        }

        public dynamic StoreActiveUser(string email, string fullname)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@Fullname", fullname)
            };
            return _adoRepo.GetResult(_ctx.Database.GetConnectionString(), "sproc_Store_Active_User", parameters);
        }

        public dynamic AddUsersToCompany(int companyID, IEnumerable<int> userIDs)
        {
            DataTable dataUserIDs = new DataTable();
            dataUserIDs.Columns.Add("UserID");
            foreach (int userID in userIDs)
            {
                DataRow rowUserID = dataUserIDs.NewRow();
                rowUserID["UserID"] = userID;
                dataUserIDs.Rows.Add(rowUserID);
            }
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@CompanyID", companyID),
                new SqlParameter("@UserIDs", dataUserIDs)
            };
            return _adoRepo.GetResult(_ctx.Database.GetConnectionString(), "sproc_Add_Users_To_Company", parameters.ToArray());
        }

        public dynamic RemoveUserFromCompany(int companyID, int userID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", companyID),
                new SqlParameter("@UserID", userID)
            };
            return _adoRepo.GetResult(_ctx.Database.GetConnectionString(), "sproc_Remove_User_From_Company", parameters);
        }

        public dynamic GetUsersByCompany(int companyID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", companyID)
            };
            return _adoRepo.GetResult(_ctx.Database.GetConnectionString(), "sproc_Get_Users_By_Company", parameters);
        }

        public dynamic GetUsersNotInCompany(int companyID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", companyID)
            };
            return _adoRepo.GetResult(_ctx.Database.GetConnectionString(), "sproc_Get_Users_Not_In_Company", parameters);
        }

        public dynamic GetUserRole(string email)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", email)
            };
            return _adoRepo.GetResult(_ctx.Database.GetConnectionString(), "sproc_Get_User_Role", parameters);
        }
    }
}
