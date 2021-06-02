<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DataBindingWithSQL.aspx.cs" Inherits="Preparation.DataBindingWithSQL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Data Binding</h1>
    <h4><asp:Literal ID="ltError" runat="server" /></h4>
    <asp:GridView ID="gvColors" runat="server" CssClass="table table-striped color-table" AutoGenerateColumns="false" OnRowDeleting="gvColors_RowDeleting" OnRowEditing="gvColors_RowEditing" OnRowUpdating="gvColors_RowUpdating" OnRowCancelingEdit="gvColors_RowCancelingEdit" GridLines="None" >
        <Columns>
            <asp:TemplateField Visible ="false">
                <ItemTemplate>
                    <asp:HiddenField ID="hdnColorId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "colorId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="name" HeaderText="Name" />
            <asp:BoundField DataField="hex" HeaderText="Hex" />
            <asp:TemplateField HeaderText="Color Swatch">
                <ItemTemplate>
                    <div class="color-swatch">
                        <div class="color-swatch" style='<%# "background-color:#" + Eval("hex") + ";color:#" + Eval("hex") + ";"  %>' >/</div>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="true" />
            <asp:CommandField ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
    <div class="row color-table">
        <asp:Button ID="btnAddRow" runat="server" Text="Add New Row" CssClass="btn btn-primary" OnClick="btnAddRow_Click" />
    </div>
</asp:Content>
