using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDRentalSystem.Models
{
    public class MergeModel
    {
        public List<int> numberofCopies { get; set; }
        public List<Producer> producer { get; set; }
        public List<DVDDetails> dvdDetails { get; set; }
        public List<CastDetails> castDetails{ get; set; }

    }
}