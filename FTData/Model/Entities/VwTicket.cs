using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class VwTicket
    {
        public int TicketId { get; set; }
        public int JobId { get; set; }
        public int? InvoiceId { get; set; }
        public int JobProductId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int CustomerId { get; set; }
        public string JobCode { get; set; }
        public string JobDescription { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerDescription { get; set; }
        public int? Number { get; set; }
    }
}
