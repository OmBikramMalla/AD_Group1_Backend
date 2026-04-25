using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplications.Application.DTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DurationYears { get; set; }

        public int ModuleCount { get; set; } // optional
    }
}
