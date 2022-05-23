<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CoordinatorBookings.aspx.cs" Inherits="BIT_WebApp.Pages.CoordinatorBookings" %>

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
                                    <h3>Requested & Rejected Bookings</h3>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 mx-auto">
                                <asp:GridView ID="gvBookings"
                                    CssClass="table table-striped table-bordered"
                                    runat="server" OnRowCommand="gvBookings_RowCommand" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlContractor" runat="server" Height="30px" Width="150px" />
                                                <asp:Button ID="btnAssign" runat="server" Height="30px" Width="70px" Text="Assign" CommandName="Assign" CommandArgument="<% #Container.DataItemIndex %>" />
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
