<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HumanResultMag.aspx.cs" Inherits="PresentResultMag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<ajaxToolkit:TabContainer id="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
<ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="人工鑑測合格成績上傳">
<ContentTemplate>
<asp:GridView ID="GridView3" runat="server" AllowPaging="True" Width="1000px" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle" PageSize = "25"
             HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
            AutoGenerateColumns="False" DataSourceID="SqlDataSource3"
            onpageindexchanged="GridView3_PageIndexChanged"
            OnRowDataBound="GridView_OnRowDataBound">
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
                <asp:BoundField DataField="sid" HeaderText="sid" Visible="false" />
            </Columns>
        </asp:GridView>
 <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="Ex104_HumanResultMag" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="status" Type="String" DefaultValue="102" />
            </SelectParameters>
        </asp:SqlDataSource>
 <hr />
 <asp:Button runat="server" ID="upload" Text="上傳合格成績" OnClick="upload_OnClick" OnClientClick="return confirm('確認上傳合格成績?');"/>
</ContentTemplate>
</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="人工鑑測不合格成績上傳">
<ContentTemplate>
<asp:GridView ID="GridView2" runat="server" AllowPaging="True" Width="1000px" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle" PageSize = "25"
             HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle" 
            AutoGenerateColumns="False" DataSourceID="SqlDataSource2"
            onpageindexchanged="GridView2_PageIndexChanged"
            OnRowDataBound="GridView_OnRowDataBound"
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
                
            </Columns>
        </asp:GridView>
 <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="Ex104_HumanResultMag" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="status" Type="String" DefaultValue="103" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:Button runat="server" ID="falseUpload" Text="上傳不合格成績" OnClick="falseUpload_OnClick" OnClientClick="return confirm('確認上傳不合格成績?');" />
</ContentTemplate>
</ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>


</asp:Content>

