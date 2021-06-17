<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PageLifeCycle.aspx.cs" Inherits="Preparation.PageLifeCycle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Page Life Cycle Example</h2>
    <div>
        <asp:Label runat="server" ID="lblinit" />
    </div>
    <div>
        <asp:Label runat="server" ID="lblPageLoad" />
    </div>
    <p>
        <asp:Label runat="server" ID="lblPostBack" />
    </p>
    <p>
        <asp:Label runat="server" ID="lblButtoneEvent" />
    </p>
    <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" CssClass="btn btn-primary btn-large" Text="Submit" />
    <br />
    <div>
        <input id="btn" type="button" value="Get All Colors" />
        <input id="btnClear" type="button" value="Clear" />
        <ul id="ulColors" />
        <script src="Scripts/jquery-3.4.1.min.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                var ulColors = $('#ulColors');
                $('#btn').click(function() {
                    $.ajax({
                        type: 'GET',
                        url: "https://localhost:44368/api/Color",
                        dataType: 'json',
                        success: function (data) {
                            ulColors.empty();
                            $.each(data, function (index, val) {
                                var name = val.nm + ' ' + val.hex;
                                ulColors.append('<li>' + name + '<li>');
                            });
                        }
                    });
                });
                $('#btnClear').click(function () {
                    ulColors.empty();
                });
            });
        </script>
    </div>
</asp:Content>
