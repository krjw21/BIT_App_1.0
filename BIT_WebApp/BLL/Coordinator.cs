using BIT_WebApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BIT_WebApp.BLL
{
    public class Coordinator
    {
        public int CoordinatorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Password { get; set; }
        private SQLHelper _db { get; set; }


        public Coordinator()
        {
            _db = new SQLHelper();
        }

        // SQL query to display all "Unassigned" and "Rejected" Service Requests
        public DataTable UnassignedBookings() 
        {
            string sql = "SELECT sr.Service_Request_ID AS ID, c.Business_Name AS Business, c.First_Name + ' ' + c.Last_Name AS Contact, c.Phone, sr.Skill_Category AS Category, sr.Priority, sr.Job_Status AS [Job Status], CONVERT(NVARCHAR, sr.Date_Created, 103) AS [Date Created], sr.Street + ', ' + sr.Suburb + ', ' + sr.State + ' ' + sr.Postcode AS Address " +
                "FROM Service_Request AS sr " +
                "INNER JOIN Client AS c ON sr.Client_ID = c.Client_ID " +
                "WHERE sr.Job_Status IN ('Requested', 'Rejected')";
            DataTable serviceRequests = _db.ExecuteSQL(sql);
            return serviceRequests;
        }

        // SQL query to display all "Completed" Service Requests
        public DataTable CompletedBookings() 
        {
            string sql = "SELECT sr.Service_Request_ID AS ID, c.Business_Name AS Business, c.First_Name + ' ' + c.Last_Name AS Contact, ct.First_Name + ' ' + ct.Last_Name AS Contractor, c.Phone, sr.Skill_Category AS Category, sr.Job_Status AS [Job Status], sr.Payment_Status AS [Payment Status], CONVERT(NVARCHAR, sr.Date_Created, 103) AS [Date Created], CONVERT(NVARCHAR, sr.Date_Completed, 103) AS [Date Completed], sr.Street + ', ' + sr.Suburb + ', ' + sr.State + ' ' + sr.Postcode AS Address, sr.Hours_Worked AS Hours, sr.Distance_Travelled AS KMs " +
                "FROM Service_Request AS sr " +
                "INNER JOIN  Client AS c ON sr.Client_ID = c.Client_ID " +
                "INNER JOIN Contractor AS ct ON sr.Contractor_ID = ct.Contractor_ID " +
                "INNER JOIN Coordinator AS cd ON sr.Coordinator_ID = cd.Coordinator_ID " +
                "WHERE sr.Job_Status = 'Completed' " +
                "AND Payment_Status IN ('Unpaid', 'Declined')";
            DataTable serviceRequests = _db.ExecuteSQL(sql);
            return serviceRequests;
        }

        // SQL query to display all Service Requests
        public DataTable AllBookings() 
        {
            string sql = "SELECT sr.Service_Request_ID AS ID, c.Business_Name AS Business, c.First_Name + ' ' + c.Last_Name AS Contact, cd.First_Name + ' ' + cd.Last_Name AS Coordinator, ct.First_Name + ' ' + ct.Last_Name AS Contractor, c.Phone, sr.Skill_Category AS Category, sr.Job_Status AS [Job Status], sr.Payment_Status AS [Payment Status], CONVERT(NVARCHAR, sr.Date_Created, 103) AS [Date Created], CONVERT(NVARCHAR, sr.Date_Completed, 103) AS [Date Completed], sr.Street + ', ' + sr.Suburb + ', ' + sr.State + ' ' + sr.Postcode AS Address, sr.Hours_Worked AS Hours, sr.Distance_Travelled AS KMs " +
                "FROM Service_Request AS sr " +
                "INNER JOIN Client AS c ON sr.Client_ID = c.Client_ID " +
                "LEFT JOIN Contractor AS ct ON sr.Contractor_ID = ct.Contractor_ID " +
                "LEFT JOIN Coordinator AS cd ON sr.Coordinator_ID = cd.Coordinator_ID";
            DataTable serviceRequests = _db.ExecuteSQL(sql);
            return serviceRequests;
        }

        // SQL query to approve a Service Request and set it's payment status as "Pending"
        public int ApproveBooking(int serviceRequestID) 
        {
            int returnValue = 0;
            string sql = "UPDATE Service_Request SET Payment_Status = 'Pending' WHERE Service_Request_ID = @ServiceRequestID";
            SqlParameter[] objParameters = new SqlParameter[1];
            objParameters[0] = new SqlParameter("@ServiceRequestID", DbType.Int32);
            objParameters[0].Value = serviceRequestID;
            returnValue = _db.ExecuteNonQuery(sql, objParameters);
            return returnValue;
        }

        // SQL query to assign a Contractor to a specific Service Request
        public int AssignBooking(int serviceRequestID, int coordinatorID, string contractorFName, string contractorLName)
        {
            int returnValue = 0;
            string sql = "UPDATE Service_Request SET Job_Status = 'Assigned', Coordinator_ID = @CoordinatorID, Contractor_ID = (SELECT Contractor_ID FROM Contractor WHERE First_Name = @FirstName AND Last_Name = @LastName) WHERE Service_Request_ID = @ServiceRequestID";
            SqlParameter[] objParameters = new SqlParameter[4];
            objParameters[0] = new SqlParameter("@ServiceRequestID", DbType.Int32);
            objParameters[0].Value = serviceRequestID;
            objParameters[1] = new SqlParameter("@CoordinatorID", DbType.Int32);
            objParameters[1].Value = coordinatorID;
            objParameters[2] = new SqlParameter("@FirstName", DbType.String);
            objParameters[2].Value = contractorFName;
            objParameters[3] = new SqlParameter("@LastName", DbType.String);
            objParameters[3].Value = contractorLName;
            
            returnValue = _db.ExecuteNonQuery(sql, objParameters);
            return returnValue;
        }
    }
}