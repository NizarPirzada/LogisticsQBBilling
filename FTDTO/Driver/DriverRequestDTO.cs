using FTDTO.UserBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTDTO.Driver
{
    public class DriverRequestDTO : UserBaseDTO
    {
        public int DriverID { get; set; }
        public int DriverTypeID { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string HireDate { get; set; }
        public int Percentage { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public bool IsInactive { get; set; }
    }
}
