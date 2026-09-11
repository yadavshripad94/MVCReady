using Microsoft.AspNetCore.Mvc;
using MVCReady.Models;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;


namespace MVCReady.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Viewdata Example
        public ViewResult Details()
        {
            ViewData["title"] = "Student Deatails Page";
            ViewData["Header"] = "Student Details";

            Student student = new Student()

            {
                StudentId = 101,
                Name = "Akshay",
                Branch = "Mech",
                Section = "A",
                Gender = "Male"
            };
            //storing Student Data
            ViewData["Student"]=student;

            return View();
        }


        //ViewBag example
        public ViewResult ViewBagDetails()
        {
            ViewBag.Title= "Student Details Page";
            ViewBag.Header = "Student Details";

            Student s1 = new Student()
            {
                StudentId = 101,
                Name = "James",
                Branch = "CSE",
                Section = "A",
                Gender = "Male"
            };

            ViewBag.Student = s1;

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

 
    }
}
