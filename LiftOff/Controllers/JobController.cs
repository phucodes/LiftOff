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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

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
                // var currentUser = _userManager.GetUserName(HttpContext.User);
                var currentUser = HttpContext.User.Identity.Name;
                Job newJob = new Job()
                {
                    Name = addJobViewModel.Name,
                    DatePosted = DateTime.UtcNow,
                    Location = addJobViewModel.Location,
                    PositionType = addJobViewModel.PositionType,
                    PositionLevel = addJobViewModel.PositionLevel,
                    Description = addJobViewModel.Description,
                    Employer = currentUser,
                    IsOpened = true
                };
                context.Job.Add(newJob);
                context.SaveChanges();

                return RedirectToAction("ViewJob", new { id = newJob.JobId });
            }

            // If we get here, something's wrong and re-render the form
            return View(addJobViewModel);
        }

        [HttpGet]
        public IActionResult ViewJob(int id)
        {   
            Job viewJob = context.Job.Find(id);
            AddJobViewModel currentJob = new AddJobViewModel
            {
                Name = viewJob.Name,
                DatePosted = viewJob.DatePosted,
                Location = viewJob.Location,
                PositionType = viewJob.PositionType,
                PositionLevel = viewJob.PositionLevel,
                Description = viewJob.Description,
                IsOpened = viewJob.IsOpened
            };

            return View(viewJob);
        }

        // TODO: FINISH EDIT ACTIONS

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<Job> jobs = context.Job.Where(job => job.IsOpened == true).ToList();
            //List<Job> jobs = context.Job.ToList();
            return View(jobs);
        }

        [HttpGet]
        [Authorize(Roles = "Employer")]
        public IActionResult YourJob()
        {
            var currentUser = HttpContext.User.Identity;
            List<Job> jobs = context.Job.Where(job => job.Employer == currentUser.Name).ToList();
            return View(jobs);
        }
    }
}
