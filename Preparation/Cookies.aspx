<%@ Page Title="Cookies" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cookies.aspx.cs" Inherits="Preparation.Cookies" %>
<%@ MasterType TypeName="SiteMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:DropDownList runat="server" id="ColorSelector" autopostback="true" onselectedindexchanged="ColorSelector_SelectedIndexChanged">
            <asp:ListItem value="White" selected="True">Select color...</asp:ListItem>
            <asp:ListItem value="Red">Red</asp:ListItem>
            <asp:ListItem value="Green">Green</asp:ListItem>
            <asp:ListItem value="Blue">Blue</asp:ListItem>
        </asp:DropDownList>
    </div>
</asp:Content>
