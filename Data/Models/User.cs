using Microsoft.AspNetCore.Identity;

namespace YESHome.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Reports = new List<Report>();
        }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
