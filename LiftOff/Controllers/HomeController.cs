using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiftOff.Models;
using Microsoft.AspNetCore.Authorization;
using LiftOff.Data;
using LiftOff.Models.JobViewModels;

namespace LiftOff.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            SearchViewModel searchViewModel = new SearchViewModel();
            return View(searchViewModel);
        }
        
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
