using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTDTO.User
{
    public class UserUpdateDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string LoginId { get; set; }
        public string Email { get; set; }
        public bool IsSystemAdmin { get; set; }

    }
}
