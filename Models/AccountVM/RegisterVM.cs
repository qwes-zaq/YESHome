using System.ComponentModel.DataAnnotations;

namespace YESHome.Models.AccountVM
{
    public class RegisterVM
    {
        [Required]
        [Display(Name = "Ad")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        [Required]
        [Display(Name = "Год рождения")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Telefon")]  
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Sifre")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
