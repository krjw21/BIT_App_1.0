using BIT_DesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_DesktopApp.Models
{
    public class JobStates : List<JobState>
    {
        public JobStates()
        {
            SQLHelper db = new SQLHelper();
            string sql = "SELECT Job_Status FROM Job_State";
            DataTable jobStates = db.ExecuteSQL(sql);
            foreach(DataRow dr in jobStates.Rows)
            {
                JobState jobState = new JobState(dr);
                this.Add(jobState);
            }
        }
    }
}
