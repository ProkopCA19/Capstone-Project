using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public partial class SchedulerContext: DbContext
    {
        public SchedulerContext() : base("DefaultConnection") { }
        public virtual DbSet<Event> Events { get; set; }

    }
}