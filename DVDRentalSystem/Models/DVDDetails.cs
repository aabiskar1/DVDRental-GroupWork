using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DVDRentalSystem.Models
{

    public class DVDDetails
    {
        public DVDDetails()
        {
            this.StockedDate = DateTime.Now;
        }
        [Key]
        public int DVDDetailsId { get; set; }
        [Required]
        public DateTime StockedDate { get; set;}
        public string Name { get; set; }
        public string Genre { get; set; }

        public bool AgeRestricted { get; set; }

        public int NumberOfCopies { get; set; }
        public DateTime ReleaseDate { get; set; }

        public bool isDeleted { get; set; }

        public int Length { get; set; }
        [NotMapped]

        public HttpPostedFileBase CoverImage { get; set; }

        [DisplayName("Cover Image")]
        public string CoverImagePath { get; set; }


        public virtual IEnumerable<CastDetails> CastDetails { get; set; }
    }
}