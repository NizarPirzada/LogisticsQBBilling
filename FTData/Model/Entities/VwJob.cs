using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class VwJob
    {
        public int JobId { get; set; }
        public int CustomerId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string PoNumber { get; set; }
        public bool IsComplete { get; set; }
        public DateTime AwardedDate { get; set; }
        public string Location { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerDescription { get; set; }
        public string CodeDescription { get; set; }
    }
}
