using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class VwInvoice
    {
        public string JobDescription { get; set; }
        public string CustomerDescription { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string TicketCode { get; set; }
        public string TruckCode { get; set; }
        public string TruckDescription { get; set; }
        public decimal Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public int JobId { get; set; }
        public string TicketTruck { get; set; }
        public decimal? LineItemTotal { get; set; }
        public int JobProductId { get; set; }
        public int Number { get; set; }
        public decimal TotalDue { get; set; }
        public string PoNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public int InvoiceId { get; set; }
        public DateTime TicketCreationDate { get; set; }
        public int TicketId { get; set; }
    }
}
