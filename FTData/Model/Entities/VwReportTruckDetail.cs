using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class VwReportTruckDetail
    {
        public int TruckId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool? IsInactive { get; set; }
        public string FullName { get; set; }
        public string DriverCode { get; set; }
        public int? DriverTypeId { get; set; }
    }
}
