using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using EmployeeManager.APIClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EmployeeManager.APIClient.Controllers
{
    //[Authorize(Roles = "Manager")]
    public class ZGitHubController : Controller
    {
        private readonly HttpClient _client = null;

        private string zgithubApiUrl = "";

        //
        public ZGitHubController(HttpClient client, IConfiguration config)
        {
            _client = client;
            zgithubApiUrl = config.GetValue<string>("AppSettings:ZGitHubApiUrl");
        }


        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _client.GetAsync(zgithubApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            m_cls_ZGithub data = JsonSerializer.Deserialize<m_cls_ZGithub>(stringData, options);

            return View(data);
        }
    }
}