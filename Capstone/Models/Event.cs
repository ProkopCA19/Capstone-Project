using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using DHTMLX.Scheduler;

namespace Capstone.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DHXJson(Alias = "id")]
        public int Id { get; set; }
        [DHXJson(Alias = "text")]
        public string text { get; set; }
        [DHXJson(Alias = "start_date")]
        public DateTime StartDate { get; set; }
        [DHXJson(Alias = "end_date")]
        public DateTime EndDate { get; set; }

        [ForeignKey("Photographer")]
        public int? PhotographerId { get; set; }
        public Photographer Photographer { get; set; }
    }
}