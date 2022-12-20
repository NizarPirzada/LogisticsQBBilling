using FTCommon.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTCommon.Utils
{
    public class Constants
    {
   
        public static class JWTConfiguration
        {
            public static readonly string JWTIssuer = UtilityHelper._config["Jwt:Issuer"];
            public static readonly string JWTAudience = UtilityHelper._config["Jwt:Audience"];
            public static readonly string JWTKey = UtilityHelper._config["Jwt:Key"];
        }
        public static class Blob
        {
            public static readonly string ConnectionString = UtilityHelper._config["Blob:ConnectionString"];
            public static readonly string ContainerName = UtilityHelper._config["Blob:ContainerName"];
        }
        public static class ResponseStrings
        {
            public static readonly string NotFound = "Not Found";
            public static readonly string Success = "Success";
            public static readonly string Unauthorized = "You are currently blocked. Please try to contact customer support.";
            public static readonly string InvalidCredentials = "Invalid username or password";
            public static readonly string InvalidPassword = "Invalid current password";
        }
    }
}
