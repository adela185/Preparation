<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DBUtilityDemo.aspx.cs" Inherits="ADO.NET_Example.DBUtilityDemo" Async="true" Culture="en-US" UICulture="en-US"%>
<%@ Register Assembly="ADO.NET Class Library Ex" Namespace="ADO.NET_Class_Library_Ex" TagPrefix="cc1"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Literal ID="ltReport" runat="server" />
            <br />
            <asp:Label ID="lblColor" runat="server" >Color: </asp:Label>
            <asp:TextBox ID="txtColor" runat="server"></asp:TextBox>
            <cc1:DataAnnotationValidator ID="valColor" runat="server" ControlToValidate="txtColor" PropertyName="nm"
                SourceTypeName="ADO.NET_Class_Library_Ex.Color, ADO.NET Class Library Ex" />
            <br />
            <asp:Label ID="lblHex" runat="server" >Hex: </asp:Label>
            <asp:TextBox ID="txtHex" runat="server" style="margin-left: 13px"></asp:TextBox>
            <cc1:DataAnnotationValidator ID="valHex" runat="server" ControlToValidate="txtHex" PropertyName="hex"
                SourceTypeName="ADO.NET_Class_Library_Ex.Color, ADO.NET Class Library Ex" />
            <br />
            <asp:Button ID="btnColor" runat="server" OnClick="btnColor_Click" Text="Add Color" />
            <br />
        </div>
        <div>
            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
            <asp:GridView ID="gvColor" runat="server" OnRowDeleting="gvColor_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="true" />
                </Columns>
            </asp:GridView>
            <asp:Button ID="btnCancelLoad" runat="server" OnClick="btnCancelLoad_Click" Text="Cancel Load" />
            <br />
            <asp:Button ID="btnTestStuff" runat="server" OnClick="btnTestStuff_Click" Text="TestStuff" />
            <br />
            <asp:Button ID="btnRefresher" runat="server" OnClick="btnRefresher_Click" Text="Refresher" />
        </div>
    </form>
</body>
</html>
