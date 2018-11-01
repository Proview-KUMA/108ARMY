<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PresentResultMag.aspx.cs" Inherits="PresentResultMag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<ajaxToolkit:TabContainer id="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
<ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="現場報名合格成績上傳">
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
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="PresentResultMag" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="status" Type="String" DefaultValue="102" />
            </SelectParameters>
        </asp:SqlDataSource>
 <hr />
 <asp:Button runat="server" ID="upload" Text="上傳合格成績" OnClick="upload_OnClick" OnClientClick="return confirm('確認上傳合格成績?');"/>
</ContentTemplate>
</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="現場報名不合格成績上傳">
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
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="PresentResultMag" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="status" Type="String" DefaultValue="103" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:Button runat="server" ID="falseUpload" Text="上傳不合格成績" OnClick="falseUpload_OnClick" OnClientClick="return confirm('確認上傳不合格成績?');" />
</ContentTemplate>
</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="現場報名免測成績上傳">
<ContentTemplate>
<asp:GridView ID="GridView4" runat="server" AllowPaging="True" Width="1000px" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle" PageSize = "25"
             HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
            AutoGenerateColumns="False" DataSourceID="SqlDataSource4"
            onpageindexchanged="GridView4_PageIndexChanged"
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
 <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="PresentResultMag" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="status" Type="String" DefaultValue="104" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:Button runat="server" ID="None" Text="上傳免測成績" OnClick="NoneUpload_OnClick" OnClientClick="return confirm('確認上傳免測成績?');" />
</ContentTemplate>
</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="現場報名補測成績上傳">
<ContentTemplate>
    <div><asp:CheckBox ID="CheckBox2" runat="server" OnCheckedChanged="ToChangeCheckedDay_OnClick" AutoPostBack="true" Text="全選30天原地補測"/></div>
<asp:GridView ID="GridView5" runat="server" Width="1000px" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle"
             HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
            AutoGenerateColumns="False" DataSourceID="SqlDataSource5"
            onpageindexchanged="GridView5_PageIndexChanged"
            OnRowDataBound="GridView_OnRowDataBound"
            >
            <Columns>
                <asp:BoundField DataField="date" HeaderText="鑑測日期" SortExpression="date" ItemStyle-Wrap="false" />        
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
                 <asp:TemplateField ItemStyle-HorizontalAlign="Left"> 
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
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="PresentResultMag" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="status" Type="String" DefaultValue="105" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:Button runat="server" ID="ReAction" Text="上傳補測成績" OnClick="ReActionUpload_OnClick" OnClientClick="return confirm('確認上傳補測成績?');"/>
</ContentTemplate>
</ajaxToolkit:TabPanel>

<ajaxToolkit:TabPanel ID="TabPanel5" runat="server" HeaderText="現場報名未檢錄處理">
<ContentTemplate>
<asp:GridView ID="GridView1" runat="server" AllowPaging="True" Width="1000px" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle" PageSize = "25"
             HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
            AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
            onpageindexchanged="GridView1_PageIndexChanged"
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
 <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="PresentResultMag" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="status" Type="String" DefaultValue="999" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:Button runat="server" ID="btn_Del999" Text="刪除現報未檢錄" OnClick="btn_Del999_Click" OnClientClick="return confirm('確認刪除前一日及之前「現報未檢錄」的資料?');" Font-Size="18px" ForeColor="Red" BackColor="#99FF99" />
    <asp:Label ID="Label2" runat="server" Text="(為避免誤刪當日資料，僅能刪除前一日及之前的資料)" Font-Size="18px" ForeColor="Red"></asp:Label>
</ContentTemplate>
</ajaxToolkit:TabPanel>

</ajaxToolkit:TabContainer>


</asp:Content>

