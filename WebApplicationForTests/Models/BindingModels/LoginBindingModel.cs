using System.ComponentModel.DataAnnotations;

namespace WebApplicationForTests.Models.BindingModels
{
    public class LoginBindingModel
    {
        [Display(Name = "Login")]
        [Required]
        public string Login { get; set; }

        [Display(Name = "Password")]
        [UIHint("Password")]
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}