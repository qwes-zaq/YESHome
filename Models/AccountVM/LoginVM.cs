using System.ComponentModel.DataAnnotations;

namespace YESHome.Models.AccountVM
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "Ad")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Sifre")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
