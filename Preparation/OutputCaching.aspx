<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OutputCaching.aspx.cs" Inherits="Preparation.OutputCaching" %>
<%@ OutputCache Duration="10" VaryByParam="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <%= DateTime.Now.ToString() %><br />
        <%--<a href="?p=1">1</a><br />
        <a href="?p=2">2</a><br />
        <a href="?p=3">3</a><br />--%>
<%--        <asp:Substitution runat="server" id="uncachedArea" MethodName="GetFreshDateTime" />--%>
    </div>
   
</asp:Content>
