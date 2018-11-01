<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ResultMag.aspx.cs" Inherits="ResultMag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
    <script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<ajaxToolkit:TabContainer id="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
<ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="合格成績上傳">
<ContentTemplate>
<asp:GridView ID="GridView3" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle" PageSize = "25"
             HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
            Width="1000px" AutoGenerateColumns="False" DataSourceID="SqlDataSource3"
            onpageindexchanged="GridView3_PageIndexChanged"
            OnRowDataBound="GridView_OnRowDataBound" onsorted="GridView3_Sorted" AllowSorting="true"
             >
            <Columns>
                <asp:BoundField DataField="date" HeaderText="鑑測日期" SortExpression="date"  ItemStyle-Wrap="false"/>
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
                <asp:BoundField DataField="sid" HeaderText="sid" Visible="False" />
            </Columns>
        </asp:GridView>
 <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="ResultMag" 
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
<asp:GridView ID="GridView2" runat="server" AllowPaging="True"  AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle" PageSize = "25"
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
                
            </Columns>
        </asp:GridView>
 <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="ResultMag" 
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
<asp:GridView ID="GridView4" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle" PageSize = "25"
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
                
            </Columns>
        </asp:GridView>
<asp:SqlDataSource ID="SqlDataSource4" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="ResultMag" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter DefaultValue="104" Name="status" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:Button runat="server" ID="None" Text="上傳免測成績" OnClick="NoneUpload_OnClick" OnClientClick="return confirm('確認上傳免測成績?');"/>
</ContentTemplate>
</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="補測成績上傳">
<ContentTemplate>
    <div><asp:CheckBox ID="CheckBox2" runat="server" OnCheckedChanged="ToChangeCheckedDay_OnClick" AutoPostBack="true" Text="全選30天原地補測"/></div>
<asp:GridView ID="GridView5" runat="server"
            AutoGenerateColumns="False" DataSourceID="SqlDataSource5" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle"
             HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
             Width="1000px" onpageindexchanged="GridView5_PageIndexChanged"
            OnRowDataBound="GridView_OnRowDataBound" AllowSorting="true" DataKeyNames="sid" onsorted="GridView5_Sorted">
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
                <asp:TemplateField SortExpression="sid" ItemStyle-HorizontalAlign="Left">
                 <HeaderTemplate>
                    <asp:Label ID="Label1" runat="server" Text="總評"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                <%--<asp:DropDownList runat="server" ID="ddl">
                <asp:ListItem Text="不合格" Value="103"></asp:ListItem>
                <asp:ListItem Text="請假" Value="106"></asp:ListItem>
                </asp:DropDownList>--%>
                <asp:RadioButtonList runat="server" ID="rb2">
                <asp:ListItem Text="不合格" Selected="True" Value="203"></asp:ListItem>
                <asp:ListItem Text="30天原地補測" Value="205"></asp:ListItem>
                </asp:RadioButtonList>       
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="sid" HeaderText="sid" SortExpression="sid" />
            </Columns>
        </asp:GridView>
 <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="ResultMag" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter DefaultValue="105" Name="status" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:Button runat="server" ID="ReAction" Text="上傳補測成績" OnClick="ReActionUpload_OnClick" OnClientClick="return confirm('確認上傳補測成績?');" />
</ContentTemplate>
</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel ID="TabPanel6" runat="server" HeaderText="未檢錄資料">
<ContentTemplate> 
<div><asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="ToChangeChecked_OnClick" AutoPostBack="true" Text="設定總評為請假"/></div>   
<asp:GridView ID="GridView1" runat="server" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle"
             HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
            AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="1000px" 
            onpageindexchanged="GridView1_PageIndexChanged" OnRowDataBound="GridView1_OnRowDataBound" onsorted="GridView1_Sorted" AllowSorting="true">
            <Columns>               
                <asp:BoundField DataField="date" HeaderText="鑑測日期" SortExpression="date" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="id" HeaderText="身分證號" SortExpression="id" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="sit_ups_score" HeaderText="仰臥起坐" ItemStyle-Wrap="false"
                    SortExpression="sit_ups_score" />
                <asp:BoundField DataField="push_ups_score" HeaderText="俯地起身" ItemStyle-Wrap="false"
                    SortExpression="push_ups_score" />
                <asp:BoundField DataField="run_score" HeaderText="三千公尺" ItemStyle-Wrap="false"
                    SortExpression="run_score" />
                <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                <HeaderTemplate>
                    <asp:Label runat="server" Text="總評"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                <%--<asp:DropDownList runat="server" ID="ddl">
                <asp:ListItem Text="不合格" Value="103"></asp:ListItem>
                <asp:ListItem Text="請假" Value="106"></asp:ListItem>
                </asp:DropDownList>--%>
                <asp:RadioButtonList runat="server" ID="rbl">
                <asp:ListItem Text="不合格" Selected="True" Value="203"></asp:ListItem>
                <asp:ListItem Text="請假" Selected="False" Value="206"></asp:ListItem>
                </asp:RadioButtonList>           
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="sid" HeaderText="sid" SortExpression="sid" />
            </Columns>
        </asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="ResultMagNoChcek" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter DefaultValue="999" Name="status" Type="String" />
                <asp:Parameter Name="date" Type="DateTime" />
            </SelectParameters>
        </asp:SqlDataSource>
  <br />
  <asp:Button ID="ToDo" runat="server" OnClick="ToDo_OnClick" Text="確定上傳" OnClientClick="return confirm('確認上傳未檢錄成績?');"/>
</ContentTemplate>
</ajaxToolkit:TabPanel>
 
</ajaxToolkit:TabContainer>


</asp:Content>

