using System;

namespace FTDTO.QuickBook
{
    public class QbInfoDTO
    {
        public int CompanyID { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long? AccessTokenExpiresIn { get; set; }
        public long? RefreshTokenExpiresIn { get; set; }
        public DateTime? TokenUpdateDate { get; set; }
        public string RealmID { get; set; }
    }
}
