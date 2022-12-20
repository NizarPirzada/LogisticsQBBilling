using FTData.BaseTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FTData.Model.LicenseModel.Entities
{
    public partial class DriverType : BaseTrackable<int>
    {
        public DriverType()
        {
            Drivers = new HashSet<Driver>();
            TicketLineItem = new HashSet<TicketLineItem>();
        }
        public string Description { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
        public virtual ICollection<TicketLineItem> TicketLineItem { get; set; }
    }
}
