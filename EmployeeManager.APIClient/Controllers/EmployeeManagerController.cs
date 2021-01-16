using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using EmployeeManager.APIClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace EmployeeManager.APIClient.Controllers
{
    [Authorize(Roles = "Manager")]
    public class EmployeeManagerController : Controller
    {
        private readonly HttpClient _client = null;
        private string employeesApiUrl = "";

        //ctor
        public EmployeeManagerController(HttpClient client, IConfiguration config)
        {
            _client = client;
            employeesApiUrl = config.GetValue<string>("AppSettings:EmployeesApiUrl");
        }

        //
        public async Task<IActionResult> List() //ListAsync
        {
            HttpResponseMessage response = await _client.GetAsync(employeesApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<m_cls_Employee> data = JsonSerializer.Deserialize<List<m_cls_Employee>>(stringData, options);

            return View(data);
        }


        //
        [HttpGet]
        public async Task<IActionResult> Insert() //InsertAsync
        {
            await FillCountriesAsync();
            await FillTitlesAsync();

            return View();
        }

        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert(m_cls_Employee model) //InsertAsync
        {
            await FillCountriesAsync();
            await FillTitlesAsync();

            if (ModelState.IsValid)
            {
                string stringData = JsonSerializer.Serialize(model);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(employeesApiUrl, contentData);
                if (response.IsSuccessStatusCode)
                    ViewBag.Message = "Employee inserted successfully";
                else
                    ViewBag.Message = "Error when calling Web API";
            }

            return View(model);
        }


        //
        [HttpGet]
        public async Task<IActionResult> Update(int id) //UpdateAsync
        {
            await FillCountriesAsync();
            await FillTitlesAsync();

            HttpResponseMessage response = await _client.GetAsync($"{employeesApiUrl}/{id}");
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            m_cls_Employee model = JsonSerializer.Deserialize<m_cls_Employee>(stringData, options);
            return View(model);
        }

        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(m_cls_Employee model) //UpdateAsync
        {
            await FillCountriesAsync();
            await FillTitlesAsync();

            if (ModelState.IsValid)
            {
                string stringData = JsonSerializer.Serialize(model);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync($"{employeesApiUrl}/{model.EmployeeID}", contentData);
                if (response.IsSuccessStatusCode)
                    ViewBag.Message = "Employee updated successfully";
                else
                    ViewBag.Message = "Error when calling Web API";
            }

            return View(model);
        }


        //
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDeleteAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"{employeesApiUrl}/{id}");
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            m_cls_Employee model = JsonSerializer.Deserialize<m_cls_Employee>(stringData, options);
            return View(model);
        }

        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int employeeId) //DeleteAsync
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{employeesApiUrl}/{employeeId}");
            if (response.IsSuccessStatusCode)
                TempData["Message"] = "Employee deleted successfully";
            else
                TempData["Message"] = "Error while calling Web Api";

            return RedirectToAction("List");
        }


        //
        //
        public async Task<bool> FillCountriesAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(employeesApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var countries = JsonSerializer.Deserialize<List<m_cls_Employee>>(stringData, options).Select(c => c.Country).Distinct();

            List<SelectListItem> listOfCountries = new SelectList(countries).ToList();
            ViewBag.Countries = listOfCountries;
            return true;
        }
        //
        public async Task<bool> FillTitlesAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(employeesApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var titles = JsonSerializer.Deserialize<List<m_cls_Employee>>(stringData, options).Select(t => t.Title).Distinct();

            List<SelectListItem> listOfTitles = new SelectList(titles).ToList();
            ViewBag.Titles = listOfTitles;
            return true;
        }
        //

    }
}