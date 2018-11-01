<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DownLoad.aspx.cs" Inherits="DownLoad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
<script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
<script type="text/javascript" src="Script/RankCode.ashx"></script>
<script type="text/javascript" src="Script/Common.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<ajaxToolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
<ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="下載受測資料">
<ContentTemplate>
<div>
<asp:Button ID="downLoad" runat="server" Text="下載" onclick="downLoad_Click" />
</div>
<div>
<br />
<span>下載記錄</span>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="761px" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle"
             HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
        DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="date" HeaderText="下載時間" SortExpression="date" />
            <asp:BoundField DataField="log" HeaderText="紀錄內容" SortExpression="log" />
            <asp:BoundField DataField="account" HeaderText="使用者" SortExpression="account" />
        </Columns>
    </asp:GridView>
 <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Center %>" 
        SelectCommand="SELECT [date], [log], [account] FROM [DownLoadLog] ORDER BY [date] DESC">
    </asp:SqlDataSource>
</div>
</ContentTemplate>
</ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>
</asp:Content>

