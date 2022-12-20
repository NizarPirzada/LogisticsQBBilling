using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTDTO.Invoice
{
    public class InvoiceDTO : UserBase.UserBaseDTO
    {
        public int Number { get; set; }
        public string Quickbooks_Invoice_Number { get; set; }
        public decimal Total_Due { get; set; }
        public string PO_Number { get; set; }
        public string Paid_Check_Number { get; set; }
        public bool? Is_Ready_For_Truck_Hire_Payment { get; set; }
    }

    public class InvoiceJobDTO 
    {
        public int JobID { get; set; }
        public int JobProductID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class InvoiceDriverDTO : UserBase.UserBaseDTO
    {
        public int TruckId { get; set; }
        public int DefaultDriverId { get; set; }
        public string Code { get; set; }
        public String Description { get; set; }
    }

    public class InvoiceDateDTO : UserBase.UserBaseDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class InvoiceCheckDTO : UserBase.UserBaseDTO
    {
        public int InvoiceID { get; set; }
        public string CheckNumber { get; set; }
    }

    public class InvoiceQB : UserBase.UserBaseDTO
    {
        public string code { get; set; }
        public string realmId { get; set; }
    }


    public class InvoiceStatusDTO
    {
        public int InvoiceID { get; set; }
        public bool Status { get; set; }
    }
}
