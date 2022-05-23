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
    public class Contractors : List<Contractor>
    {
        private SQLHelper _db;

        public Contractors()
        {
            _db = new SQLHelper();
            string sql = "SELECT Contractor_ID, First_Name, Last_Name, DOB, Email, Phone, Street, Suburb, [State], Postcode, Password FROM Contractor WHERE [Status] = 1";
            DataTable dataTable = _db.ExecuteSQL(sql);

            foreach (DataRow dr in dataTable.Rows)
            {
                Contractor newContractor = new Contractor(dr);
                this.Add(newContractor);
            }
        }

        // TODO WPF implement contractor assignment for service requests
        public Contractors(string skill, string suburb, DateTime date)
        {
            _db = new SQLHelper();
            string sql = "SELECT * " +
                "FROM Contractor AS c " +
                "INNER JOIN Contractor_Skill AS csk ON c.Contractor_ID = csk.Contractor_ID AND (csk.Skill_Category = @Skill AND csk.Status = 1) " +
                "INNER JOIN Contractor_Suburb AS csb ON c.Contractor_ID = csb.Contractor_ID AND csb.Suburb_Name = @Suburb " +
                "INNER JOIN Contractor_Availability AS ca ON c.Contractor_ID = ca.Contractor_ID AND(ca.Day_Name = @Date AND ca.Start_Time IS NOT NULL)";
            SqlParameter[] objParameters = new SqlParameter[3];
            objParameters[0] = new SqlParameter("@Skill", DbType.Int32);
            objParameters[0].Value = skill;
            objParameters[1] = new SqlParameter("@Suburb", DbType.Int32);
            objParameters[1].Value = suburb;
            objParameters[2] = new SqlParameter("@Date", DbType.Int32);
            objParameters[2].Value = date.ToString("dddd");
            DataTable dataTable = _db.ExecuteSQL(sql, objParameters);

            foreach (DataRow dr in dataTable.Rows)
            {
                Contractor availableContractor = new Contractor(dr);
                this.Add(availableContractor);
            }
        }
    }
}
