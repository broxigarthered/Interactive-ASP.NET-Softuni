using System.ComponentModel.DataAnnotations;

namespace Interactive.Models.ViewModels
{
    public class PostViewModel
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }
    }
}