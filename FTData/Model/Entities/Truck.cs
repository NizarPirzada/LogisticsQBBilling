using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class Truck
    {
        public Truck()
        {
            TicketLineItems = new HashSet<TicketLineItem>();
        }

        public int TruckId { get; set; }
        public int? DefaultDriverId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool? IsInactive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public virtual Driver DefaultDriver { get; set; }
        public virtual ICollection<TicketLineItem> TicketLineItems { get; set; }
    }
}
