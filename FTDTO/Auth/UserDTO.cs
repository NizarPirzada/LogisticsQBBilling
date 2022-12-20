namespace FTDTO.Auth
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsCompany { get; set; }
        public string CompanyId { get; set; }
    }
}