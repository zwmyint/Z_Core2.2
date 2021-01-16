using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.API.Models;
using EmployeeManager.API.Services.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository = null;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        //
        [HttpGet]
        public List<m_cls_Employee> Get()
        {
            return _employeeRepository.SelectAll();
        }

        //
        [HttpGet("{id}")]
        public m_cls_Employee Get(int id)
        {
            return _employeeRepository.SelectByID(id);
        }

        //
        [HttpPost]
        public void Post([FromBody] m_cls_Employee emp)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Insert(emp);
            }
        }

        //
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] m_cls_Employee emp)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Update(emp);
            }
        }

        //
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _employeeRepository.Delete(id);
        }


        //
    }
}