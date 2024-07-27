using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsClub.DbContext;
using SportsClub.Models;

namespace SportsClub.Controllers
{
    public class PlayersController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PlayersController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult GetIndexView(string? search)
        {
            ViewBag.PlayersCountries = new List<string>() { "Portugal", "Argentina", "United States" };
            ViewBag.SearchText = search;

			var players = from p in _context.Players.Include(p => p.sport)
						  select p;

			if (string.IsNullOrEmpty(search) == false)
            {

				players = players.Where(p => p.FullName.Contains(search) || p.PlayerPosition.Contains(search));
            }
            return View("Index", players.ToList());
        }
        public IActionResult GetDetailsView(int id)
        {
            //Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            Player player = _context.Players.Include(p => p.sport).FirstOrDefault(p => p.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            else
            {
                return View("Details", player);
            }

        }


        [HttpGet]
        public IActionResult GetCreateView()
        {
            ViewBag.AllSports = _context.Sports.ToList();
            return View("Create");


        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddNew(Player p)
        {
            if (((p.PlayerRegistrationDate - p.Birthdate).Days / 365) < 18)
            {
                ModelState.AddModelError(string.Empty, "Not allowed age (under 18 years old)");
            }
            if (ModelState.IsValid == true)
            {

                if (p.ImageFile == null)
                {
                    p.ImagePath = "\\images\\No_Image.png";

                }
                else
                {
                    Guid imageGuid = Guid.NewGuid();
                    string imageExtension = Path.GetExtension(p.ImageFile.FileName);
                    p.ImagePath = "\\images\\" + imageGuid + imageExtension;
                    string imageUploadPath = _webHostEnvironment.WebRootPath + p.ImagePath;
                    FileStream imageStream = new FileStream(imageUploadPath, FileMode.Create);
                    p.ImageFile.CopyTo(imageStream);
                    imageStream.Dispose();

                }
                _context.Players.Add(p);
                _context.SaveChanges();

                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.AllSports = _context.Sports.ToList();
                return View("Create");
            }


        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult EditCurrent(Player p)
        {
            if (((p.PlayerRegistrationDate - p.Birthdate).Days / 365) < 18)
            {
                ModelState.AddModelError(string.Empty, "Not allowed age (under 18 years old)");
            }
            if (ModelState.IsValid == true)
            {


                if (p.ImageFile != null)
                {
                    if (p.ImagePath != "\\images\\No_Image.png")
                    {
                        System.IO.File.Delete(_webHostEnvironment.WebRootPath + p.ImagePath);
                    }
                    Guid imageGuid = Guid.NewGuid();
                    string imageExtension = Path.GetExtension(p.ImageFile.FileName);
                    p.ImagePath = "\\images\\" + imageGuid + imageExtension;
                    string imageUploadPath = _webHostEnvironment.WebRootPath + p.ImagePath;
                    FileStream imageStream = new FileStream(imageUploadPath, FileMode.Create);
                    p.ImageFile.CopyTo(imageStream);
                    imageStream.Dispose();

                }
                _context.Players.Update(p);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");


            }
            else
            {
				ViewBag.AllSports = _context.Sports.ToList();
                return View("Edit");
            }

        }

        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            //Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            Player player = _context.Players.Find(id);

            if (player == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.AllSports = _context.Sports.ToList();
                return (View("Edit", player));
            }
        }
        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            //Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            Player employee = _context.Players.Include(p => p.sport).FirstOrDefault(emp => emp.Id == id);

            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                return (View("Delete", employee));
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {

            //Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            Player employee = _context.Players.Find(id);

            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                if (employee.ImagePath != "\\images\\No_Image.png")
                {
                    System.IO.File.Delete(_webHostEnvironment.WebRootPath + employee.ImagePath);
                }
                _context.Remove(employee);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }

        }

        //public IActionResult Index()
        //{
        //	return View();
        //}
        public string GreetVisitor()
        {
            return "Welcome to SPorts Club";
        }
        public string CalculateAge(string name, int birthYear)
        {
            return $"Hi , {name}. You are {DateTime.Now.Year - birthYear} years old";
        }
    }
}
