using FTData.Model.Entities;
using FTDTO.ApiResponse;
using FTDTO.DriverType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FTBusiness.ServiceManager.ServiceInterface
{
    public interface IDriverTypeRepo
    {

        Task<List<DriverType>> GetAllDriverTypes();
        dynamic GetAllDriverTypes1();
        dynamic GetDriverListWithInactive(string userEmail);
        dynamic GetDriverList(string userEmail);
        dynamic GetEmployeeDriverReport(bool ActiveOnly);
        dynamic GetTruckHireReport(bool ActiveOnly);
        dynamic GetDriverDetail(int itemID);
        dynamic GetDriverDetail(string code);
        dynamic GetDriversHistory(int driverId);
        dynamic UpdateDriver(string userEmail, int driverID, int driverTypeID, string code, string fullName, string hireDate, int percentage, string address1, string address2, string city, string state, string zipCode, string phone, string fax, bool IsInactive);
    }
}