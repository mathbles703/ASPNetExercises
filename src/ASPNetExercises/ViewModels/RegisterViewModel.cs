using System.ComponentModel.DataAnnotations;
namespace ASPNetExercises.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Firstname is required.")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Lastname is required.")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [CompareAttribute("Password", ErrorMessage = "Passwords don't match.")]
        public string RepeatPassword { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "email needs valid format")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Street address is required.")]
        public string Address1 { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
    }
}