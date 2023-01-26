using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALShared.Models.AnimeStore
{
    public abstract class AnimeStore
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Product> Catalogue { get; set; }
        public List<Discount> ActiveDiscounts { get; set; }
        public string AverageDeliveryInHours { get; set; }
    }
}
