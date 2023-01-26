using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALShared.Models
{
    public class Comic : Product
    {
        public int ReleaseNumber { get; set; }
        public int TotalNumberOfReleases { get; set; }
        public int SeasonNumber { get; set; }
        public Manga RelatedManga { get; set; }
    }
}
