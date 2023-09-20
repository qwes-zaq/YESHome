using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YesHome.Data;
using YESHome.Data.Models;
using YESHome.Models.ReportVM;

namespace YESHome.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly YESHomeDb _context;
        private readonly UserManager<User> _userManager;
        public ReportController(YESHomeDb context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: ReportController
        public IActionResult Index()
        {
            var id = _userManager.GetUserId(User);
            var report = _context.Reports?.FirstOrDefault(r => r.WorkStart==DateTime.Today);
            return View(new ReportVM()
            {
                IsAlredyOnPlace = report != null
            }) ;
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(ReportVM model)
        {
            var user = await _userManager.GetUserAsync(User);
            var place = GetPlace(model);
            if (place == null)
            {
                return NotFound();
            }

            Report report = new Report()
            {
                User = user,
                Place = place,
                WorkStart = DateTime.Today
            };
            _context.Reports.Add(report);
            _context.SaveChanges();
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        public Place GetPlace(ReportVM model)
        {
            var PlaceList = _context.Places.ToList();
            Place closestPlace = null;
            double minDistance = double.MaxValue;

            foreach (var place in PlaceList)
            {
                if (CalculateDistance(Convert.ToDouble(model.Latitude.Replace(".", ",")), Convert.ToDouble(model.Longitude.Replace(".", ",")), place.Latitude, place.Longitude) < 200)
                    return place;
            }

            return null;
        }
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double earthRadius = 6371; // Радиус Земли в километрах

            // Переводим координаты в радианы
            double lat1Rad = DegToRad(lat1);
            double lon1Rad = DegToRad(lon1);
            double lat2Rad = DegToRad(lat2);
            double lon2Rad = DegToRad(lon2);

            // Разница широты и долготы
            double deltaLat = lat2Rad - lat1Rad;
            double deltaLon = lon2Rad - lon1Rad;

            // Формула гаверсинусов
            double a = Math.Pow(Math.Sin(deltaLat / 2), 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) * Math.Pow(Math.Sin(deltaLon / 2), 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Расстояние между точками
            double distance = earthRadius * c;

            return distance;
        }

        private static double DegToRad(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}
