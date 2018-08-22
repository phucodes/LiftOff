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
    public class JobController : Controller
    {
        private JobDbContext context;

        private readonly UserManager<ApplicationUser> _userManager;


        public JobController(JobDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            context = dbContext;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Employer")]
        public IActionResult Add()
        {
            AddJobViewModel addJobViewModel = new AddJobViewModel();
            return View(addJobViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employer")]
        public IActionResult Add(AddJobViewModel addJobViewModel, List<string> Requirements, List<string> Benefits, List<string> TagNames)
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
                };

                foreach(var item in Benefits)
                {
                    Benefit newBenefit = new Benefit()
                    {
                        BenefitName = item,
                        JobId = newJob.JobId
                    };
                    context.Benefits.Add(newBenefit);
                    context.SaveChanges();
                };

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
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ViewJob(int id)
        {
            Job viewJob = context.Job.Find(id);

            List<Requirement> RequirementList = context.Requirements.Where(r => r.JobId == id).ToList();

            List<RequirementViewModel> Requirements = new List<RequirementViewModel>();
            List<BenefitViewModel> Benefits = new List<BenefitViewModel>();
            List<TagViewModel> Tags = new List<TagViewModel>();
            List<ApplicantViewModel> Applicants = new List<ApplicantViewModel>();

            foreach (var item in RequirementList)
            {
                RequirementViewModel currentRequirementItem = new RequirementViewModel
                {
                    RequirementName = item.RequirementName,
                    JobId = item.JobId,
                    Id = item.Id
                };
                Requirements.Add(currentRequirementItem);
            };

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
            };

            List<Tag> currentTags = context.Tag.Where(j => j.JobId == id).ToList();

            foreach (var item in currentTags)
            {
                TagViewModel CurrentTags = new TagViewModel
                {
                    TagName = item.TagName,
                    JobId = item.JobId,
                    Id = item.Id
                };
                Tags.Add(CurrentTags);
            };

            List<Applicant> currentApplicants = context.Applicants.Where(a => a.JobId == id).ToList();

            foreach(var item in currentApplicants)
            {
                var matchedApplicants = context.Applicants.FirstOrDefault(a => a.Id == item.Id);
                ApplicantViewModel CurrentApplicants = new ApplicantViewModel
                {
                    UserId = matchedApplicants.UserId,
                    JobId = item.JobId,
                    Name = matchedApplicants.Name
                };
                Applicants.Add(CurrentApplicants);
            }

            AddJobViewModel currentJobViewModel = new AddJobViewModel
            {
                Name = viewJob.Name,
                DatePosted = viewJob.DatePosted,
                Location = viewJob.Location,
                PositionType = viewJob.PositionType,
                PositionLevel = viewJob.PositionLevel,
                Description = viewJob.Description,
                Employer = viewJob.Employer,
                Requirements = Requirements,
                Benefits = Benefits,
                Tags = Tags,
                Applicants = Applicants,
                JobId = viewJob.JobId
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

            List<RequirementViewModel> Requirements = new List<RequirementViewModel>();
            List<BenefitViewModel> Benefits = new List<BenefitViewModel>();
            List<TagViewModel> Tags = new List<TagViewModel>();

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
            };

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
            };

            List<Tag> currentTags = context.Tag.Where(j => j.JobId == id).ToList();

            foreach (var item in currentTags)
            {
                TagViewModel CurrentTags = new TagViewModel
                {
                    TagName = item.TagName,
                    JobId = item.JobId,
                    Id = item.Id
                };
                Tags.Add(CurrentTags);
            };

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
                Benefits = Benefits,
                Tags = Tags
            };

            return View(currentJob);
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]
        public IActionResult Edit(int id, AddJobViewModel currentJob, List<string> Requirements)
        {
            var viewJob = context.Job.FirstOrDefault(j => j.JobId == id);

            List<Requirement> currentRequirements = context.Requirements.Where(r => r.JobId == id).ToList();

            List<Requirement> newRequirements = new List<Requirement>();

            foreach(var item in currentJob.Requirements)
            {
                foreach(var requirementItem in currentRequirements)
                {
                    if(item.Id == requirementItem.Id)
                    {
                        requirementItem.RequirementName = item.RequirementName;
                        newRequirements.Add(requirementItem);
                    }
                }
            };

            List<Benefit> currentBenefits = context.Benefits.Where(r => r.JobId == id).ToList();

            List<Benefit> newBenefits = new List<Benefit>();

            foreach (var item in currentJob.Benefits)
            {
                foreach (var benefitItem in currentBenefits)
                {
                    if (item.Id == benefitItem.Id)
                    {
                        benefitItem.BenefitName = item.BenefitName;
                        newBenefits.Add(benefitItem);
                    }
                }
            };

            List<Tag> currentTags = context.Tag.Where(t => t.JobId == id).ToList();

            List<Tag> newTags = new List<Tag>();

            foreach (var item in currentJob.Tags)
            {
                foreach (var tagItem in currentTags)
                {
                    if (item.Id == tagItem.Id)
                    {
                        tagItem.TagName = item.TagName;
                        newTags.Add(tagItem);
                    }
                }
            };

            if (viewJob != null)
            {
                viewJob.Location = currentJob.Location;
                viewJob.Name = currentJob.Name;
                viewJob.PositionLevel = currentJob.PositionLevel;
                viewJob.PositionType = currentJob.PositionType;
                viewJob.Description = currentJob.Description;
                viewJob.Requirements = newRequirements;
                viewJob.Benefits = newBenefits;
                viewJob.Tags = newTags;
                context.SaveChanges();
            };

            return RedirectToAction("ViewJob", new { id = viewJob.JobId });
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Search(SearchViewModel searchViewModel)
        {
            List<Job> matchedJobs = context.Job.Where(j => j.Name.Contains(searchViewModel.SearchString)).ToList();

            return View(matchedJobs);
        }

        //TODO: dave@launchcode.org

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult Apply()
        {
            string userId = _userManager.GetUserId(User);
            List<Applicant> appliedJobs = context.Applicants.Where(j => j.UserId == userId).ToList();

            List<Job> matchedJobs = new List<Job>();

            foreach (var item in appliedJobs)
            {
                Job matchedJob = context.Job.FirstOrDefault(j => j.JobId == item.JobId);
                matchedJobs.Add(matchedJob);
            }

            return View(matchedJobs);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult Apply(int id, ApplicationUser applicationUser, AddJobViewModel viewJob)
        {
            string currentUser = HttpContext.User.Identity.Name;

            string currentId = _userManager.GetUserId(User);

            Applicant newApplicant = new Applicant
            {
                UserId = currentId,
                JobId = id,
                Name = currentUser
            };

            context.Applicants.Add(newApplicant);
            context.SaveChanges();

            return RedirectToAction("/Apply");
        }

    }
}
