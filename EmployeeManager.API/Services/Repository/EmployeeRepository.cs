using EmployeeManager.API.Models;
using EmployeeManager.API.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManager.API.Services.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        // SQL Repository
        // ------------------------------

        private readonly AppDbContext _db = null;

        public EmployeeRepository(AppDbContext db)
        {
            _db = db;
        }

        //
        public void Delete(int id)
        {
            var model = _db.Employees.Find(id);
            _db.Employees.Remove(model);
            _db.SaveChanges();
        }

        public void Insert(m_cls_Employee emp)
        {
            _db.Employees.Add(emp);
            _db.SaveChanges();
        }

        public List<m_cls_Employee> SelectAll()
        {
            List<m_cls_Employee> model = (from e in _db.Employees orderby e.EmployeeID select e).ToList();
            return model;
        }

        public m_cls_Employee SelectByID(int id)
        {
            return _db.Employees.Find(id);
        }

        public void Update(m_cls_Employee emp)
        {
            _db.Employees.Update(emp);
            _db.SaveChanges();
        }

        //
    }
}
