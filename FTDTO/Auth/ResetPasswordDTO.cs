namespace FTDTO.Auth
{
    public class ResetPasswordDTO
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
