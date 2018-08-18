using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LiftOff.Models;

namespace LiftOff.Models.JobViewModels
{
    public class RequirementViewModel
    {
        [Required]
        [Display(Name = "Requirement Names")]
        public string RequirementName { get; set; }

        [Microsoft.AspNetCore.Mvc.HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Microsoft.AspNetCore.Mvc.HiddenInput(DisplayValue = false)]
        public int JobId { get; set; }
    }
}
