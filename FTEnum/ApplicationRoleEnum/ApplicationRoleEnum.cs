using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEnum.ApplicationRoleEnum
{
    public enum ApplicationRoleEnum : byte
    {
        SuperAdmin = 1,
        CompanyAdmin,
        CompanyUser,
        ExternalUser
    }
}
