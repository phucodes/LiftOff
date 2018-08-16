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
        public IActionResult Add(AddJobViewModel addJobViewModel, List<string> RequirementNames, List<string> BenefitNames, List<string> TagNames)
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
                    IsOpened = true,
                    //Requirement = addJobViewModel.Requirement
                    //RequirementName = addJobViewModel.RequirementName,
                    //ListRequirements = addJobViewModel.ListRequirements
                };
                context.Job.Add(newJob);
                context.SaveChanges();

                int ReqNameLength = RequirementNames.Count();

                foreach (var item in RequirementNames)
                {
                    //int currentId = newJob.JobId;

                    Requirement newRequirement = new Requirement()
                    {
                        RequirementName = item,
                        JobId = newJob.JobId
                    };
                    context.Requirements.Add(newRequirement);
                    context.SaveChanges();
                }

                foreach (var item in BenefitNames)
                {
                    Benefit newBenefit = new Benefit()
                    {
                        BenefitName = item,
                        JobId = newJob.JobId
                    };
                    context.Benefits.Add(newBenefit);
                    context.SaveChanges();
                }

                foreach (var item in TagNames)
                {
                    Tag newTag = new Tag()
                    {
                        TagName = item,
                        JobId = newJob.JobId
                    };
                    context.Tag.Add(newTag);
                    context.SaveChanges();
                }

                return RedirectToAction("ViewJob", new { id = newJob.JobId });
            }

            // If we get here, something's wrong and re-render the form
            return View(addJobViewModel);
        }

        [HttpGet]
        public IActionResult ViewJob(int id)
        {
            Job viewJob = context.Job.Find(id);

            List<Requirement> currentReq = context.Requirements.Where(j => j.JobId == id).ToList();

            List<Benefit> currentBenefits = context.Benefits.Where(j => j.JobId == id).ToList();

            List<Tag> currentTags = context.Tag.Where(j => j.JobId == id).ToList();

            AddJobViewModel currentJobViewModel = new AddJobViewModel
            {
                Name = viewJob.Name,
                DatePosted = viewJob.DatePosted,
                Location = viewJob.Location,
                PositionType = viewJob.PositionType,
                PositionLevel = viewJob.PositionLevel,
                Description = viewJob.Description,
                RequirementNames = currentReq,
                BenefitNames = currentBenefits,
                TagNames = currentTags
                // Employer = currentUser,
            };

            return View(currentJobViewModel);
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

        [HttpGet]
        [Authorize(Roles = "Employer")]
        public IActionResult DeleteJob(int id)
        {
            Job viewJob = context.Job.Find(id);
            AddJobViewModel currentJob = new AddJobViewModel
            {
                Name = viewJob.Name,
                DatePosted = viewJob.DatePosted,
                Location = viewJob.Location
            };

            return View(viewJob);
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]
        public IActionResult Delete(int id)
        {
            Job viewJob = context.Job.SingleOrDefault(j => j.JobId == id);
            context.Job.Remove(viewJob);
            context.SaveChanges();

            return RedirectToAction("YourJob");
        }

        [HttpGet]
        [Authorize(Roles = "Employer")]
        public IActionResult Edit (int id)
        {
            Job viewJob = context.Job.Find(id);

            List<Requirement> currentRequirements = context.Requirements.Where(r => r.JobId == id).ToList();

            List<Benefit> currentBenefits = context.Benefits.Where(j => j.JobId == id).ToList();

            List<Tag> currentTags = context.Tag.Where(j => j.JobId == id).ToList();

            AddJobViewModel currentJob = new AddJobViewModel
            {
                Name = viewJob.Name,
                DatePosted = viewJob.DatePosted,
                Location = viewJob.Location,
                PositionType = viewJob.PositionType,
                PositionLevel = viewJob.PositionLevel,
                Description = viewJob.Description,
                IsOpened = viewJob.IsOpened,
                RequirementNames = currentRequirements,
                BenefitNames = currentBenefits,
                TagNames = currentTags
            };

            return View(currentJob);
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]
        public IActionResult Edit(int id, AddJobViewModel currentJob, List<string> RequirementNames, List<string> BenefitNames, List<string> TagNames)
        {
            Job viewJob = context.Job.SingleOrDefault(j => j.JobId == id);

            if (viewJob != null)
            {
                viewJob.Location = currentJob.Location;
                viewJob.Name = currentJob.Name;
                viewJob.PositionLevel = currentJob.PositionLevel;
                viewJob.PositionType = currentJob.PositionType;
                viewJob.Description = currentJob.Description;
                context.SaveChanges();
            }

            List<Requirement> currentRequirements = context.Requirements.Where(r => r.JobId == id).ToList();

            Dictionary<string, string> UpdateRequirements = new Dictionary<string, string>();

            for(int i = 0; i < RequirementNames.Count; i++)
            {
                foreach(var item in currentRequirements)
                {
                    item.RequirementName = RequirementNames[i];
                }
            }

            return RedirectToAction("ViewJob", new { id = viewJob.JobId });
        }
    }
}
