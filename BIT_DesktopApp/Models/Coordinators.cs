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
    }
}
