using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YESHome.Data.Models;

namespace YESHome.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AdminCreate()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                await _roleManager.CreateAsync(new IdentityRole { Name = "Employee" });

            }

            if (!_userManager.Users.Any())
            {
                var Nizami = new User()
                {
                    UserName = "Nizami",
                    //FirstName="Nizami",
                    PhoneNumber = "+994 50 591 4163",
                    Email = "info@yeshome.az",
                    SecurityStamp = Guid.NewGuid().ToString() // Set a unique security stamp
                };
                var testUser = new User()
                {
                    //FirstName = "",
                    UserName = "TestName",
                    PhoneNumber = "+994 50 123 4567",
                    Email = "info@yeshome.az",
                    SecurityStamp = Guid.NewGuid().ToString() // Set a unique security stamp
                };
                var result = await _userManager.CreateAsync(Nizami, "YesHomeAdmin.1");

                result = await _userManager.CreateAsync(testUser, "YesHomeTest.1");

                await _userManager.AddToRoleAsync(Nizami, "Admin");
                await _userManager.AddToRoleAsync(testUser, "Employee");


            }


            return View();
        }
    }
}
