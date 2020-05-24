using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DVDRentalSystem.Models
{
    public class LoanType
    {
        [Key]
        public int LoanTypeId { get; set; }
        [Required]

        public string LoanTypeName { get; set; }
        public int LoanDays { get; set; }
    }
}