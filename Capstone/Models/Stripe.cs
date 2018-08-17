using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class Stripe
    {

        [Key]
        public string stripePublishKey { get; set; }

    }
}