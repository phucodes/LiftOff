using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiftOff.Models
{
    public class Job
    {
        public int JobId { get; set; }

        public string Name { get; set; }

        public DateTime DatePosted { get; set; }

        public string Location { get; set; }

        public string PositionType { get; set; }

        public string PositionLevel { get; set; }

        public string Description { get; set; }

         public string Employer { get; set; }

        public bool IsOpened { get; set; }
    }
}
