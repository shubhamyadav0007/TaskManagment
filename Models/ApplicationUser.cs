using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TaskBackend.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? Name { get; set; }
        public string? OrganizationName { get; set; }
        public override string PhoneNumber { get; set; }

        //navigationor we cansay relationshipmaintain
        public ICollection<ApplicationUser> User { get; set; } = new List<ApplicationUser>();
        public ICollection<Task> CreatedTasks { get; set; } = new List<Task>();
        public ICollection<Task> AssignedTasks { get; set; } = new List<Task>();

    }
}
