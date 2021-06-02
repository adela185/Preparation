<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectCalculation.aspx.cs" Inherits="Preparation.ProjectCalculation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Get A Quote, Any Quote</h1>
    <p class="lead">Base Price: <asp:Literal ID="ltBasePrice" runat="server" /></p>
    <p>East of Colorado gets a flat rate of $49.99.West coast states are less.</p>

    <div class="form-group">
        <label>State</label>
        <asp:DropDownList ID="ddlStates" CssClass="form-control" OnSelectedIndexChanged="ddlStates_SelectedIndexChanged" AutoPostBack="true" runat="server" >
            <asp:ListItem Value="">Please Choose Your State</asp:ListItem>
            <asp:ListItem Value="AL">Alabama</asp:ListItem>
            <asp:ListItem Value="AK">Alaska</asp:ListItem>
            <asp:ListItem Value="AZ">Arizona</asp:ListItem>
            <asp:ListItem Value="CA">California</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="jumbotron">
        <h3>Your Custom Price: <asp:Literal Id="ltCustomPrice" runat="server" Text="Select State For Price" /></h3>
    </div>
</asp:Content>
