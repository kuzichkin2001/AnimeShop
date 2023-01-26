using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALShared.Models
{
    public class Single : Product
    {
        public Comic RelatedComic { get; set; }
        public int Release { get; set; }
    }
}
