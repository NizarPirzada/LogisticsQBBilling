using FTDTO.QuickBook;

namespace FTBusiness.ServiceManager.ServiceInterface
{
    public interface ICompanyRepo
    {
        dynamic GetAllCompanies();
        dynamic GetCompanyDetails(string userEmail);
        dynamic GetActiveCompany(string userEmail);
        dynamic GetQuickBookDetails(string userEmail = null);
        dynamic UpdateCompany(int userId, int companyId, string companyName, string address1, string address2, string city, string state, string zipCode, string phone, string fax, string url);
        dynamic UpdateCompanyPartial(string userEmail, QbInfoDTO qbInfo);
        dynamic SetCompanyAsActive(int companyID, string userEmail);
    }
}
