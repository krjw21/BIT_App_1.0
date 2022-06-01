using BIT_DesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_DesktopApp.Models
{
    public class Clients : List<Client>
    {
        private SQLHelper _db;

        // SQL query to display ALL Clients
        public Clients()
        {
            _db = new SQLHelper();
            string sql = "SELECT Client_ID, Business_Name, ABN, First_Name, Last_Name, Email, Phone, Street, Suburb, [State], Postcode, Password FROM Client WHERE [Status] = 1";
            DataTable dataTable = _db.ExecuteSQL(sql);

            foreach (DataRow dr in dataTable.Rows)
            {
                Client newClient = new Client(dr);
                this.Add(newClient);
            }
        }

        // SQL query to display Clients with added search filter
        public Clients(string searchText, string searchFilter)
        {
            _db = new SQLHelper();

            if (searchFilter == "ID") { searchFilter = "Client_ID"; }
            if (searchFilter == "Business Name") { searchFilter = "Business_Name"; }
            // + ABN, Email, Phone as possible search filters

            string sql = $"SELECT Client_ID, Business_Name, ABN, First_Name, Last_Name, Email, Phone, Street, Suburb, [State], Postcode, Password FROM Client WHERE [Status] = 1 AND {searchFilter} LIKE '%{searchText}%'";
            DataTable dataTable = _db.ExecuteSQL(sql);

            foreach (DataRow dr in dataTable.Rows)
            {
                Client newClient = new Client(dr);
                this.Add(newClient);
            }
        }
    }
}
