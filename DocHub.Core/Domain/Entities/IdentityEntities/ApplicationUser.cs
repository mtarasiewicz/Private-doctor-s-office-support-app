using Microsoft.AspNetCore.Identity;
using System;

namespace DocHub.Core.Domain.Entities.IdentityEntities
{
    /// <summary>
    ///  A class representing a userin the authentication system, inheriting from IdentityUser with a unique GUID.
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public virtual Patient? Patient { get; set; }
    }
}
