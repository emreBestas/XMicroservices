using System.ComponentModel.DataAnnotations;

namespace X.Web.Models
{
    public class SignInInput
    {
        [Required]
        [Display(Name ="Email Address")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember me")]
        public bool IsRemember { get; set; }
    }
}
