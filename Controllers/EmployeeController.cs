using Employee_Attendance_2026.DAL;
using Employee_Attendance_2026.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Attendance_2026.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDAL _dal;
        public EmployeeController(EmployeeDAL dal) { _dal = dal; }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(EmployeeModel model)
        {
            _dal.InsertEmployee(model);
            TempData["Msg"] = "Employee Saved Successfully!";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
