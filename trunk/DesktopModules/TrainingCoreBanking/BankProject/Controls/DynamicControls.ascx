<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DynamicControls.ascx.cs" Inherits="BankProject.Controls.DynamicControls" %>
<%@ Register Src="~/DesktopModules/TrainingCoreBanking/BankProject/Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>
<%@ Register src="~/DesktopModules/TrainingCoreBanking/BankProject/Controls/VVComboBox.ascx" tagname="VVComboBox" tagprefix="uc2" %>
<uc1:VVTextBox runat="server" id="VVTextBox" VVTLabel="Địa chỉ" Text="Texxt cais nafo"/>
<uc2:VVComboBox ID="VVComboBox1" runat="server" VVTLabel="Custommer" SourceTable="CUSTOMMERS" Width="350" />
<uc1:VVTextBox runat="server" id="VVTextBox1" VVTLabel="Giống gì đó" />
<uc2:VVComboBox ID="VVComboBox2" runat="server" VVTLabel="Custommer" SourceTable="TESTDATA" Width="350" />