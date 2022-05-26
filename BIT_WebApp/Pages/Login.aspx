<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BIT_WebApp.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="login-container" style="background-color: #040315">
        <div class="container">
            <div class="row">
                <div class="col-md-6 mx-auto">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <img width="150" src="../Images/login_icon2.png" />
                                    </center>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <h3 class="login-labels">User Login</h3>
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
                                    <label class="login-labels">Username:</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="txtUsername" runat="server" placeholder="Username"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <label class="login-labels">Password:</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="txtPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <div class="form-group">
                                        <asp:Button ID="btnLogin" CssClass="btn btn-success btn-block btn-lg" runat="server" Text="Login" onClick="btnLogin_Click"/>
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
                                    <center>
                                        <label class="login-labels">Don't have an account? Contact us at <em>+61 13 16 01!</em></label>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
