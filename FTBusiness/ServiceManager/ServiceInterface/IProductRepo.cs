using FTDTO.ApiResponse;
using FTDTO.Product;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTBusiness.ServiceManager.ServiceInterface
{
    public interface IProductRepo
    {
        dynamic GetProductDetailByProductId(int product_ID, string userEmail);
        dynamic GetProductsHistory(int productId);
        dynamic GetProductDetailByCode(string code, string userEmail);
        dynamic GetProductList(string userEmail);
        dynamic UpdateProduct(string userEmail, int itemID, string code, string description, double price);
        dynamic GetProducts(int jobId);
    }
}