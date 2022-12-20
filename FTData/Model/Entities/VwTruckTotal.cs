using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class VwTruckTotal
    {
        public string TruckCode { get; set; }
        public string TruckDescription { get; set; }
        public decimal? TicketTotal { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal? QuantityTotal { get; set; }
    }
}
