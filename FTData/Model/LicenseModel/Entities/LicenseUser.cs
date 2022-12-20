using FTEnum.ApplicationRoleEnum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTData.Model.LicenseModel.Entities
{
    public class LicenseUser : IdentityUser<long>
    {
        public LicenseUser()
        {
            Estimates = new HashSet<Estimate>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ApplicationRoleEnum UserRole { get; set; }
        public virtual ICollection<Estimate> Estimates { get; set; }
    }
}
