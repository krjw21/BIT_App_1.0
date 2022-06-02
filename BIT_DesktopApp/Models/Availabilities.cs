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
    public class Availabilities : List<Availability>
    {
        private SQLHelper _db;


        // SQL query to display availabilities for a specific Contractor
        public Availabilities(int? contractorID)
        {
            _db = new SQLHelper();
            string sql = "SELECT Contractor_ID, Day_Name, Start_Time, Finish_Time " +
                "FROM Contractor_Availability " +
                "WHERE Contractor_ID = @ContractorID " +
                "ORDER BY " +
                "CASE " +
                "WHEN Day_Name = 'Monday' THEN 1 " +
                "WHEN Day_Name = 'Tuesday' THEN 2 " +
                "WHEN Day_Name = 'Wednesday' THEN 3 " +
                "WHEN Day_Name = 'Thursday' THEN 4 " +
                "WHEN Day_Name = 'Friday' THEN 5 " +
                "WHEN Day_Name = 'Saturday' THEN 6 " +
                "WHEN Day_Name = 'Sunday' THEN 7 " +
                "END ASC";
            SqlParameter[] objParameters = new SqlParameter[1];
            objParameters[0] = new SqlParameter("@ContractorID", DbType.Int32);
            objParameters[0].Value = contractorID;
            DataTable availabilities = _db.ExecuteSQL(sql, objParameters);
            foreach (DataRow dr in availabilities.Rows)
            {
                Availability availability = new Availability(dr);
                this.Add(availability);
            }
        }
    }
}
