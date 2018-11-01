<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangeItem_old.aspx.cs" Inherits="ChangeItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
<script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ajaxToolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
<ajaxToolkit:TabPanel ID="TabPanel" runat="server" HeaderText="設定鑑測項目">
<ContentTemplate>
<span>身分證字號：</span><asp:TextBox ID="TB_id" runat="server" MaxLength="10"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="查詢原項目" OnClick="Button1_Click" /><span style="margin-left:10px">此功能僅限今日鑑測人員</span><br />
<div style="margin-top:5px; margin-bottom:5px">
 <asp:Label ID="Label1" runat="server" Text="原項目一"></asp:Label> 
        <asp:DropDownList ID="DropDownList4" AutoPostBack="True"
            runat="server" Enabled="False">
            <asp:ListItem Selected="True" Value="0">仰臥起坐</asp:ListItem>
            <asp:ListItem Value="F">800公尺游走</asp:ListItem>
            <asp:ListItem Value="G">5公里健走</asp:ListItem>
            <asp:ListItem Value="H">5分鐘跳繩</asp:ListItem>
            <asp:ListItem Value="I">單槓引體向上</asp:ListItem>
            <asp:ListItem Value="J">曲臂懸垂</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label2" runat="server" Text="原項目二"></asp:Label>
        <asp:DropDownList ID="DropDownList5" AutoPostBack="True"
            runat="server" Enabled="False">
            <asp:ListItem Selected="True" Value="0">俯地起身</asp:ListItem>
            <asp:ListItem Value="F">800公尺游走</asp:ListItem>
            <asp:ListItem Value="G">5公里健走</asp:ListItem>
            <asp:ListItem Value="H">5分鐘跳繩</asp:ListItem>
            <asp:ListItem Value="I">單槓引體向上</asp:ListItem>
            <asp:ListItem Value="J">曲臂懸垂</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label3" runat="server" Text="原項目三"></asp:Label>
        <asp:DropDownList ID="DropDownList6" AutoPostBack="True"
            runat="server" Enabled="False">
            <asp:ListItem Selected="True" Value="0">三千公尺徒手跑步</asp:ListItem>
            <asp:ListItem Value="F">800公尺游走</asp:ListItem>
            <asp:ListItem Value="G">5公里健走</asp:ListItem>
            <asp:ListItem Value="H">5分鐘跳繩</asp:ListItem>
            <asp:ListItem Value="I">單槓引體向上</asp:ListItem>
            <asp:ListItem Value="J">曲臂懸垂</asp:ListItem>
        </asp:DropDownList><br />
        </div>

<div style="margin-top:5px; margin-bottom:5px">
    <asp:Label ID="Label7" runat="server" Text="新項目一"></asp:Label>
        <asp:DropDownList ID="DropDownList1" 
            runat="server">
        </asp:DropDownList>
        <asp:Label ID="Label8" runat="server" Text="新項目二"></asp:Label>
        <asp:DropDownList ID="DropDownList2"
            runat="server">
        </asp:DropDownList>
        <asp:Label ID="Label9" runat="server" Text="新項目三"></asp:Label>
        <asp:DropDownList ID="DropDownList3"
            runat="server">
        </asp:DropDownList><br />
        </div>
        <asp:Button ID="Button7" runat="server" Text="設定鑑測項目" 
        onclick="Button7_Click" />
</ContentTemplate>
</ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>      
</asp:Content>

