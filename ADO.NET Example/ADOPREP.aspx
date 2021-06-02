<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ADOPREP.aspx.cs" Inherits="ADO.NET_Example.ADOPREP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Literal ID="ltConfirm" runat="server" Visible="false" />
        <div class="form-group">
            <p class="bg-error">
                <asp:Literal runat="server" ID="ltMsg" />
                <asp:ValidationSummary runat="server" ShowModelStateErrors="true" ID="valSummaryForm" CssClass="bg-error" ValidationGroup="valForm" DisplayMode="BulletList" HeaderText="Fix The Following:" Visible="false" />
        </div>
        <div class="form-group">
            <label class="control-label">Color: </label>
            <asp:TextBox runat="server" ID="txtColor" CssClass="form-control" />
        </div>
        <div class="form-group">
            <label class="control-label">Hex: </label>
            <asp:TextBox runat="server" ID="txtHex" CssClass="form-control" />
        </div>
        <asp:Button ID="btnColor" runat="server" OnClick="btnColor_Click" Text="Add" />
    </form>
</body>
</html>
<%--@{  
    if (@TempData["Msg"] != null)  
    {  
        <script>  
            alert('@TempData["msg"]')  
        </script>  
    }  
} --%>