<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fileupload.aspx.cs" Inherits="Preparation.fileupload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload id="FileUploadControl" runat="server" />
            <asp:Button runat="server" id="UploadButton" text="Upload" onclick="UploadButton_Click" />
            <br /><br />
            <asp:Label runat="server" id="StatusLabel" text="Upload status: " />
        </div>
    </form>
</body>
</html>
