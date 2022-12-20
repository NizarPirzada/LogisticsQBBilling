using FTData.BaseTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FTData.Model.LicenseModel.Entities
{
    public partial class CompanyInformation : BaseTrackable<long>
    {
        public string Company_Name { get; set; }
        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip_Code { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string URL { get; set; }
    }
}
