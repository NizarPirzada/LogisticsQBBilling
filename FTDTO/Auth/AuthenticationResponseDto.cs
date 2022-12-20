namespace FTDTO.Auth
{
    public class AuthenticationResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string ReelmId { get; set; }
        public bool IsAuthenticated { get; set; }

        public string UserGuid { get; set; }
        public string UserCode { get; set; }
        public UserDTO User { get; set; }
    }
}
