<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ToExcel.aspx.cs" Inherits="ToExcel" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
<script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
<script type="text/javascript" src="Script/RankCode.ashx"></script>
<script type="text/javascript" src="Script/Common.js"></script>
<script type="text/javascript">
    $(function() {
        $('.go').click(function() {
        window.open('PrintDetails.aspx?date=' + $(':text').val(), '', '');
        });

        $('#ToExcel').click(function() {
        var curTbl = document.getElementById('detailtable');
            var oXL = new ActiveXObject("Excel.Application");
            //var ExcelSheet = new ActiveXObject("Excel.Sheet");
            var oWB = oXL.Workbooks.Add();
            var oSheet = oWB.ActiveSheet;
            var sel = document.body.createTextRange();
            sel.moveToElementText(curTbl);
            sel.select();
            sel.execCommand("Copy");
            oSheet.Paste();
            oXL.Visible = true;
        });
    });
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<ajaxToolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
<ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="清冊下載">
<ContentTemplate>
<div>請輸入日期<asp:TextBox ID="txtDate" runat="server" MaxLength="9"></asp:TextBox>(ex:99/3/10)
<asp:Button ID="Button1" runat="server" Text="查詢" onclick="Button1_Click" />
<%--<input type="button" value="存成Excel" id="ToExcel" />--%>
<%--<asp:Button ID="Button2" runat="server" Text="儲存" onclick="Button2_Click" />--%>
<input type="button" value="列印" id="go" class="go" />
</div>
<table id="detailtable">
<tbody>
<tr>
<%--    <td>請輸入日期</td>
    <td><asp:TextBox ID="txtDate" runat="server"></asp:TextBox>(ex:99/3/10)</td>
    <td><asp:Button ID="Button1" runat="server" Text="查詢" onclick="Button1_Click" /></td>
    <td><input type="button" value="存成Excel" id="ToExcel" class="go" /></td>
    <td><asp:Button ID="Button2" runat="server" Text="儲存" onclick="Button2_Click" /></td>
    <td><input type="button" value="列印" id="go" class="go" /></td>--%>
</tr>
<tr>
    <td colspan="5">
    <div><span>軍總人數統計:</span></div>
    <div>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
    </div>
    <%--<div><span>替代項目人數統計:</span></div>--%>
    <div>
        <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label13" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label14" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label15" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label16" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label17" runat="server" Text=""></asp:Label>
    </div>
    </td>
</tr>
<tr>
<td colspan="5">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle"
             HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
            Width="761px" AllowPaging="true" PageSize="20"
        DataSourceID="SqlDataSource1" onrowdatabound="GridView1_RowDataBound" >
        <Columns>
            <asp:BoundField DataField="sid" HeaderText="編號" 
                SortExpression="sid" />
            <asp:BoundField DataField="service_code" HeaderText="軍種" 
                SortExpression="service_code" />
            <asp:BoundField DataField="unit_title" HeaderText="單位" 
                SortExpression="unit_title" />
            <asp:BoundField DataField="rank_title" HeaderText="級職" 
                SortExpression="rank_title" />
            <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name" />
            <asp:BoundField DataField="age" HeaderText="年齡" SortExpression="age" />
            <asp:BoundField DataField="birth" HeaderText="生日" SortExpression="birth" DataFormatString="{0:yyyy/MM/dd}" />     
            <asp:BoundField DataField="result" HeaderText="報名方式" 
                SortExpression="result" />      
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="Toexcel" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="date" Type="DateTime" />
            </SelectParameters>
        </asp:SqlDataSource>
    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Center %>"  
        SelectCommand="SELECT Result.sid ,Unit.service_code, Unit.unit_title, Rank.rank_title, Result.name, Result.birth, Result.age  FROM Result INNER JOIN Unit ON Result.unit_code = Unit.unit_code INNER JOIN Rank ON Result.rank_code = Rank.rank_code WHERE (Result.date = @date) Order By Unit.service_code,Unit.unit_title,Rank.rank_code">
        <SelectParameters>
            <asp:Parameter Name="date" Type="DateTime" />
        </SelectParameters>
    </asp:SqlDataSource>--%>
</td>
</tr>
</tbody>
</table>
</ContentTemplate>
</ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>
</asp:Content>

