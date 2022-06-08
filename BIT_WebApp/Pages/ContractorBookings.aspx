<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContractorBookings.aspx.cs" Inherits="BIT_WebApp.Pages.ContractorBookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" style="margin: 100px 0 100px 0; background-color: #040315">
        <center>
            <div id="txtLoginName" runat="server" style="color: #B8A5AE; font-style:italic; font-size: 28px; font-weight: bold; margin-bottom: -5px; margin-top: -10px; font-family: 'Goudy Old Style'"></div>
            <div style="color: #B8A5AE; font-size: 26px; margin-bottom: 10px; font-family: 'Goudy Old Style'">Here are your assigned bookings:</div>
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
                                    <EmptyDataTemplate>There are currently no assigned jobs.</EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Accept">
                                            <ItemTemplate>
                                                <asp:Button ID="btnAccept" runat="server" Height="30px" Width="70px" Text="Accept" CommandName="Accept" CommandArgument="<% #Container.DataItemIndex %>" OnClientClick="return confirm('Are you sure you want to accept this job?');"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reject">
                                            <ItemTemplate>
                                                <asp:Button ID="btnReject" runat="server" Height="30px" Width="70px" Text="Reject" CommandName="Reject" CommandArgument="<% #Container.DataItemIndex %>" OnClientClick="return confirm('Are you sure you want to reject this job?');"/>
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
