using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALShared.Models
{
    public class Manga : Product
    {
        public string CreatorStudio { get; set; }
        public DateTime CreationDate { get; set; }
        public int Episode { get; set; }
        public int Chapter { get; set; }
        public int Tome { get; set; }
    }
}
