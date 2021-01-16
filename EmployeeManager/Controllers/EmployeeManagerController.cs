using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManager.Controllers
{
    [Authorize(Roles = "Manager")]
    public class EmployeeManagerController : Controller
    {
        private AppDbContext db = null;

        public EmployeeManagerController(AppDbContext db)
        {
            this.db = db;
        }

        //
        public IActionResult List()
        {
            List<m_cls_Employee> model = (from e in db.Employees orderby e.EmployeeID select e).ToList();
                                    
            return View(model);
        }

        //
        [HttpGet]
        public IActionResult Insert()
        {
            FillCountries();
            FillTitle();

            return View();
        }

        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(m_cls_Employee model)
        {
            FillCountries();
            FillTitle();

            if (ModelState.IsValid)
            {
                db.Employees.Add(model);
                db.SaveChanges();
                ViewBag.Message = "Employee added successfully";
            }

            return View(model);
        }


        //
        [HttpGet]
        public IActionResult Update(int id)
        {
            FillCountries();
            FillTitle();

            var model = db.Employees.Find(id);
            return View(model);
        }

        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(m_cls_Employee model)
        {
            FillCountries();
            if (ModelState.IsValid)
            {
                db.Employees.Update(model);
                db.SaveChanges();
                ViewBag.Message = "Employee updated successfully";
            }

            return View(model);
        }


        //
        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            var model = db.Employees.Find(id);
            return View(model);
        }

        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var model = db.Employees.Find(id);
            db.Employees.Remove(model);
            db.SaveChanges();
            TempData["Message"] = "Employee Deleted Successfully";
            return RedirectToAction("List");
        }



        //
        private void FillTitle()
        {
            List<SelectListItem> listOfTitles = (from c in db.Employees
                                                    select new SelectListItem()
                                                    {
                                                        Text = c.Title,
                                                        Value = c.Title
                                                    }).Distinct().ToList();
            ViewBag.Titles = listOfTitles;

        }

        //
        private void FillCountries()
        {
            List<SelectListItem> listOfCountries = (from c in db.Employees
                                                    select new SelectListItem()
                                                    {
                                                        Text = c.Country,
                                                        Value = c.Country
                                                    }).Distinct().ToList();
            ViewBag.Countries = listOfCountries;

        }





        //
    }
}