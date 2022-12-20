using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTDTO.Job
{
    public class JobDto : JobProductDto
    {
        public int CustomerId { get; set; }
        public string PoNumber { get; set; }
        public bool IsComplete { get; set; }
        public string AwardDate{ get; set; }
        public string Location{ get; set; }
    }
}
