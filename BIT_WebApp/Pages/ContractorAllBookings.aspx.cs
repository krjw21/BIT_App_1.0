using BIT_WebApp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BIT_WebApp.Pages
{
    public partial class ContractorAllBookings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ContractorID"] != null)
                {
                    LinkButton login = (LinkButton)Master.FindControl("lbtnLogin");
                    LinkButton assignedBookings = (LinkButton)Master.FindControl("lbtnAssignedBookings");
                    LinkButton acceptedBookings = (LinkButton)Master.FindControl("lbtnAcceptedBookings");
                    LinkButton allBookings = (LinkButton)Master.FindControl("lbtnAllBookings");
                    LinkButton logout = (LinkButton)Master.FindControl("lbtnLogout");
                    login.Visible = false;
                    assignedBookings.Visible = true;
                    acceptedBookings.Visible = true;
                    allBookings.Visible = false;
                    logout.Visible = true;

                    Contractor currentContractor = new Contractor();
                    currentContractor.ContractorID = Convert.ToInt32(Session["ContractorID"].ToString());
                    gvBookings.DataSource = currentContractor.AllBookings().DefaultView;
                    gvBookings.DataBind();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
    }
}