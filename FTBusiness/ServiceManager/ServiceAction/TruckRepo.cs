using FTBusiness.BaseRepository;
using FTBusiness.ServiceManager.ServiceInterface;
using FTData.Context;
using FTData.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTBusiness.ServiceManager.ServiceAction
{
	public class TruckRepo : Repository<Truck>, ITruckRepo
	{
        SLCContext currentContext;
        AdoRepository adoRepo;
        public TruckRepo(SLCContext dbContext, AdoRepository repo) : base(dbContext)
        {
            currentContext = dbContext;
            adoRepo = repo;
        }

        public dynamic GetTruckDetailReport(bool ActiveOnly)
		{
			SqlParameter[] spParams = new SqlParameter[]
			{
				new SqlParameter("@Is_Active", ActiveOnly)
			};
			return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Report_Truck_Detail", spParams);
			
		}
		public dynamic GetTruckTotals(DateTime startDate, DateTime endDate)
		{
			SqlParameter[] spParams = new SqlParameter[]
			{
				new SqlParameter("@Start_Date", startDate),
				new SqlParameter("@End_Date", endDate)
			};
			return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Truck_Totals", spParams);
			
		}
		public dynamic GetTruckListWithInactive(string userEmail)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@UserEmail", userEmail)
			};
			return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Truck_List_With_Inactive", parameters);
		}
		public dynamic GetTruckList(string userEmail)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@UserEmail", userEmail)
			};
			return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Truck_List", parameters);
		}
		public dynamic GetTruckDetail(int itemID)
		{
			SqlParameter[] spParams = new SqlParameter[]
			{
				new SqlParameter("@Truck_ID", itemID)
			};
			return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Truck_Detail", spParams);			
		}
		public dynamic GetTruckDetail(string code)
		{
			SqlParameter[] spParams = new SqlParameter[]
			{
				new SqlParameter("@Code", code)
			};
			return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Truck_Detail", spParams);			
		}
		public dynamic GetTrucksHistory(int truckId)
		{
			SqlParameter[] spParams = new SqlParameter[]
			{
				new SqlParameter("@Truck_ID", truckId)
			};
			return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Truck_History", spParams);			
		}

		public dynamic GetTruckTypes()
        {
			return adoRepo.GetResult(currentContext.Database.GetConnectionString(), "sproc_Get_Truck_Types", null);
        }

		public dynamic UpdateTruck(string userEmail, int truckID, int defaultDriverID, string code, string description, bool IsInactive, int truckTypeID)
		{
			string errorMsg = "";

			if (code.Trim().Length == 0 || code.Trim().Length > 50) errorMsg += "Code is required and must be no more than 50 characters.";
			if (description != null && description.Trim().Length == 0 || description.Trim().Length > 75) errorMsg += "Description is required and must be no more than 50 characters.";
			if (defaultDriverID < 1) errorMsg += "A default driver is required and was not supplied.";
			//Incomplete
			//if (errorMsg.Length > 0) throw new BusinessRuleViolation(errorMsg);

			SqlParameter[] spParams = new SqlParameter[]
			{
				new SqlParameter("@Truck_ID", truckID),
				new SqlParameter("@Default_Driver_ID", defaultDriverID),
				new SqlParameter("@Code", code.Trim()),
				new SqlParameter("@Description", description.Trim()),
				new SqlParameter("@Is_Inactive", IsInactive),
				new SqlParameter("@TruckTypeID", truckTypeID),
				new SqlParameter("@Creation_Date", DateTime.UtcNow),
				new SqlParameter("@Last_Updated_Date", DateTime.UtcNow),
				new SqlParameter("@UserEmail", userEmail)
			};
			return adoRepo.ExecuteNonQuery(currentContext.Database.GetConnectionString(), "sproc_Update_Truck", spParams);
		}
	}
}
