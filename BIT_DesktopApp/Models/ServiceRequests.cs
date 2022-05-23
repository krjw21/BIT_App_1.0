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

        public ServiceRequests()
        {
            _db = new SQLHelper();
            string sql = "SELECT sr.Service_Request_ID, sr.Client_ID, sr.Contractor_ID, sr.Coordinator_ID, c.Business_Name, c.First_Name, c.Last_Name, sr.Skill_Category, prs.Priority, js.Job_Status, ps.Payment_Status, sr.Date_Created, sr.Date_Completed, sr.Street, sr.Suburb, sr.[State], sr.Postcode, sr.Hours_Worked, sr.Distance_Travelled " +
                "FROM Service_Request AS sr " +
                "INNER JOIN Client AS c ON sr.Client_ID = c.Client_ID " +
                "INNER JOIN Payment_State AS ps ON sr.Payment_Status = ps.Payment_Status " +
                "INNER JOIN Job_State AS js ON sr.Job_Status = js.Job_Status " +
                "INNER JOIN Priority_State AS prs ON sr.Priority = prs.Priority";
            DataTable dataTable = _db.ExecuteSQL(sql);
            foreach(DataRow dr in dataTable.Rows)
            {
                ServiceRequest newServiceRequest = new ServiceRequest(dr);
                this.Add(newServiceRequest);
            }
        }
        public ServiceRequests(string jobStatus)
        {
            _db = new SQLHelper();
            string sql = "SELECT sr.Service_Request_ID, sr.Client_ID, sr.Contractor_ID, sr.Coordinator_ID, c.Business_Name, c.First_Name, c.Last_Name, sr.Skill_Category, prs.Priority, js.Job_Status, ps.Payment_Status, sr.Date_Created, sr.Date_Completed, sr.Street, sr.Suburb, sr.[State], sr.Postcode, sr.Hours_Worked, sr.Distance_Travelled " +
                "FROM Service_Request AS sr " +
                "INNER JOIN Client AS c ON sr.Client_ID = c.Client_ID " +
                "INNER JOIN Payment_State AS ps ON sr.Payment_Status = ps.Payment_Status " +
                "INNER JOIN Job_State AS js ON sr.Job_Status = js.Job_Status " +
                "INNER JOIN Priority_State AS prs ON sr.Priority = prs.Priority " +
                "WHERE sr.Job_Status = @JobStatus";
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
        public ServiceRequests(string jobStatus1, string jobStatus2)
        {
            _db = new SQLHelper();
            string sql = "SELECT sr.Service_Request_ID, sr.Client_ID, sr.Contractor_ID, sr.Coordinator_ID, c.Business_Name, c.First_Name, c.Last_Name, sr.Skill_Category, prs.Priority, js.Job_Status, ps.Payment_Status, sr.Date_Created, sr.Date_Completed, sr.Street, sr.Suburb, sr.[State], sr.Postcode, sr.Hours_Worked, sr.Distance_Travelled " +
                "FROM Service_Request AS sr " +
                "INNER JOIN Client AS c ON sr.Client_ID = c.Client_ID " +
                "INNER JOIN Payment_State AS ps ON sr.Payment_Status = ps.Payment_Status " +
                "INNER JOIN Job_State AS js ON sr.Job_Status = js.Job_Status " +
                "INNER JOIN Priority_State AS prs ON sr.Priority = prs.Priority " +
                "WHERE sr.Job_Status IN (@JobStatus1, @JobStatus2)";
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
