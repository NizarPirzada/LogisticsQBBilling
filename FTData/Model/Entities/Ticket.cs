using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class Ticket
    {
        public Ticket()
        {
            TicketLineItems = new HashSet<TicketLineItem>();
        }

        public int TicketId { get; set; }
        public int JobId { get; set; }
        public int? InvoiceId { get; set; }
        public int JobProductId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Job Job { get; set; }
        public virtual JobProduct JobProduct { get; set; }
        public virtual ICollection<TicketLineItem> TicketLineItems { get; set; }
    }
}
