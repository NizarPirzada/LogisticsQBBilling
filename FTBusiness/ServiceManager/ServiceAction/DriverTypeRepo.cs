using FTBusiness.BaseRepository;
using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Helpers;
using FTData.Context;
using FTData.Model.Entities;
using FTDTO.ApiResponse;
using FTDTO.DriverType;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTBusiness.ServiceManager.ServiceAction
{
    public class DriverTypeRepo : Repository<DriverType>, IDriverTypeRepo
    {
        SLCContext currentContext;
        AdoRepository adoRepo;
        public DriverTypeRepo(SLCContext dbContext, AdoRepository repo) : base(dbContext)
        {
            currentContext = dbContext;
            adoRepo = repo;
        }


        public async Task<List<DriverType>> GetAllDriverTypes()
        {
            var query = currentContext.DriverTypes.FromSqlRaw("sproc_Get_Driver_Types");
            return await query.ToListAsync();

            //if (result?.Count() > 0)
            //{
            //    return null;
            //}
            //return null;
        }


        public dynamic GetAllDriverTypes1()
        {
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Driver_Types", null);
        }       
        public dynamic GetDriverListWithInactive(string userEmail)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Driver_List_With_Inactive", parameters);            
        }
        public dynamic GetDriverList(string userEmail)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Driver_List", parameters);
        }
        public dynamic GetEmployeeDriverReport(bool ActiveOnly)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Is_Active", ActiveOnly)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Report_Employee_Driver", spParams);
        }
        public dynamic GetTruckHireReport(bool ActiveOnly)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Is_Active", ActiveOnly)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Report_Truck_Hires", spParams);
        }

        public dynamic GetDriverDetail(int itemID)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Driver_ID", itemID)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Driver_Detail", spParams);            
        }
        public dynamic GetDriverDetail(string code)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Code", code)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Driver_Detail", spParams);
        }
        public dynamic GetDriversHistory(int driverId)
        {
            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Driver_ID", driverId)
            };
            return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Driver_History", spParams);
        }

        public dynamic UpdateDriver(string userEmail, int driverID, int driverTypeID, string code, string fullName, string hireDate, int percentage, string address1, string address2, string city, string state, string zipCode, string phone, string fax, bool IsInactive)
        {
            string errorMsg = "";

            if (code.Trim().Length == 0 || code.Trim().Length > 50) errorMsg += "Code is required and must be no more than 50 characters.";
            if (driverTypeID < 1) errorMsg += "A driver type must be selected.";
            if (fullName != null && fullName.Trim().Length == 0 || fullName.Trim().Length > 75) errorMsg += "Full Name is required and must be no more than 75 characters.";
            //Incomplete
            //if (hireDate != null && hireDate.Trim().Length > 0 && !utils.IsDate(hireDate)) errorMsg += "Hire date must be a valid date.";
            if (percentage < 1 || percentage > 100) errorMsg += "Percentage must be a whole number in the range from 1 to 100.";
            if (address1.Trim().Length > 75) errorMsg += "Address 1 can not be longer than 75 characters.";
            if (address2?.Trim().Length > 50) errorMsg += "Address 2 can not be longer than 50 characters.";
            if (city.Trim().Length > 50) errorMsg += "City can not be longer than 50 characters.";
            if (state.Trim().Length > 2) errorMsg += "State can not be longer than 2 characters.";
            if (zipCode.Trim().Length > 10) errorMsg += "Zip code can not be longer than 10 characters.";
            if (phone.Trim().Length > 15) errorMsg += "Phone can not be longer than 15 characters.";
            if (fax?.Trim().Length > 15) errorMsg += "Fax can not be longer than 15 characters.";
            //Incomplete
            //if (errorMsg.Length > 0) throw new BusinessRuleViolation(errorMsg);

            SqlParameter[] spParams = new SqlParameter[]
            {
                new SqlParameter("@Driver_ID", driverID),
                new SqlParameter("@Driver_Type_ID", driverTypeID),
                new SqlParameter("@Code", code.Trim()),
                new SqlParameter("@Full_Name", fullName.Trim()),
                new SqlParameter("@Hire_Date", hireDate !=null && hireDate.Trim().Length>0?Convert.ToDateTime(hireDate.Trim()):null),
                new SqlParameter("@Percentage", percentage != 0 ? percentage:null),
                new SqlParameter("@Address_1", address1 != null && address1.Trim().Length > 0 ? address1.Trim() :null),
                new SqlParameter("@Address_2", address2 != null && address2.Trim().Length > 0 ? address2.Trim() :null),
                new SqlParameter("@City", city != null && city.Trim().Length > 0 ? city.Trim() :null),
                new SqlParameter("@State", state != null && state.Trim().Length > 0 ? state.Trim() :null),
                new SqlParameter("@Zip_Code", zipCode != null && zipCode.Trim().Length > 0 ?zipCode.Trim() :null),
                new SqlParameter("@Phone", phone != null && phone.Trim().Length > 0 ? phone.Trim() :null ),
                new SqlParameter("@Fax", fax != null && fax.Trim().Length > 0 ? fax.Trim():null),
                new SqlParameter("@Is_Inactive", IsInactive),
                new SqlParameter("@Creation_Date", DateTime.UtcNow),
                new SqlParameter("@Last_Updated_Date", DateTime.UtcNow),
                new SqlParameter("@UserEmail", userEmail)
            };
            return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Update_Driver", spParams);
        }
    }
}
