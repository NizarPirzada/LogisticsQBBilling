using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class Estimate
    {
        public int EstimateId { get; set; }
        public int CustomerId { get; set; }
        public string Code { get; set; }
        public DateTime EnteredDate { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public decimal? Total { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int CreatedById { get; set; }
        //public int CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
