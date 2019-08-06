using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace August6thExercise.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectPlan { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }

    }
}