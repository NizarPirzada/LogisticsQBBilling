using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class TicketLineItem
    {
        public int TicketLineItemId { get; set; }
        public int TicketId { get; set; }
        public int DriverId { get; set; }
        public int TruckId { get; set; }
        public decimal Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal DriverPricePerUnit { get; set; }
        public int? PaymentId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public virtual Driver Driver { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual Truck Truck { get; set; }
    }
}
