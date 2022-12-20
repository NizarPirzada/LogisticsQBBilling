using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTDTO.Payment
{
    public class PaymentDTO : UserBase.UserBaseDTO
    {
        public decimal Gross_Amount { get; set; }
        public decimal Payment_Amount { get; set; }
        public int Percentage { get; set; }
        public DateTime? Date_Range_Start { get; set; }
        public DateTime? Date_Range_End { get; set; }
    }

    public class PaymentDriverDTO : UserBase.UserBaseDTO
    {
        public int DriverId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class PaymentDriverTypeDTO : UserBase.UserBaseDTO
    {
        public int DriverType { get; set; }
        public int DriverId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class EmployeePayrollDTO
    {
        public int DriverID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
