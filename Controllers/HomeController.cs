using Microsoft.AspNetCore.Mvc;
using Employee_Attendance_2026.DAL;

namespace Employee_Attendance_2026.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbHelper db;

        // ✅ DI will inject DbHelper
        public HomeController(DbHelper dbHelper)
        {
            db = dbHelper;
        }

        public IActionResult Index()
        {
            // ✅ Your actual tables (Developers/Engineers/Interns)
            int totalDev = Convert.ToInt32(db.Scalar("SELECT COUNT(*) FROM Developers"));
            int totalEng = Convert.ToInt32(db.Scalar("SELECT COUNT(*) FROM Engineers"));
            int totalIntern = Convert.ToInt32(db.Scalar("SELECT COUNT(*) FROM Interns"));

            // ✅ Optional: Today present counts (all attendance tables)
            string today = DateTime.Today.ToString("yyyy-MM-dd");

            int devPresent = Convert.ToInt32(db.Scalar(
                $"SELECT COUNT(*) FROM DeveloperAttendance WHERE AttendanceDate='{today}' AND Status='Present'"));

            int engPresent = Convert.ToInt32(db.Scalar(
                $"SELECT COUNT(*) FROM EngineerAttendance WHERE AttendanceDate='{today}' AND Status='Present'"));

            int internPresent = Convert.ToInt32(db.Scalar(
                $"SELECT COUNT(*) FROM InternAttendance WHERE AttendanceDate='{today}' AND Status='Present'"));

            ViewBag.TotalDev = totalDev;
            ViewBag.TotalEng = totalEng;
            ViewBag.TotalIntern = totalIntern;

            ViewBag.TodayDevPresent = devPresent;
            ViewBag.TodayEngPresent = engPresent;
            ViewBag.TodayInternPresent = internPresent;

            ViewBag.TodayTotalPresent = devPresent + engPresent + internPresent;

            return View();
        }

        
    }
}
