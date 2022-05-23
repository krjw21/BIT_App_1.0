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
    public class Suburbs : List<Suburb>
    {
        private SQLHelper _db;


        public Suburbs(int? contractorID)
        {
            _db = new SQLHelper();
            string sql = "SELECT s.Suburb_Name, s.Postcode, s.Region " +
                "FROM Suburb AS s " +
                "INNER JOIN Contractor_Suburb AS cs ON s.Suburb_Name = cs.Suburb_Name AND s.Postcode = cs.Postcode " +
                "WHERE cs.Contractor_ID = @ContractorID";
            SqlParameter[] objParameters = new SqlParameter[1];
            objParameters[0] = new SqlParameter("@ContractorID", DbType.Int32);
            objParameters[0].Value = contractorID;
            DataTable suburbs = _db.ExecuteSQL(sql, objParameters);
            foreach (DataRow dr in suburbs.Rows)
            {
                Suburb suburb = new Suburb(dr);
                this.Add(suburb);
            }
        }
    }
}
