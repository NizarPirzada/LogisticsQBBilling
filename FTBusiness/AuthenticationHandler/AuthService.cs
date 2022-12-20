using FTBusiness.TokenAuth;
using FTCommon.Helpers;
using FTCommon.Utils;
using FTData.Model.Entities;
using FTDTO.ApiResponse;
using FTDTO.Auth;
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
    public class AuthService : IAuthService
    {
        //var LicenseUser = new IdentityUser { UserName = "", Email = ""};

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserSessionProfileService _userSession;

        public AuthService(UserManager<ApplicationUser> userManager,
                           SignInManager<ApplicationUser> signInManager,
                           UserSessionProfileService userSession,
        ITokenService tokenService)
        {
            this._userManager = userManager;
            this._tokenService = tokenService;
            this._signInManager = signInManager;
            _userSession = userSession;
        }
        //public async Task<ResponseDTO<AuthenticationResponseDto>> ValidateUserAsync(UserLoginDto model)
        //{
            
        //    var user = _userManager.Users
        //        .FirstOrDefault(p => p.EmailAddress == model.Email);
        //    if (user != null)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);
        //        if (!result.Succeeded)
        //        {
        //            var res = ValidateGenericPassword(model.Password);
        //            if (res)
        //            {
        //                var _roles = await _userManager.GetRolesAsync(user);
        //                var claims = AddUpdateUserClaims(_roles, user);
        //                var accessToken = _tokenService.GenerateAccessToken(claims);
        //                var refreshToken = _tokenService.GenerateRefreshToken();
        //                return Responses.OK("User Logged in Successfully", new AuthenticationResponseDto
        //                {
        //                    AccessToken = accessToken,
        //                    RefreshToken = refreshToken,
        //                    IsAuthenticated = true,
        //                    User = GetUserDTO(user, _roles)
        //                });
        //            }
        //        }
        //        if (result.Succeeded)
        //        {
        //            var _roles = await _userManager.GetRolesAsync(user);
        //            var claims = AddUpdateUserClaims(_roles, user);
        //            var accessToken = _tokenService.GenerateAccessToken(claims);
        //            var refreshToken = _tokenService.GenerateRefreshToken();
        //            return Responses.OK("User Logged in Successfully", new AuthenticationResponseDto
        //            {
        //                AccessToken = accessToken,
        //                RefreshToken = refreshToken,
        //                IsAuthenticated = true,
        //                User = GetUserDTO(user, _roles)
        //            });
        //        }
        //    }

        //    return Responses.BadRequest("Invalid username or Password", new AuthenticationResponseDto { IsAuthenticated = false, User = null });
        //    //return Responses.OK("Invalid username or Password", new AuthenticationResponseDto { IsAuthenticated = true, User = null });
        //}
        //public async Task<UserDTO> GetCurrentUserDetails()
        //{
        //    //var userId = Convert.ToInt64(_userSession.GetUserModel().LicenseUserId);
        //    var userId = _userSession.GetUserModel().LicenseUserId;
        //    var user = _userManager.Users
        //      .FirstOrDefault(p => p.Id == userId);
        //    if (user != null)
        //    {
        //        var _roles = await _userManager.GetRolesAsync(user);
        //        return GetUserDTO(user, _roles);
        //    }
        //    return null;
        //}
        public async Task<AuthenticationResponseDto> RegisterUser(UserRegisterDto model)
        {
            try
            {
                
                var user = new ApplicationUser()
                {
                    UserName = model.UserName.ToLower(),
                    Email = model.Email                    
                };
                var data = await _userManager.CreateAsync(user, model.Password);

                return new AuthenticationResponseDto
                {
                    
                    IsAuthenticated = data.Succeeded,
                    UserGuid = ""
                };
            }
            catch (Exception)
            {
                return new AuthenticationResponseDto
                {
                    IsAuthenticated = false,
                    UserGuid = String.Empty
                };
            }
        }

        public async Task<AuthenticationResponseDto> RegisterUserQB(UserRegisterDto model)
        {
            try
            {

                var user = new ApplicationUser()
                {
                    UserName = model.UserName.ToLower(),
                    Email = model.Email,
                    ReelmId= model.ReelmId,
                    QbRefreshToken = model.QbRefreshToken,
                    QbToken = model.QbAccessToken
                };
                var data = await _userManager.CreateAsync(user, model.Password);

                return new AuthenticationResponseDto
                {

                    IsAuthenticated = data.Succeeded,
                    UserGuid = ""
                };
            }
            catch (Exception)
            {
                return new AuthenticationResponseDto
                {
                    IsAuthenticated = false,
                    UserGuid = String.Empty
                };
            }
        }
        public async Task<AuthenticationResponseDto> CheckEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return new AuthenticationResponseDto
                {
                    IsAuthenticated = true,
                };
            }
            return new AuthenticationResponseDto
            {
                IsAuthenticated = false
            };
        }
        public async Task<AuthenticationResponseDto> CheckUserName(string username)
        {
            if (string.IsNullOrEmpty(username))
                return new AuthenticationResponseDto
                {
                    IsAuthenticated = false
                };
            var user = await _userManager.FindByNameAsync(username.ToLower());
            if (user != null)
            {
                return new AuthenticationResponseDto
                {
                    IsAuthenticated = true,
                    AccessToken = user.QbToken,
                    RefreshToken = user.QbRefreshToken,
                    ReelmId = user.ReelmId,
                    User = new UserDTO() {
                        Email = user.Email,
                        FirstName = user.UserName } 
                };
            }
            return new AuthenticationResponseDto
            {
                IsAuthenticated = false
            };
        }
        private List<Claim> AddUpdateUserClaims(IList<string> _roles, IdentityUser user, bool authorizeuser = false)
        {
            var claims = new List<Claim>()
                    {
                       new Claim("UserRole", _roles.Count > 0 ? _roles.FirstOrDefault() : string.Empty),
                       new Claim("UserId", user.Id.ToString()),
                       new Claim("Username", user.UserName ?? string.Empty),
                       new Claim("Email", user.Email),
                      // new Claim("FirstName", user.FirstName),
                       //new Claim("LastName", user.LastName),
                    };
            return claims;
        }

        private UserDTO GetUserDTO(IdentityUser user, IList<string> _roles)
        {
            return new UserDTO()
            {
                Email = user.Email,
                //FirstName = user.FirstName,
                Id = user.Id.ToString(),
                //LastName = user.LastName,
                Role = _roles.FirstOrDefault(),
            };
        }

        //public async Task<bool> GenerateForgotPasswordToken(string email, bool isFromAdmin)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);
        //    if (user == null)
        //        return false;
        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //    var domain = isFromAdmin ? UtilityHelper._config["DomainConfiguration:SuperAdmin"] : UtilityHelper._config["DomainConfiguration:Portal"] + $"#/auth/new-password?userid={user.Id}&token={token}";
        //    var url = domain + $"#/auth/new-password?userid={user.Id}&token={token}";
        //    var body = $"Please <a href='" + url + "'>click here</a> to set new password";
        //    await _emailService.SendEmailUsingSendGrid(user.Email, "Reset Password", body);
        //    return true;
        //}

        private bool ValidateGenericPassword(string password)
        {
            var genericpassword = UtilityHelper._config["GenericPassword:Password"];
            if (genericpassword == password)
                return true;
            return false;
        }
        //public async Task<(bool, string)> ResetPasswordWithToken(ResetPasswordDTO model)
        //{
        //    var user = await _userManager.FindByIdAsync(model.UserId);
        //    if (user == null)
        //        return (false, "User not found");

        //    if (user != null && !string.IsNullOrWhiteSpace(model.Token))
        //    {
        //        model.Token = model.Token.Replace(" ", "+");
        //        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
        //        if (result.Succeeded)
        //        {
        //            user.EmailConfirmed = true;
        //            await _userManager.UpdateAsync(user);
        //            return (true, "Password changed Successfully");
        //        }
        //        else
        //            return (false, "Invalid or Expired token");
        //    }
        //    else
        //        return (false, "Invalid or Expired token");
        //}
    }
}
