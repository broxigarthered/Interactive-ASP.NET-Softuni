using System.ComponentModel.DataAnnotations;

namespace Interactive.Models.ViewModels.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Microsoft.Build.Framework.Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}