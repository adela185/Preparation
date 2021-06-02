<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="gmr_reg.aspx.cs" Inherits="Preparation.gmr_reg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section id ="main-content">
        <section id ="wrapper">
            <div class="row">
                <div class =" col-lg-12">
                    <section class="panel">
                        <header class ="panel-heading">
                            <div class="col-md-4 col-md-offset-4">
                                <h1>Registration For GaMeRs YURRRRR</h1>
                            </div>
                        </header>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-4 col-md-offset-1">
                                    <div class="form-group">
                                        <asp:Label Text="gAmEr Name" runat="server" />
                                        <asp:TextBox runat="server" Enabled="true" CssClass="form-control input-sm" placeholder="1337" />
                                    </div>
                                </div>
                                <div class="col-md-4 col-md-offset-1">
                                    <div class="form-group">
                                        <asp:Label Text="wAiFu Name" runat="server" />
                                        <asp:TextBox runat="server" Enabled="true" CssClass="form-control input-sm" placeholder="ofc 2b is best" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 col-md-offset-1">
                                    <div class="form-group">
                                        <asp:Label Text="Date Of Reincarnation" runat="server" />
                                        <asp:TextBox runat="server" Enabled="true" TextMode="Date" CssClass="form-control input-sm" placeholder="1337" />
                                    </div>
                                </div>
                                <div class="col-md-4 col-md-offset-1">
                                    <div class="form-group">
                                        <asp:Label Text="Loadout" runat="server" />
                                        <asp:TextBox runat="server" Enabled="true" CssClass="form-control input-sm" placeholder="M4 Paired With A Bannana Sidearm" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 col-md-offset-1">
                                    <div class="form-group">
                                        <asp:Label Text="Realm From Which You Hail" runat="server" />
                                        <asp:DropDownList runat="server" CssClass="form-control input-sm" >
                                            <asp:ListItem Text="Hell" />
                                            <asp:ListItem Text="Heavens" />
                                            <asp:ListItem Text="Cosmos" />
                                            <asp:ListItem Text="Deez Nuts" />
                                            <asp:ListItem Text="Gottem" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4 col-md-offset-1">
                                    <div class="form-group">
                                        <asp:Label Text="Location of gAmEr PaD" runat="server" />
                                        <asp:TextBox runat="server" Enabled="true" CssClass="form-control input-sm" placeholder="Wherever Abdelh Keeps His Questionable gmod Babies" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 col-md-offset-1">
                                    <div class="form-group">
                                        <asp:Label Text="Your Telly Ol'Chap" runat="server" />
                                        <asp:TextBox runat="server" Enabled="true" TextMode="Phone" CssClass="form-control input-sm" placeholder="1337" />
                                    </div>
                                </div>
                                <div class="col-md-4 col-md-offset-1">
                                    <div class="form-group">
                                        <asp:Label Text="Gender: You're Not Special" runat="server" />
                                        <asp:RadioButtonList runat="server" >
                                            <asp:ListItem Text="    Male" />
                                            <asp:ListItem Text="    Female" />
                                            <asp:ListItem Text="    There are ony two genders" />
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-8 col-md-offset-2">
                                    <asp:Button Text="Save" ID="btnsave" CssClass="btn btn-primary' Width=170px" runat="server" />
                                    <asp:Button Text="Update" ID="Button1" CssClass="btn btn-primary' Width=170px" runat="server" />
                                    <asp:Button Text="Delete" ID="Button2" CssClass="btn btn-danger' Width=170px" runat="server" />
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
        </section>
    </section>

</asp:Content>
