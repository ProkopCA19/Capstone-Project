using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }


    }
}