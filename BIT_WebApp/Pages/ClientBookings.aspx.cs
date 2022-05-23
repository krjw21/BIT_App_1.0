using BIT_WebApp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BIT_WebApp.Pages
{
    public partial class ClientBookings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ClientID"] != null)
                {
                    LinkButton login = (LinkButton)Master.FindControl("lbtnLogin");
                    LinkButton newBooking = (LinkButton)Master.FindControl("lbtnNewBooking");
                    LinkButton completedBookings = (LinkButton)Master.FindControl("lbtnAllBookings");
                    LinkButton logout = (LinkButton)Master.FindControl("lbtnLogout");
                    login.Visible = false;
                    newBooking.Visible = true;
                    completedBookings.Visible = true;
                    logout.Visible = true;

                    Client currentClient = new Client();
                    currentClient.ClientID = Convert.ToInt32(Session["ClientID"].ToString());
                    gvBookings.DataSource = currentClient.AllBookings().DefaultView;
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