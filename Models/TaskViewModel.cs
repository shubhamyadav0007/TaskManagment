using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBackend.Models
{
    public class TaskViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; }

        public string CreatedById { get; set; }

        [Display(Name = "Assigned Users")]
        public IEnumerable<string> AssignedUserIds { get; set; } = new List<string>();
        public IEnumerable<string> AssignedUserNames { get; set; } = new List<string>();

    }
}
