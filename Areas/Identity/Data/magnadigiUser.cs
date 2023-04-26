using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace magnadigi.Areas.Identity.Data;

// Add profile data for application users by adding properties to the magnadigiUser class
public class magnadigiUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? BusinessName { get; set; }
    
}

