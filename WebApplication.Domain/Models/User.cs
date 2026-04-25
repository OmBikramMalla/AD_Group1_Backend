using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace WebApplications.Domain.Models
{
    public class Users: IdentityUser<long> 
    {
       
        public string FullName { get; set; }

        public int? StudentId { get; set; }
        public int? InstructorId { get; set; }

    }
}
