using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.Domain.Entities.IdentityEntities
{
    /// <summary>
    /// A class representing a role in the authentication system, inheriting from IdentityRole with a unique GUID.
    /// </summary>
    public class ApplicationRole : IdentityRole<Guid>
    {
    }
}
