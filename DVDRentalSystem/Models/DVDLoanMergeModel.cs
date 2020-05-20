using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDRentalSystem.Models
{
    public class DVDLoanMergeModel
    {
        public List<Loan> Loans { get; set; }
        public List<DVDDetails> DVDDetails { get; set; }
    }
}