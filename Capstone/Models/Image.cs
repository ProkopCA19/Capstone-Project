using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class Image
    {
        [Key]

        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }


    }
}