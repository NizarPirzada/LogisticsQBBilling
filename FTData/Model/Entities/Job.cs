using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class Job
    {
        public Job()
        {
            JobProducts = new HashSet<JobProduct>();
            Tickets = new HashSet<Ticket>();
        }

        public int JobId { get; set; }
        public int CustomerId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string PoNumber { get; set; }
        public bool IsComplete { get; set; }
        public DateTime AwardedDate { get; set; }
        public string Location { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<JobProduct> JobProducts { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
