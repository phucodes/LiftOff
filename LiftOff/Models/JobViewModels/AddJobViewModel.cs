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
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Position Type")]
        public string PositionType { get; set; }

        [Required]
        [Display(Name = "Position Level")]
        public string PositionLevel { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatePosted { get; set; }

        public bool IsOpened { get; set; }
    }
}
