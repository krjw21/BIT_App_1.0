<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContractorAcceptedBookings.aspx.cs" Inherits="BIT_WebApp.Pages.ContractorAcceptedBookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" style="margin: 100px 0 100px 0; background-color: #040315">
        <center>
            <div id="txtLoginName" runat="server" style="color: #B8A5AE; font-style:italic; font-size: 28px; font-weight: bold; margin-bottom: -5px; margin-top: -10px; font-family: 'Goudy Old Style'"></div>
            <div style="color: #B8A5AE; font-size: 26px; margin-bottom: 10px; font-family: 'Goudy Old Style'">Here are your currently accepted bookings:</div>
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
                                    <EmptyDataTemplate>There are currently no accepted jobs.</EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Hours">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtHoursWorked" runat="server" Height="30px" Width="40px" CommandName="Hours" CommandArgument="<% #Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="KMs">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDistanceTravelled" runat="server" Height="30px" Width="40px" CommandName="Distance" CommandArgument="<% #Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="btnComplete" runat="server" Height="30px" Width="90px" Text="Complete" CommandName="Complete" CommandArgument="<% #Container.DataItemIndex %>" OnClientClick="return confirm('Are you sure you want to mark this job as completed?');"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
