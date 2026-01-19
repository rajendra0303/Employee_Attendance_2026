using Employee_Attendance_2026.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Employee_Attendance_2026.DAL;

namespace Employee_Attendance_2026.DAL
{
    public class AttendanceDAL
    {
        private readonly DbHelper _db;
        public AttendanceDAL(DbHelper db) { _db = db; }

        public void SaveAttendance(AttendanceModel model)
        {
            using var con = _db.GetConnection();
            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;

            if (model.GroupName == "Developer")
            {
                cmd.CommandText = "sp_DeveloperAttendance_Insert";
                cmd.Parameters.AddWithValue("@DevId", model.EmployeeId);
            }
            else if (model.GroupName == "Engineer")
            {
                cmd.CommandText = "sp_EngineerAttendance_Insert";
                cmd.Parameters.AddWithValue("@EngId", model.EmployeeId);
            }
            else
            {
                cmd.CommandText = "sp_InternAttendance_Insert";
                cmd.Parameters.AddWithValue("@InternId", model.EmployeeId);
            }

            cmd.Parameters.AddWithValue("@AttendanceDate", model.AttendanceDate.Date);
            cmd.Parameters.AddWithValue("@Status", model.Status);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        // Attendance history fetch (simple query no SP)
        public Dictionary<DateTime, string> GetAttendance(string groupName, int employeeId)
        {
            var data = new Dictionary<DateTime, string>();

            string table = groupName switch
            {
                "Developer" => "DeveloperAttendance",
                "Engineer" => "EngineerAttendance",
                _ => "InternAttendance"
            };

            string idCol = groupName switch
            {
                "Developer" => "DevId",
                "Engineer" => "EngId",
                _ => "InternId"
            };

            using var con = _db.GetConnection();
            using var cmd = new SqlCommand(
                $"SELECT AttendanceDate, Status FROM {table} WHERE {idCol}=@Id",
                con
            );
            cmd.Parameters.AddWithValue("@Id", employeeId);

            con.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DateTime dt = Convert.ToDateTime(dr["AttendanceDate"]);
                string status = dr["Status"].ToString()!;
                data[dt.Date] = status;
            }

            return data;
        }
    }
}
