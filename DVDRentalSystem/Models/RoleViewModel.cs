using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DVDRentalSystem.Models
{

    public class RoleViewModel
    {

        public RoleViewModel() { }

        public RoleViewModel(ApplicationRole role) {
            Id = role.Id;
            Name = role.Name;
        }

     
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

  

    }
}