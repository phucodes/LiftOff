using Microsoft.AspNetCore.Identity;
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

        public List<Requirement> Requirements { get; set; }

        public List<Benefit> Benefits { get; set; }

        public List<Applicant> Applicants { get; set; }

        public List<Tag> Tags { get; set; }
    }

    public class Requirement
    {   
        public int Id { get; set; }
        public int JobId { get; set; }
        public string RequirementName { get; set; }
    }

    public class Benefit
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string BenefitName { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string TagName { get; set; }
    }

    public class Applicant
    {   
        public int Id { get; set; }
        public string UserId { get; set; }
        public int JobId { get; set; }
        public string Name { get; set; }
    }
}
