using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplications.Application.DTOs
{
    public class EnrollmentDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
