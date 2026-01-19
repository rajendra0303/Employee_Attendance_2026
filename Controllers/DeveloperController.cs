using Employee_Attendance_2026.DAL;
using Employee_Attendance_2026.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Employee_Attendance_2026.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly GroupDAL _groupDal;
        private readonly AttendanceDAL _attDal;
       
        public DeveloperController(GroupDAL groupDal, AttendanceDAL attDal)
        {
            _groupDal = groupDal;
            _attDal = attDal;
        }

        public IActionResult Index(string search = "")
        {
            var list = _groupDal.GetAll("Developer", search);
            ViewBag.Search = search;
            return View(list);
        }


        public IActionResult Calendar(int id)
        {
            string groupName = "Developer";
            var attendance = _attDal.GetAttendance(groupName, id);

            // ✅ current month range
            DateTime monthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime monthEnd = DateTime.Today;

            // ✅ calendar list - current month only
            var days = new List<CalendarDayModel>();
            for (DateTime d = monthStart; d <= monthEnd; d = d.AddDays(1))
            {
                bool isSunday = d.DayOfWeek == DayOfWeek.Sunday;   // ✅ ADD

                attendance.TryGetValue(d.Date, out string? status);

                days.Add(new CalendarDayModel
                {
                    Date = d.Date,
                    Status = status,
                    IsToday = d.Date == DateTime.Today,
                    IsSunday = isSunday   // ✅ ADD
                });
            }


            // ✅ count only current month
            int presentCount = attendance.Count(x =>
                x.Key.Date >= monthStart &&
                x.Key.Date <= monthEnd &&
                x.Key.DayOfWeek != DayOfWeek.Sunday &&
                x.Value == "Present");

            int absentCount = attendance.Count(x =>
                x.Key.Date >= monthStart &&
                x.Key.Date <= monthEnd &&
                x.Key.DayOfWeek != DayOfWeek.Sunday &&
                x.Value == "Absent");


            ViewBag.PresentCount = presentCount;
            ViewBag.AbsentCount = absentCount;

            ViewBag.EmployeeId = id;
            ViewBag.GroupName = groupName;

            return View(days);
        }

        [HttpPost]
        public IActionResult MarkAttendance(AttendanceModel model)
        {
            model.GroupName = "Developer";
            model.AttendanceDate = DateTime.Today; // only today allowed
            _attDal.SaveAttendance(model);

            return RedirectToAction("Calendar", new { id = model.EmployeeId });
        }

    }
}
