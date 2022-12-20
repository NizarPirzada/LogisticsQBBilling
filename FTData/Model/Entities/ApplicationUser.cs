using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;

#nullable disable

namespace FTData.Model.Entities
{
    public class ApplicationUser : IdentityUser
    {
      
        public string UserId { get; set; }
        //public string UserName { get; set; }
        public string UserNameNormalized => UserName?.ToUpper();
        //public string Email { get; set; }
        public string EmailNormalized => Email?.ToUpper();
        public string  QbRefreshToken { get; set; }
        public string QbToken { get; set; }
        public string ReelmId { get; set; }
        //public string PhoneNumber { get; set; }
        //public bool PhoneNumberConfirmed { get; set; }
        //public string PasswordHash { get; set; }
        //public bool TwoFactorEnabled { get; set; }
    }

}
