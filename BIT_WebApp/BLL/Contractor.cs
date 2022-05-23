using BIT_WebApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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


        public DataTable AssignedBookings() // SQL query to select all service requests assigned to the contractor
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
        public DataTable AcceptedBookings() // SQL query to select all service requests accepted by the contractor
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
        public DataTable AllBookings() // SQL query to select service requests marked as "Completed" or "Rejected" by the contractor
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
        public int AcceptBooking(int serviceRequestID) // SQL query to accept an assigned service request
        {
            int returnValue = 0;
            string sql = "UPDATE Service_Request SET Job_Status = 'Accepted' WHERE Service_Request_ID = @ServiceRequestID";
            SqlParameter[] objParameters = new SqlParameter[1];
            objParameters[0] = new SqlParameter("@ServiceRequestID", DbType.Int32);
            objParameters[0].Value = serviceRequestID;
            returnValue = _db.ExecuteNonQuery(sql, objParameters);
            return returnValue;
        }
        public int RejectBooking(int serviceRequestID) // SQL query to reject an assigned service request
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
        public int CompleteBooking(int serviceRequestID, int hours, int distance) // SQL query to mark a service request as completed
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


        // TODO write sql to find available contractors for a job
        public DataTable AvailableContractors(int contractorID, string suburb, string skill, DateTime date, int serviceRequestID)
        {
            string sql = "";
            SqlParameter[] objParameters = new SqlParameter[5];
            objParameters[1] = new SqlParameter("@ContractorID", DbType.Int32);
            objParameters[1].Value = contractorID;
            objParameters[1] = new SqlParameter("@Suburb", DbType.Int32);
            objParameters[1].Value = suburb;
            objParameters[2] = new SqlParameter("@Skill", DbType.Int32);
            objParameters[2].Value = skill;
            objParameters[3] = new SqlParameter("@Availability", DbType.Int32);
            objParameters[3].Value = date.ToString("dddd");
            objParameters[4] = new SqlParameter("@ServiceRequestID", DbType.Int32);
            objParameters[4].Value = serviceRequestID;
            DataTable serviceRequests = _db.ExecuteSQL(sql, objParameters);
            return serviceRequests;
        }
    }
}