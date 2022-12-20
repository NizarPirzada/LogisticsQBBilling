using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTData.Model.LicenseModel.Entities
{
    public class LicenseUserRole: IdentityRole<long>
    {
        public LicenseUserRole(): base()
        {
        }
        public LicenseUserRole(string roleName) : base(roleName)
        {

        }
    }
}
