using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class VwUnpaidInvoice
    {
        public int CustomerId { get; set; }
        public int Number { get; set; }
        public decimal TotalDue { get; set; }
        public int InvoiceId { get; set; }
    }
}
