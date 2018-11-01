<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="LogView.aspx.cs" Inherits="HQ_LogView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />

    <script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js">
    </script>

    <script type="text/javascript" src="Script/RankCode.ashx"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<ajaxToolkit:TabContainer ID="TabContainer" runat="server" CssClass="ajax__tab_yuitabview-theme">
<ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="事件檢視錄">
<ContentTemplate>
<div>
<table>
<tr>
<td>事件種類</td>
<td><asp:DropDownList ID="DropDownList1" runat="server" 
        DataSourceID="SqlDataSourceTType" DataTextField="type" DataValueField="type">
    </asp:DropDownList></td>
    <td>
        <asp:Button ID="Button1" runat="server" Text="確定" onclick="Button1_Click" /></td>
</tr>
</table>
    
    <asp:SqlDataSource ID="SqlDataSourceTType" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Center %>" 
        SelectCommand="SELECT DISTINCT type FROM SysLog where type is not null">
    </asp:SqlDataSource>
</div>
<div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle" PageSize = 25
             HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
            Width="761px" AllowSorting="true" AllowPaging="true" 
        DataSourceID="SqlDataSourceLog" onrowdatabound="GridView1_RowDataBound">
        <Columns>
            <asp:BoundField DataField="acc" HeaderText="帳號" SortExpression="acc" >
                <ControlStyle Width="200px" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="event" HeaderText="事件內容" SortExpression="event" >
                <HeaderStyle HorizontalAlign="Center" />
                
                <ItemStyle Width="395px" />
            </asp:BoundField>
            <asp:BoundField DataField="eventTime" HeaderText="事件時間" 
                SortExpression="eventTime" >
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceLog" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Center %>" 
        
        SelectCommand="SELECT [acc], [event], [eventTime] FROM [SysLog] WHERE ([type] = @type) order by eventTime desc">
        <SelectParameters>
            <asp:Parameter Name="type" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</div>
</ContentTemplate>
</ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>

</asp:Content>
