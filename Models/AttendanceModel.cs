namespace Employee_Attendance_2026.Models
{
    public class AttendanceModel
    {
        public int EmployeeId { get; set; }
        public string GroupName { get; set; } = "";
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; } = ""; // Present/Absent
    }
}
