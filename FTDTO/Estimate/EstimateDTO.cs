using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTDTO.Estimate
{
    public class EstimateDTO : UserBase.UserBaseDTO
    {
        public int EstimateId { get; set; }
        public int CustomerId { get; set; }
        public string Code { get; set; }
        public DateTime Entered_Date { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public double Total { get; set; }
        public string ExpirationDate { get; set; }
    }
}
