using FTData.BaseTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTData.Model.LicenseModel.Entities
{
    public partial class TicketLineItem : BaseTrackable<int>
    {
        [ForeignKey("Ticket")]
        public int Ticket_Id { get; set; }
        [ForeignKey("Driver")]
        public long Driver_Id { get; set; }
        [ForeignKey("Truck")]
        public long Truck_Id { get; set; }
        [ForeignKey("Payment")]
        public long? Payment_Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price_Per_Unit { get; set; }
        public decimal Driver_Price_Per_Unit { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual Truck Truck { get; set; }
    }
}
