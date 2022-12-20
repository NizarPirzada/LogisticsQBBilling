using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class VwReportJobPrice
    {
        public string CustomerCode { get; set; }
        public string CustomerDescription { get; set; }
        public string JobCode { get; set; }
        public string JobDescription { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? DriverPrice { get; set; }
        public bool? IsInActive { get; set; }
        public bool IsComplete { get; set; }
        public int CustomerId { get; set; }
        public int JobId { get; set; }
    }
}
