using AutoMapper;
using FTBusiness.AuthenticationHandler;
using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Helpers;
using FTCommon.Utils;
using FTDTO.ApiResponse;
using FTDTO.Truck;
using FTEnum.ResponseMessageEnum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace FineToonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckController : BaseController
    {
        private readonly ITruckRepo truckRepo;
        private readonly UserSessionProfileService _userSession;
        private readonly IMapper _mapper;
        public TruckController(ITruckRepo driverTypeRepo, UserSessionProfileService userSession, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(httpContextAccessor)
        {
            this.truckRepo = driverTypeRepo;
            _userSession = userSession;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("GetTruckDetailReport")]
        public ApiResponseDto GetTruckDetailReport(bool activeOnly)
        {
            return ApiOkResult(truckRepo.GetTruckDetailReport(activeOnly), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTruckTotals")]
        public ApiResponseDto GetTruckTotals(System.DateTime startDate, System.DateTime endDate)
        {
            return ApiOkResult(truckRepo.GetTruckTotals(startDate, endDate), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTruckListWithInactive")]
        public ApiResponseDto GetTruckListWithInactive()
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            return ApiOkResult(truckRepo.GetTruckListWithInactive(userEmail), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTruckList")]
        public ApiResponseDto GetTruckList()
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            return ApiOkResult(truckRepo.GetTruckList(userEmail), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTruckDetailByItemId")]
        public ApiResponseDto GetTruckDetail(int itemID)
        {
            return ApiOkResult(truckRepo.GetTruckDetail(itemID), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTruckHistory")]
        public ApiResponseDto GetTruckHistory(int truckId)
        {
            return ApiOkResult(truckRepo.GetTrucksHistory(truckId), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTruckDetailByCode")]
        public ApiResponseDto GetTruckDetail(string code)
        {
            return ApiOkResult(truckRepo.GetTruckDetail(code), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTruckTypes")]
        public ApiResponseDto GetTruckTypes()
        {
            var data = truckRepo.GetTruckTypes();
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("UpdateTruck")]
        public ApiResponseDto UpdateTruck(TruckUpdateDto truckUpdateDto)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            return ApiOkResult(truckRepo.UpdateTruck(userEmail, truckUpdateDto.TruckID, truckUpdateDto.DefaultDriverID, truckUpdateDto.Code, truckUpdateDto.Description, truckUpdateDto.IsInactive, truckUpdateDto.TruckTypeID), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Update.GetEnumDescription());
        }
    }
}
