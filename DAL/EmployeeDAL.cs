using Employee_Attendance_2026.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Employee_Attendance_2026.DAL;

namespace Employee_Attendance_2026.DAL
{
    public class EmployeeDAL
    {
        private readonly DbHelper _db;

        public EmployeeDAL(DbHelper db)
        {
            _db = db;
        }

        public void InsertEmployee(EmployeeModel model)
        {
            using var con = _db.GetConnection();
            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = model.GroupName switch
            {
                "Developer" => "sp_Developer_Insert",
                "Engineer" => "sp_Engineer_Insert",
                _ => "sp_Intern_Insert"
            };

            cmd.Parameters.AddWithValue("@FullName", model.FullName);
            cmd.Parameters.AddWithValue("@Address", model.Address);
            cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
            cmd.Parameters.AddWithValue("@Email", model.Email);
            cmd.Parameters.AddWithValue("@JoiningDate", model.JoiningDate);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
