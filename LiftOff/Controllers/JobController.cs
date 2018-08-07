using Microsoft.AspNetCore.Mvc;
using LiftOff.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LiftOff.Models.JobViewModels;
using LiftOff.Models;
using Microsoft.AspNetCore.Authorization;

namespace LiftOff.Controllers
{
    [Authorize(Roles = "Employer")]
    public class JobController : Controller
    {
        private JobDbContext context;

        public JobController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddJobViewModel addJobViewModel = new AddJobViewModel();
            return View(addJobViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddJobViewModel addJobViewModel)
        {
            if (ModelState.IsValid)
            {
                Job newJob = new Job()
                {
                    Name = addJobViewModel.Name,
                    DatePosted = DateTime.UtcNow,
                    Location = addJobViewModel.Location,
                    PositionType = addJobViewModel.PositionType,
                    PositionLevel = addJobViewModel.PositionLevel,
                    Description = addJobViewModel.Description
                };
                context.Job.Add(newJob);
                context.SaveChanges();

                return Redirect(String.Format("/job?id={0}", newJob.Id));
            }

            // If we get here, something's wrong and re-render the form
            return View(addJobViewModel);
        }
    }
}
