using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.API.Models
{
    public class AppDbContext : DbContext
    {
        // inject

        // ctor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<m_cls_Employee> Employees { get; set; }
        //public DbQuery<m_cls_Employee> Employees { get; set; }

       

        //
    }
}
