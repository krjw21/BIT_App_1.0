using BIT_WebApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BIT_WebApp.BLL
{
    public class Client
    {
        public int ClientID { get; set; }
        public string BusinessName { get; set; }
        public int ABN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Password { get; set; }
        private SQLHelper _db { get; set; }


        public Client()
        {
            _db = new SQLHelper();
        }

        // SQL query to select all active Service Requests for a Client
        public DataTable AllBookings() 
        {
            string sql = "SELECT sr.Service_Request_ID AS ID, ct.First_Name + ' ' + ct.Last_Name AS Contractor, cd.First_Name + ' ' + cd.Last_Name AS Coordinator, sr.Skill_Category AS Category, sr.Priority, sr.Job_Status AS [Job Status], sr.Payment_Status AS [Payment Status], CONVERT(NVARCHAR, sr.Date_Created, 103) AS [Date Created], sr.Street + ', ' + sr.Suburb + ', ' + sr.State + ' ' + sr.Postcode AS Address " +
                "FROM Service_Request AS sr " +
                "LEFT JOIN Contractor AS ct ON sr.Contractor_ID = ct.Contractor_ID " +
                "LEFT JOIN Coordinator AS cd ON sr.Coordinator_ID = cd.Coordinator_ID " +
                "WHERE Client_ID = @ClientID " +
                "AND Job_Status IN ('Requested', 'Assigned', 'Accepted', 'Rejected')";
            SqlParameter[] objParameters = new SqlParameter[1];
            objParameters[0] = new SqlParameter("@ClientID", DbType.Int32);
            objParameters[0].Value = this.ClientID;
            DataTable bookings = _db.ExecuteSQL(sql, objParameters);
            return bookings;
        }

        // SQL query to select Service Requests marked as "Completed" for a Client
        public DataTable CompletedBookings() 
        {
            string sql = "SELECT sr.Service_Request_ID AS ID, ct.First_Name + ' ' + ct.Last_Name AS Contractor, cd.First_Name + ' ' + cd.Last_Name AS Coordinator, sr.Skill_Category AS Category, sr.Priority, sr.Job_Status AS [Job Status], sr.Payment_Status AS [Payment Status], CONVERT(NVARCHAR, sr.Date_Created, 103) AS [Date Created], CONVERT(NVARCHAR, sr.Date_Completed, 103) AS [Date Completed], sr.Street + ', ' + sr.Suburb + ', ' + sr.State + ' ' + sr.Postcode AS Address " +
                "FROM Service_Request AS sr " +
                "INNER JOIN Contractor AS ct ON sr.Contractor_ID = ct.Contractor_ID " +
                "INNER JOIN Coordinator AS cd ON sr.Coordinator_ID = cd.Coordinator_ID " +
                "WHERE Client_ID = @ClientID " +
                "AND Job_Status = 'Completed'";
            SqlParameter[] objParameters = new SqlParameter[1];
            objParameters[0] = new SqlParameter("@ClientID", DbType.Int32);
            objParameters[0].Value = this.ClientID;
            DataTable bookings = _db.ExecuteSQL(sql, objParameters);
            return bookings;
        }
    }
}