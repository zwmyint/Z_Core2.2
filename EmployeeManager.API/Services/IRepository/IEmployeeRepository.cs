using EmployeeManager.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.API.Services.IRepository
{
    public interface IEmployeeRepository
    {

        List<m_cls_Employee> SelectAll();
        m_cls_Employee SelectByID(int id);
        void Insert(m_cls_Employee emp);
        void Update(m_cls_Employee emp);
        void Delete(int id);

        //
    }
}
