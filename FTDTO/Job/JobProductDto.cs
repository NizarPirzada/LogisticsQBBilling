using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTDTO.Job
{
    public class JobProductDto: UserBase.UserBaseDTO
    {
        public int ItemId { get; set; }
        public int JobId { get; set; }
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double DriverPrice { get; set; }
    }
}
