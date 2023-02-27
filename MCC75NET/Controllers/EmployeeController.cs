using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.Repositories;
using MCC75NET.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MCC75NET.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        //private readonly MyContext context;
        private readonly EmployeeRepository empRepo;

        public EmployeeController(EmployeeRepository empRepo)
        {
            //this.context = context;
            this.empRepo = empRepo;
        }
        public IActionResult Index()
        {
            /*if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }*/

            //var employee = context.Employees.ToList();
            //var results = context.Employees.Join(
            //    context.Employees,
            //    e => e.Nik,
            //    u => u.Nik,
            //    (e, u) => new EmployeeVM
            //    {
            //        Nik = e.Nik,
            //        FirstName = e.FirstName,
            //        LastName = e.LastName,
            //        BirthDate = e.BirthDate,
            //        Gender = e.Gender,
            //        HiringDate = e.HiringDate,
            //        Email = e.Email,
            //        PhoneNumber = e.PhoneNumber
            //    });
            var results = empRepo.GetEmployees();

            return View(results);
        }
        public IActionResult Details(string nik)
        {
            /*if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }*/

            //var employee = context.Employees.Find(id);
            var results = empRepo.GetEmployeeById(nik);
            return View(results);
            //return View(new EmployeeVM
            //{
            //    Nik = employee.Nik,
            //    FirstName = employee.FirstName,
            //    LastName = employee.LastName,
            //    BirthDate = employee.BirthDate,
            //    Gender = employee.Gender,
            //    HiringDate = employee.HiringDate,
            //    Email = employee.Email,
            //    PhoneNumber = employee.PhoneNumber
            //});

        }

        
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

            var genders = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "0",
                    Text = "Male"
                },
                new SelectListItem
                {
                    Value = "1",
                    Text = "Female"
                }
            };
            ViewBag.Genders = genders;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeVM employeeVm)
        {
            //context.Add(employeeVm)
            var result = empRepo.Insert(new Employee
            {
                Nik = employeeVm.Nik,
                FirstName = employeeVm.FirstName,
                LastName = employeeVm.LastName,
                BirthDate = employeeVm.BirthDate,
                Gender =   (Models.GenderEnum)employeeVm.Gender,
                HiringDate = employeeVm.HiringDate,
                Email = employeeVm.Email,
                PhoneNumber = employeeVm.PhoneNumber                
            });
            //var result = context.SaveChanges();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }

        public IActionResult Edit(string nik)
        {
            /*if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }*/

            var employee = empRepo.GetEmployeeById(nik);
            var genders = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "0",
                    Text = "Male"
                },
                new SelectListItem
                {
                    Value = "1",
                    Text = "Female"
                }
            };
            ViewBag.Genders = genders;

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeVM employeeVm)
        {
            //context.Entry(employee).State = EntityState.Modified;
            var result = empRepo.Update(new Employee
            {
                Nik = employeeVm.Nik,
                FirstName = employeeVm.FirstName,
                LastName = employeeVm.LastName,
                BirthDate = employeeVm.BirthDate,
                Gender = (Models.GenderEnum)employeeVm.Gender,
                HiringDate = employeeVm.HiringDate,
                Email = employeeVm.Email,
                PhoneNumber = employeeVm.PhoneNumber
            });
            //var result = context.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(string nik)
        {
            /*if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }*/

            //var employee = context.Employees.Find(nik);
            var employee = empRepo.GetEmployeeById(nik);

            return View(employee);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(string nik)
        {
            //var employee = context.Employees.Find(nik);
            var result = empRepo.Delete(nik);

            //context.Remove(employee);
            //var result = context.SaveChanges();
            if (result == 0)
            {

            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Delete));
        }
    }
}
