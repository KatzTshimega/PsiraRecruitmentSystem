using System.ComponentModel.DataAnnotations;

namespace PsiraRecruitmentSystem.ViewModels.Account
{
    public class RegisterViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "ID Number")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "ID Number must be 13 digits.")]
        public string IdNumber { get; set; }

        [Required]
        [Display(Name = "Cellphone Number")]
        [Phone]
        public string CellPhoneNumber { get; set; }

        [Display(Name = "Work Number")]
        [Phone]
        public string WorkNumber { get; set; }

        [Required]
        [Display(Name = "Home Address")]
        public string HomeAddress { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        [RegularExpression(@"^\d{4,6}$", ErrorMessage = "Enter a valid postal code.")]
        public string PostalCode { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string Role { get; set; } = "Applicant";
    }
}
