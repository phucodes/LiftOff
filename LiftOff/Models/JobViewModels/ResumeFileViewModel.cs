using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiftOff.Models.JobViewModels
{
    public class ResumeFileViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FilePath { get; set; }
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Resume Upload")]
        [DataType(DataType.Upload)]
        public IFormFile File {get;set; }
    }
}
