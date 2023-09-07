using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YesHome.Data;
using YESHome.Areas.Admin.Models.UserVM;
using YESHome.Data.Models;

namespace YESHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly YESHomeDb _context;
        public UserController(UserManager<User> userManager, YESHomeDb context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var userList = _context.Users
                .Include(x => x.Reports)
                    .ThenInclude(x => x.Place)
                //.Select(x => x.Reports.FirstOrDefault(r => r.WorkStart == DateTime.Today))
                .ToList();
            List<UserVM> userVMList = new List<UserVM>();
            foreach (var item in userList)
            {
                if (item.Id != null)
                {
                    userVMList.Add(new UserVM()
                    {
                        Id = item.Id,
                        Name = item.UserName,
                        PlaceName = item.Reports.FirstOrDefault(r => r.WorkStart == DateTime.Today)?.Place?.Name

                    });
                }

            }

            return View(userVMList);
        }

        // GET: UserController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: UserController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // GET: UserController/Delete/5
        public IActionResult Delete(string id)
        {
            
            return View(new UserDeleteVM() { 
                Id= id,
                Name=_userManager.Users.FirstOrDefault(x=> x.Id==id).Email            
            });
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                _context.Remove(_context.Users.Find(id));
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
