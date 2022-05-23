using BIT_WebApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIT_WebApp.BLL
{
    public class ServiceRequest
    {
        public int ServiceRequestID { get; set; }
        public int ClientID { get; set; }
        public int ContractorID { get; set; }
        public int CoordinatorID { get; set; }
        public string SkillCategory { get; set; }
        public string Priority { get; set; }
        public string JobStatus { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateCompleted { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public int HoursWorked { get; set; }
        public int DistanceTravelled { get; set; }
        public string StartTime { get; set; }
        private SQLHelper _db;


        public ServiceRequest()
        {
            _db = new SQLHelper();
        }


        public int InsertBooking()
        {
            int returnValue = 0;
            string sql = $"INSERT INTO Service_Request(Client_ID, Skill_Category, Priority, Job_Status, Payment_Status, Date_Created, Street, Suburb, [State], Postcode) VALUES({this.ClientID}, '{this.SkillCategory}', '{this.Priority}', 'Requested', 'Unpaid', '{this.DateCreated.ToString("yyyy-MM-dd")}', '{this.Street}', '{this.Suburb}', '{this.State}', '{this.Postcode}')";
            returnValue = _db.ExecuteNonQuery(sql);

            return returnValue;
        }
    }
}