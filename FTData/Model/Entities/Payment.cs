using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class Payment
    {
        public Payment()
        {
            TicketLineItems = new HashSet<TicketLineItem>();
        }

        public int PaymentId { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public int Percentage { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DateRangeStart { get; set; }
        public DateTime? DateRangeEnd { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public virtual ICollection<TicketLineItem> TicketLineItems { get; set; }
    }
}
