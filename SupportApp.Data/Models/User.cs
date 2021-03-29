using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportApp.Data.Models
{
    
    public class User : CommonEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime LastLoggedIn { get; set; }
        public bool IsActive { get; set; }
        public int CounrtyId { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual Country Country { get; set; }
        public virtual IdentityUser ApplicationUser { get; set; }

    }
}
