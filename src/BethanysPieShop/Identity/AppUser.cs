using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BethanysPieShop.Identity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // public string AddressLine1 { get; set; }
        // public string AddressLine2 { get; set; }
        public DateTime Birthdate { get; set; }
        // public string Country { get; set; }
        public string State { get; set;}
        public string City { get; set; }
        // public string ZipCode { get; set; }

        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; } = new List<IdentityUserClaim<string>>();
    }
}