using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class VwReportTruckHire
    {
        public int? TruckId { get; set; }
        public string TruckCode { get; set; }
        public string TruckDescription { get; set; }
        public int DriverId { get; set; }
        public string FullName { get; set; }
        public string Code { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public bool? IsInactive { get; set; }
    }
}
