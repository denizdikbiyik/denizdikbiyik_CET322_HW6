using System;
using System.Collections.Generic;
using System.Text;
using denizdikbiyik_CET322_HW5.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using denizdikbiyik_CET322_HW5.ViewModels;

namespace denizdikbiyik_CET322_HW5.Data
{
    public class ApplicationDbContext : IdentityDbContext<CetUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<denizdikbiyik_CET322_HW5.Models.Student> Student { get; set; }

        public DbSet<denizdikbiyik_CET322_HW5.Models.Department> Department { get; set; }

        
    }
}
