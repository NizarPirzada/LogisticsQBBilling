using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class VwEstimate
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
        public string CustomerDescription { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string FullName { get; set; }
    }
}
