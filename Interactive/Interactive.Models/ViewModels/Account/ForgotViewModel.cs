using System.ComponentModel.DataAnnotations;

namespace Interactive.Models.ViewModels.Account
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}