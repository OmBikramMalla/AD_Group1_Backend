using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace WebApplications.Domain.Models
{
    public class ModuleInstructor
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Module))]  // defining foreign key
        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }

        [ForeignKey(nameof(Instructor))]  // defining foreign key
        public int InstructorId { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}
