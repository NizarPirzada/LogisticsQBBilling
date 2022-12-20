using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTDTO.Product
{
    public class ProductDTO: UserBase.UserBaseDTO
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
