using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiftOff.Models
{
    public class Job
    {
        public int Id { get; set; }

        private static int nextId = 1;

        public string Name { get; set; }

        public DateTime DatePosted { get; set; }

        public string Location { get; set; }

        public string PositionType { get; set; }

        public string PositionLevel { get; set; }

        public string Description { get; set; }

        // public Employer Employer { get; set; }

        public Job()
        {
            Id = nextId;
            nextId++;
        }

    }
}
