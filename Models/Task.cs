using System.ComponentModel.DataAnnotations;

namespace TaskBackend.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]

        public string? Status { get; set; }
        public string? CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public ICollection<ApplicationUser> AssignedUsers { get; set; } = new List<ApplicationUser>();
    }

}
