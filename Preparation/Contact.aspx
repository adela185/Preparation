<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Preparation.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <div runat="server" id="divMessage" class="message">Where you can get in touch with your local <span runat="server" id="spanDouchebag">douchebag</span>.</div>
    <h3>Your contact page.</h3>
    <div>
        <label>Name</label>
        <asp:TextBox ID="txtName" runat="server" CssClass="text-box" />
        <asp:RequiredFieldValidator runat="server" ID="rfvName" Controltovalidate="txtName" ErrorMessage="*" Display="Dynamic" />
    </div>
    <div>
        <label>Email</label>
        <asp:TextBox id="txtEmail" runat="server" />
        <asp:RegularExpressionValidator runat="server" ID="revEmail" ControlToValidate="txtEmail" ErrorMessage="Valid Email Is Required." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" />
        <asp:RequiredFieldValidator runat="server" ID="rfvEmail" ControlToValidate="txtEmail" ErrorMessage="*" Display="Dynamic" />
    </div>
    <div>
        <label>Age</label>
        <asp:TextBox Id="txtAge" runat="server" />
    </div>
    <div>
        <label>Your Favorite Color, You Fairy:</label>
        <asp:DropDownList iD="ddlColor" runat="server">
            <asp:ListItem Text="Please Pick A Selection." Value="" />
            <asp:ListItem Text="Red" value="Red" />
            <asp:ListItem Text="Blue" value="Blue" />
            <asp:ListItem Text="Green" value="Green" />
            <asp:ListItem Text="'Merica" value="'Merica" />
            <asp:ListItem>sumthing</asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator runat="server" ID="rfvColor" ControlToValidate="ddlColor" ErrorMessage="*" Display="Dynamic"/>
    </div>
    <div>
        <asp:Button ID="btnSubmit" runat="server" text="Submit Info" OnClick="btnSubmit_Click"/>
    </div>
    <address>
        One Microsoft Way<br />
        Redmond, WA 98052-6399<br />
        <abbr title="Phone">P:</abbr>
        425.555.0100
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
    </address>
    <div class="message">
        <asp:Literal runat="server" ID="ltMessage" />
    </div>
</asp:Content>
