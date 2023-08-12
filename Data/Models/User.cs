using Microsoft.AspNetCore.Identity;

namespace YesHome.Data.Models
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            Reports = new List<Report>();
        }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public ICollection<Report> Reports { get; set; }

    }
}
