using FTData.BaseTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FTData.Model.LicenseModel.Entities
{
    public partial class Invoice : BaseTrackable<int>
    {
        public Invoice()
        {
            Tickets = new HashSet<Ticket>();
        }
        public int Number { get; set; }
        public string Quickbooks_Invoice_Number { get; set; }
        public decimal Total_Due { get; set; }
        public string PO_Number { get; set; }
        public string Paid_Check_Number { get; set; }
        public bool? Is_Ready_For_Truck_Hire_Payment { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
