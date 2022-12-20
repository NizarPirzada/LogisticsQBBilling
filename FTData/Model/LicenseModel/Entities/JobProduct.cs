using FTData.BaseTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTData.Model.LicenseModel.Entities
{
    public partial class JobProduct: BaseTrackable<long>
    {
        public JobProduct()
        {
            //Tickets = new HashSet<Ticket>();
        }
        [ForeignKey("Job")]
        public int Job_Id { get; set; }
        [ForeignKey("Product")]
        public long Product_Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Driver_Price { get; set; }
        public virtual Job Job { get; set; }
        public virtual Product Product { get; set; }
        //public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
