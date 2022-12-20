using FTData.BaseTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTData.Model.LicenseModel.Entities
{
    public partial class Truck : BaseTrackable<long>
    {
        public Truck()
        {
            TicketLineItems = new HashSet<TicketLineItem>();
        }
        [ForeignKey("DefaultDriver")]
        public long? Default_Driver_Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool? Is_Inactive { get; set; }
        public virtual Driver DefaultDriver { get; set; }
        public virtual ICollection<TicketLineItem> TicketLineItems { get; set; }
    }
}
