using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class DriverType
    {
        public DriverType()
        {
            Drivers = new HashSet<Driver>();
        }

        public int DriverTypeId { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
