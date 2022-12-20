using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTBusiness.ServiceManager.ServiceInterface
{
    public interface ITruckRepo
    {
		dynamic GetTruckDetailReport(bool ActiveOnly);
		dynamic GetTruckTotals(DateTime startDate, DateTime endDate);
		dynamic GetTruckListWithInactive(string userEmail);
		dynamic GetTruckList(string userEmail);
		dynamic GetTruckDetail(int itemID);
		dynamic GetTruckDetail(string code);
		dynamic GetTrucksHistory(int truckId);
		dynamic GetTruckTypes();
		dynamic UpdateTruck(string userEmail, int truckID, int defaultDriverID, string code, string description, bool IsInactive, int truckTypeID);
	}
}
