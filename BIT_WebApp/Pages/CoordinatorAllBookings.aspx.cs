using BIT_WebApp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BIT_WebApp.Pages
{
    public partial class CoordinatorAllBookings : System.Web.UI.Page
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
                    LinkButton completedBookings = (LinkButton)Master.FindControl("lbtnCompletedBookings");
                    LinkButton logout = (LinkButton)Master.FindControl("lbtnLogout");
                    login.Visible = false;
                    unassignedBookings.Visible = true;
                    completedBookings.Visible = true;
                    logout.Visible = true;

                    Coordinator currentCoordinator = new Coordinator();
                    currentCoordinator.CoordinatorID = Convert.ToInt32(Session["CoordinatorID"].ToString());
                    gvBookings.DataSource = currentCoordinator.AllBookings().DefaultView;
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