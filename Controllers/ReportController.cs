using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YesHome.Data;
using YESHome.Data.Models;
using YESHome.Models.ReportVM;

namespace YESHome.Controllers
{
    public class ReportController : Controller
    {
        private readonly YESHomeDb _context;
        public ReportController(YESHomeDb context)
        {
            _context = context;
        }
        // GET: ReportController
        public IActionResult Index()
        {
            return View(new ReportVM());
        }

        [HttpPost]
        public IActionResult Index(ReportVM model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var place = GetPlace(model);
            if (place == null)
            {
                return NotFound();
            }

            Report report = new Report()
            {
                UserId = userId,
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
