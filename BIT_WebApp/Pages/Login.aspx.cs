using BIT_WebApp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BIT_WebApp
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            User user = new User()
            {
                Email = txtUsername.Text,
                Password = txtPassword.Text
            };

            int clientID = user.CheckClient();
            int contractorID = user.CheckContractor();
            int coordinatorID = user.CheckCoordinator();
            if (clientID > 0)
            {
                Session.Add("ClientID", clientID);
                Session.Add("ClientName", user.FullName);
                Response.Redirect("ClientBookings.aspx");
            }
            else if (contractorID > 0)
            {
                Session.Add("ContractorID", contractorID);
                Session.Add("ContractorName", user.FullName);
                Response.Redirect("ContractorBookings.aspx");
            }
            else if (coordinatorID > 0)
            {
                Session.Add("CoordinatorID", coordinatorID);
                Session.Add("CoordinatorName", user.FullName);
                Response.Redirect("CoordinatorBookings.aspx");
            }
            else
            {
                Response.Write("<script>alert('Username or password is incorrect. Please try again!')</script>");
            }
        }
    }
}