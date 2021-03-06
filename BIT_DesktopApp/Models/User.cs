using BIT_DesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_DesktopApp.Models
{
    public class User
    {
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static int ID { get; set; }
        public static string Name { get; set; }
        private static SQLHelper _db;

        public User()
        {
            _db = new SQLHelper();
        }

        // Method/SQL query to check user (Coordinator/Administrator) login details
        public int CheckUser()
        {
            string sql = "SELECT Coordinator_ID, First_Name, Last_Name FROM Coordinator WHERE Email = @Email AND [Password] = @Password";
            SqlParameter[] objParameters = new SqlParameter[2];
            objParameters[0] = new SqlParameter("@Email", DbType.String);
            objParameters[0].Value = Email;
            objParameters[1] = new SqlParameter("@Password", DbType.String);
            objParameters[1].Value = Password;
            DataTable dataTable = _db.ExecuteSQL(sql, objParameters);

            int id = -1;
            if (dataTable.Rows.Count > 0)
            {
                id = Convert.ToInt32(dataTable.Rows[0][0]);
                string firstName = (string)dataTable.Rows[0][1];
                string lastName = (string)dataTable.Rows[0][2];
                Name = $"{firstName} {lastName}";
            }

            return id;
        }
    }
}
