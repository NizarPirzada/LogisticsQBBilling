using FTData.BaseTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FTData.Model.LicenseModel.Entities
{
    public partial class Payment : BaseTrackable<long>
    {
        public Payment()
        {
            TicketLineItems = new HashSet<TicketLineItem>();
        }
        public decimal Gross_Amount { get; set; }
        public decimal Payment_Amount { get; set; }
        public int Percentage { get; set; }
        public DateTime? Date_Range_Start { get; set; }
        public DateTime? Date_Range_End { get; set; }
        public virtual ICollection<TicketLineItem> TicketLineItems { get; set; }
    }
}
