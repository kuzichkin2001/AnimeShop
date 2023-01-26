using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALShared.Models.AnimeStore
{
    public class AnimeOfflineStore : AnimeStore
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
