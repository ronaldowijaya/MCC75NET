using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.Repositories;
using MCC75NET.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace MCC75NET.Controllers
{
    public class UniversityController : Controller
    {       
        private readonly UniversityRepository repository;
        public UniversityController(UniversityRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            //if (HttpContext.Session.GetString("role") != "admin")
            //{
            //    return RedirectToAction("Forbidden", "Error");
            //}
            var universities = repository.GetAll();
            return View(universities);
        }
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            var university = repository.GetById(id);
            return View(university);
        }

        public IActionResult Create()
        {
            //var universities = context.Universities.ToList()
            //    .Select(u => new SelectListItem
            //    {
            //        Value=u.Id.ToString(),
            //        Text=u.Name
            //    });
            //ViewBag.UniversityId = universities;
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(University university)
        {
      
            var result = repository.Insert(university);
            if (result > 0)
            return RedirectToAction(nameof(Index));
            return View();
        }
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }
            var university = repository.GetById(id);
            return View(university);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(University university)
        {
            var result = repository.Update(university);
            if (result > 0)
{
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }
            var university = repository.GetById(id);
            return View(university);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            var result = repository.Delete(id);          
           
            if (result == 0)
            {

            }
            else
            {
                return RedirectToAction(nameof(Index));

            }
            return View();
        }
    }
}
