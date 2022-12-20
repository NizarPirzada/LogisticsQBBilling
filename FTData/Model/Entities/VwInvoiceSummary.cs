using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class VwInvoiceSummary
    {
        public int InvoiceId { get; set; }
        public string CustomerDescription { get; set; }
        public string JobDescription { get; set; }
        public int Number { get; set; }
        public decimal TotalDue { get; set; }
        public bool? IsReadyForTruckHirePayment { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
