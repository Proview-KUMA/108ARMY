<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Race_ResultMag.aspx.cs" Inherits="Race_ResultMag" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
 <link id="Login_css" rel="stylesheet" href="MasterPage.css" type="text/css" />
    <title>成績上傳</title>
</head>
<body>
    <form id="form1" runat="server">
    <span>每次只可檢視50筆成績且上傳此50筆成績</span><br />
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<ajaxToolkit:TabContainer id="TabContainer1" runat="server" 
        CssClass="ajax__tab_yuitabview-theme" AutoPostBack="True" 
        onactivetabchanged="TabContainer1_ActiveTabChanged">
<ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="合格成績上傳">
    <HeaderTemplate>
        合格成績上傳
    </HeaderTemplate>
<ContentTemplate>
<asp:GridView ID="GridView3" runat="server" AllowPaging="false"
        CssClass="GridViewStyle" PageSize = "25"
            Width="1000px" AutoGenerateColumns="False" DataSourceID="SqlDataSource3"
            onpageindexchanged="GridView3_PageIndexChanged"
            OnRowDataBound="GridView_OnRowDataBound" onsorted="GridView3_Sorted" AllowSorting="True"
             >
            <AlternatingRowStyle CssClass="AltRowStyle" />
            <Columns>
                <asp:BoundField DataField="date" HeaderText="鑑測日期" SortExpression="date">
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="id" HeaderText="身分證號" SortExpression="id">
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name">
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="sit_ups" HeaderText="仰臥起坐"
                    SortExpression="sit_ups" >
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="sit_ups_score" HeaderText="仰臥起坐成績"
                    SortExpression="sit_ups_score" >
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="push_ups" HeaderText="俯地起身"
                    SortExpression="push_ups" >
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                    <asp:BoundField DataField="push_ups_score" HeaderText="俯地起身成績"
                    SortExpression="push_ups_score" >
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="run" HeaderText="三千公尺"
                    SortExpression="run" >
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                    <asp:BoundField DataField="run_score" HeaderText="三千公尺成績"
                    SortExpression="run_score" >
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="sid" HeaderText="sid" Visible="False" />
            </Columns>
            <EditRowStyle CssClass="EditRowStyle" />
            <HeaderStyle CssClass="HeaderStyle" />
            <PagerStyle CssClass="PagerStyle" />
            <RowStyle CssClass="RowStyle" />
            <SelectedRowStyle CssClass="SelectedRowStyle" />
        </asp:GridView>
 <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="Race_ResultMag" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter DefaultValue="102" Name="status" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
 <hr />
 <asp:Button runat="server" ID="upload" Text="上傳合格成績" OnClick="upload_OnClick" OnClientClick="return confirm('確認上傳合格成績?');" />
</ContentTemplate>
</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="不合格成績上傳">
<ContentTemplate>
<asp:GridView ID="GridView2" runat="server"  AllowPaging="false"  AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle" PageSize = "25"
             HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
            Width="1000px" AutoGenerateColumns="False" DataSourceID="SqlDataSource2"
            onpageindexchanged="GridView2_PageIndexChanged"
            OnRowDataBound="GridView_OnRowDataBound" onsorted="GridView2_Sorted" AllowSorting="true"
            >
            <Columns>
                <asp:BoundField DataField="date" HeaderText="鑑測日期" SortExpression="date" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="id" HeaderText="身分證號" SortExpression="id" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="sit_ups" HeaderText="仰臥起坐" ItemStyle-Wrap="false"
                    SortExpression="sit_ups" />
                <asp:BoundField DataField="sit_ups_score" HeaderText="仰臥起坐成績" ItemStyle-Wrap="false"
                    SortExpression="sit_ups_score" />
                <asp:BoundField DataField="push_ups" HeaderText="俯地起身" ItemStyle-Wrap="false"
                    SortExpression="push_ups" />
                    <asp:BoundField DataField="push_ups_score" HeaderText="俯地起身成績" ItemStyle-Wrap="false"
                    SortExpression="push_ups_score" />
                <asp:BoundField DataField="run" HeaderText="三千公尺" ItemStyle-Wrap="false"
                    SortExpression="run" />
                    <asp:BoundField DataField="run_score" HeaderText="三千公尺成績" ItemStyle-Wrap="false"
                    SortExpression="run_score" />
                <asp:BoundField DataField="status" HeaderText="狀態" ItemStyle-Wrap="false"
                    SortExpression="status" />    
                
            </Columns>
        </asp:GridView>
 <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="Race_ResultMag" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter DefaultValue="103" Name="status" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:Button runat="server" ID="falseUpload" Text="上傳不合格成績" OnClick="falseUpload_OnClick" OnClientClick="return confirm('確認上傳不合格成績?');" />
</ContentTemplate>
</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="免測成績上傳">
<ContentTemplate>
<asp:GridView ID="GridView4" runat="server"  AllowPaging="false" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle" PageSize = "25"
             HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
             Width="1000px" AutoGenerateColumns="False" DataSourceID="SqlDataSource4"
            onpageindexchanged="GridView4_PageIndexChanged"
            OnRowDataBound="GridView_OnRowDataBound" onsorted="GridView4_Sorted" AllowSorting="true"
            >
            <Columns>
                <asp:BoundField DataField="date" HeaderText="鑑測日期" SortExpression="date" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="id" HeaderText="身分證號" SortExpression="id" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="sit_ups" HeaderText="仰臥起坐" ItemStyle-Wrap="false"
                    SortExpression="sit_ups" />
                <asp:BoundField DataField="sit_ups_score" HeaderText="仰臥起坐成績" ItemStyle-Wrap="false"
                    SortExpression="sit_ups_score" />
                <asp:BoundField DataField="push_ups" HeaderText="俯地起身" ItemStyle-Wrap="false"
                    SortExpression="push_ups" />
                    <asp:BoundField DataField="push_ups_score" HeaderText="俯地起身成績" ItemStyle-Wrap="false"
                    SortExpression="push_ups_score" />
                <asp:BoundField DataField="run" HeaderText="三千公尺" ItemStyle-Wrap="false"
                    SortExpression="run" />
                    <asp:BoundField DataField="run_score" HeaderText="三千公尺成績" ItemStyle-Wrap="false"
                    SortExpression="run_score" />
                    <asp:BoundField DataField="status" HeaderText="狀態" ItemStyle-Wrap="false"
                    SortExpression="status" />  
                
            </Columns>
        </asp:GridView>
<asp:SqlDataSource ID="SqlDataSource4" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="Race_ResultMag" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter DefaultValue="104" Name="status" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:Button runat="server" ID="None" Text="上傳免測成績" OnClick="NoneUpload_OnClick" OnClientClick="return confirm('確認上傳免測成績?');"/>
</ContentTemplate>
</ajaxToolkit:TabPanel> 
</ajaxToolkit:TabContainer>
    </form>
</body>
</html>
