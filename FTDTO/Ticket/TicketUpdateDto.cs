using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTDTO.Ticket
{
    public class TicketUpdateDto: UserBase.UserBaseDTO
    {
        public int TicketId { get; set; }
        public int JobId { get; set; }
        public int JobProductId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; } 
        public string CreationDate { get; set; }
    }
}
