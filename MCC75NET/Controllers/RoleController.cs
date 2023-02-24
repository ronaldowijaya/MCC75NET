﻿using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace MCC75NET.Controllers
{
    public class RoleController : Controller
    {
        private readonly MyContext context;
        private readonly RoleRepository roleRepo;

        public RoleController(MyContext context, RoleRepository roleRepo)
        {
            this.context = context;
            this.roleRepo = roleRepo;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }
            var roles = roleRepo.GetAll();
            return View(roles);
        }
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            if (HttpContext.Session.GetString("role") != "admin")
            {
                return RedirectToAction("Forbidden", "Error");
            }
            var role = roleRepo.GetById(id);
            return View(role);
        }
        public IActionResult Create()
        {
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
        public IActionResult Create(Role role)
        {
            //context.Add(role);
            var result = roleRepo.Insert(role);
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
            var role = roleRepo.GetById(id);
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Role role)
        {
            //context.Entry(role).State = EntityState.Modified;
            var result = roleRepo.Update(role);
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
            var role = roleRepo.GetById(id);
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            var result = roleRepo.Delete(id);
            //context.Remove(role);
            //var result = context.SaveChanges();
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
