using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string PhotoPath { get; set; }

        [ForeignKey("Photographer")]
        public int? PhotographerId { get; set; }
        public Photographer Photographer { get; set; }





    }
}