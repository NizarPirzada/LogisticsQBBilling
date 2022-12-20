using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class VwPaymentDetail
    {
        public int PaymentId { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public int Percentage { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DateRangeStart { get; set; }
        public DateTime? DateRangeEnd { get; set; }
        public int TicketLineItemId { get; set; }
        public decimal DriverPricePerUnit { get; set; }
        public decimal Quantity { get; set; }
        public string TruckDescription { get; set; }
        public string ProductDescription { get; set; }
        public string JobDescription { get; set; }
        public string CustomerDescription { get; set; }
        public int DriverId { get; set; }
        public int DriverTypeId { get; set; }
        public string FullName { get; set; }
        public DateTime TicketCreationDate { get; set; }
        public string Code { get; set; }
        public decimal? LineItemTotal { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}
