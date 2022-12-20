using System;
using System.Collections.Generic;

#nullable disable

namespace FTData.Model.Entities
{
    public partial class ApplicationError
    {
        public int ApplicationErrorId { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
