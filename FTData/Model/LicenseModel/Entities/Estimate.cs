using FTData.BaseTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTData.Model.LicenseModel.Entities
{
    public partial class Estimate : BaseTrackable<long>
    {
        [ForeignKey("Customer")]
        public long Customer_Id { get; set; }
        public string Code { get; set; }
        public DateTime Entered_Date { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public decimal? Total { get; set; }
        public DateTime? Expiration_Date { get; set; }
        [ForeignKey("LicenseUser")]
        public long LicenseUserId { get; set; }
        public virtual LicenseUser LicenseUser { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
