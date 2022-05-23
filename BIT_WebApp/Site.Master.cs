using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BIT_WebApp
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbtnLogin_Click(object sender, EventArgs e)
        {
            // in this method we will redirect the control to the new page we added (Login.aspx)
            // Browser - Client - Request
            // ASP.Net - Server - Responds
            Response.Redirect("Login.aspx");
        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            //Session.Remove("ClientID");
            //Session.Remove("ContractorID");
            //Session.Remove("CoordinatorID");
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }


        // Clients
        protected void lbtnNewBooking_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClientNewBooking.aspx");
        }
        protected void lbtnCurrentBookings_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClientBookings.aspx");
        }

        // Contractors
        protected void lbtnAssignedBookings_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContractorBookings.aspx");
        }
        protected void lbtnAcceptedBookings_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContractorAcceptedBookings.aspx");
        }


        // Coordinators
        protected void lbtnUnassignedBookings_Click(object sender, EventArgs e)
        {
            Response.Redirect("CoordinatorBookings.aspx");
        }
        protected void lbtnCompletedBookings_Click(object sender, EventArgs e)
        {
            Response.Redirect("CoordinatorCompletedBookings.aspx");
        }


        // All - Clients, Contractors, Coordinators
        protected void lbtnAllBookings_Click(object sender, EventArgs e)
        {
            if (Session["ClientID"] != null)
            {
                Response.Redirect("ClientCompletedBookings.aspx");
            }
            else if (Session["ContractorID"] != null)
            {
                Response.Redirect("ContractorAllBookings.aspx");
            }
            else if (Session["CoordinatorID"] != null)
            {
                Response.Redirect("CoordinatorAllBookings.aspx");
            }
        }
    }
}