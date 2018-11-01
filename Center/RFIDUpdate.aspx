<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RFIDUpdate.aspx.cs" Inherits="RFIDUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ajaxToolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
<ajaxToolkit:TabPanel ID="TabPanel" runat="server" HeaderText="RFID對照表">
<ContentTemplate>
    <div>
        <asp:Label ID="Label1" runat="server" Text="請選擇RFID檔案(格式: .CSV):"></asp:Label><asp:FileUpload ID="FileUpload1" runat="server"  Height="26px"/><asp:Button ID="Button1" 
        runat="server" Text="確認更新" onclick="Button1_Click" OnClientClick="return confirm('風險性警告:此動作會清除全部腕錶的資訊，請確認欲更新的檔案無誤，再進行更新!');" />
    </div>
    <div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle"
             HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
             Width="761px"
        DataSourceID="">
        <Columns>
            <asp:BoundField DataField="code" HeaderText="外碼" SortExpression="code" />
            <asp:BoundField DataField="LF_Tag_ID" HeaderText="LF_Tag_ID" 
                SortExpression="LF_Tag_ID" />
            <asp:BoundField DataField="UHF_Tag_ID" HeaderText="UHF_Tag_ID" 
                SortExpression="UHF_Tag_ID" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Center %>" 
        SelectCommand="SELECT [code], [LF_Tag_ID], [UHF_Tag_ID] FROM [Rfid] ORDER BY [code]">
    </asp:SqlDataSource>
    </div>
</ContentTemplate>
</ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>
</asp:Content>

