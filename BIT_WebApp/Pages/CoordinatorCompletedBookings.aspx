<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CoordinatorCompletedBookings.aspx.cs" Inherits="BIT_WebApp.Pages.CoordinatorCompletedBookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" style="margin: 100px 0 100px 0; background-color: #040315">
        <center>
            <div id="txtLoginName" runat="server" style="color: #B8A5AE; font-style:italic; font-size: 28px; font-weight: bold; margin-bottom: -5px; font-family: 'Goudy Old Style'"></div>
            <div style="color: #B8A5AE; font-size: 26px; margin-bottom: 10px; font-family: 'Goudy Old Style'">Here are the completed bookings that require payment:</div>
        </center>
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-12 mx-auto">
                                <asp:GridView ID="gvBookings"
                                    CssClass="table table-striped table-bordered"
                                    runat="server" OnRowCommand="gvBookings_RowCommand" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" ForeColor="#040315">
                                    <EmptyDataRowStyle HorizontalAlign="Center" Font-Size="14" />
                                    <EmptyDataTemplate>There are currently no completed jobs.</EmptyDataTemplate>
                                    <columns>
                                        <asp:TemplateField HeaderText="Action">
                                            <itemtemplate>
                                                <asp:Button ID="btnApprove" runat="server" Height="30px" Width="80px" Text="Approve" CommandName="Approve" CommandArgument="<% #Container.DataItemIndex %>" OnClientClick="return confirm('Are you sure you want to approve this Service Request for payment?');"/>
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
