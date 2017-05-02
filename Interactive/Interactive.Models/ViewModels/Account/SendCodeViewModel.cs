using System.Collections.Generic;
using System.Web.Mvc;

namespace Interactive.Models.ViewModels.Account
{
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public List<SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}