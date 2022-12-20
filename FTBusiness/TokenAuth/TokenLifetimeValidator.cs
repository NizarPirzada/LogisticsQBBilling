using FTCommon.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTBusiness.TokenAuth
{
    public static class TokenLifetimeValidator
    {
        public static bool Validate(
            DateTime? notBefore,
            DateTime? expires,
            SecurityToken tokenToValidate,
            TokenValidationParameters @param
        )
        {
            using var serviceScope = ServiceActivator.GetScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
            var _tokenService = serviceScope.ServiceProvider.GetRequiredService<ITokenService>();
            var token = context.Request.Headers["Authorization"].ToString().Substring(7);
            var result = _tokenService.ValidateToken(token);
            if (result)
                return true;
            else
            {
                 
                return false;
            }
        }
    }
}
