<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="CoordinatorBookings.aspx.cs" Inherits="BIT_WebApp.Pages.CoordinatorBookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" style="margin: 100px 0 100px 0; background-color: #040315">
        <center>
            <div id="txtLoginName" runat="server" style="color: #B8A5AE; font-style: italic; font-size: 28px; font-weight: bold; margin-bottom: -5px; font-family: 'Goudy Old Style'"></div>
            <div style="color: #B8A5AE; font-size: 26px; margin-bottom: 10px; font-family: 'Goudy Old Style'">Here are the unassigned bookings:</div>
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
                                    <EmptyDataTemplate>There are currently no unassigned jobs.</EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Find">
                                            <ItemTemplate>
                                                <asp:Button ID="btnFind" runat="server" Height="30px" Width="50px" Text="Find" CommandName="Find" CommandArgument="<% #Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contractor">
                                            <ItemTemplate>
                                                <asp:DropDownList CssClass="ddl" ID="ddlContractors" runat="server" Height="30px" Width="150px" DataTextField="FullName" DataValueField="FullName" AutoPostBack="True"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Assign">
                                            <ItemTemplate>
                                                <asp:Button ID="btnAssign" runat="server" Height="30px" Width="70px" Text="Assign" CommandName="Assign" CommandArgument="<% #Container.DataItemIndex %>" OnClientClick="return confirm('Are you sure you want to assign this Contractor?');" />
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
