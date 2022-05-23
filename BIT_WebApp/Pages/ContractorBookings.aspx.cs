﻿using BIT_WebApp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BIT_WebApp.Pages
{
    public partial class ContractorBookings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ContractorID"] != null)
                {
                    LinkButton login = (LinkButton)Master.FindControl("lbtnLogin");
                    LinkButton acceptedBookings = (LinkButton)Master.FindControl("lbtnAcceptedBookings");
                    LinkButton allBookings = (LinkButton)Master.FindControl("lbtnAllBookings");
                    LinkButton logout = (LinkButton)Master.FindControl("lbtnLogout");
                    login.Visible = false;
                    acceptedBookings.Visible = true;
                    allBookings.Visible = true;
                    logout.Visible = true;

                    Contractor currentContractor = new Contractor();
                    currentContractor.ContractorID = Convert.ToInt32(Session["ContractorID"].ToString());
                    gvBookings.DataSource = currentContractor.AssignedBookings().DefaultView;
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

            if (e.CommandName == "Accept")
            {
                currentContractor.AcceptBooking(Convert.ToInt32(row.Cells[2].Text));
            }
            else if (e.CommandName == "Reject")
            {
                currentContractor.RejectBooking(Convert.ToInt32(row.Cells[2].Text));
            }

            gvBookings.DataSource = currentContractor.AssignedBookings().DefaultView;
            gvBookings.DataBind();
        }
    }
}