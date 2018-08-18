using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiftOff.Models.JobViewModels
{
    public class BenefitViewModel
    {
        [Required]
        [Display(Name = "Requirement Names")]
        public string BenefitName { get; set; }

        [Microsoft.AspNetCore.Mvc.HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Microsoft.AspNetCore.Mvc.HiddenInput(DisplayValue = false)]
        public int JobId { get; set; }
    }
}
