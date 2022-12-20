using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class Invoice
    {
        public Invoice()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int InvoiceId { get; set; }
        public int Number { get; set; }
        public string QuickbooksInvoiceNumber { get; set; }
        public decimal TotalDue { get; set; }
        public string PoNumber { get; set; }
        public string PaidCheckNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsReadyForTruckHirePayment { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
