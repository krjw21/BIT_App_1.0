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

        // SQL query to display ALL Contractors
        public Contractors()
        {
            _db = new SQLHelper();
            string sql = "SELECT Contractor_ID, First_Name, Last_Name, First_Name + ' ' + Last_Name AS FullName, DOB, Email, Phone, Street, Suburb, [State], Postcode, Password FROM Contractor WHERE [Status] = 1";
            DataTable dataTable = _db.ExecuteSQL(sql);

            foreach (DataRow dr in dataTable.Rows)
            {
                Contractor newContractor = new Contractor(dr);
                this.Add(newContractor);
            }
        }

        // SQL query to display Contractors with added search filter
        public Contractors(string searchText, string searchFilter)
        {
            _db = new SQLHelper();

            if (searchFilter == "ID") { searchFilter = "Contractor_ID"; }
            if (searchFilter == "First Name") { searchFilter = "First_Name"; }
            if (searchFilter == "Last Name") { searchFilter = "Last_Name"; }
            // + Email, Phone as possible search filters

            string sql = $"SELECT Contractor_ID, First_Name, Last_Name, First_Name + ' ' + Last_Name AS FullName, DOB, Email, Phone, Street, Suburb, [State], Postcode, Password FROM Contractor WHERE [Status] = 1 AND {searchFilter} LIKE '%{searchText}%'";
            DataTable dataTable = _db.ExecuteSQL(sql);

            foreach (DataRow dr in dataTable.Rows)
            {
                Contractor newContractor = new Contractor(dr);
                this.Add(newContractor);
            }
        }

        // SQL query to list available Contractors for a specific Service Sequest
        public Contractors(int? serviceRequestID, string skill, string suburb, DateTime date)
        {
            _db = new SQLHelper();
            string sql = "SELECT c.Contractor_ID, c.First_Name, c.Last_Name, c.First_Name + ' ' + c.Last_Name AS FullName, c.DOB, c.Email, c.Phone, c.Street, c.Suburb, c.[State], c.Postcode, c.Password " +
                "FROM Contractor AS c " +
                "INNER JOIN Contractor_Skill AS csk ON c.Contractor_ID = csk.Contractor_ID AND (csk.Skill_Category = @Skill AND csk.Status = 1) " +
                "INNER JOIN Contractor_Suburb AS csb ON c.Contractor_ID = csb.Contractor_ID AND csb.Suburb_Name = @Suburb " +
                "INNER JOIN Contractor_Availability AS ca ON c.Contractor_ID = ca.Contractor_ID AND(ca.Day_Name = @Date AND ca.Start_Time IS NOT NULL) " +
                "WHERE c.Contractor_ID NOT IN (SELECT Contractor_ID FROM Rejected_Request WHERE Service_Request_ID = @ServiceRequestID)";
            SqlParameter[] objParameters = new SqlParameter[4];
            objParameters[0] = new SqlParameter("@Skill", DbType.Int32);
            objParameters[0].Value = skill;
            objParameters[1] = new SqlParameter("@Suburb", DbType.Int32);
            objParameters[1].Value = suburb;
            objParameters[2] = new SqlParameter("@Date", DbType.Int32);
            objParameters[2].Value = date.ToString("dddd");
            objParameters[3] = new SqlParameter("@ServiceRequestID", DbType.Int32);
            objParameters[3].Value = serviceRequestID;
            DataTable dataTable = _db.ExecuteSQL(sql, objParameters);

            foreach (DataRow dr in dataTable.Rows)
            {
                Contractor availableContractor = new Contractor(dr);
                this.Add(availableContractor);
            }
        }
    }
}
