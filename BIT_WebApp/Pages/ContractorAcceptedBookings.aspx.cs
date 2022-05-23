using BIT_WebApp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BIT_WebApp.Pages
{
    public partial class ContractorAcceptedBookings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ContractorID"] != null)
                {
                    LinkButton login = (LinkButton)Master.FindControl("lbtnLogin");
                    LinkButton assignedBookings = (LinkButton)Master.FindControl("lbtnAssignedBookings");
                    LinkButton allBookings = (LinkButton)Master.FindControl("lbtnAllBookings");
                    LinkButton logout = (LinkButton)Master.FindControl("lbtnLogout");
                    login.Visible = false;
                    assignedBookings.Visible = true;
                    allBookings.Visible = true;
                    logout.Visible = true;

                    Contractor currentContractor = new Contractor();
                    currentContractor.ContractorID = Convert.ToInt32(Session["ContractorID"].ToString());
                    gvBookings.DataSource = currentContractor.AcceptedBookings().DefaultView;
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
            Contractor currentContractor = new Contractor();
            currentContractor.ContractorID = Convert.ToInt32(Session["ContractorID"].ToString());
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvBookings.Rows[rowIndex];

            if (e.CommandName == "Complete")
            {
                bool success1 = int.TryParse(((TextBox)row.FindControl("txtHoursWorked")).Text.Trim(), out int hours);
                bool success2 = int.TryParse(((TextBox)row.FindControl("txtDistanceTravelled")).Text.Trim(), out int distance);
                if (success1 && success2)
                {
                    currentContractor.CompleteBooking(Convert.ToInt32(row.Cells[3].Text), hours, distance);
                }
                else
                {
                    Response.Write("<script>alert('Hours and KMs must have only numbers entered.')</script>");
                }
            }

            gvBookings.DataSource = currentContractor.AcceptedBookings().DefaultView;
            gvBookings.DataBind();
        }
    }
}