using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}