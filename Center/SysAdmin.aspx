<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SysAdmin.aspx.cs" Inherits="SysAdmin" %>

<script runat="server">

    protected void Button1_Click(object sender, EventArgs e)
    {
        new Lib.DataUtility().executeNonQueryBysp("clearresult");
        Response.Redirect("SysAdmin.aspx");
    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ajaxToolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
<ajaxToolkit:TabPanel ID="TabPanel" runat="server" HeaderText="國軍單位更新">
<ContentTemplate>
 <table>
 <tr>
   <td>
       <asp:Button ID="Button1" runat="server" Text="清除所有受測成績" 
           onclick="Button1_Click" 
           OnClientClick="return confirm('風險性警告:此動作會清除所有受測成績,確定要清除嗎? (註:此動作只可使用一次)');" 
           Visible="False"/></td><td></td>
 </tr>
 <tr>
 <td>目前單位檔版本 : </td><td>
     <asp:Label ID="version" runat="server"></asp:Label></td>
 </tr>
 <tr>
 <td>總部單位檔版本 : </td><td>
     <asp:Label ID="HQ_version" runat="server"></asp:Label>
 </td>
 </tr>
 <tr>
 <td colspan="2">
     <asp:Button ID="ReNew" runat="server" Text="下載資料" onclick="ReNew_Click" />
 </td>
 </tr>
 <tr>
 <td colspan="2">
 
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="761px">
         <AlternatingRowStyle CssClass="AltRowStyle" />
         <Columns>
             <asp:BoundField DataField="unit_code" HeaderText="單位代碼" 
                 SortExpression="unit_code" />
             <asp:BoundField DataField="unit_title" HeaderText="單位名稱" 
                 SortExpression="unit_title" />
             <asp:BoundField DataField="parent_unit_code" HeaderText="父階代碼" 
                 SortExpression="parent_unit_code" />
<asp:BoundField DataField="service_code" HeaderText="service_code" 
                 SortExpression="service_code" />
         </Columns>
         <EditRowStyle CssClass="EditRowStyle" />
         <HeaderStyle CssClass="HeaderStyle" />
         <PagerStyle CssClass="PagerStyle" />
         <RowStyle CssClass="RowStyle" />
         <SelectedRowStyle CssClass="SelectedRowStyle" />
     </asp:GridView>
     <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
         ConnectionString="<%$ ConnectionStrings:Center %>" 
         SelectCommand="SELECT [unit_code], [unit_title], [parent_unit_code] FROM [Unit]">
     </asp:SqlDataSource>
 
 </td>
 </tr>
 </table>
</ContentTemplate>
</ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>
</asp:Content>

