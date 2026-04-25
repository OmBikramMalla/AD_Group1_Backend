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


        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<SalesInvoice> SalesInvoices { get; set; }
        public DbSet<SalesInvoiceItem> SalesInvoiceItems { get; set; }
        public DbSet<ServiceAppointment> ServiceAppointments { get; set; }
        public DbSet<ServiceReview> ServiceReviews { get; set; }
        public DbSet<PartRequest> PartRequests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Roles>().HasData(
    new Roles { Id = 1, Name = "Admin", ConcurrencyStamp = Guid.NewGuid().ToString() },
    new Roles { Id = 2, Name = "Staff", ConcurrencyStamp = Guid.NewGuid().ToString() },
    new Roles { Id = 3, Name = "Customer", ConcurrencyStamp = Guid.NewGuid().ToString() }
);
        }

    }

}