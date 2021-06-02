<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorHandling.aspx.cs" Inherits="Preparation.ErrorHandling" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Error Handling</h1>
    <asp:Label runat="server" ID="lblMessage" CssClass="bg-error" Visible="false" />
    <div class="form-group">
        <label>This Should Be A Decimal</label>
        <asp:TextBox runat="server" ID="txtDecimal" CssClass="form-control" />
    </div>
    <div class="form-group">
        <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-success" Text="Submit" OnClick="btnSubmit_Click" />
    </div>
</asp:Content>
