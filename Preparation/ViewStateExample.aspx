<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewStateExample.aspx.cs" Inherits="Preparation.ViewStateExample" %>
<%--<%@ OutputCache duration="10" varybyparam="none" varybycontrol="ddlState" %>--%>
<%--<%@ OutputCache duration="120" varybyparam="None" varybycustom="Browser" %>--%>
<%@ OutputCache duration="120" varybyparam="None" varybyheader="Accept-Language" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>View State Example</h2>
    <div class="lead"><asp:Literal runat="server" ID="ltPostBack" /></div>
    <p class="bg-primary">
        <asp:Literal runat="server" ID="ltMessage" />
        <asp:ValidationSummary runat="server" ID="valSummaryForm" CssClass="bg-error" ValidationGroup="valForm" DisplayMode="BulletList" HeaderText="Please Fix The Following:" Visible="false" />
    </p>
    <div class="form-group">
        <label>Nick Name</label>
        <asp:TextBox runat="server" ID="txtNickName" CssClass="form-control" />
    </div>
    <div class="form-group">
        <label>Name</label>
        <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" ValidationGroup="valForm" ErrorMessage="*" Display="Dynamic" />
        <asp:CustomValidator runat="server" ID="cv8chars" ControlToValidate="txtName" ValidationGroup="valForm" ErrorMessage="Input must be exactly 8 characters long!" Display="Dynamic" OnServerValidate="cv8chars_ServerValidate" />
        <asp:TextBox runat="server" ID="txtName" CssClass="form-control" />
    </div>
    <div class="form-group">
        <label>Age</label>
        <asp:RequiredFieldValidator runat="server" ID="rfvAge" ControlToValidate="txtAge" ErrorMessage="*" Display="Dynamic" CssClass="bg-error" ValidationGroup="valForm" />
        <asp:RangeValidator runat="server" ID="rvAge" ControlToValidate="txtAge" ErrorMessage="Please input valid age" Display="Dynamic" CssClass="bg-error" ValidationGroup="valForm" Type="Integer" MinimumValue="4" MaximumValue="120" />
        <asp:TextBox runat="server" ID="txtAge" CssClass="form-control" />
    </div>
    <div class="form-group">
        <label>Email</label>
        <asp:RequiredFieldValidator runat="server" ID="rfvEmail" ControlToValidate="txtEmail" ValidationGroup="valForm" ErrorMessage="*" Display="Dynamic" />
        <asp:RegularExpressionValidator runat="server" ID="revEmail" ControlToValidate="txtEmail" ValidationGroup="valForm" ErrorMessage="Valid email is required." Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
    </div>
    <div class="form-group">
        <label>Phone</label>
        <asp:RequiredFieldValidator runat="server" ID="rfvPhone" ControlToValidate="txtPhone" ValidationGroup="valForm" ErrorMessage="*" Display="Dynamic" />
        <asp:RegularExpressionValidator runat="server" ID="revPhone" ControlToValidate="txtPhone" ValidationGroup="valForm" ErrorMessage="Valid phone number required." Display="Dynamic" ValidationExpression="^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$" />
        <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" />
    </div>
    <div class="form-group">
        <label>City</label>
        <asp:RequiredFieldValidator runat="server" ID="rfvCity" ControlToValidate="txtCity" ValidationGroup="valForm" ErrorMessage="*" Display="Dynamic" />
        <asp:TextBox runat="server" ID="txtCity" CssClass="form-control" />
    </div>
    <div class="form-group">
        <label>State</label>
        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlState" ValidationGroup="valForm" ErrorMessage="*" Display="Dynamic" />
        <asp:DropDownList runat="server" ID="ddlState" CssClass="form-control">
            <asp:ListItem Value="" Text="Please Pick Selection" />
            <asp:ListItem Value="FL" Text="Florida" />
            <asp:ListItem Value="CA" Text="California" />
            <asp:ListItem Value="NY" Text="New York" />
        </asp:DropDownList>
    </div>
    <div class="form-group">
        <label>Price</label>
        <asp:RequiredFieldValidator runat="server" ID="rfvPrice" ControlToValidate="txtPrice" ErrorMessage="*" Display="Dynamic" CssClass="bg-error" ValidationGroup="valForm" />
        <asp:CompareValidator runat="server" ID="cvPrice" ControlToValidate="txtPrice" ErrorMessage="Must be valid format" Display="Dynamic" CssClass="bg-error" ValidationGroup="valForm" Operator="DataTypeCheck" Type="Currency" />
        <asp:TextBox runat="server" ID="txtPrice" CssClass="form-control" />
    </div>
    <div class="form-group">
        <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit Form" CssClass="btn btn-success" ValidationGroup="valForm" CausesValidation="true" />
    </div>
</asp:Content>
