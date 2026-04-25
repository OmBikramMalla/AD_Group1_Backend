using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebApplications.Domain.Models
{
    public class Module
    {
        [Key]  // defining primary key
        public int Id { get; set; }
        public String Title { get; set; }
        public int Credtis { get; set; }

        [ForeignKey(nameof(Course))]  // defining foreign key
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public ICollection<ModuleInstructor> ModuleInstructors { get; set; }
    }
}
