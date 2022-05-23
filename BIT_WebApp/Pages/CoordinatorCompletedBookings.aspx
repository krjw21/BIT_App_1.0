<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CoordinatorCompletedBookings.aspx.cs" Inherits="BIT_WebApp.Pages.CoordinatorCompletedBookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Completed Bookings</h3>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 mx-auto">
                                <asp:GridView ID="gvBookings"
                                    CssClass="table table-striped table-bordered"
                                    runat="server" OnRowCommand="gvBookings_RowCommand" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center">
                                    <columns>
                                        <asp:TemplateField HeaderText="Action">
                                            <itemtemplate>
                                                <asp:Button ID="btnApprove" runat="server" Height="30px" Width="80px" Text="Approve" CommandName="Approve" CommandArgument="<% #Container.DataItemIndex %>" />
                                            </itemtemplate>
                                        </asp:TemplateField>
                                    </columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
