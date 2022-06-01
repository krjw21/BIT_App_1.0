using BIT_DesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_DesktopApp.Models
{
    public class Coordinators : List<Coordinator>
    {
        private SQLHelper _db;

        // SQL query to display ALL Coordinators
        public Coordinators()
        {
            _db = new SQLHelper();
            string sql = "SELECT Coordinator_ID, First_Name, Last_Name, DOB, Email, Phone, Street, Suburb, [State], Postcode, Password FROM Coordinator WHERE [Status] = 1";
            DataTable dataTable = _db.ExecuteSQL(sql);

            foreach(DataRow dr in dataTable.Rows)
            {
                Coordinator newCoordinator = new Coordinator(dr);
                this.Add(newCoordinator);
            }
        }

        // SQL query to display Coordinators with added search filter
        public Coordinators(string searchText, string searchFilter)
        {
            _db = new SQLHelper();

            if (searchFilter == "ID") { searchFilter = "Coordinator_ID"; }
            if (searchFilter == "First Name") { searchFilter = "First_Name"; }
            if (searchFilter == "Last Name") { searchFilter = "Last_Name"; }
            // + Email, Phone as possible search filters

            string sql = $"SELECT Coordinator_ID, First_Name, Last_Name, DOB, Email, Phone, Street, Suburb, [State], Postcode, Password FROM Coordinator WHERE [Status] = 1 AND {searchFilter} LIKE '%{searchText}%'";
            DataTable dataTable = _db.ExecuteSQL(sql);

            foreach (DataRow dr in dataTable.Rows)
            {
                Coordinator newCoordinator = new Coordinator(dr);
                this.Add(newCoordinator);
            }
        }
    }
}
