using System.ComponentModel.DataAnnotations;

namespace Interactive.Models.ViewModels
{
    public class PostViewModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }
    }
}