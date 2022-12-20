using System;

namespace FTDTO.Report
{
    public class TruckHireReportsFilterDTO
    {
        public int? InvoiceID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
