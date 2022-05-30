using BIT_WebApp.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BIT_WebApp.Pages
{
    public partial class CoordinatorBookings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CoordinatorID"] != null)
                {
                    LinkButton login = (LinkButton)Master.FindControl("lbtnLogin");
                    LinkButton completedBookings = (LinkButton)Master.FindControl("lbtnCompletedBookings");
                    LinkButton allBookings = (LinkButton)Master.FindControl("lbtnAllBookings");
                    LinkButton logout = (LinkButton)Master.FindControl("lbtnLogout");
                    login.Visible = false;
                    completedBookings.Visible = true;
                    allBookings.Visible = true;
                    logout.Visible = true;

                    Coordinator currentCoordinator = new Coordinator();
                    currentCoordinator.CoordinatorID = Convert.ToInt32(Session["CoordinatorID"].ToString());
                    DataView unassignedServiceRequests = currentCoordinator.UnassignedBookings().DefaultView;
                    gvBookings.DataSource = unassignedServiceRequests;
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
            DropDownList ddlContractors = (DropDownList)row.FindControl("ddlContractors");

            if (e.CommandName == "Assign")
            {
                // TODO message boxes to notify user
                string[] contractorName = ddlContractors.SelectedValue.Split(' ');
                currentCoordinator.AssignBooking(Convert.ToInt32(row.Cells[3].Text), currentCoordinator.CoordinatorID, contractorName[0], contractorName[1]);
                gvBookings.DataSource = currentCoordinator.UnassignedBookings().DefaultView;
                gvBookings.DataBind();
            }
            else if (e.CommandName == "Find")
            {
                Contractor availableContractors = new Contractor();
                string skill = row.Cells[7].Text;
                string suburb = row.Cells[11].Text.ToString().Split(',')[1].Trim();
                DateTime date = Convert.ToDateTime(row.Cells[10].Text);
                ddlContractors.DataSource = availableContractors.AvailableContractors(skill, suburb, date);
                ddlContractors.DataBind();
                if(ddlContractors.Items.Count == 0)
                {
                    ddlContractors.Items.Insert(0, new ListItem("None available."));
                }
                else
                {
                    ddlContractors.Items.Insert(0, new ListItem("- Select -"));
                }
            }
        }
    }
}