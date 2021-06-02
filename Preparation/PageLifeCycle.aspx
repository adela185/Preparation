<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PageLifeCycle.aspx.cs" Inherits="Preparation.PageLifeCycle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Page Life Cycle Example</h2>
    <div>
        <asp:Label runat="server" ID="lblinit" />
    </div>
    <div>
        <asp:Label runat="server" ID="lblPageLoad" />
    </div>
    <p>
        <asp:Label runat="server" ID="lblPostBack" />
    </p>
    <p>
        <asp:Label runat="server" ID="lblButtoneEvent" />
    </p>
    <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" CssClass="btn btn-primary btn-large" Text="Submit" />
</asp:Content>
