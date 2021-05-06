using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDeactivation.Data
{
    public class EmployeeDeactivationContext : DbContext
    {
        public EmployeeDeactivationContext (DbContextOptions<EmployeeDeactivationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Models.Employee> Employee { get; set; }
        public DbSet<Models.Teams> Teams { get; set; }
    }
}
