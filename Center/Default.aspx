<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Center %>" 
        SelectCommand="SELECT * FROM [Result] WHERE ([status] != @status)">
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
        SelectCommand="SELECT [sid], [id], [name], [birth], [age], [gender], [rank_code], [unit_code], [height], [weight], [BMI], [run], [push_ups_score], [push_ups], [sit_ups_score], [sit_ups], [bodyfat], [run_score], [date], [center_code], [status], [op_id], [result], [memo], [code], [UHF_Tag_ID], [LF_Tag_ID], [clothesNum] FROM [Result] WHERE ([status] != @status)">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="999" Name="status" 
                QueryStringField="status" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

