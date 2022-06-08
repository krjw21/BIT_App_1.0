<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="BIT_WebApp.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="gradient-bg" style="background-color: #040315">
        <div class="container" id="enquiryform" style="padding-top: 100px;">
            <div class="row">
                <div class="col-md-6 mx-auto" style="padding-right: 45px;">
                    <div class="card" style="background-color: #494E6B">
                        <div class="card-body">
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <h3 class="login-labels" style="font-family: 'Goudy Old Style'; font-size: 32px">Enquiry Form:</h3>
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
                                    <label class="login-labels">Full Name:</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="txtFullName" runat="server" placeholder="Full Name"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <label class="login-labels">Email Address:</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="txtEmail" runat="server" placeholder="Email Address"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <label class="login-labels">Phone Number (Optional):</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="txtPhone" runat="server" placeholder="Phone Number"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <label class="login-labels">Write Your Question Here:</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" Height="100" ID="txtQuestion" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <div class="form-group">
                                        <asp:Button ID="btnEnquire" CssClass="btn btn-success btn-block btn-lg" runat="server" Text="Enquire" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-6" id="contact">
                    <div class="os-header" style="font-family: 'Goudy Old Style'; font-size: 30px">
                        Head Office
                    </div>
                    <div class="os-content">
                        205 Peats Ferry Rd
                    </div>
                    <div class="os-content">
                        Hornsby NSW 2077
                    </div>
                    <div class="os-content">
                        <em><strong>Phone:</strong> +61 13 16 01</em> (Mon - Fri)
                    </div>
                    <div class="os-content">
                        <em><strong>Email:</strong> FieldSupportServices@BIT.com.au</em>
                    </div>
                    <div class="os-content">
                        <em><strong>Fax:</strong> +61 9222 8333</em>
                    </div>
                    <div class="os-sub-header">
                        Email or call us with any questions or inquiries. Free quotes and consultations are also provided!
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
