using BIT_DesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_DesktopApp.Models
{
    public class PriorityStates : List<PriorityState>
    {
        public PriorityStates()
        {
            SQLHelper db = new SQLHelper();
            string sql = "SELECT Priority FROM Priority_State";
            DataTable priorityStates = db.ExecuteSQL(sql);
            foreach(DataRow dr in priorityStates.Rows)
            {
                PriorityState priorityState = new PriorityState(dr);
                this.Add(priorityState);
            }
        }
    }
}
