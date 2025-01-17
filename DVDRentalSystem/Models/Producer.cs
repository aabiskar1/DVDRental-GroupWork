﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DVDRentalSystem.Models
{
    public class Producer
    {
        [Key]
        public int ProducerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Studio_name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }

        public int DVDDetailsId { get; set; }

        [ForeignKey("DVDDetailsId")]
        public virtual DVDDetails DVDDetails { get; set; }


        public int CastDetailsId { get; set; }
        [ForeignKey("CastDetailsId")]
        public virtual CastDetails CastDetails { get; set; }



    }
}