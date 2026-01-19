using Employee_Attendance_2026.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Employee_Attendance_2026.DAL;

namespace Employee_Attendance_2026.DAL
{
    public class GroupDAL
    {
        private readonly DbHelper _db;
        public GroupDAL(DbHelper db) { _db = db; }

        public List<EmployeeListModel> GetAll(string groupName, string search = "")
        {
            var list = new List<EmployeeListModel>();
            using var con = _db.GetConnection();

            string table = groupName switch
            {
                "Developer" => "Developers",
                "Engineer" => "Engineers",
                _ => "Interns"
            };

            string idCol = groupName switch
            {
                "Developer" => "DevId",
                "Engineer" => "EngId",
                _ => "InternId"
            };

            // ✅ Search Query
            string query = $@"
                SELECT {idCol}, FullName 
                FROM {table}
                WHERE FullName LIKE @Search
                ORDER BY FullName";

            using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Search", "%" + search + "%");

            con.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new EmployeeListModel
                {
                    EmployeeId = Convert.ToInt32(dr[0]),
                    FullName = dr[1].ToString()!
                });
            }

            return list;
        }
    }
}
