﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LiftOff.Models;

namespace LiftOff.Models.JobViewModels
{
    public class AddJobViewModel
    {
        [Required]
        [Display(Name = "Name", Prompt = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Location", Prompt = "Location")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Position Type", Prompt = "Position Type")]
        public string PositionType { get; set; }

        [Required]
        [Display(Name = "Position Level", Prompt = "Position Level")]
        public string PositionLevel { get; set; }

        [Required]
        [MaxLength(5000)]
        [Display(Name = "Description", Prompt = "Description")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatePosted { get; set; }

        public bool IsOpened { get; set; }

        public List<ApplicantViewModel> Applicants { get; set; }
        
        public List<RequirementViewModel> Requirements { get; set; }

        public List<BenefitViewModel> Benefits { get; set; }

        public List<TagViewModel> Tags { get; set; }

        public int JobId { get; set; }

        public string Employer { get; set; }
    }
    
}
