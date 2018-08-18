using Microsoft.AspNetCore.Mvc;
using LiftOff.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LiftOff.Models.JobViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using LiftOff.Models;

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

        public static List<RequirementViewModel> Requirements = new List<RequirementViewModel>();

        public static List<BenefitViewModel> Benefits = new List<BenefitViewModel>();

        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddJobViewModel addJobViewModel, List<string> Requirements, List<string> Benefits)
        {

            if (ModelState.IsValid)
            {

                var currentUser = HttpContext.User.Identity.Name;
                Job newJob = new Job()
                {   
                    Name = addJobViewModel.Name,
                    DatePosted = DateTime.Now,
                    Location = addJobViewModel.Location,
                    PositionLevel = addJobViewModel.PositionLevel,
                    PositionType = addJobViewModel.PositionType,
                    Description = addJobViewModel.Description,
                    Employer = currentUser,
                    IsOpened = true
                };
                context.Job.Add(newJob);
                context.SaveChanges();

                foreach (var item in Requirements)
                {
                    Requirement newRequirement = new Requirement()
                    {
                        RequirementName = item,
                        JobId = newJob.JobId
                    };
                    context.Requirements.Add(newRequirement);
                    context.SaveChanges();
                }

                foreach(var item in Benefits)
                {
                    Benefit newBenefit = new Benefit()
                    {
                        BenefitName = item,
                        JobId = newJob.JobId
                    };
                    context.Benefits.Add(newBenefit);
                    context.SaveChanges();
                }

                //foreach (var item in BenefitNames)
                //{
                //    Benefit newBenefit = new Benefit()
                //    {
                //        BenefitName = item,
                //        JobId = newJob.JobId
                //    };
                //    context.Benefits.Add(newBenefit);
                //    context.SaveChanges();
                //}

                //foreach (var item in TagNames)
                //{
                //    Tag newTag = new Tag()
                //    {
                //        TagName = item,
                //        JobId = newJob.JobId
                //    };
                //    context.Tag.Add(newTag);
                //    context.SaveChanges();
                //}

                return RedirectToAction("ViewJob", new { id = newJob.JobId });
            }

            // If we get here, something's wrong and re-render the form
            return View();
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
                //RequirementNames = currentReq,
                //BenefitNames = currentBenefits,
                //TagNames = currentTags
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
        public IActionResult Edit(int id)
        {
            Job viewJob = context.Job.Find(id);

            List<Requirement> RequirementList = context.Requirements.Where(r => r.JobId == id).ToList();

            foreach (var item in RequirementList)
            {
                RequirementViewModel currentRequirementItem = new RequirementViewModel
                {
                    RequirementName = item.RequirementName,
                    JobId = item.JobId,
                    Id = item.Id
                };
                Requirements.Add(currentRequirementItem);
            }

            List<Benefit> BenefitList = context.Benefits.Where(b => b.JobId == id).ToList();

            foreach (var item in BenefitList)
            {
                BenefitViewModel currentBenefitItems = new BenefitViewModel
                {
                    BenefitName = item.BenefitName,
                    JobId = item.JobId,
                    Id = item.Id
                };
                Benefits.Add(currentBenefitItems);
            }

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
                Requirements = Requirements,
                Benefits = Benefits
                //RequirementNames = currentRequirements,
                //BenefitNames = currentBenefits,
                //TagNames = currentTags
            };

            return View(currentJob);
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]
        public IActionResult Edit(int id, AddJobViewModel currentJob)
        {
            List<Requirement> RequirementList = context.Requirements.Where(r => r.JobId == id).ToList();

            List<Benefit> BenefitList = context.Benefits.Where(b => b.JobId == id).ToList();

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

            //Dictionary<string, string> UpdateRequirements = new Dictionary<string, string>();

            //for(int i = 0; i < RequirementNames.Count; i++)
            //{
            //    foreach(var item in currentRequirements)
            //    {
            //        item.RequirementName = RequirementNames[i];
            //    }
            //}

            return RedirectToAction("ViewJob", new { id = viewJob.JobId });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Search()
        {
            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public IActionResult Search(string searchKeyword)
        //{

        //}
    }
}
