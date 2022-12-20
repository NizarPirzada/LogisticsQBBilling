using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class VwTicketsForPayment
    {
        public int TicketLineItemId { get; set; }
        public int DriverId { get; set; }
        public int DriverTypeId { get; set; }
        public string FullName { get; set; }
        public int Percentage { get; set; }
        public decimal Quantity { get; set; }
        public decimal DriverPricePerUnit { get; set; }
        public decimal? TicketGross { get; set; }
        public decimal? TicketTotalPayment { get; set; }
        public string TruckNumber { get; set; }
        public string ProductDescription { get; set; }
        public string JobDescription { get; set; }
        public DateTime CreationDate { get; set; }
        public int? PaymentId { get; set; }
        public bool? IsReadyForTruckHirePayment { get; set; }
        public DateTime? InvoiceCreationDate { get; set; }
    }
}
