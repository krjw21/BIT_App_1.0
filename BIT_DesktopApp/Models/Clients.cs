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

        public Clients()
        {
            _db = new SQLHelper();
            string sql = "SELECT Client_ID, Business_Name, ABN, First_Name, Last_Name, Email, Phone, Street, Suburb, [State], Postcode, Password FROM Client WHERE [Status] = 1";
            DataTable dataTable = _db.ExecuteSQL(sql);

            foreach(DataRow dr in dataTable.Rows)
            {
                Client newClient = new Client(dr);
                this.Add(newClient);
            }
        }
    }
}
