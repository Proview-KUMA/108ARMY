<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style type="text/css">
.GridViewStyle
{
    font-family: Arial, Sans-Serif;
    font-size:small;
    table-layout: auto;
    border-collapse: collapse;
    border:#91a7b4 1px solid;
}
/*Header and Pager styles*/
.HeaderStyle, .PagerStyle /*Common Styles*/
{
    background-image: url(images/YahooSprite.gif);
    background-position:top;
    background-repeat:repeat-x;
    background-color:#d1dbe0;
}
.HeaderStyle th
{
    padding: 5px;
    color: #16387c;
}
.HeaderStyle a
{
    text-decoration:none;
    color:#16387c;
    display:block;
    text-align:left;
    font-weight:normal;
}
.PagerStyle table
{
    text-align:center;
    margin:auto;
}
.PagerStyle table td
{
    border:0px;
    padding:5px;
}
.PagerStyle td
{
    border-top: #91a7b4 1px solid;
}
.PagerStyle a
{
    color:#16387c;
    text-decoration:none;
    padding:2px 10px 2px 10px;
    border-top:solid 1px #fff;
    border-right:solid 1px #91a7b4;
    border-bottom:solid 1px #91a7b4;
    border-left:solid 1px #fff;
}
.PagerStyle span
{
    font-weight:bold;
    color:#16387c;
    text-decoration:none;
    padding:2px 10px 2px 10px;
}
/*RowStyles*/
.RowStyle td, .AltRowStyle td, .SelectedRowStyle td, .EditRowStyle td /*Common Styles*/
{
    padding: 5px;
    border-right: solid 1px #91a7b4;
}
.RowStyle td
{
    background-color: #f1f5f6;
}
.AltRowStyle td
{
    background-color: #9ab2ca;
	background-image: url(images/YahooSprite.gif);
    background-position:0 -80px;
    background-repeat:repeat-x;
}
.SelectedRowStyle td
{
    background-color: #fcb814;
	background-image: url(images/YahooSprite.gif);
    background-position:center;
    background-repeat:repeat-x;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="sid" HeaderText="sid" SortExpression="sid" />
            <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />
            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
            <asp:BoundField DataField="birth" HeaderText="birth" SortExpression="birth" />
            <asp:BoundField DataField="age" HeaderText="age" SortExpression="age" />
            <asp:BoundField DataField="gender" HeaderText="gender" 
                SortExpression="gender" />
            <asp:BoundField DataField="unit_code" HeaderText="unit_code" 
                SortExpression="unit_code" />
            <asp:BoundField DataField="rank_code" HeaderText="rank_code" 
                SortExpression="rank_code" />
            <asp:BoundField DataField="height" HeaderText="height" 
                SortExpression="height" />
            <asp:BoundField DataField="weight" HeaderText="weight" 
                SortExpression="weight" />
            <asp:BoundField DataField="BMI" HeaderText="BMI" SortExpression="BMI" />
            <asp:BoundField DataField="bodyfat" HeaderText="bodyfat" 
                SortExpression="bodyfat" />
            <asp:BoundField DataField="sit_ups" HeaderText="sit_ups" 
                SortExpression="sit_ups" />
            <asp:BoundField DataField="sit_ups_score" HeaderText="sit_ups_score" 
                SortExpression="sit_ups_score" />
            <asp:BoundField DataField="push_ups" HeaderText="push_ups" 
                SortExpression="push_ups" />
            <asp:BoundField DataField="push_ups_score" HeaderText="push_ups_score" 
                SortExpression="push_ups_score" />
            <asp:BoundField DataField="run" HeaderText="run" SortExpression="run" />
            <asp:BoundField DataField="run_score" HeaderText="run_score" 
                SortExpression="run_score" />
            <asp:BoundField DataField="date" HeaderText="date" SortExpression="date" />
            <asp:BoundField DataField="center_code" HeaderText="center_code" 
                SortExpression="center_code" />
            <asp:BoundField DataField="result" HeaderText="result" 
                SortExpression="result" />
            <asp:BoundField DataField="status" HeaderText="status" 
                SortExpression="status" />
            <asp:BoundField DataField="op_id" HeaderText="op_id" SortExpression="op_id" />
            <asp:BoundField DataField="clothesNum" HeaderText="clothesNum" 
                SortExpression="clothesNum" />
            <asp:BoundField DataField="LF_Tag_ID" HeaderText="LF_Tag_ID" 
                SortExpression="LF_Tag_ID" />
            <asp:BoundField DataField="UHF_Tag_ID" HeaderText="UHF_Tag_ID" 
                SortExpression="UHF_Tag_ID" />
            <asp:BoundField DataField="code" HeaderText="code" SortExpression="code" />
            <asp:BoundField DataField="memo" HeaderText="memo" SortExpression="memo" />
             <asp:TemplateField HeaderText="鑑測當日照片">
                <ItemTemplate>
                    <asp:Image runat="server" ID="image1" ImageUrl='<%# "ImageHandler2.ashx?id=" + Eval("id")%>' AlternateText="受測者相片" />      
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Center %>" 
        SelectCommand="SELECT * FROM [Result] WHERE ([status] &lt;&gt; @status)">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="999" Name="status" 
                QueryStringField="status" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource2">
        <Columns>
            <asp:BoundField DataField="sid" HeaderText="sid" SortExpression="sid" />
            <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />
            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
            <asp:BoundField DataField="birth" HeaderText="birth" SortExpression="birth" />
            <asp:BoundField DataField="age" HeaderText="age" SortExpression="age" />
            <asp:BoundField DataField="gender" HeaderText="gender" 
                SortExpression="gender" />
            <asp:BoundField DataField="rank_code" HeaderText="rank_code" 
                SortExpression="rank_code" />
            <asp:BoundField DataField="unit_code" HeaderText="unit_code" 
                SortExpression="unit_code" />
            <asp:BoundField DataField="height" HeaderText="height" 
                SortExpression="height" />
            <asp:BoundField DataField="weight" HeaderText="weight" 
                SortExpression="weight" />
            <asp:BoundField DataField="BMI" HeaderText="BMI" SortExpression="BMI" />
            <asp:BoundField DataField="run" HeaderText="run" SortExpression="run" />
            <asp:BoundField DataField="push_ups_score" HeaderText="push_ups_score" 
                SortExpression="push_ups_score" />
            <asp:BoundField DataField="push_ups" HeaderText="push_ups" 
                SortExpression="push_ups" />
            <asp:BoundField DataField="sit_ups_score" HeaderText="sit_ups_score" 
                SortExpression="sit_ups_score" />
            <asp:BoundField DataField="sit_ups" HeaderText="sit_ups" 
                SortExpression="sit_ups" />
            <asp:BoundField DataField="bodyfat" HeaderText="bodyfat" 
                SortExpression="bodyfat" />
            <asp:BoundField DataField="run_score" HeaderText="run_score" 
                SortExpression="run_score" />
            <asp:BoundField DataField="date" HeaderText="date" SortExpression="date" />
            <asp:BoundField DataField="center_code" HeaderText="center_code" 
                SortExpression="center_code" />
            <asp:BoundField DataField="status" HeaderText="status" 
                SortExpression="status" />
            <asp:BoundField DataField="op_id" HeaderText="op_id" SortExpression="op_id" />
            <asp:BoundField DataField="result" HeaderText="result" 
                SortExpression="result" />
            <asp:BoundField DataField="memo" HeaderText="memo" SortExpression="memo" />
            <asp:BoundField DataField="code" HeaderText="code" SortExpression="code" />
            <asp:BoundField DataField="UHF_Tag_ID" HeaderText="UHF_Tag_ID" 
                SortExpression="UHF_Tag_ID" />
            <asp:BoundField DataField="LF_Tag_ID" HeaderText="LF_Tag_ID" 
                SortExpression="LF_Tag_ID" />
            <asp:BoundField DataField="clothesNum" HeaderText="clothesNum" 
                SortExpression="clothesNum" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Center %>" 
        SelectCommand="SELECT [sid], [id], [name], [birth], [age], [gender], [rank_code], [unit_code], [height], [weight], [BMI], [run], [push_ups_score], [push_ups], [sit_ups_score], [sit_ups], [bodyfat], [run_score], [date], [center_code], [status], [op_id], [result], [memo], [code], [UHF_Tag_ID], [LF_Tag_ID], [clothesNum] FROM [Result] WHERE ([status] &lt;&gt; @status)">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="999" Name="status" 
                QueryStringField="status" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
