﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewResult.aspx.cs" Inherits="ViewResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
    <script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
    <script type="text/javascript" src="Script/RankCode.ashx"></script>
    <script type="text/javascript" src="Script/Common.js"></script>
    <script type="text/javascript" language="javascript">
        function confirm_user() {
            if (confirm("確認結束今日鑑測工作，並結算剩餘人員之成績?\r\n (測試人員資料將自動刪除)") == true) {
                var btn = document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel6_btn_CalculationResult");
                btn.innerText = "結算中，請稍候…";
                return true;
            }         
            else {
                return false;
            }
                
        }
        //接收子視窗回傳值要執行的方法。
        function outside(count) {
            if (count == "Err") {
                alert("結算成績失敗");
            } else {
                alert("成功結算成績：「" + count + "」筆。");
            }

        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
        <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="合格">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server" CssClass="GridViewStyle"
                    Width="1000px" PageSize="15" AllowPaging="True"
                    AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                    OnPageIndexChanged="GridView1_PageIndexChanged"
                    OnRowDataBound="GridView_OnRowDataBound">
                    <Columns>
                        <asp:BoundField DataField="clothesNum" HeaderText="背號" SortExpression="clothesNum" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="id" HeaderText="身分證號" SortExpression="id" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="sit_ups" HeaderText="仰臥起坐"
                            SortExpression="sit_ups" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="sit_ups_score" HeaderText="仰臥起坐成績"
                            SortExpression="sit_ups_score" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="push_ups" HeaderText="俯地起身"
                            SortExpression="push_ups" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="push_ups_score" HeaderText="俯地起身成績"
                            SortExpression="push_ups_score" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="run" HeaderText="三千公尺" ItemStyle-Wrap="false"
                            SortExpression="run" />
                        <asp:BoundField DataField="run_score" HeaderText="三千公尺成績" ItemStyle-Wrap="false"
                            SortExpression="run_score" />
                        <asp:BoundField DataField="sid" HeaderText="sid" Visible="false" ItemStyle-Wrap="false" />
                    </Columns>
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="Ex108_ViewStatus"
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter Name="type" Type="String" DefaultValue="102,202" />
                        <asp:Parameter Name="value" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
                <asp:Button ID="Button1" runat="server" Text="更新" OnClick="Button1_OnClick" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="不合格">
            <ContentTemplate>
                <asp:GridView ID="GridView2" runat="server" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle"
                    HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
                    Width="1000px" PageSize="15" AllowPaging="true"
                    AutoGenerateColumns="False" DataSourceID="SqlDataSource2"
                    OnPageIndexChanged="GridView2_PageIndexChanged"
                    OnRowDataBound="GridView_OnRowDataBound">
                    <Columns>
                        <asp:BoundField DataField="clothesNum" HeaderText="背號" SortExpression="clothesNum" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="id" HeaderText="身分證號" SortExpression="id" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="sit_ups" HeaderText="仰臥起坐"
                            SortExpression="sit_ups" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="sit_ups_score" HeaderText="仰臥起坐成績"
                            SortExpression="sit_ups_score" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="push_ups" HeaderText="俯地起身"
                            SortExpression="push_ups" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="push_ups_score" HeaderText="俯地起身成績"
                            SortExpression="push_ups_score" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="run" HeaderText="三千公尺" ItemStyle-Wrap="false"
                            SortExpression="run" />
                        <asp:BoundField DataField="run_score" HeaderText="三千公尺成績" ItemStyle-Wrap="false"
                            SortExpression="run_score" />
                        <asp:BoundField DataField="sid" HeaderText="sid" Visible="false" ItemStyle-Wrap="false" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                    ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="Ex108_ViewStatus"
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter Name="type" Type="String" DefaultValue="103,203" />
                        <asp:Parameter Name="value" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
                <asp:Button ID="Button2" runat="server" Text="更新" OnClick="Button2_OnClick" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="免測">
            <ContentTemplate>
                <asp:GridView ID="GridView3" runat="server" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle"
                    HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
                    Width="1000px" PageSize="15" AllowPaging="true"
                    AutoGenerateColumns="False" DataSourceID="SqlDataSource3"
                    OnPageIndexChanged="GridView3_PageIndexChanged"
                    OnRowDataBound="GridView_OnRowDataBound">
                    <Columns>

                        <asp:BoundField DataField="id" HeaderText="身分證號" SortExpression="id" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="sit_ups" HeaderText="仰臥起坐"
                            SortExpression="sit_ups" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="sit_ups_score" HeaderText="仰臥起坐成績"
                            SortExpression="sit_ups_score" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="push_ups" HeaderText="俯地起身"
                            SortExpression="push_ups" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="push_ups_score" HeaderText="俯地起身成績"
                            SortExpression="push_ups_score" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="run" HeaderText="三千公尺" ItemStyle-Wrap="false"
                            SortExpression="run" />
                        <asp:BoundField DataField="run_score" HeaderText="三千公尺成績" ItemStyle-Wrap="false"
                            SortExpression="run_score" />
                        <asp:BoundField DataField="sid" HeaderText="sid" Visible="false" ItemStyle-Wrap="false" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server"
                    ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="Ex108_ViewStatus"
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter Name="type" Type="String" DefaultValue="104,204" />
                        <asp:Parameter Name="value" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
                <asp:Button ID="Button3" runat="server" Text="更新" OnClick="Button3_OnClick" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="補測">
            <ContentTemplate>
                <asp:GridView ID="GridView4" runat="server" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle"
                    HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
                    Width="1000px" PageSize="15" AllowPaging="true"
                    AutoGenerateColumns="False" DataSourceID="SqlDataSource4"
                    OnPageIndexChanged="GridView4_PageIndexChanged"
                    OnRowDataBound="GridView_OnRowDataBound">
                    <Columns>
                        <asp:BoundField DataField="clothesNum" HeaderText="背號" SortExpression="clothesNum" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="id" HeaderText="身分證號" SortExpression="id" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="sit_ups" HeaderText="仰臥起坐"
                            SortExpression="sit_ups" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="sit_ups_score" HeaderText="仰臥起坐成績"
                            SortExpression="sit_ups_score" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="push_ups" HeaderText="俯地起身"
                            SortExpression="push_ups" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="push_ups_score" HeaderText="俯地起身成績"
                            SortExpression="push_ups_score" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="run" HeaderText="三千公尺" ItemStyle-Wrap="false"
                            SortExpression="run" />
                        <asp:BoundField DataField="run_score" HeaderText="三千公尺成績" ItemStyle-Wrap="false"
                            SortExpression="run_score" />
                        <asp:BoundField DataField="sid" HeaderText="sid" Visible="false" ItemStyle-Wrap="false" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server"
                    ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="Ex108_ViewStatus"
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter Name="type" Type="String" DefaultValue="105,205" />
                        <asp:Parameter Name="value" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
                <asp:Button ID="Button4" runat="server" Text="更新" OnClick="Button4_OnClick" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="TabPanel5" runat="server" HeaderText="未檢錄">
            <ContentTemplate>
                <asp:GridView ID="GridView5" runat="server" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle"
                    HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
                    Width="1000px" PageSize="15" AllowPaging="true"
                    AutoGenerateColumns="False" DataSourceID="SqlDataSource5"
                    OnPageIndexChanged="GridView5_PageIndexChanged"
                    OnRowDataBound="GridView_OnRowDataBound">
                    <Columns>

                        <asp:BoundField DataField="id" HeaderText="身分證號" SortExpression="id" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="sit_ups" HeaderText="仰臥起坐"
                            SortExpression="sit_ups" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="sit_ups_score" HeaderText="仰臥起坐成績"
                            SortExpression="sit_ups_score" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="push_ups" HeaderText="俯地起身"
                            SortExpression="push_ups" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="push_ups_score" HeaderText="俯地起身成績"
                            SortExpression="push_ups_score" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="run" HeaderText="三千公尺" ItemStyle-Wrap="false"
                            SortExpression="run" />
                        <asp:BoundField DataField="run_score" HeaderText="三千公尺成績" ItemStyle-Wrap="false"
                            SortExpression="run_score" />
                        <asp:BoundField DataField="sid" HeaderText="sid" Visible="false" ItemStyle-Wrap="false" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource5" runat="server"
                    ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="Ex108_ViewStatus"
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter Name="type" Type="String" DefaultValue="999" />
                        <asp:Parameter Name="value" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
                <asp:Button ID="Button5" runat="server" Text="更新" OnClick="Button5_OnClick" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="TabPanel6" runat="server" HeaderText="鑑測中">
            <ContentTemplate>
                <asp:GridView ID="GridView6" runat="server" AlternatingRowStyle-CssClass="AltRowStyle" CssClass="GridViewStyle" EditRowStyle-CssClass="EditRowStyle"
                    HeaderStyle-CssClass="HeaderStyle" PagerStyle-CssClass="PagerStyle" RowStyle-CssClass="RowStyle" SelectedRowStyle-CssClass="SelectedRowStyle"
                    Width="1000px" PageSize="15" AllowPaging="true"
                    AutoGenerateColumns="False" DataSourceID="SqlDataSource6"
                    OnPageIndexChanged="GridView6_PageIndexChanged"
                    OnRowDataBound="GridView_OnRowDataBound">
                    <Columns>
                        <asp:BoundField DataField="clothesNum" HeaderText="背號" SortExpression="clothesNum" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="id" HeaderText="身分證號" SortExpression="id" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="sit_ups" HeaderText="仰臥起坐"
                            SortExpression="sit_ups" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="sit_ups_score" HeaderText="仰臥起坐成績"
                            SortExpression="sit_ups_score" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="push_ups" HeaderText="俯地起身"
                            SortExpression="push_ups" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="push_ups_score" HeaderText="俯地起身成績"
                            SortExpression="push_ups_score" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="run" HeaderText="三千公尺" ItemStyle-Wrap="false"
                            SortExpression="run" />
                        <asp:BoundField DataField="run_score" HeaderText="三千公尺成績" ItemStyle-Wrap="false"
                            SortExpression="run_score" />
                        <asp:BoundField DataField="sid" HeaderText="sid" Visible="false" ItemStyle-Wrap="false" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource6" runat="server"
                    ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="Ex108_ViewStatus"
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter Name="type" Type="String" DefaultValue="001" />
                        <asp:Parameter Name="value" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
                <div>
                    <asp:Button ID="Button6" runat="server" Text="更新" OnClick="Button6_OnClick" />
                    <asp:Button ID="btn_CalculationResult" runat="server"  Text="結算今日剩餘人員成績" ForeColor="Red" Enabled="true" BackColor="LightPink"  OnClientClick="return confirm_user();" OnClick="btn_CalculationResult_Click"  Style="float: right" />
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>

