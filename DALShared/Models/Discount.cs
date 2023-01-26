using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALShared.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<ProductType> ProductsOnDiscount { get; set; }
    }
}
