using FTData.BaseTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTData.Model.LicenseModel.Entities
{
    public partial class Job : BaseTrackable<int>
    {
        public Job()
        {
            JobProducts = new HashSet<JobProduct>();
            Tickets = new HashSet<Ticket>();
        }
        [ForeignKey("Customer")]
        public long Customer_Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string PO_Number { get; set; }
        public bool Is_Complete { get; set; }
        public DateTime Awarded_Date { get; set; }
        public string Location { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<JobProduct> JobProducts { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
