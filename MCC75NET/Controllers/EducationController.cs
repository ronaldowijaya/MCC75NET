using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.Repositories;
using MCC75NET.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MCC75NET.Controllers
{
    [Authorize]
    public class EducationController : Controller
    {
        //private readonly MyContext context;
        private readonly EducationRepository eduRepo;
        private readonly UniversityRepository uniRepo;

        public EducationController(EducationRepository eduRepo, UniversityRepository uniRepo)
        {
            //this.context = context;
            this.eduRepo = eduRepo;
            this.uniRepo = uniRepo;
        }
        public IActionResult Index()
        {
            //var educations = context.Educations.ToList();
            //var results = context.Educations.Join(
            //    context.Universities,
            //    e => e.UniversityId,
            //    u => u.Id,
            //    (e, u) => new EducationVM
            //    {
            //        Id = e.Id,
            //        Degree = e.Degree,
            //        Gpa = e.Gpa,
            //        Major = e.Major,
            //        UniversityName = u.Name,
            //    });

            /*if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }*/

            var results = eduRepo.GetEducationUniversities();
            return View(results);            
        }
        public IActionResult Details(int id)
        {
            /*if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }*/

            //var educations = context.Educations.Find(id);
            var results = eduRepo.GetEducationUniversitiesById(id);
            return View(results);


            //return View(new EducationUniversityVM
            //{                
            //    Id = educations.Id,
            //    Degree = educations.Degree,
            //    Gpa = educations.Gpa,
            //    Major = educations.Major,
            //    UniversityName = context.Universities.Find(educations.UniversityId).Name,

            //});
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            /*if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }*/

            var universities = uniRepo.GetAll()
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                });
            ViewBag.UniversityId = universities;
            
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EducationUniversityVM educationVM)
        {
            var result = eduRepo.Insert(new Education
            {
                Id = educationVM.Id,
                Degree = educationVM.Degree,
                Gpa = educationVM.Gpa,
                Major = educationVM.Major,
                UniversityId = Convert.ToInt16(educationVM.UniversityName)
            });

            //context.Add(education);
            
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            /*if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }*/

            var results = eduRepo.GetEducationUniversitiesById(id);
            var universities = uniRepo.GetAll()
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                });
            ViewBag.UniversityId = universities;
            return View(results);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EducationUniversityVM education)
        {
            var result = eduRepo.Update(new Education
            {
                Id = education.Id,
                Degree = education.Degree,
                Gpa = education.Gpa,
                Major = education.Major,
                UniversityId = Convert.ToInt16(education.UniversityName),
            });
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            /*if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }*/

            //var education = context.Educations.Find(id);
            var results = eduRepo.GetEducationUniversitiesById(id);
            return View(results);
            //return View(new EducationUniversityVM
            //{
            //    Id = education.Id,
            //    Degree = education.Degree,
            //    Gpa = education.Gpa,
            //    Major = education.Major,
            //    UniversityName = context.Universities.Find(education.UniversityId).Name,

            //});
            //return View(education);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            var result = eduRepo.Delete(id);
            if(result == 0)
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
