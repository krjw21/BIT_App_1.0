<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientNewBooking.aspx.cs" Inherits="BIT_WebApp.Pages.ClientNewBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="login-container"  style="margin: 80px 0 60px 0; background-color: #040315">
        <div class="container">
            <div class="row">
                <div class="col-md-6 mx-auto">
                    <div class="card" style="background-color: #494E6B !important;">
                        <div class="card-body">
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <h3 class="login-labels">New Service Request Booking:</h3>
                                    </center>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <hr class="login-line" />
                                    </center>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <label class="login-labels">Skill Category:</label>
                                    <div class="form-group">
                                        <asp:DropDownList CssClass="form-control" ID="ddlSkillCategory" runat="server">
                                            <asp:ListItem Text="Select" Value="Select" />
                                            <asp:ListItem Text="Hardware Troubleshooting" Value="Hardware Troubleshooting" />
                                            <asp:ListItem Text="Software Troubleshooting" Value="Software Troubleshooting" />
                                            <asp:ListItem Text="Hardware Installations" Value="Hardware Installations" />
                                            <asp:ListItem Text="Software Installations" Value="Software Installations" />
                                            <asp:ListItem Text="Cabling" Value="Cabling" />
                                            <asp:ListItem Text="Project Management" Value="Project Management" />
                                            <asp:ListItem Text="Analysis" Value="Analysis" />
                                            <asp:ListItem Text="Database" Value="Database" />
                                            <asp:ListItem Text="Development" Value="Development" />
                                            <asp:ListItem Text="Cybersecurity" Value="Cybersecurity" />
                                            <asp:ListItem Text="Audits" Value="Audits" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <label class="login-labels">Priority:</label>
                                    <div class="form-group">
                                        <asp:DropDownList CssClass="form-control" ID="ddlPriority" runat="server">
                                            <asp:ListItem Text="Select" Value="Select" />
                                            <asp:ListItem Text="Low" Value="Low" />
                                            <asp:ListItem Text="Medium" Value="Medium" />
                                            <asp:ListItem Text="High" Value="High" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <label class="login-labels">Street:</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="txtStreet" runat="server" placeholder="Street"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-6">
                                    <label class="login-labels">Suburb:</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="txtSuburb" runat="server" placeholder="Suburb"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <label class="login-labels">State:</label>
                                    <asp:DropDownList CssClass="form-control" ID="ddlState" runat="server">
                                        <asp:ListItem Text="Select" Value="Select" />
                                        <asp:ListItem Text="NSW" Value="NSW" />
                                        <asp:ListItem Text="QLD" Value="QLD" />
                                        <asp:ListItem Text="VIC" Value="VIC" />
                                        <asp:ListItem Text="ACT" Value="ACT" />
                                        <asp:ListItem Text="NT" Value="NT" />
                                        <asp:ListItem Text="SA" Value="SA" />
                                        <asp:ListItem Text="WA" Value="WA" />
                                        <asp:ListItem Text="TAS" Value="TAS" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-3">
                                    <label class="login-labels">Postcode:</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="txtPostcode" runat="server" placeholder="Postcode"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-8">
                                    <label class="login-labels">Booking Date:</label>
                                    <div class="form-group">
                                        <asp:Calendar ID="calBookingDate" runat="server"></asp:Calendar>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <label class="login-labels">Start Time:</label>
                                    <div class="form-group">
                                        <asp:DropDownList CssClass="form-control" ID="ddlStartTime" runat="server">
                                            <asp:ListItem Text="Select" Value="Select" />
                                            <asp:ListItem Text="09:00AM" Value="09:00:00" />
                                            <asp:ListItem Text="10:00AM" Value="10:00:00" />
                                            <asp:ListItem Text="11:00AM" Value="11:00:00" />
                                            <asp:ListItem Text="12:00AM" Value="12:00:00" />
                                            <asp:ListItem Text="01:00PM" Value="13:00:00" />
                                            <asp:ListItem Text="02:00PM" Value="14:00:00" />
                                            <asp:ListItem Text="03:00PM" Value="15:00:00" />
                                            <asp:ListItem Text="04:00PM" Value="16:00:00" />
                                            <asp:ListItem Text="05:00PM" Value="17:00:00" />
                                            <asp:ListItem Text="06:00PM" Value="18:00:00" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <hr class="login-line" />
                                    </center>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <div class="form-group">
                                        <asp:Button ID="btnNewServiceRequest" CssClass="btn btn-success btn-block btn-lg" runat="server" Text="Request" OnClientClick="return confirm('Are you sure you want to request a new booking?');" OnClick="btnNewServiceRequest_Click"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
