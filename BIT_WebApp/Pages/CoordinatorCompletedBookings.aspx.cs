using BIT_WebApp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BIT_WebApp.Pages
{
    public partial class CoordinatorCompletedBookings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CoordinatorID"] != null)
                {
                    txtLoginName.InnerText = $"Welcome back {Session["CoordinatorName"]},";

                    LinkButton login = (LinkButton)Master.FindControl("lbtnLogin");
                    LinkButton unassignedBookings = (LinkButton)Master.FindControl("lbtnUnassignedBookings");
                    LinkButton allBookings = (LinkButton)Master.FindControl("lbtnAllBookings");
                    LinkButton logout = (LinkButton)Master.FindControl("lbtnLogout");
                    login.Visible = false;
                    unassignedBookings.Visible = true;
                    allBookings.Visible = true;
                    logout.Visible = true;

                    Coordinator currentCoordinator = new Coordinator();
                    currentCoordinator.CoordinatorID = Convert.ToInt32(Session["CoordinatorID"].ToString());
                    gvBookings.DataSource = currentCoordinator.CompletedBookings().DefaultView;
                    gvBookings.DataBind();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void gvBookings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Coordinator currentCoordinator = new Coordinator();
            currentCoordinator.CoordinatorID = Convert.ToInt32(Session["CoordinatorID"].ToString());
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvBookings.Rows[rowIndex];

            // mark a Service Request's payment status as "Pending"
            if (e.CommandName == "Approve")
            {
                currentCoordinator.ApproveBooking(Convert.ToInt32(row.Cells[1].Text));
                Response.Write($"<script>alert('Service Request ID: \"{row.Cells[1].Text}\" has been approved for payment.')</script>");
            }

            gvBookings.DataSource = currentCoordinator.CompletedBookings().DefaultView;
            gvBookings.DataBind();
        }
    }
}