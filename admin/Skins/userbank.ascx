<%@ Control Language="C#" AutoEventWireup="false" Inherits="DotNetNuke.UI.Skins.Controls.User" CodeFile="User.ascx.cs" %>
<%@ Register TagPrefix="Banking" TagName="ChiNhanh" src="~/DesktopModules/TrainingCoreBanking/BankProject/Controls/ThongTinChiNhanh.ascx" %>
<asp:HyperLink ID="registerLink" runat="server" CssClass="SkinObject" />
<div class="registerGroup" runat="server" id="registerGroup" style="height:0; visibility:hidden; width:0; overflow:hidden;display: block;">
    <ul class="buttonGroup">
        <li class="userMessages alpha" runat="server" ID="messageGroup"><asp:HyperLink ID="messageLink" runat="server"/></li>
        <li class="userNotifications omega" runat="server" ID="notificationGroup"><asp:HyperLink ID="notificationLink" runat="server"/></li>
    	<li class="userDisplayName"></li>
        <li class="userProfileImg" runat="server" ID="avatarGroup"><asp:HyperLink ID="avatar" runat="server"/></li>                                       
    </ul>
</div>
<span id="spTimeNow"></span>
<span class="ChiNhanh"> &nbsp;&nbsp;&nbsp;<Banking:ChiNhanh id="tbChiNhanh" runat="server"/> &nbsp;&nbsp;&nbsp;| </span>
<asp:HyperLink ID="enhancedRegisterLink" runat="server"/> 