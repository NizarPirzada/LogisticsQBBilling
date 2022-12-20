using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTDTO.Ticket
{
    public class TicketLineUpdateDto: UserBase.UserBaseDTO
    {
        public int TicketLineItemId { get; set; }
        public int TicketId { get; set; }
        public int DriverId { get; set; }
        public int TruckId { get; set; }
        public double Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public double DriverPrice { get; set; }
    }
}
