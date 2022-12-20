using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Estimates = new HashSet<Estimate>();
            Jobs = new HashSet<Job>();
        }

        public int CustomerId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public bool? IsInActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public virtual ICollection<Estimate> Estimates { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
