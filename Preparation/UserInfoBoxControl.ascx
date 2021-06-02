<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserInfoBoxControl.ascx.cs" Inherits="Preparation.UserInfoBoxControl" %>
<b>Information About <%= this.UserName %>:</b>
<br /> 
<%= this.UserName %> is <%= this.UserAge %> years old and lives in <%= this.UserCountry %>.
<br /> <br />