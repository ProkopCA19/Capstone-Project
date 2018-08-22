using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class Photographer
    {
      
        [Key]
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }
        [Display(Name = "Price Tier 1: $50-100")]
        public bool PriceRange1 { get; set; }
        [Display(Name = "Price Tier 2: $100-200")]
        public bool PriceRange2 { get; set; }
        [Display(Name = "Price Tier 3: $200-300")]
        public bool PriceRange3 { get; set; }
        [Display(Name = "Price Tier 4: $300+")]
        public bool PriceRange4 { get; set; }
        public string Bio { get; set; }

        public double? AccountBalance { get; set; }



        


        [ForeignKey("Appointment")]
        public int? AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


    }

}