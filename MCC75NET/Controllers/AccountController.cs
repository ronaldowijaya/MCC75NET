using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.Repositories;
using MCC75NET.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Data;

namespace MCC75NET.Controllers
{
    public class AccountController : Controller
    {
        //private readonly MyContext context;
        private readonly AccountRepository accRepo;
        private readonly EmployeeRepository empRepo;


        public AccountController( AccountRepository accRepo, EmployeeRepository empRepo)
        {
            //this.context = context;
            this.accRepo = accRepo;
            this.empRepo = empRepo;
        }
        public IActionResult Index()
        {
            var accounts = accRepo.GetAccountEmployees();
            //return View(new RegisterVM
            //{
            //    Password = accounts.Password,
            //    Nik = accounts.EmployeeNik,
            //});
            return View(accounts);
        }
/*        public IActionResult Details(string nik)
        {

            var account = accRepo.GetById(nik);
            return View(new RegisterVM
            {
                Password = account.Password,
                Nik = account.EmployeeNik,
            });
            return View(account);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Account account)
        {
            var result = accRepo.Insert(account);
            
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }

        public IActionResult Edit(string nik)
        {
            var account = accRepo.GetById(nik);
            var result = accRepo.Update;

            return View(new RegisterVM
            {
                Password = account.Password,
                //Nik = account.EmployeeNik,
            });
            //return View(account);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Account account)
        {
            var result = accRepo.Update(account);
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(string nik)
        {
            var account = accRepo.GetById(nik);
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(string nik)
        {
            var result = accRepo.Delete(nik);

            if (result == 0)
            {

            }
            else
            {
                return RedirectToAction(nameof(Index));

            }
            return View();
        }

        //get: Account/Register
        public IActionResult Register()
        {
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

            //var universities = context.Universities.ToList()
            //    .Select(u => new SelectListItem
            //    {
            //        Value = u.Id.ToString(),
            //        Text = u.Name
            //    });
            //ViewBag.UniversityId = universities;
            return View();
        }

        //post: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                var result = accRepo.Register(registerVM);
                if (result > 0)
                {
                    return RedirectToAction("Index", "Home");
                }                                
            }
            return View();
        }

        public IActionResult Login(LoginVM loginVM)
        {

            //var getAccounts = context.Employees.Join(
            //    context.Accounts,
            //    e => e.Nik,
            //    a => a.EmployeeNik,
            //    (e, a) => new LoginVM
            //    {
            //        Email = e.Email,
            //        Password = a.Password,
            //    });
            if (accRepo.Login(loginVM))
            {
                //var userdata = repository.Login(loginVM);
                var userdata = accRepo.GetUserdata(loginVM.Email);
                HttpContext.Session.SetString("email", userdata.Email);
                HttpContext.Session.SetString("fullName", userdata.FullName);
                HttpContext.Session.SetString("role", userdata.Role);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Account or Password Not Found!");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
