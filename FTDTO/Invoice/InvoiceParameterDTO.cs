using System;

namespace FTDTO.Invoice
{
    public class InvoiceParameterDTO : PagingParameterDTO
    {
        public int? Paid { get; set; } = null;
        public int? Funded { get; set; } = null;
        public DateTime? StartDate { get; set; } = DateTime.Today;
        public DateTime? EndDate { get; set; } = DateTime.Today.AddDays(1);
    }
}
