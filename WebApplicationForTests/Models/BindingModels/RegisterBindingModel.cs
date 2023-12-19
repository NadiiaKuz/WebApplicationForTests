using System.ComponentModel.DataAnnotations;

namespace WebApplicationForTests.Models.BindingModels
{
    public class RegisterBindingModel
    {
        [Display(Name = "Login")]
        [Required]
        public string Login { get; set; }
        [Display(Name = "Password")]
        [UIHint("Password")]
        [MinLength(6)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [UIHint("Password")]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}
