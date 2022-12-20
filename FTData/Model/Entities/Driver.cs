using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class Driver
    {
        public Driver()
        {
            TicketLineItems = new HashSet<TicketLineItem>();
            Trucks = new HashSet<Truck>();
        }

        public int DriverId { get; set; }
        public int DriverTypeId { get; set; }
        public string FullName { get; set; }
        public string Code { get; set; }
        public DateTime? HireDate { get; set; }
        public int Percentage { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public bool? IsInactive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public virtual DriverType DriverType { get; set; }
        public virtual ICollection<TicketLineItem> TicketLineItems { get; set; }
        public virtual ICollection<Truck> Trucks { get; set; }
    }
}
