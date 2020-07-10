using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Padaria.Domain.Entities.Identity
{
    public class Role : IdentityRole<Guid>
    {
        public List<UserRole> UserRoles { get; set; }
    }
}