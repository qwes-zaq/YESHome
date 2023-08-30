using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YESHome.Data.Models;
using YESHome.Models.AccountVM;

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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { PhoneNumber = model.PhoneNumber, UserName = model.UserName, RegistrationDate=DateTime.Today };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl="")
        {
            return View(new LoginVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                //User user = await _userManager.FindByNameAsync(model.UserName);
                if (result.Succeeded)
                {
                    if (model.UserName=="Nizami")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
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
                    Email = "Test@mail.com",
                    SecurityStamp = Guid.NewGuid().ToString() // Set a unique security stamp
                };
                var result = await _userManager.CreateAsync(Nizami, "YesHomeAdmin");

                result = await _userManager.CreateAsync(testUser, "YesHomeTest");

                await _userManager.AddToRoleAsync(Nizami, "Admin");
                await _userManager.AddToRoleAsync(testUser, "Employee");


            }


            return View();
        }
    }
}
