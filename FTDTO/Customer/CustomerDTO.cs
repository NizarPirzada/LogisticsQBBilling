using FTDTO.UserBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTDTO.Customer
{
    public class CustomerDTO : UserBaseDTO
    {
        public int CustomerId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip_Code { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string PaymentTerms { get; set; }
        public bool IsInActive { get; set; }
    }
}
