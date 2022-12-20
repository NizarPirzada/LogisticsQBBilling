using FTData.BaseTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTData.Model.LicenseModel.Entities
{
    public partial class Ticket : BaseTrackable<int>
    {
        public Ticket()
        {
            TicketLineItems = new HashSet<TicketLineItem>();
        }

        [ForeignKey("Job")]
        public int Job_Id { get; set; }
        [ForeignKey("Invoice")]
        public int? Invoice_Id { get; set; }
        public int Ticket_Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Job Job { get; set; }
        public virtual ICollection<TicketLineItem> TicketLineItems { get; set; }
    }
}
