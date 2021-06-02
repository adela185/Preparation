<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Localization.aspx.cs" Culture="en-US" UICulture="en-US" Inherits="Preparation.Localization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label runat="server" ID="lblHelloWorld" Text="Hello, World!" Font-Names="Verdana" ForeColor="Blue" meta:resourcekey="lblHelloWorld" />
            <br />
            <% Response.Write("Your current culture: " + System.Globalization.CultureInfo.CurrentCulture.DisplayName);%>
            <br />
            <% Response.Write("Current date, in a culture specific format: " + DateTime.Now.ToString()); %>
            <br />
            <% Response.Write("Current date, in a culture specific format: " + DateTime.Now.ToString(System.Globalization.CultureInfo.GetCultureInfo("de-DE").DateTimeFormat)); %>
            <br />
            <asp:Label runat="server" ID="lblHelloWorld2" Text="<%$ Resources:lblHelloWorld.Text %>" Font-Names="Veranda" ForeColor="<%$ Resources:lblHelloWorld.ForeColor %>"  />
           <%-- <%$ Resources: MyGlobalResources,NameOfRow %> This is a way to explicity call global resources--%>

            <br />
            <asp:Label runat="server" ID="lblHello" Text="Hello" />
        </div>
    </form>
</body>
</html>
