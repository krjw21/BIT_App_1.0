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

                    //// GET THE INDEX[0] FROM THIS DATAVIEW TO GET THE SERVICE REQUEST ID
                    //List<int> serviceRequestIDs = new List<int>();
                    //List<string> skills = new List<string>();
                    //List<string> suburbs = new List<string>();
                    //List<DateTime> dates = new List<DateTime>();
                    //for (int i = 0; i < unassignedServiceRequests.Count; i++)
                    //{
                    //    serviceRequestIDs.Add((int)unassignedServiceRequests[i]["ID"]);
                    //    skills.Add((string)unassignedServiceRequests[i]["Category"]);
                    //    suburbs.Add((string)unassignedServiceRequests[i]["ID"]); // DO THIS FOR SUBURB
                    //    dates.Add((DateTime)unassignedServiceRequests[i]["Date Created"]);
                    //}
                    //DropDownList ddlContractors = (DropDownList)Master.FindControl("ddlContractors");
                    //Contractor availableContractors = new Contractor();
                    //DataView contractors = availableContractors.AvailableContractors().DefaultView;
                    //ddlContractors.DataSource = serviceRequestIDs;
                    //ddlContractors.DataBind();

                    // TODO bind available contractor's to the drop down list in order for coordinator to assign the job
                    //int rowIndex = Convert.ToInt32(e.CommandArgument);
                    //GridViewRow row = gvBookings.Rows[rowIndex];
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

            string[] contractorName = row.Cells[0].Text.Split(' ');

            if (e.CommandName == "Assign")
            {
                currentCoordinator.AssignBooking(Convert.ToInt32(row.Cells[1].Text), currentCoordinator.CoordinatorID, contractorName[0], contractorName[1]);
            }

            gvBookings.DataSource = currentCoordinator.UnassignedBookings().DefaultView;
            gvBookings.DataBind();
        }
    }
}