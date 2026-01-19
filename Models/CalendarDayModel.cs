namespace Employee_Attendance_2026.Models
{
    public class CalendarDayModel
    {
        public DateTime Date { get; set; }
        public string? Status { get; set; } // null means not marked
        public bool IsToday { get; set; }
        public bool IsSunday { get; set; }

    }
}
