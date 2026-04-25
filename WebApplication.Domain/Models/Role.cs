using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplications.Domain.Models
{
    public class Roles : IdentityRole<long>
    {
        public Roles() { }
    }
}
