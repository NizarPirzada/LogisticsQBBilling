using AutoMapper;
using FTBusiness.ServiceManager.ServiceInterface;
using FTCommon.Helpers;
using FTCommon.Utils;
using FTDTO.ApiResponse;
using FTDTO.Driver;
using FTDTO.DriverType;
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
    public class DriverTypeController : BaseController
    {
        private readonly IDriverTypeRepo driverTypeRepo;

        private readonly UserSessionProfileService _userSession;
        private readonly IMapper _mapper;
        public DriverTypeController(IDriverTypeRepo driverTypeRepo, UserSessionProfileService userSession, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(httpContextAccessor)
        {
            this.driverTypeRepo = driverTypeRepo;
            _userSession = userSession;
            _mapper = mapper;
        }

        //[HttpPost]
        //[Route("SaveUpdateDriverType")]
        //public async Task<ApiResponseDto> SaveDriverType(DriverTypeDTO model)
        //{
        //    model.UserId = SessionUser.LicenseUserId;
        //    var response = await driverTypeRepo.SaveUpdateDriverType(model);
        //    if (response.Status is 1)
        //        return ApiOkResult(response.Data, (int)HttpStatusCode.OK, true, response.Message);
        //    else
        //        return ApiOkResult(response.Data, (int)HttpStatusCode.BadRequest, false, response.Message);
        //}

        //[HttpGet]
        //[Route("GetDriverTypeById/{id}")]
        //public async Task<ApiResponseDto> GetDriverTypeById(int id)
        //{
        //    return ApiOkResult(await driverTypeRepo.GetDriverTypeById(id), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        //}

        //[HttpGet]
        //[Route("GetDriverTypeList")]
        //public async Task<ApiResponseDto> GetDriverTypeList()
        //{
        //    //return ApiOkResult(await driverTypeRepo.GetDriverTypes(), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        //    dynamic result = null;
        //    var data = await driverTypeRepo.GetAllDriverTypes();
        //    if (data?.Count > 0)
        //    {
        //        var mappedData = _mapper.Map<List<DriverTypeDTO>>(data);
        //        result = mappedData;
        //    }
        //    return ApiOkResult(result, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());

        //}

        [HttpGet]
        [Route("GetDriverTypeList")]
        public ApiResponseDto GetDriverTypeList()
        {
            var data = driverTypeRepo.GetAllDriverTypes1();
            return ApiOkResult(data, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());

        }

        [HttpGet]
        [Route("GetDriverListWithInactive")]
        public ApiResponseDto GetDriverListWithInactive()
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            return ApiOkResult(driverTypeRepo.GetDriverListWithInactive(userEmail), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetDriverList")]
        public ApiResponseDto GetDriverList()
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            return ApiOkResult(driverTypeRepo.GetDriverList(userEmail), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetEmployeeDriverReport")]
        public ApiResponseDto GetEmployeeDriverReport(bool ActiveOnly)
        {
            return ApiOkResult(driverTypeRepo.GetEmployeeDriverReport(ActiveOnly), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetTruckHireReport")]
        public ApiResponseDto GetTruckHireReport(bool ActiveOnly)
        {
            return ApiOkResult(driverTypeRepo.GetTruckHireReport(ActiveOnly), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetDriverDetailByItemId")]
        public ApiResponseDto GetDriverDetailByItemId(int itemID)
        {
            return ApiOkResult(driverTypeRepo.GetDriverDetail(itemID), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetDriverHistory")]
        public ApiResponseDto GetDriverHistory(int itemID)
        {
            return ApiOkResult(driverTypeRepo.GetDriversHistory(itemID), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpGet]
        [Route("GetDriverDetailByCode")]
        public ApiResponseDto GetDriverDetailByCode(string code)
        {
            return ApiOkResult(driverTypeRepo.GetDriverDetail(code), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.GetRecord.GetEnumDescription());
        }

        [HttpPost]
        [Route("UpdateDriver")]
        public ApiResponseDto UpdateDriver(DriverRequestDTO driverRequestDTO)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            return ApiOkResult(driverTypeRepo.UpdateDriver(userEmail, driverRequestDTO.DriverID, driverRequestDTO.DriverTypeID, driverRequestDTO.Code,
                                                            driverRequestDTO.FullName, driverRequestDTO.HireDate, driverRequestDTO.Percentage, driverRequestDTO.Address1,
                                                            driverRequestDTO.Address2, driverRequestDTO.City, driverRequestDTO.State, driverRequestDTO.ZipCode, driverRequestDTO.Phone,
                                                            driverRequestDTO.Fax, driverRequestDTO.IsInactive), (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Update.GetEnumDescription());
        }

        //[HttpDelete("RemoveDriverType/{id}")]
        //public async Task<ApiResponseDto> Remove(long id)
        //{
        //    var res = await driverTypeRepo.RemoveDriverType(id, SessionUser.LicenseUserId);
        //    if (res)
        //        return ApiOkResult(res, (int)HttpStatusCode.OK, true, ResponseMessagesEnum.Deleted.GetEnumDescription());
        //    return ApiOkResult(null, (int)HttpStatusCode.BadRequest, false, ResponseMessagesEnum.Error.GetEnumDescription());
        //}

    }
}
