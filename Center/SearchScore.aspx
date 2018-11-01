<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchScore.aspx.cs" Inherits="SearchScore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager> 
<ajaxToolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">

<ajaxToolkit:TabPanel runat="server" ID="TabPanel1" HeaderText="身份證查詢">
<ContentTemplate>
<div>
<asp:Label ID="Label1" runat="server" Text="請輸入身分證 :"></asp:Label><asp:TextBox ID="id" runat="server"></asp:TextBox>
<asp:Button ID="search1" runat="server" CssClass="search" onclick="search1_Click"/>
<asp:Label ID="Label5" runat="server" Text="請選擇日期 :"></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server" Width="100px" AutoPostBack="True"
        DataSourceID="SqlDataSource5" DataTextField="date" DataValueField="date" 
         OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" OnDataBound="DropDownList1_OnDataBound">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Center %>" 
        SelectCommand="SELECT DISTINCT CONVERT(VARCHAR(10), [date], 111) AS [date] FROM [Result] WHERE ([id] = @id) ORDER BY [date] DESC">
        <SelectParameters>
            <asp:Parameter Name="id" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</div>
    <asp:DetailsView ID="DetailsView1" runat="server" DataSourceID="SqlDataSource1" 
        AutoGenerateRows="False" 
        ondatabound="DetailsView1_DataBound"  >
        <Fields>
            <asp:BoundField DataField="id" HeaderText="身份證" SortExpression="id" />
            <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name" />
            <asp:BoundField DataField="age" HeaderText="年齡" SortExpression="age" />
	    <asp:BoundField DataField="clothesNum" HeaderText="背號" SortExpression="clothesNum" />
            <asp:BoundField DataField="sit_ups" HeaderText="仰臥起坐次數" 
                SortExpression="sit_ups" />
            <asp:BoundField DataField="sit_ups_score" HeaderText="仰臥起坐成績" 
                SortExpression="sit_ups_score" />
            <asp:BoundField DataField="push_ups" HeaderText="俯地挺身次數" 
                SortExpression="push_ups" />
            <asp:BoundField DataField="push_ups_score" HeaderText="俯地挺身成績" 
                SortExpression="push_ups_score" />
            <asp:BoundField DataField="run" HeaderText="三千公尺(分/秒)" SortExpression="run" />
            <asp:BoundField DataField="run_score" HeaderText="三千公尺成績" 
                SortExpression="run_score" />
            <asp:BoundField DataField="status" HeaderText="鑑測結果" 
                SortExpression="status" />    
            <asp:BoundField DataField="date" HeaderText="鑑測日期" SortExpression="date" />
            <asp:TemplateField HeaderText="鑑測當日照片">
                <ItemTemplate>
                    <asp:Image runat="server" ID="image1" ImageUrl='<%# "ImageHandler.ashx?id=" + Eval("id") +"&date=" + Eval("date")%>' AlternateText="受測者相片" />      
                </ItemTemplate>
            </asp:TemplateField>
        </Fields>
    </asp:DetailsView>
    
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="QueryResultByID" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="id" Type="String" />
                <asp:Parameter Name="value" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
</ContentTemplate>
</ajaxToolkit:TabPanel>

<ajaxToolkit:TabPanel runat="server" ID="TabPanel2" HeaderText="姓名查詢">
<ContentTemplate>
<div><asp:Label ID="Label2" runat="server" Text="請輸入姓名 :"></asp:Label><asp:TextBox ID="name" runat="server"></asp:TextBox>
<asp:Button ID="search2" runat="server"  CssClass="search" onclick="search2_Click"/></div>
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView_OnRowDataBound" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle"
             HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
            Width="1000px" PageSize = "25" AllowPaging="true"
            DataSourceID="SqlDataSource2" AllowSorting="True" OnSorting="GridView2_Sorting" OnPageIndexChanged="GridView2_PageIndexChanged" >
            <Columns>
                <asp:BoundField DataField="date" HeaderText="鑑測日期" SortExpression="date"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="id" HeaderText="身份證" SortExpression="id"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="age" HeaderText="年齡" SortExpression="age"  ItemStyle-Wrap="false"/>               
                <asp:BoundField DataField="sit_ups" HeaderText="仰臥起坐" SortExpression="sit_ups"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="sit_ups_score" HeaderText="仰臥起坐成績" SortExpression="sit_ups_score"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="push_ups" HeaderText="俯地挺身" SortExpression="push_ups" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="push_ups_score" HeaderText="俯地挺身成績" SortExpression="push_ups_score"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="run" HeaderText="三千公尺" SortExpression="run"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="run_score" HeaderText="三千公尺成績" SortExpression="run_score"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="status" HeaderText="鑑測結果" SortExpression="status"  ItemStyle-Wrap="false"/>
            </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="QueryResult" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="type" Type="String"  DefaultValue="name"/>
                <asp:Parameter Name="value" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
</ContentTemplate>
</ajaxToolkit:TabPanel>

<ajaxToolkit:TabPanel runat="server" ID="TabPanel3" HeaderText="單位代碼查詢">
<ContentTemplate>
<div><asp:Label ID="Label3" runat="server" Text="請輸入單位代碼 :"></asp:Label><asp:TextBox ID="unit_code" runat="server"></asp:TextBox>
<asp:Button ID="search3" runat="server"  CssClass="search" onclick="search3_Click"/></div>
    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView_OnRowDataBound" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle"
             HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
            Width="1000px" PageSize = "25" AllowPaging="true"
            DataSourceID="SqlDataSource3" AllowSorting="True" OnSorting="GridView3_Sorting" OnPageIndexChanged="GridView3_PageIndexChanged">
            <Columns>
                <asp:BoundField DataField="date" HeaderText="鑑測日期" SortExpression="date"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="id" HeaderText="身份證" SortExpression="id"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="age" HeaderText="年齡" SortExpression="age"  ItemStyle-Wrap="false"/>        
                <asp:BoundField DataField="sit_ups" HeaderText="仰臥起坐" SortExpression="sit_ups"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="sit_ups_score" HeaderText="仰臥起坐成績" SortExpression="sit_ups_score"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="push_ups" HeaderText="俯地挺身" SortExpression="push_ups"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="push_ups_score" HeaderText="俯地挺身成績" SortExpression="push_ups_score"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="run" HeaderText="三千公尺" SortExpression="run" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="run_score" HeaderText="三千公尺成績" SortExpression="run_score"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="status" HeaderText="鑑測結果" SortExpression="status"  ItemStyle-Wrap="false"/>
            </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="QueryResult" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="type" Type="String"  DefaultValue="unit_code"/>
                <asp:Parameter Name="value" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
</ContentTemplate>
</ajaxToolkit:TabPanel>

<ajaxToolkit:TabPanel runat="server" ID="TabPanel4" HeaderText="鑑測時間查詢">
<ContentTemplate>
<div><asp:Label ID="Label4" runat="server" Text="請輸入鑑測時間 :"></asp:Label><asp:TextBox ID="date" runat="server"></asp:TextBox>
<asp:Button ID="search4" runat="server"  CssClass="search" onclick="search4_Click" /></div>
    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView_OnRowDataBound" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle"
             HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
            Width="1000px" PageSize = "25"  AllowPaging ="true"
            DataSourceID="SqlDataSource4" AllowSorting="True" OnSorting="GridView4_Sorting" OnPageIndexChanged="GridView4_PageIndexChanged" DataKeyNames="id">
            <Columns>
                <asp:BoundField DataField="date" HeaderText="鑑測日期" SortExpression="date" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="id" HeaderText="身份證" SortExpression="id"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="age" HeaderText="年齡" SortExpression="age"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="sit_ups" HeaderText="仰臥起坐" SortExpression="sit_ups"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="sit_ups_score" HeaderText="仰臥起坐成績" SortExpression="sit_ups_score"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="push_ups" HeaderText="俯地挺身" SortExpression="push_ups"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="push_ups_score" HeaderText="俯地挺身成績" SortExpression="push_ups_score"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="run" HeaderText="三千公尺" SortExpression="run"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="run_score" HeaderText="三千公尺成績" SortExpression="run_score"  ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="status" HeaderText="鑑測結果" SortExpression="status"  ItemStyle-Wrap="false"/>
            </Columns>
    </asp:GridView>
     <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="QueryResult" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="type" Type="String"  DefaultValue="date"/>
                <asp:Parameter Name="value" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
</ContentTemplate>
</ajaxToolkit:TabPanel>

</ajaxToolkit:TabContainer>
</asp:Content>
