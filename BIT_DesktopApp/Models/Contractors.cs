using BIT_DesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_DesktopApp.Models
{
    public class Contractors : List<Contractor>
    {
        private SQLHelper _db;

        public Contractors()
        {
            _db = new SQLHelper();
            string sql = "SELECT Contractor_ID, First_Name, Last_Name, DOB, Email, Phone, Street, Suburb, [State], Postcode, Password FROM Contractor WHERE [Status] = 1";
            DataTable dataTable = _db.ExecuteSQL(sql);

            foreach (DataRow dr in dataTable.Rows)
            {
                Contractor newContractor = new Contractor(dr);
                this.Add(newContractor);
            }
        }
    }
}
