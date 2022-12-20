using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class VwAvailableTicketsForInvoice
    {
        public string CustomerDescription { get; set; }
        public string JobDescription { get; set; }
        public string TicketDescription { get; set; }
        public string ProductDescription { get; set; }
        public int JobId { get; set; }
        public int TicketId { get; set; }
        public int JobProductId { get; set; }
        public decimal? Total { get; set; }
        public decimal? TotalDue { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
