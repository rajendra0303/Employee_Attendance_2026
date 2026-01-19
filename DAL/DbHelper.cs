using Microsoft.Data.SqlClient;
using System.Data;

namespace Employee_Attendance_2026.DAL
{
    public class DbHelper
    {
        private readonly string _cs;

        public DbHelper(string connectionString)
        {
            _cs = connectionString;
        }

        // ✅ This fixes GetConnection error
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_cs);
        }

        public object Scalar(string query)
        {
            using SqlConnection con = new SqlConnection(_cs);
            using SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            return cmd.ExecuteScalar();
        }

        public int Execute(string query)
        {
            using SqlConnection con = new SqlConnection(_cs);
            using SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            return cmd.ExecuteNonQuery();
        }

        public DataTable GetData(string query)
        {
            using SqlConnection con = new SqlConnection(_cs);
            using SqlCommand cmd = new SqlCommand(query, con);

            using SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
