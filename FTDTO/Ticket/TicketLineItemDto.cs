using System;

namespace FTDTO.Ticket
{
    public class TicketLineItemDto : UserBase.UserBaseDTO
    {
        public int Ticket_Line_Item_ID { get; set; }
        public int Ticket_ID { get; set; }
        public int Driver_ID { get; set; }
        public int Truck_ID { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price_Per_Unit { get; set; }
        public decimal Driver_Price_Per_Unit { get; set; }
        public string Truck_Code { get; set; }
        public string Truck_Description { get; set; }
        public string Driver_Code { get; set; }
        public string Full_Name { get; set; }
        public DateTime Creation_Date { get; set; }
        public string Code { get; set; }
    }
}
