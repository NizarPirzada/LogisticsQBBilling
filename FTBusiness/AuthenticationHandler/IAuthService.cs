using FTDTO.ApiResponse;
using FTDTO.Auth;
using System.Threading.Tasks;

namespace FTBusiness.AuthenticationHandler
{
    public interface IAuthService
    {
        Task<AuthenticationResponseDto> CheckEmail(string email);
        Task<AuthenticationResponseDto> CheckUserName(string username);
        //Task<bool> GenerateForgotPasswordToken(string email, bool isFromAdmin);
       // Task<UserDTO> GetCurrentUserDetails();
        Task<AuthenticationResponseDto> RegisterUser(UserRegisterDto model);
        //Task<(bool, string)> ResetPasswordWithToken(ResetPasswordDTO model);
        //Task<ResponseDTO<AuthenticationResponseDto>> ValidateUserAsync(UserLoginDto model);
        Task<AuthenticationResponseDto> RegisterUserQB(UserRegisterDto model);
    }
}