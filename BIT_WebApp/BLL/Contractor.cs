using BIT_WebApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BIT_WebApp.BLL
{
    public class Contractor
    {
        public int ContractorID { get; set; }
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


        public Contractor()
        {
            _db = new SQLHelper();
        }

        // SQL query to select all Service Requests assigned to a specific Contractor
        public DataTable AssignedBookings() 
        {
            string sql = "SELECT sr.Service_Request_ID AS ID, c.Business_Name AS Business, c.First_Name + ' ' + c.Last_Name AS Contact, c.Phone, cd.First_Name + ' ' + cd.Last_Name AS Coordinator, sr.Skill_Category AS Category, sr.Priority, sr.Job_Status AS [Job Status], CONVERT(NVARCHAR, sr.Date_Created, 103) AS [Date Created], sr.Street + ', ' + sr.Suburb + ', ' + sr.State + ' ' + sr.Postcode AS Address " +
                "FROM Service_Request AS sr " +
                "INNER JOIN  Contractor AS ct ON sr.Contractor_ID = ct.Contractor_ID " +
                "INNER JOIN  Client AS c ON sr.Client_ID = c.Client_ID " +
                "INNER JOIN  Coordinator AS cd ON sr.Coordinator_ID = cd.Coordinator_ID " +
                "WHERE ct.Contractor_ID = @ContractorID AND sr.Job_Status = 'Assigned'";
            SqlParameter[] objParameters = new SqlParameter[1];
            objParameters[0] = new SqlParameter("@ContractorID", DbType.Int32);
            objParameters[0].Value = this.ContractorID;
            DataTable serviceRequests = _db.ExecuteSQL(sql, objParameters);
            return serviceRequests;
        }

        // SQL query to select all Service Requests accepted by a specific Contractor
        public DataTable AcceptedBookings() 
        {
            string sql = "SELECT sr.Service_Request_ID AS ID, c.Business_Name AS Business, c.First_Name + ' ' + c.Last_Name AS Contact, c.Phone, cd.First_Name + ' ' + cd.Last_Name AS Coordinator, sr.Skill_Category AS Category, sr.Priority, sr.Job_Status AS [Job Status], CONVERT(NVARCHAR, sr.Date_Created, 103) AS [Date Created], sr.Street + ', ' + sr.Suburb + ', ' + sr.State + ' ' + sr.Postcode AS Address " +
                "FROM Service_Request AS sr " +
                "INNER JOIN  Contractor AS ct ON sr.Contractor_ID = ct.Contractor_ID " +
                "INNER JOIN  Client AS c ON sr.Client_ID = c.Client_ID " +
                "INNER JOIN  Coordinator AS cd ON sr.Coordinator_ID = cd.Coordinator_ID " +
                "WHERE ct.Contractor_ID = @ContractorID AND sr.Job_Status = 'Accepted'";
            SqlParameter[] objParameters = new SqlParameter[1];
            objParameters[0] = new SqlParameter("@ContractorID", DbType.Int32);
            objParameters[0].Value = this.ContractorID;
            DataTable serviceRequests = _db.ExecuteSQL(sql, objParameters);
            return serviceRequests;
        }

        // SQL query to select Service Requests marked as "Completed" or "Rejected" by a specific Contractor
        public DataTable AllBookings() 
        {
            string sql = "SELECT sr.Service_Request_ID AS ID, c.Business_Name AS Business, c.First_Name + ' ' + c.Last_Name AS Contact, c.Phone, cd.First_Name + ' ' + cd.Last_Name AS Coordinator, sr.Skill_Category AS Category, sr.Priority, sr.Job_Status AS [Job Status], CONVERT(NVARCHAR, sr.Date_Created, 103) AS [Date Created], CONVERT(NVARCHAR, sr.Date_Completed, 103) AS [Date Completed], sr.Street + ', ' + sr.Suburb + ', ' + sr.State + ' ' + sr.Postcode AS Address, sr.Hours_Worked AS Hours, sr.Distance_Travelled AS KMs " +
                "FROM Service_Request AS sr " +
                "INNER JOIN  Contractor AS ct ON sr.Contractor_ID = ct.Contractor_ID " +
                "INNER JOIN  Client AS c ON sr.Client_ID = c.Client_ID " +
                "INNER JOIN  Coordinator AS cd ON sr.Coordinator_ID = cd.Coordinator_ID " +
                "WHERE ct.Contractor_ID = @ContractorID and sr.Job_Status IN ('Completed', 'Rejected') ORDER BY sr.Job_Status";
            SqlParameter[] objParameters = new SqlParameter[1];
            objParameters[0] = new SqlParameter("@ContractorID", DbType.Int32);
            objParameters[0].Value = this.ContractorID;
            DataTable serviceRequests = _db.ExecuteSQL(sql, objParameters);
            return serviceRequests;
        }

        // SQL query to accept an assigned Aervice Request
        public int AcceptBooking(int serviceRequestID) 
        {
            int returnValue = 0;
            string sql = "UPDATE Service_Request SET Job_Status = 'Accepted' WHERE Service_Request_ID = @ServiceRequestID";
            SqlParameter[] objParameters = new SqlParameter[1];
            objParameters[0] = new SqlParameter("@ServiceRequestID", DbType.Int32);
            objParameters[0].Value = serviceRequestID;
            returnValue = _db.ExecuteNonQuery(sql, objParameters);
            return returnValue;
        }

        // SQL query to reject an assigned Service Request
        public int RejectBooking(int serviceRequestID) 
        {
            int returnValue = 0;
            string updateSQL = "UPDATE Service_Request SET Job_Status = 'Rejected' WHERE Service_Request_ID = @ServiceRequestID";
            string rejectSQL = "INSERT INTO Rejected_Request (Service_Request_ID, Client_ID, Contractor_ID, Job_Status) SELECT Service_Request_ID, Client_ID, Contractor_ID, Job_Status FROM Service_Request WHERE Service_Request_ID = @ServiceRequestID";
            SqlParameter[] objParameters1 = new SqlParameter[1];
            objParameters1[0] = new SqlParameter("@ServiceRequestID", DbType.Int32);
            objParameters1[0].Value = serviceRequestID;
            SqlParameter[] objParameters2 = new SqlParameter[1];
            objParameters2[0] = new SqlParameter("@ServiceRequestID", DbType.Int32);
            objParameters2[0].Value = serviceRequestID;
            returnValue = _db.ExecuteNonQuery(updateSQL, objParameters1);
            int rejectValue = _db.ExecuteNonQuery(rejectSQL, objParameters2);
            return returnValue;
        }

        // SQL query to mark a Service Request as "Completed"
        public int CompleteBooking(int serviceRequestID, int hours, int distance) 
        {
            int returnValue = 0;
            string sql = "UPDATE Service_Request SET Job_Status = 'Completed', Hours_Worked = @HoursWorked, Distance_Travelled = @DistanceTravelled, Date_Completed = GETDATE() WHERE Service_Request_ID = @ServiceRequestID";
            SqlParameter[] objParameters = new SqlParameter[3];
            objParameters[0] = new SqlParameter("@ServiceRequestID", DbType.Int32);
            objParameters[0].Value = serviceRequestID;
            objParameters[1] = new SqlParameter("@HoursWorked", DbType.Int32);
            objParameters[1].Value = hours;
            objParameters[2] = new SqlParameter("@DistanceTravelled", DbType.Int32);
            objParameters[2].Value = distance;
            returnValue = _db.ExecuteNonQuery(sql, objParameters);
            return returnValue;
        }

        // SQL query to display all available Contractors for a specific Service Request
        public DataTable AvailableContractors(int serviceRequestID, string skill, string suburb, DateTime date)
        {
            string sql = "SELECT c.First_Name + ' ' + c.Last_Name AS FullName FROM Contractor AS c " +
                "INNER JOIN Contractor_Skill AS csk ON c.Contractor_ID = csk.Contractor_ID AND (csk.Skill_Category = @Skill AND csk.Status = 1) " +
                "INNER JOIN Contractor_Suburb AS csb ON c.Contractor_ID = csb.Contractor_ID AND csb.Suburb_Name = @Suburb " +
                "INNER JOIN Contractor_Availability AS ca ON c.Contractor_ID = ca.Contractor_ID AND(ca.Day_Name = @Date AND ca.Start_Time IS NOT NULL) " +
                "WHERE c.Contractor_ID NOT IN (SELECT Contractor_ID FROM Rejected_Request WHERE Service_Request_ID = @ServiceRequestID)";
            SqlParameter[] objParameters = new SqlParameter[4];
            objParameters[0] = new SqlParameter("@Skill", DbType.String);
            objParameters[0].Value = skill;
            objParameters[1] = new SqlParameter("@Suburb", DbType.String);
            objParameters[1].Value = suburb;
            objParameters[2] = new SqlParameter("@Date", DbType.String);
            objParameters[2].Value = date.ToString("dddd");
            objParameters[3] = new SqlParameter("@ServiceRequestID", DbType.Int32);
            objParameters[3].Value = serviceRequestID;
            DataTable serviceRequests = _db.ExecuteSQL(sql, objParameters);
            return serviceRequests;
        }
    }
}