using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplications.Domain.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        // ✅ CORRECT RELATIONSHIP
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}