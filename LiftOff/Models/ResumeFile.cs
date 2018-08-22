using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiftOff.Models
{
    public class ResumeFile
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; }
        public string UserName { get; set; }
        // public IFormFile File { get; set; }
        //TODO: FIGURE OUT FILE UPLOAD
    }
}
