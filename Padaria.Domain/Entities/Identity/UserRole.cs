using Microsoft.AspNetCore.Identity;
using System;

namespace Padaria.Domain.Entities.Identity
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}