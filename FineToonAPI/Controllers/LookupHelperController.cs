using FTBusiness.ServiceManager.ServiceInterface;
using FTDTO.ApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FineToonAPI.Controllers
{
    public class LookupHelperController : BaseController
    {
        private readonly IDriverTypeRepo driverTypeRepo;
        public LookupHelperController(IDriverTypeRepo driverTypeRepo, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.driverTypeRepo = driverTypeRepo;
        }

        //[HttpGet("GetDriverTypeList")]
        //public async Task<ApiResponseDto> GetDriverType()
        //{
        //    var res = await driverTypeRepo.GetDriverTypes();
        //    return ApiOkResult(res.Select(x => new LookupResponseDTO<string> { Key = x.Id.ToString(), Value = x.Description }), (int)HttpStatusCode.OK, true);
        //}
    }
}
