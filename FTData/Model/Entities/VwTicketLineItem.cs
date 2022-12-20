using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class VwTicketLineItem
    {
        public int TicketLineItemId { get; set; }
        public int TicketId { get; set; }
        public int DriverId { get; set; }
        public int TruckId { get; set; }
        public decimal Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal DriverPricePerUnit { get; set; }
        public int? PaymentId { get; set; }
        public string TruckCode { get; set; }
        public string TruckDescription { get; set; }
        public string DriverCode { get; set; }
        public string FullName { get; set; }
        public DateTime CreationDate { get; set; }
        public string Code { get; set; }
    }
}
