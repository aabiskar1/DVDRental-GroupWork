using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDRentalSystem.Models
{
    public class MergeModel
    {
        public List<Member> Members { get; set; }
        public List<Loan> Loans { get; set; }


    }
}