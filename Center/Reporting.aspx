<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Reporting.aspx.cs" Inherits="Reporting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
<script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
<script type="text/javascript">
    $(function() {
        $('h2').attr({ style: "cursor:pointer" });

        $('#item').click(function() {
            window.open('ReportingSumByItem.aspx', '', '');
        });

        $('#age').click(function() {
            window.open('ReportingByAge.aspx', '', '');
        });

        $('#rank').click(function() {
            window.open('ReportingByRank.aspx', '', '');
        });

    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<ajaxToolkit:TabContainer id="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
<ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="成績統計">
<ContentTemplate>
<div style="text-align:center">
<table>
<tr><td><h2 id="item">項目報表</h2></td></tr>
<tr><td style="display:none"><h2 id="age">年齡報表</h2></td></tr>
<tr><td style="display:none"><h2 id="rank">級職報表</h2></td></tr>
</table>
</div>
 
</ContentTemplate>
</ajaxToolkit:TabPanel>





 
</ajaxToolkit:TabContainer>
</asp:Content>

