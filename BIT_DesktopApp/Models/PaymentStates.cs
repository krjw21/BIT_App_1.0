using BIT_DesktopApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_DesktopApp.Models
{
    public class PaymentStates : List<PaymentState>
    {
        public PaymentStates()
        {
            SQLHelper db = new SQLHelper();
            string sql = "SELECT Payment_Status FROM Payment_State";
            DataTable paymentStates = db.ExecuteSQL(sql);
            foreach(DataRow dr in paymentStates.Rows)
            {
                PaymentState paymentState = new PaymentState(dr);
                this.Add(paymentState);
            }
        }
    }
}
