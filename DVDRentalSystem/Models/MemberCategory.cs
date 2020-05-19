﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DVDRentalSystem.Models
{
    public class MemberCategory
    {
        [Key]
        public int MemberCategoryId { get; set; }


        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalLoan { get; set; }
    }
}