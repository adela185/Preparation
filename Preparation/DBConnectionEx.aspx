<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DBConnectionEx.aspx.cs" Inherits="Preparation.DBConnectionEx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>DBConnection Won't Work Without SQL Server</h1>
    <p>Though what if code still in code-behind.</p>
    <div class="row">
        <asp:Literal runat="server" ID="ltConnectionMessage" />
        <ul>
            <li style="color:blue">Color Test</li>
            <asp:Literal runat="server" ID="ltOutput" />
        </ul>
    </div>
</asp:Content>
