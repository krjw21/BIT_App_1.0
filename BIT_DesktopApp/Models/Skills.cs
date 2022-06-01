using BIT_DesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_DesktopApp.Models
{
    public class Skills : List<Skill>
    {
        // SQL query to display ALL skill categories
        public Skills()
        {
            SQLHelper db = new SQLHelper();
            string sql = "SELECT Skill_Category, Skill_Description FROM Skill";
            DataTable skills = db.ExecuteSQL(sql);
            foreach(DataRow dr in skills.Rows)
            {
                Skill skill = new Skill(dr);
                this.Add(skill);
            }
        }

        // SQL query to display skills for a specific Contractor
        public Skills(int? contractorID)
        {
            SQLHelper db = new SQLHelper();
            string sql = "SELECT s.Skill_Category, s.Skill_Description " +
                "FROM Skill AS s " +
                "INNER JOIN Contractor_Skill AS cs ON s.Skill_Category = cs.Skill_Category " +
                "WHERE cs.Contractor_ID = @ContractorID " +
                "AND cs.[Status] = 1";
            SqlParameter[] objParameters = new SqlParameter[1];
            objParameters[0] = new SqlParameter("@ContractorID", DbType.Int32);
            objParameters[0].Value = contractorID;
            DataTable skills = db.ExecuteSQL(sql, objParameters);
            foreach(DataRow dr in skills.Rows)
            {
                Skill skill = new Skill(dr);
                this.Add(skill);
            }
        }
    }
}
