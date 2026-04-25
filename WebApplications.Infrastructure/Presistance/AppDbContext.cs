using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplications.Domain.Models;

namespace WebApplications.Infrastructure.Presistance
{
    public class AppDbContext : IdentityDbContext<Users, Roles, long>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<ModuleInstructor> ModuleInstructors { get; set; } 

        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Name = "Admin",ConcurrencyStamp= "f6c52c5f-af53-4124-98c2-0f65d2d590e8" },
                new Roles { Id = 2, Name = "Instructor", ConcurrencyStamp = "a1ba56f5-76e0-4067-8ebb-4e8dc04cdb94" },
                new Roles { Id = 3, Name = "Student", ConcurrencyStamp = "ed6d49d2-0222-400f-aa77-4448a9194f93" }
            );
        }

    }

}