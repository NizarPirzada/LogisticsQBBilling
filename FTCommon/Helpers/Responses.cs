using FTDTO.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static FTCommon.Utils.Constants;

namespace FTCommon.Helpers
{
    public static class Responses
    {
        public static ResponseDTO<dynamic> NotFound()
        {
            return new ResponseDTO<dynamic>()
            {
                Status = Convert.ToInt32(HttpStatusCode.NotFound),
                Message = ResponseStrings.NotFound,
                Data = null
            };
        }

        public static ResponseDTO<dynamic> Unauthorized()
        {
            return new ResponseDTO<dynamic>()
            {
                Status = Convert.ToInt32(HttpStatusCode.Unauthorized),
                Message = ResponseStrings.Unauthorized,
                Data = null
            };
        }
        public static ResponseDTO<T> OK<T>(string message, T data)
        {
            return new ResponseDTO<T>()
            {
                Message = string.Format("{0}", message),
                Data = data
            };
        }

        public static ResponseDTO<T> OKGet<T>(string message, T data)
        {
            return new ResponseDTO<T>()
            {
                Message = string.Format("{0} get successfully", message),
                Data = data
            };
        }

        public static ResponseDTO<T> OKGetAll<T>(string message, T data)
        {
            return new ResponseDTO<T>()
            {
                Message = string.Format("{0}s get successfully", message),
                Data = data
            };
        }

        public static ResponseDTO<T> OKAdded<T>(string message, T data)
        {
            return new ResponseDTO<T>()
            {
                Message = string.Format("{0} added successfully", message),
                Data = data
            };
        }

        public static ResponseDTO<T> OKUpdated<T>(string message, T data)
        {
            return new ResponseDTO<T>()
            {
                Message = string.Format("{0} updated successfully", message),
                Data = data
            };
        }
        public static ResponseDTO<T> OKExisted<T>(string message, T data)
        {
            return new ResponseDTO<T>()
            {
                Message = string.Format("{0} already exits. Please try unique with name", message),
                Data = data,
                Status = -1
            };
        }

        public static ResponseDTO<T> OKFreeExisted<T>(string message, T data)
        {
            return new ResponseDTO<T>()
            {
                Message = string.Format("Free {0} already exits. Please add paid subscription", message),
                Data = data,
                Status = -2
            };
        }

        public static ResponseDTO<T> OKDeleted<T>(string message, T data)
        {
            return new ResponseDTO<T>()
            {
                Message = string.Format("{0} deleted successfully", message),
                Data = data
            };
        }

        public static ResponseDTO<T> NotFound<T>(string message, T data)
        {
            return new ResponseDTO<T>()
            {
                Status = Convert.ToInt32(HttpStatusCode.NotFound),
                Message = string.Format("{0} not found.", message),
                Data = data
            };
        }

        public static ResponseDTO<T> SomethingWentWrong<T>(string message, T data)
        {
            return new ResponseDTO<T>()
            {
                Status = 0,
                Message = string.Format("Something went wrong <b>{0}</b>", message),
                Data = data
            };
        }

        public static ResponseDTO<T> BadRequest<T>(string message, T data)
        {
            return new ResponseDTO<T>()
            {
                Status = Convert.ToInt32(HttpStatusCode.BadRequest),
                Message = message,
                Data = data
            };
        }
    }
}
