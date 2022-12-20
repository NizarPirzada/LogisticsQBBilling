using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTDTO.Ticket
{
    public class TicketDTO : UserBase.UserBaseDTO
    {
        public long Job_Id { get; set; }
        public long? Invoice_Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class PayableTicketFilterDTO
    {
        public int DriverType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
