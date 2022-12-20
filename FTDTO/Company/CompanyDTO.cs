using FTDTO.UserBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTDTO.Company
{
    public class CompanyDTO : UserBaseDTO
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip_Code { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Url { get; set; }
        public string QBAccessToken { get; set; }
        public string ReelmId { get; set; }

    }
}
