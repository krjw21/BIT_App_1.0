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
    public class ServiceRequests : List<ServiceRequest>
    {
        private SQLHelper _db;


        // SQL query to display Service Requests in "History" tab
        public ServiceRequests()
        {
            _db = new SQLHelper();
            string sql = "SELECT sr.Service_Request_ID, sr.Client_ID, c.Business_Name, sr.Contractor_ID, sr.Coordinator_ID, ct.First_Name + ' ' + ct.Last_Name AS FullName, c.Business_Name, c.First_Name, c.Last_Name, sr.Skill_Category, prs.Priority, js.Job_Status, ps.Payment_Status, sr.Date_Created, sr.Date_Completed, sr.Street, sr.Suburb, sr.[State], sr.Postcode, sr.Hours_Worked, sr.Distance_Travelled " +
                "FROM Service_Request AS sr " +
                "INNER JOIN Client AS c ON sr.Client_ID = c.Client_ID " +
                "INNER JOIN Payment_State AS ps ON sr.Payment_Status = ps.Payment_Status " +
                "INNER JOIN Job_State AS js ON sr.Job_Status = js.Job_Status " +
                "LEFT JOIN Contractor AS ct ON sr.Contractor_ID = ct.Contractor_ID " +
                "INNER JOIN Priority_State AS prs ON sr.Priority = prs.Priority";
            DataTable dataTable = _db.ExecuteSQL(sql);
            foreach (DataRow dr in dataTable.Rows)
            {
                ServiceRequest newServiceRequest = new ServiceRequest(dr);
                this.Add(newServiceRequest);
            }
        }


        // SQL query to display Service Requests in "Completed" tab with a job status of "Completed" and payment status of "Unpaid/Declined"
        public ServiceRequests(string jobStatus)
        {
            _db = new SQLHelper();
            string sql = "SELECT sr.Service_Request_ID, sr.Client_ID, c.Business_Name, sr.Contractor_ID, sr.Coordinator_ID, ct.First_Name + ' ' + ct.Last_Name AS FullName, c.Business_Name, c.First_Name, c.Last_Name, sr.Skill_Category, prs.Priority, js.Job_Status, ps.Payment_Status, sr.Date_Created, sr.Date_Completed, sr.Street, sr.Suburb, sr.[State], sr.Postcode, sr.Hours_Worked, sr.Distance_Travelled " +
                "FROM Service_Request AS sr " +
                "INNER JOIN Client AS c ON sr.Client_ID = c.Client_ID " +
                "INNER JOIN Payment_State AS ps ON sr.Payment_Status = ps.Payment_Status " +
                "INNER JOIN Job_State AS js ON sr.Job_Status = js.Job_Status " +
                "INNER JOIN Priority_State AS prs ON sr.Priority = prs.Priority " +
                "LEFT JOIN Contractor AS ct ON sr.Contractor_ID = ct.Contractor_ID " +
                "WHERE sr.Job_Status = @JobStatus " +
                "AND sr.Payment_Status IN ('Unpaid', 'Declined')";
            SqlParameter[] objParameters = new SqlParameter[1];
            objParameters[0] = new SqlParameter("@JobStatus", DbType.String);
            objParameters[0].Value = jobStatus;
            DataTable dataTable = _db.ExecuteSQL(sql, objParameters);
            foreach (DataRow dr in dataTable.Rows)
            {
                ServiceRequest newServiceRequest = new ServiceRequest(dr);
                this.Add(newServiceRequest);
            }
        }


        // 1. searchToggle == true
        // SQL query to display Service Requests with a job status of "Requested/Rejected" OR "Assigned/Accepted"
        // 2. seachToggle == false
        // SQL query to display Service Requests in "History" tab with added search filter
        public ServiceRequests(string jobStatusOrSearchText, string jobStatusOrSearchFilter, bool searchToggle)
        {
            if (searchToggle == true)
            {
                _db = new SQLHelper();
                string sql = "SELECT sr.Service_Request_ID, sr.Client_ID, c.Business_Name, sr.Contractor_ID, sr.Coordinator_ID, ct.First_Name + ' ' + ct.Last_Name AS FullName, c.Business_Name, c.First_Name, c.Last_Name, sr.Skill_Category, prs.Priority, js.Job_Status, ps.Payment_Status, sr.Date_Created, sr.Date_Completed, sr.Street, sr.Suburb, sr.[State], sr.Postcode, sr.Hours_Worked, sr.Distance_Travelled " +
                    "FROM Service_Request AS sr " +
                    "INNER JOIN Client AS c ON sr.Client_ID = c.Client_ID " +
                    "INNER JOIN Payment_State AS ps ON sr.Payment_Status = ps.Payment_Status " +
                    "INNER JOIN Job_State AS js ON sr.Job_Status = js.Job_Status " +
                    "INNER JOIN Priority_State AS prs ON sr.Priority = prs.Priority " +
                    "LEFT JOIN Contractor AS ct ON sr.Contractor_ID = ct.Contractor_ID " +
                    "WHERE sr.Job_Status IN (@JobStatus1, @JobStatus2)";
                SqlParameter[] objParameters = new SqlParameter[2];
                objParameters[0] = new SqlParameter("@JobStatus1", DbType.String);
                objParameters[0].Value = jobStatusOrSearchText;
                objParameters[1] = new SqlParameter("@JobStatus2", DbType.String);
                objParameters[1].Value = jobStatusOrSearchFilter;
                DataTable dataTable = _db.ExecuteSQL(sql, objParameters);
                foreach (DataRow dr in dataTable.Rows)
                {
                    ServiceRequest newServiceRequest = new ServiceRequest(dr);
                    this.Add(newServiceRequest);
                }
            }
            else
            {
                _db = new SQLHelper();

                if (jobStatusOrSearchFilter == "ID") { jobStatusOrSearchFilter = "Service_Request_ID"; }
                if (jobStatusOrSearchFilter == "Skill Category") { jobStatusOrSearchFilter = "Skill_Category"; }
                if (jobStatusOrSearchFilter == "Job Status") { jobStatusOrSearchFilter = "Job_Status"; }
                if (jobStatusOrSearchFilter == "Payment Status") { jobStatusOrSearchFilter = "Payment_Status"; }
                if (jobStatusOrSearchFilter == "Date Created") { jobStatusOrSearchFilter = "Date_Created"; }

                string queryFilter = $"WHERE sr.{jobStatusOrSearchFilter} LIKE '%{jobStatusOrSearchText}%'";
                if (jobStatusOrSearchFilter == "Business Name")
                {
                    queryFilter = $"WHERE sr.Client_ID IN (SELECT Client_ID FROM Client WHERE (Business_Name LIKE '%{jobStatusOrSearchText}%'))";
                }
                if (jobStatusOrSearchFilter == "Contractor")
                {
                    queryFilter = $"WHERE sr.Contractor_ID IN (SELECT Contractor_ID FROM Contractor WHERE (First_Name LIKE '%{jobStatusOrSearchText}%') OR (Last_Name LIKE '%{jobStatusOrSearchText}%'))";
                }
                // + Priority as possible search filter
                
                string sql = $"SELECT sr.Service_Request_ID, sr.Client_ID, c.Business_Name, sr.Contractor_ID, sr.Coordinator_ID, ct.First_Name + ' ' + ct.Last_Name AS FullName, c.Business_Name, c.First_Name, c.Last_Name, sr.Skill_Category, prs.Priority, js.Job_Status, ps.Payment_Status, sr.Date_Created, sr.Date_Completed, sr.Street, sr.Suburb, sr.[State], sr.Postcode, sr.Hours_Worked, sr.Distance_Travelled " +
                    "FROM Service_Request AS sr " +
                    "INNER JOIN Client AS c ON sr.Client_ID = c.Client_ID " +
                    "INNER JOIN Payment_State AS ps ON sr.Payment_Status = ps.Payment_Status " +
                    "INNER JOIN Job_State AS js ON sr.Job_Status = js.Job_Status " +
                    "INNER JOIN Priority_State AS prs ON sr.Priority = prs.Priority " +
                    "LEFT JOIN Contractor AS ct ON sr.Contractor_ID = ct.Contractor_ID " + queryFilter;
                
                DataTable dataTable = _db.ExecuteSQL(sql);
                foreach (DataRow dr in dataTable.Rows)
                {
                    ServiceRequest newServiceRequest = new ServiceRequest(dr);
                    this.Add(newServiceRequest);
                }
            }
        }


        // SQL query to display Service Requests in "Completed" tab with added search filter
        public ServiceRequests(string jobStatus, string searchText, string searchFilter)
        {
            _db = new SQLHelper();

            if (searchFilter == "ID") { searchFilter = "Service_Request_ID"; }
            if (searchFilter == "Skill Category") { searchFilter = "Skill_Category"; }
            if (searchFilter == "Job Status") { searchFilter = "Job_Status"; }
            if (searchFilter == "Payment Status") { searchFilter = "Payment_Status"; }
            if (searchFilter == "Date Created") { searchFilter = "Date_Created"; }

            string queryFilter = $"AND sr.{searchFilter} LIKE '%{searchText}%'";
            if (searchFilter == "Business Name")
            {
                queryFilter = $"AND sr.Client_ID IN (SELECT Client_ID FROM Client WHERE (Business_Name LIKE '%{searchText}%'))";
            }
            if (searchFilter == "Contractor")
            {
                queryFilter = $"AND sr.Contractor_ID IN (SELECT Contractor_ID FROM Contractor WHERE (First_Name LIKE '%{searchText}%') OR (Last_Name LIKE '%{searchText}%'))";
            }
            // + Priority as possible search filter

            string sql = "SELECT sr.Service_Request_ID, sr.Client_ID, c.Business_Name, sr.Contractor_ID, sr.Coordinator_ID, ct.First_Name + ' ' + ct.Last_Name AS FullName, c.Business_Name, c.First_Name, c.Last_Name, sr.Skill_Category, prs.Priority, js.Job_Status, ps.Payment_Status, sr.Date_Created, sr.Date_Completed, sr.Street, sr.Suburb, sr.[State], sr.Postcode, sr.Hours_Worked, sr.Distance_Travelled " +
                "FROM Service_Request AS sr " +
                "INNER JOIN Client AS c ON sr.Client_ID = c.Client_ID " +
                "INNER JOIN Payment_State AS ps ON sr.Payment_Status = ps.Payment_Status " +
                "INNER JOIN Job_State AS js ON sr.Job_Status = js.Job_Status " +
                "INNER JOIN Priority_State AS prs ON sr.Priority = prs.Priority " +
                "LEFT JOIN Contractor AS ct ON sr.Contractor_ID = ct.Contractor_ID " +
                "WHERE sr.Job_Status = @JobStatus " +
                "AND sr.Payment_Status IN ('Unpaid', 'Declined') " + queryFilter;

            SqlParameter[] objParameters = new SqlParameter[1];
            objParameters[0] = new SqlParameter("@JobStatus", DbType.String);
            objParameters[0].Value = jobStatus;
            DataTable dataTable = _db.ExecuteSQL(sql, objParameters);
            foreach (DataRow dr in dataTable.Rows)
            {
                ServiceRequest newServiceRequest = new ServiceRequest(dr);
                this.Add(newServiceRequest);
            }
        }


        // SQL query to display Service Requests in "Requested/Rejected" OR "Assigned/Accepted" tabs with added search filter
        public ServiceRequests(string jobStatus1, string jobStatus2, string searchText, string searchFilter)
        {
            _db = new SQLHelper();

            if (searchFilter == "ID") { searchFilter = "Service_Request_ID"; }
            if (searchFilter == "Skill Category") { searchFilter = "Skill_Category"; }
            if (searchFilter == "Job Status") { searchFilter = "Job_Status"; }
            if (searchFilter == "Payment Status") { searchFilter = "Payment_Status"; }
            if (searchFilter == "Date Created") { searchFilter = "Date_Created"; }

            string queryFilter = $"AND sr.{searchFilter} LIKE '%{searchText}%'";
            if (searchFilter == "Business Name")
            {
                queryFilter = $"AND sr.Client_ID IN (SELECT Client_ID FROM Client WHERE (Business_Name LIKE '%{searchText}%'))";
            }
            if (searchFilter == "Contractor")
            {
                queryFilter = $"AND sr.Contractor_ID IN (SELECT Contractor_ID FROM Contractor WHERE (First_Name LIKE '%{searchText}%') OR (Last_Name LIKE '%{searchText}%'))";
            }
            // + Priority as possible search filter

            string sql = "SELECT sr.Service_Request_ID, sr.Client_ID, c.Business_Name, sr.Contractor_ID, sr.Coordinator_ID, ct.First_Name + ' ' + ct.Last_Name AS FullName, c.Business_Name, c.First_Name, c.Last_Name, sr.Skill_Category, prs.Priority, js.Job_Status, ps.Payment_Status, sr.Date_Created, sr.Date_Completed, sr.Street, sr.Suburb, sr.[State], sr.Postcode, sr.Hours_Worked, sr.Distance_Travelled " +
                    "FROM Service_Request AS sr " +
                    "INNER JOIN Client AS c ON sr.Client_ID = c.Client_ID " +
                    "INNER JOIN Payment_State AS ps ON sr.Payment_Status = ps.Payment_Status " +
                    "INNER JOIN Job_State AS js ON sr.Job_Status = js.Job_Status " +
                    "INNER JOIN Priority_State AS prs ON sr.Priority = prs.Priority " +
                    "LEFT JOIN Contractor AS ct ON sr.Contractor_ID = ct.Contractor_ID " +
                    "WHERE sr.Job_Status IN (@JobStatus1, @JobStatus2) " + queryFilter;
            
            SqlParameter[] objParameters = new SqlParameter[2];
            objParameters[0] = new SqlParameter("@JobStatus1", DbType.String);
            objParameters[0].Value = jobStatus1;
            objParameters[1] = new SqlParameter("@JobStatus2", DbType.String);
            objParameters[1].Value = jobStatus2;
            DataTable dataTable = _db.ExecuteSQL(sql, objParameters);
            foreach (DataRow dr in dataTable.Rows)
            {
                ServiceRequest newServiceRequest = new ServiceRequest(dr);
                this.Add(newServiceRequest);
            }
        }
    }
}
