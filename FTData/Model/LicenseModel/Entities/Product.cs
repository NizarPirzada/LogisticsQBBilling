using FTData.BaseTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FTData.Model.LicenseModel.Entities
{
    public partial class Product : BaseTrackable<long>
    {
        public Product()
        {
            JobProducts = new HashSet<JobProduct>();
        }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<JobProduct> JobProducts { get; set; }
    }
}
