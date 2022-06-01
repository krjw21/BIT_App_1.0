using BIT_WebApp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BIT_WebApp.Pages
{
    public partial class ClientNewBooking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ClientID"] != null)
            {
                LinkButton login = (LinkButton)Master.FindControl("lbtnLogin");
                LinkButton currentBookings = (LinkButton)Master.FindControl("lbtnCurrentBookings");
                LinkButton completedBookings = (LinkButton)Master.FindControl("lbtnAllBookings");
                LinkButton logout = (LinkButton)Master.FindControl("lbtnLogout");
                login.Visible = false;
                currentBookings.Visible = true;
                completedBookings.Visible = true;
                logout.Visible = true;

                Client currentClient = new Client();
                currentClient.ClientID = Convert.ToInt32(Session["ClientID"].ToString());
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        // creating a new service request - from the Client
        protected void btnNewServiceRequest_Click(object sender, EventArgs e)
        {
            if (calBookingDate.SelectedDate == null || ddlStartTime.SelectedValue == "Select" || ddlSkillCategory.SelectedValue == "Select" || ddlPriority.SelectedValue == "Select" || txtStreet.Text == "" || txtSuburb.Text == "" || ddlState.SelectedValue == "Select" || txtPostcode.Text == "")
            {
                Response.Write("<script>alert('All fields must be filled before a booking can be requested.')</script>");
            }
            else
            {
                ServiceRequest serviceRequest = new ServiceRequest();
                serviceRequest.ClientID = Convert.ToInt32(Session["ClientID"].ToString());
                serviceRequest.SkillCategory = ddlSkillCategory.SelectedValue;
                serviceRequest.Priority = ddlPriority.SelectedValue;
                serviceRequest.Street = txtStreet.Text;
                serviceRequest.Suburb = txtSuburb.Text;
                serviceRequest.State = ddlState.SelectedValue;
                serviceRequest.Postcode = txtPostcode.Text;
                serviceRequest.DateCreated = calBookingDate.SelectedDate;
                serviceRequest.StartTime = ddlStartTime.SelectedValue;
                int returnValue = serviceRequest.InsertBooking();
                if (returnValue > 0)
                {
                    Response.Write(@"<script>
                                        alert('Booking requested. Please wait for a BIT Services Co-ordinator or Contractor to contact you.');
                                        window.location = '" + "ClientBookings.aspx" + @"';
                                     </script>");
                }
                else
                {
                    Response.Write("<script>alert('Booking request failed. Please try again or contact a Co-ordinator.')</script>");
                }
            }
        }
    }
}