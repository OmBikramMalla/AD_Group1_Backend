using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebApplications.Domain.Models
{
    public  class Enrollment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Student))]  // defining foreign key
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        [ForeignKey(nameof(Course))]  // defining foreign key
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}
