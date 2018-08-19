using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiftOff.Models.JobViewModels
{
    public class SearchViewModel
    {
        [Required]
        public string SearchString { get; set; }
    }
}
