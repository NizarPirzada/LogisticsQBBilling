using FTData.BaseTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTData.Model.LicenseModel.Entities
{
    public partial class Driver : BaseTrackable<long>
    {
        public Driver()
        {
            TicketLineItems = new HashSet<TicketLineItem>();
            Trucks = new HashSet<Truck>();
        }
        [ForeignKey("DriverType")]
        public int Driver_Type_Id { get; set; }
        public string Full_Name { get; set; }
        public string Code { get; set; }
        public DateTime? Hire_Date { get; set; }
        public int Percentage { get; set; }
        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip_Code { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public bool? Is_Inactive { get; set; }
        public virtual DriverType DriverType { get; set; }
        public virtual ICollection<TicketLineItem> TicketLineItems { get; set; }
        public virtual ICollection<Truck> Trucks { get; set; }
    }
}
