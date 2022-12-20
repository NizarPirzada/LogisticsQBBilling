using FTData.BaseTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FTData.Model.LicenseModel.Entities
{
    public partial class Customer : BaseTrackable<long>
    {
        public Customer()
        {
            Estimates = new HashSet<Estimate>();
            Jobs = new HashSet<Job>();
        }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip_Code { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public bool? Is_InActive { get; set; }
        public virtual ICollection<Estimate> Estimates { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
