﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkplaceManagementSystem.Models;

namespace WorkplaceManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // I added these three database sets to my Management database.
        public DbSet<EmployeeTasks> Tasks { get; set; }
        public DbSet<EmployeeInfo> Info { get; set; }
        public DbSet<Rewards> Rewards { get; set; }
    }
}