using FTDTO.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTCommon.Helpers
{
    public class UserSessionProfileService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserSessionProfileService(IHttpContextAccessor _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;
        }
        public UserSessionDto GetUserModel()
        {

            UserSessionDto ob = new UserSessionDto();
            if (httpContextAccessor != null && httpContextAccessor.HttpContext!=null&& httpContextAccessor.HttpContext.User != null && httpContextAccessor.HttpContext.User.Identity != null && httpContextAccessor.HttpContext.User.Claims != null)
            {
                ob.LicenseUserId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                ob.UserRole = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserRole")?.Value;
                ob.Username = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Username")?.Value;
                ob.FirstName = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "FirstName")?.Value;
                ob.LastName = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "LastName")?.Value;
                ob.Email = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Email")?.Value;
                return ob;
            }
            return null;
        }

        

    }
}
