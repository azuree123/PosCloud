using System.ComponentModel.DataAnnotations;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
        public bool IsAdmin { get; set; }
    }
}