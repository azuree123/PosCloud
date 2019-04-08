using System.ComponentModel.DataAnnotations;

namespace POSApp.Core.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
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
        public string Name { get; set; }
        public string CompanyName { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9])\)?[-. ]?([0-9])$", ErrorMessage = "Entered phone format like (03211234567)")]
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
        [Display(Name = "Photo/Logo")]
        public string CompanyLogo { get; set; }
        [Required]
        public string Address { get; set; }
        public string Type { get; set; }
        public string Slug { get; set; }
        public bool RegisterMe { get; set; }


    }
}