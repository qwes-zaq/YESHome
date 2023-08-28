using System.ComponentModel.DataAnnotations;

namespace YESHome.Models.AccountVM
{
    public class UserRegisterVM
    {
        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nomre")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Sifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Sifreni tesdiqle")]
        public string PasswordConfirm { get; set; }
    }
}