using FTBusiness.TokenAuth;
using FTCommon.Helpers;
using FTCommon.Utils;
using FTData.Model.Entities;
using FTDTO.ApiResponse;
using FTDTO.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FTBusiness.AuthenticationHandler
{

    /// <summary>
    /// This class has the purpose to handle authenticate the user
    /// </summary>
    public class AllowAnonymous : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            foreach (IAuthorizationRequirement requirement in context.PendingRequirements.ToList())
                context.Succeed(requirement); //Simply pass all requirements

            return Task.CompletedTask;
        }
    }
}
