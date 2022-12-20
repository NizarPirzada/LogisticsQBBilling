using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Utils;
using FTDTO.ApiResponse;
using FTDTO.Product;
using FTEnum.ResponseMessageEnum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FineToonAPI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductRepo productRepo;
        public ProductController(IProductRepo productRepo, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.productRepo = productRepo;
        }

        [HttpPost]
        [Route("UpdateProduct")]
        public ApiResponseDto UpdateProduct(ProductDTO model)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            var response = productRepo.UpdateProduct(userEmail, model.ProductId, model.Code, model.Description, model.Price);
            //if (response.Status is 1)
            return ApiOkResult(response, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
            //else
            //  return ApiOkResult(response.Data, (int)HttpStatusCode.BadRequest, false, response.Message);
        }

        [HttpGet]
        [Route("GetProductDetailByProductId")]
        public ApiResponseDto GetProductDetailByProductId(int id)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            return ApiOkResult(productRepo.GetProductDetailByProductId(id, userEmail), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetProductHistory")]
        public ApiResponseDto GetProductHistory(int productId)
        {
            return ApiOkResult(productRepo.GetProductsHistory(productId), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetProductDetailByCode")]
        public ApiResponseDto GetProductDetailByCode(string code)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            return ApiOkResult(productRepo.GetProductDetailByCode(code, userEmail), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetProductByJobId")]
        public ApiResponseDto GetProductByJobId(int jobId)
        {
            var data = productRepo.GetProducts(jobId);
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        //[HttpPost]
        //[Route("SaveUpdateProduct")]
        //public async Task<ApiResponseDto> SaveProduct(ProductDTO model)
        //{
        //    model.UserId = SessionUser.LicenseUserId;
        //    var response = await productRepo.SaveUpdateProduct(model);
        //    if (response.Status is 1)
        //        return ApiOkResult(response.Data, (int)HttpStatusCode.OK, true, response.Message);
        //    else
        //        return ApiOkResult(response.Data, (int)HttpStatusCode.BadRequest, false, response.Message);
        //}

        [HttpGet]
        [Route("GetProductList")]
        public ApiResponseDto GetProductList()
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            return ApiOkResult(productRepo.GetProductList(userEmail), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        //[HttpGet]
        //[Route("GetProductList")]
        //public async Task<ApiResponseDto> GetProductList()
        //{
        //    return ApiOkResult(await productRepo.GetProducts(), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        //}
        //[HttpDelete("RemoveProduct/{id}")]
        //public async Task<ApiResponseDto> Remove(long id)
        //{
        //    var res = await productRepo.RemoveProduct(id, SessionUser.LicenseUserId);
        //    if (res)
        //        return ApiOkResult(res, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Deleted.GetEnumDescription());
        //    return ApiOkResult(null, (int)HttpStatusCode.BadRequest, false, ResponseMessagesEnum.Error.GetEnumDescription());
        //}

    }
}
