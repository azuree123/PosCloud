using System.ComponentModel.DataAnnotations;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^[0-9]{1,6}$", ErrorMessage = "Must Enter Digits in password")]
        //[StringLength(6, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }

        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Contact", ResourceType = typeof(Resource))]
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
        [Display(Name = "Photo/Logo")]
        public string CompanyLogo { get; set; }
        [Required]
        [Display(Name = "Address", ResourceType = typeof(Resource))]
        public string Address { get; set; }
        public string Type { get; set; }
        public string Slug { get; set; }
        public bool RegisterMe { get; set; }


    }
}