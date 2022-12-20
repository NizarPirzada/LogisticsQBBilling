using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class JobProduct
    {
        public JobProduct()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int JobProductId { get; set; }
        public int JobId { get; set; }
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DriverPrice { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public virtual Job Job { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
