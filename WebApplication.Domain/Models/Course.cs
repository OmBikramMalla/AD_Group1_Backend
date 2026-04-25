using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplications.Domain.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int DurationYears { get; set; }

        public ICollection<Module> Modules { get; set; }

        // ✅ ADD THIS
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}