using FTBusiness.BaseRepository;
using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Helpers;
using FTData.Context;
using FTData.Model.Entities;
using FTDTO.ApiResponse;
using FTDTO.Customer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTBusiness.ServiceManager.ServiceAction
{
    public class ApplicationUserRepo : Repository<ApplicationUser>, IApplicationUserRepo
    {
        SLCContext currentContext;
        AdoRepository adoRepo;
        public ApplicationUserRepo(SLCContext dbContext, AdoRepository repo) : base(dbContext)
        {
            currentContext = dbContext;
            adoRepo = repo;
        }

        public dynamic GetUsers()
        {
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Application_User_List", null);
        }

        public dynamic GetUserDetails(int userId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
               new SqlParameter("@User_ID", userId)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Application_User_Detail", spParams);
        }

        public dynamic GetUserDetails(string code)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
               new SqlParameter("@GetUserDetail", code)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Application_User_Detail", spParams);
        }

        public dynamic GetApplicationSettings(string applicationSetting)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
               new SqlParameter("@Application_Setting_ID", applicationSetting)
            };

            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Application_Setting", null);
        }

        public dynamic UpdateUser(int userId, string fullName, string logonId, string email, bool isSystemAdministrator)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Application_User_ID", userId),
                new SqlParameter("@Logon_ID", fullName),
                new SqlParameter("@Full_Name", logonId),
                new SqlParameter("@Email_Address", email),
                new SqlParameter("@Is_System_Administrator", isSystemAdministrator),
                new SqlParameter("@Created_By", userId),
                new SqlParameter("@Creation_Date", DateTime.UtcNow),
                new SqlParameter("@Updated_By", userId),
                new SqlParameter("@Last_Updated_Date", DateTime.UtcNow)
            };

            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Update_Application_User", spParams);
        }
    }
}
