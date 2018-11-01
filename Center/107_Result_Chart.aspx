<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="107_Result_Chart.aspx.cs" Inherits="_107_Result_Chart" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdf_CheckDate" runat="server" />
    <script type="text/javascript">
        //檢查日期1
        function CheckDate1() {
            var date;
            date = document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel4_txb_OtherItem_Date").value;
            if (dateValidationCheck(date))
                //document.getElementById("ctl00_ContentPlaceHolder1_hdf_CheckDate").value = "";
                return true;
            else {
                //document.getElementById("ctl00_ContentPlaceHolder1_hdf_CheckDate").value = "0";
                alert("請輸入 YYYY/MM/DD 日期格式");
            }
        }
        //檢查日期2
        function CheckDate2() {
            var date;
            date = document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel5_txb_ItemPassRate_Date").value;
            if (dateValidationCheck(date))
                //document.getElementById("ctl00_ContentPlaceHolder1_hdf_CheckDate").value = "";
                return true;
            else {
                //document.getElementById("ctl00_ContentPlaceHolder1_hdf_CheckDate").value = "0";
                alert("請輸入 YYYY/MM/DD 日期格式");
            }
        }
        //檢查日期格式
        function dateValidationCheck(str) {
            var re = new RegExp("^([0-9]{4})[./]{1}([0-9]{1,2})[./]{1}([0-9]{1,2})$");
            var strDataValue;
            var infoValidation = true;
            if ((strDataValue = re.exec(str)) != null) {
                var i;
                i = parseFloat(strDataValue[1]);
                if (i <= 0 || i > 9999) { /*年*/
                    infoValidation = false;
                }
                i = parseFloat(strDataValue[2]);
                if (i <= 0 || i > 12) { /*月*/
                    infoValidation = false;
                }
                i = parseFloat(strDataValue[3]);
                if (i <= 0 || i > 31) { /*日*/
                    infoValidation = false;
                }
            } else {
                infoValidation = false;
            }
            //if (!infoValidation) {
            //    alert("請輸入 YYYY/MM/DD 日期格式");
            //    return false;
            //}
            return infoValidation;
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">

        <%--第一頁--%>
        <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="未來七日網路報進人數">
            <ContentTemplate>

                <div>
                    <asp:Chart ID="chart_7DayPlayerCount" runat="server" BackColor="#D3DFF0" Width="1010px" Height="700px" BorderColor="26, 59, 105" Palette="BrightPastel" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2">
                        <%--圖表的標題文字--%>
                        <Titles>
                            <asp:Title ShadowColor="32,0,0,0" Font="Times New Roman, 20pt, style=Bold" ShadowOffset="3" Text="未來七日網路報進人數" ForeColor="26,59,105"></asp:Title>
                        </Titles>
                        <%--圖表的說明內容--%>
                        <Legends>
                            <asp:Legend LegendStyle="Column" IsTextAutoFit="false" DockedToChartArea="LineChartArea" Docking="Right" Name="Line" BackColor="Transparent" Font="Times New Roman, 20pt, style=Bold"></asp:Legend>
                        </Legends>
                        <%--圖表的外觀--%>
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <%--圖表的資料數據--%>
                        <Series>
                            <asp:Series IsValueShownAsLabel="true" ChartArea="LineChartArea" Name="Line" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105" LabelFormat="" Font="Times New Roman, 20pt, style=Bold" BorderWidth="8" Color="Green"></asp:Series>
                        </Series>
                        <%--圖表的顯示區域--%>
                        <ChartAreas>
                            <asp:ChartArea Name="LineChartArea" BorderColor="64,64,64,64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64,165,191,228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="false" WallWidth="0" IsClustered="false" />
                                <AxisY LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle" TitleFont="Times New Roman, 20pt, style=Bold">
                                    <LabelStyle Font="Times New Roman, 20pt, style=Bold" ForeColor="Red" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisY>
                                <AxisX LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle" TitleFont="Times New Roman, 20pt, style=Bold">
                                    <LabelStyle Font="Times New Roman, 20pt, style=Bold" IsStaggered="true" ForeColor="Blue" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                    <%--<table width="1010px" height="100px"   style="border:5px #000000 solid; rules="all" cellpadding='5';>
                        <tr align="center">
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="日期" Font-Size="18px" Font-Bold="true"></asp:Label></td>
                            <td><asp:Label ID="Label2" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td><asp:Label ID="Label3" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td><asp:Label ID="Label4" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td><asp:Label ID="Label5" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td><asp:Label ID="Label6" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td><asp:Label ID="Label7" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td><asp:Label ID="Label8" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                        </tr>
                        <tr align="center">
                            <td><asp:Label ID="Label9" runat="server" Text="人數" Font-Size="18px" Font-Bold="true"></asp:Label></td>
                            <td><asp:Label ID="Label10" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td><asp:Label ID="Label11" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td><asp:Label ID="Label12" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td><asp:Label ID="Label13" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td><asp:Label ID="Label14" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td><asp:Label ID="Label15" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td><asp:Label ID="Label16" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                        </tr>
                    </table>--%>
                </div>

            </ContentTemplate>
        </ajaxToolkit:TabPanel>

        <%--第二頁--%>
        <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="過去七日網路報進到測率">
            <ContentTemplate>
                <div>
                    <asp:Chart ID="chart_7DayInTest" runat="server" BackColor="#D3DFF0" Width="1010px" Height="700px" BorderColor="26, 59, 105" Palette="BrightPastel" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2">
                        <%--圖表的標題文字--%>
                        <Titles>
                            <asp:Title ShadowColor="32,0,0,0" Font="Times New Roman, 20pt, style=Bold" ShadowOffset="3" Text="過去七日網路報進到測率" ForeColor="26,59,105"></asp:Title>
                        </Titles>
                        <%--圖表的說明內容--%>
                        <Legends>
                            <asp:Legend LegendStyle="Column" IsTextAutoFit="false" Name="Column" BackColor="Transparent" Font="Times New Roman, 20pt, style=Bold"></asp:Legend>
                        </Legends>
                        <%--圖表的外觀--%>
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <%--圖表的資料數據--%>
                        <Series>
                            <asp:Series IsValueShownAsLabel="true" ChartArea="ColumnChartArea" Name="Column" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105" LabelFormat="" Font="Times New Roman, 16pt, style=Bold"></asp:Series>
                            <%--<asp:Series IsValueShownAsLabel="true" ChartArea="ColumnChartArea" Name="Column2" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105" LabelFormat="" Font="Times New Roman, 16pt, style=Bold"></asp:Series>--%>
                        </Series>
                        <%--圖表的顯示區域--%>
                        <ChartAreas>
                            <asp:ChartArea Name="ColumnChartArea" BorderColor="64,64,64,64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64,165,191,228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="false" WallWidth="0" IsClustered="false" />
                                <AxisY LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle" TitleFont="Times New Roman, 20pt, style=Bold">
                                    <LabelStyle Font="Times New Roman, 20pt, style=Bold" Format="" ForeColor="Red" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisY>
                                <AxisX LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle" Interval="1" TitleFont="Times New Roman, 20pt, style=Bold">
                                    <LabelStyle Font="Times New Roman, 20pt, style=Bold" IsStaggered="true" ForeColor="Blue" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>

                    <table width="1010px" height="100px" style="border:5px #000000 solid" rules="all" cellpadding="5">
                        <tr align="center">
                            <td>
                                <asp:Label ID="Label17" runat="server" Text="日期" Font-Size="18px" Font-Bold="true"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_date1" runat="server" Text="Label" Font-Size="18px" ForeColor="Black"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_date2" runat="server" Text="Label" Font-Size="18px" ForeColor="Black"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_date3" runat="server" Text="Label" Font-Size="18px" ForeColor="Black"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_date4" runat="server" Text="Label" Font-Size="18px" ForeColor="Black"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_date5" runat="server" Text="Label" Font-Size="18px" ForeColor="Black"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_date6" runat="server" Text="Label" Font-Size="18px" ForeColor="Black"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_date7" runat="server" Text="Label" Font-Size="18px" ForeColor="Black"></asp:Label></td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Label ID="Label25" runat="server" Text="應到" Font-Size="18px" Font-Bold="true"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_AllTest1" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_AllTest2" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_AllTest3" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_AllTest4" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_AllTest5" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_AllTest6" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_AllTest7" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="實到" Font-Size="18px" Font-Bold="true"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_InTest1" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_InTest2" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_InTest3" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_InTest4" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_InTest5" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_InTest6" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_InTest7" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Label ID="Label33" runat="server" Text="到測率(%)" Font-Size="18px" Font-Bold="true"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_Rate1" runat="server" Text="Label" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_Rate2" runat="server" Text="Label" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_Rate3" runat="server" Text="Label" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_Rate4" runat="server" Text="Label" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_Rate5" runat="server" Text="Label" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_Rate6" runat="server" Text="Label" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Test_Rate7" runat="server" Text="Label" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>

        <%--第三頁--%>
        <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="過去七日多元選項比例">
            <ContentTemplate>
                <div>
                    <asp:Chart ID="chart_7DaySeleteOther" runat="server" BackColor="#D3DFF0" Width="1010px" Height="700px" BorderColor="26, 59, 105" Palette="BrightPastel" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2">
                        <%--圖表的標題文字--%>
                        <Titles>
                            <asp:Title ShadowColor="32,0,0,0" Font="Times New Roman, 20pt, style=Bold" ShadowOffset="3" Text="過去七日多元選項比例" ForeColor="26,59,105"></asp:Title>
                        </Titles>
                        <%--圖表的說明內容--%>
                        <Legends>
                            <asp:Legend LegendStyle="Column" IsTextAutoFit="false" Name="Column" BackColor="Transparent" Font="Times New Roman, 20pt, style=Bold"></asp:Legend>
                        </Legends>
                        <%--圖表的外觀--%>
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <%--圖表的資料數據--%>
                        <Series>
                            <asp:Series IsValueShownAsLabel="true" ChartArea="ColumnChartArea" Name="Column" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105" LabelFormat="" Font="Times New Roman, 16pt, style=Bold"></asp:Series>
                            <%--<asp:Series IsValueShownAsLabel="true" ChartArea="ColumnChartArea" Name="Column2" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105" LabelFormat="" Font="Times New Roman, 16pt, style=Bold"></asp:Series>--%>
                        </Series>
                        <%--圖表的顯示區域--%>
                        <ChartAreas>
                            <asp:ChartArea Name="ColumnChartArea" BorderColor="64,64,64,64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64,165,191,228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="false" WallWidth="0" IsClustered="false" />
                                <AxisY LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle" TitleFont="Times New Roman, 20pt, style=Bold">
                                    <LabelStyle Font="Times New Roman, 20pt, style=Bold" Format="" ForeColor="Red" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisY>
                                <AxisX LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle" Interval="1" TitleFont="Times New Roman, 20pt, style=Bold">
                                    <LabelStyle Font="Times New Roman, 20pt, style=Bold" IsStaggered="true" ForeColor="Blue" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>

                    <table width="1010px" height="100px" style="border:5px #000000 solid" rules="all" cellpadding="5">
                        <tr align="center">
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="日期" Font-Size="18px" Font-Bold="true"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_date1" runat="server" Text="Label" Font-Size="18px" ForeColor="Black"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_date2" runat="server" Text="Label" Font-Size="18px" ForeColor="Black"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_date3" runat="server" Text="Label" Font-Size="18px" ForeColor="Black"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_date4" runat="server" Text="Label" Font-Size="18px" ForeColor="Black"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_date5" runat="server" Text="Label" Font-Size="18px" ForeColor="Black"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_date6" runat="server" Text="Label" Font-Size="18px" ForeColor="Black"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_date7" runat="server" Text="Label" Font-Size="18px" ForeColor="Black"></asp:Label></td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Label ID="Label41" runat="server" Text="總人數" Font-Size="18px" Font-Bold="true"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_All1" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_All2" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_All3" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_All4" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_All5" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_All6" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_All7" runat="server" Text="Label" Font-Size="18px" ForeColor="Blue"></asp:Label></td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Label ID="Label49" runat="server" Text="多元人數" Font-Size="18px" Font-Bold="true"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_Other1" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_Other2" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_Other3" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_Other4" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_Other5" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_Other6" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_Other7" runat="server" Text="Label" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Label ID="Label57" runat="server" Text="多元比例(%)" Font-Size="18px" Font-Bold="true"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_Rate1" runat="server" Text="Label" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_Rate2" runat="server" Text="Label" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_Rate3" runat="server" Text="Label" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_Rate4" runat="server" Text="Label" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_Rate5" runat="server" Text="Label" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_Rate6" runat="server" Text="Label" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_Item_Rate7" runat="server" Text="Label" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>

        <%--第四頁--%>
        <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="單日多元選項項目使用比例">
            <ContentTemplate>
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lab_inqtitle1" runat="server" Text="請選取查詢日期：" Font-Size="16px" ForeColor="Black"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txb_OtherItem_Date" runat="server"  Font-Size="16px"></asp:TextBox>
                            </td>
                            <td>&nbsp<asp:Button ID="btn_Inq_OtherItem" runat="server" Text="查詢" Font-Size="16px" BackColor="#ff99ff" ForeColor="Blue" Font-Bold="true" OnClientClick="CheckDate1()" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="日期格式：YYYY/MM/DD" Font-Size="14px" ForeColor="Red" Font-Bold="True"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                    <asp:Chart ID="chart_SeleteOtherItem" runat="server" BackColor="#D3DFF0" Width="1010px" Height="700px" BorderColor="26, 59, 105" Palette="BrightPastel" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2">
                        <Titles>
                            <asp:Title ShadowColor="32,0,0,0" Font="Times New Roman, 20pt, style=Bold" ShadowOffset="3" Text="單日多元選項項目使用比例" ForeColor="26,59,105"></asp:Title>
                        </Titles>
                        <Legends>
                            <asp:Legend LegendStyle="Column" IsTextAutoFit="false" DockedToChartArea="PieChartArea" Docking="Left" Name="Pie" BackColor="Transparent" Font="Times New Roman, 16pt, style=Bold"></asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <Series>
                            <asp:Series IsValueShownAsLabel="true" ChartArea="PieChartArea" Name="Pie" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105" LabelFormat="" Font="Times New Roman, 24pt, style=Bold"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="PieChartArea" BorderColor="64,64,64,64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64,165,191,228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="false" WallWidth="0" IsClustered="false" />
                                <AxisY LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle" TitleFont="Times New Roman, 20pt, style=Bold">
                                    <LabelStyle Font="Times New Roman, 20pt, style=Bold" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisY>
                                <AxisX LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle" TitleFont="Times New Roman, 20pt, style=Bold">
                                    <LabelStyle Font="Times New Roman, 20pt, style=Bold" IsStaggered="true" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                    <table width="1010px" height="100px" style="border:5px #000000 solid" rules="all" cellpadding="5">
                        <tr align="center">
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="項目" Font-Size="18px" Font-Bold="true"></asp:Label></td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="基本項目(3km跑步)" Font-Size="18px" ForeColor="#0066FF" Font-Bold="true"></asp:Label></td>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="800公尺游走" Font-Size="18px" ForeColor="#FF9900" Font-Bold="true"></asp:Label></td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="5公里健走" Font-Size="18px" ForeColor="#CC3300" Font-Bold="true"></asp:Label></td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="5分鐘跳繩" Font-Size="18px" ForeColor="#006699" Font-Bold="true"></asp:Label></td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="人數" Font-Size="18px" Font-Bold="true"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_OtherItem_count1" runat="server" Text="0" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_OtherItem_count2" runat="server" Text="0" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_OtherItem_count3" runat="server" Text="0" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_OtherItem_count4" runat="server" Text="0" Font-Size="18px" ForeColor="Green"></asp:Label></td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Label ID="Label23" runat="server" Text="百分比(%)" Font-Size="18px" Font-Bold="true"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_OtherItem_rate1" runat="server" Text="0" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_OtherItem_rate2" runat="server" Text="0" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_OtherItem_rate3" runat="server" Text="0" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                            <td>
                                <asp:Label ID="lab_OtherItem_rate4" runat="server" Text="0" Font-Size="18px" ForeColor="Red"></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>

        <%--第五頁--%>
        <ajaxToolkit:TabPanel ID="TabPanel5" runat="server" HeaderText="單日單項合格率">
            <ContentTemplate>
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="請選取查詢日期：" Font-Size="16px" ForeColor="Black"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txb_ItemPassRate_Date" runat="server" Font-Size="16px"></asp:TextBox>
                            </td>
                            <td>&nbsp<asp:Button ID="btn_Inq_ItemPassRate" runat="server" Text="查詢" Font-Size="16px" BackColor="#ff99ff" ForeColor="Blue" Font-Bold="true" OnClientClick="CheckDate2()" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                 <asp:Label ID="Label5" runat="server" Text="日期格式：YYYY/MM/DD" Font-Size="14px" ForeColor="Red" Font-Bold="True"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                    <%--正常三項--%>
                    <asp:Chart ID="chart_sit_ups" runat="server" BackColor="#D3DFF0" Width="320px" Height="300px" BorderColor="26, 59, 105" Palette="BrightPastel" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2">
                        <Titles>
                            <asp:Title ShadowColor="32,0,0,0" Font="Times New Roman, 14pt, style=Bold" ShadowOffset="3" Text="仰臥起坐" ForeColor="26,59,105"></asp:Title>
                        </Titles>
                        <Legends>
                            <asp:Legend LegendStyle="Table" IsTextAutoFit="false" DockedToChartArea="PieChartArea" Docking="Right" Name="Pie" BackColor="Transparent" Font="Times New Roman, 7pt" ></asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <Series>
                            <asp:Series IsValueShownAsLabel="true" ChartArea="PieChartArea" Name="Pie" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105" LabelFormat="" Font="Times New Roman, 16pt, style=Bold"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="PieChartArea" BorderColor="64,64,64,64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64,165,191,228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="false" WallWidth="0" IsClustered="false" />
                                <AxisY LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle" >
                                    <LabelStyle Font="Times New Roman, 10pt, style=Bold" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisY>
                                <AxisX LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle">
                                    <LabelStyle Font="Times New Roman, 10pt, style=Bold" IsStaggered="true" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                    <asp:Chart ID="chart_push_ups" runat="server" BackColor="#D3DFF0" Width="320px" Height="300px" BorderColor="26, 59, 105" Palette="BrightPastel" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2">
                        <Titles>
                            <asp:Title ShadowColor="32,0,0,0" Font="Times New Roman, 14pt, style=Bold" ShadowOffset="3" Text="俯地挺身" ForeColor="26,59,105"></asp:Title>
                        </Titles>
                        <Legends>
                            <asp:Legend LegendStyle="Column" IsTextAutoFit="false" DockedToChartArea="PieChartArea" Docking="Right" Name="Pie" BackColor="Transparent" Font="Times New Roman, 7pt"></asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <Series>
                            <asp:Series IsValueShownAsLabel="true" ChartArea="PieChartArea" Name="Pie" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105" LabelFormat="" Font="Times New Roman, 16pt, style=Bold"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="PieChartArea" BorderColor="64,64,64,64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64,165,191,228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="false" WallWidth="0" IsClustered="false" />
                                <AxisY LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle">
                                    <LabelStyle Font="Times New Roman, 10pt, style=Bold" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisY>
                                <AxisX LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle">
                                    <LabelStyle Font="Times New Roman, 10pt, style=Bold" IsStaggered="true" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                    <asp:Chart ID="chart_run" runat="server" BackColor="#D3DFF0" Width="320px" Height="300px" BorderColor="26, 59, 105" Palette="BrightPastel" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2">
                        <Titles>
                            <asp:Title ShadowColor="32,0,0,0" Font="Times New Roman, 14pt, style=Bold" ShadowOffset="3" Text="三千公尺跑步" ForeColor="26,59,105"></asp:Title>
                        </Titles>
                        <Legends>
                            <asp:Legend LegendStyle="Column" IsTextAutoFit="false" DockedToChartArea="PieChartArea" Docking="Right" Name="Pie" BackColor="Transparent" Font="Times New Roman, 7pt"></asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <Series>
                            <asp:Series IsValueShownAsLabel="true" ChartArea="PieChartArea" Name="Pie" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105" LabelFormat="" Font="Times New Roman, 16pt, style=Bold"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="PieChartArea" BorderColor="64,64,64,64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64,165,191,228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="false" WallWidth="0" IsClustered="false" />
                                <AxisY LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle">
                                    <LabelStyle Font="Times New Roman, 10pt, style=Bold" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisY>
                                <AxisX LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle">
                                    <LabelStyle Font="Times New Roman, 10pt, style=Bold" IsStaggered="true" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </div>
                <%--替代項目--%>
                <div>
                    <asp:Chart ID="chart_800m_swin" runat="server" BackColor="#D3DFF0" Width="320px" Height="300px" BorderColor="26, 59, 105" Palette="BrightPastel" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2">
                        <Titles>
                            <asp:Title ShadowColor="32,0,0,0" Font="Times New Roman, 14pt, style=Bold" ShadowOffset="3" Text="800公尺游走" ForeColor="26,59,105"></asp:Title>
                        </Titles>
                        <Legends>
                            <asp:Legend LegendStyle="Column" IsTextAutoFit="false" DockedToChartArea="PieChartArea" Docking="Right" Name="Pie" BackColor="Transparent" Font="Times New Roman, 7pt"></asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <Series>
                            <asp:Series IsValueShownAsLabel="true" ChartArea="PieChartArea" Name="Pie" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105" LabelFormat="" Font="Times New Roman, 16pt, style=Bold"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="PieChartArea" BorderColor="64,64,64,64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64,165,191,228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="false" WallWidth="0" IsClustered="false" />
                                <AxisY LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle">
                                    <LabelStyle Font="Times New Roman, 10pt, style=Bold" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisY>
                                <AxisX LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle">
                                    <LabelStyle Font="Times New Roman, 10pt, style=Bold" IsStaggered="true" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                    <asp:Chart ID="chart_5km_walk" runat="server" BackColor="#D3DFF0" Width="320px" Height="300px" BorderColor="26, 59, 105" Palette="BrightPastel" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2">
                        <Titles>
                            <asp:Title ShadowColor="32,0,0,0" Font="Times New Roman, 14pt, style=Bold" ShadowOffset="3" Text="5公里健走" ForeColor="26,59,105"></asp:Title>
                        </Titles>
                        <Legends>
                            <asp:Legend LegendStyle="Column" IsTextAutoFit="false" DockedToChartArea="PieChartArea" Docking="Right" Name="Pie" BackColor="Transparent" Font="Times New Roman, 7pt"></asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <Series>
                            <asp:Series IsValueShownAsLabel="true" ChartArea="PieChartArea" Name="Pie" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105" LabelFormat="" Font="Times New Roman, 16pt, style=Bold"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="PieChartArea" BorderColor="64,64,64,64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64,165,191,228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="false" WallWidth="0" IsClustered="false" />
                                <AxisY LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle">
                                    <LabelStyle Font="Times New Roman, 10pt, style=Bold" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisY>
                                <AxisX LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle">
                                    <LabelStyle Font="Times New Roman, 10pt, style=Bold" IsStaggered="true" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                    <asp:Chart ID="chart_5min_jump" runat="server" BackColor="#D3DFF0" Width="320px" Height="300px" BorderColor="26, 59, 105" Palette="BrightPastel" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2">
                        <Titles>
                            <asp:Title ShadowColor="32,0,0,0" Font="Times New Roman, 14pt, style=Bold" ShadowOffset="3" Text="5分鐘跳繩" ForeColor="26,59,105"></asp:Title>
                        </Titles>
                        <Legends>
                            <asp:Legend LegendStyle="Column" IsTextAutoFit="false" DockedToChartArea="PieChartArea" Docking="Right" Name="Pie" BackColor="Transparent" Font="Times New Roman, 7pt"></asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <Series>
                            <asp:Series IsValueShownAsLabel="true" ChartArea="PieChartArea" Name="Pie" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105" LabelFormat="" Font="Times New Roman, 16pt, style=Bold"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="PieChartArea" BorderColor="64,64,64,64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64,165,191,228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="false" WallWidth="0" IsClustered="false" />
                                <AxisY LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle">
                                    <LabelStyle Font="Times New Roman, 10pt, style=Bold" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisY>
                                <AxisX LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle">
                                    <LabelStyle Font="Times New Roman, 10pt, style=Bold" IsStaggered="true" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </div>
                <div>
                    <asp:Chart ID="chart_up_Pole" runat="server" BackColor="#D3DFF0" Width="320px" Height="300px" BorderColor="26, 59, 105" Palette="BrightPastel" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2">
                        <Titles>
                            <asp:Title ShadowColor="32,0,0,0" Font="Times New Roman, 14pt, style=Bold" ShadowOffset="3" Text="單槓引體向上" ForeColor="26,59,105"></asp:Title>
                        </Titles>
                        <Legends>
                            <asp:Legend LegendStyle="Column" IsTextAutoFit="false" DockedToChartArea="PieChartArea" Docking="Right" Name="Pie" BackColor="Transparent" Font="Times New Roman, 7pt"></asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <Series>
                            <asp:Series IsValueShownAsLabel="true" ChartArea="PieChartArea" Name="Pie" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105" LabelFormat="" Font="Times New Roman, 16pt, style=Bold"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="PieChartArea" BorderColor="64,64,64,64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64,165,191,228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="false" WallWidth="0" IsClustered="false" />
                                <AxisY LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle">
                                    <LabelStyle Font="Times New Roman, 10pt, style=Bold" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisY>
                                <AxisX LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle">
                                    <LabelStyle Font="Times New Roman, 10pt, style=Bold" IsStaggered="true" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                    <asp:Chart ID="chart_set_Pole" runat="server" BackColor="#D3DFF0" Width="320px" Height="300px" BorderColor="26, 59, 105" Palette="BrightPastel" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2">
                        <Titles>
                            <asp:Title ShadowColor="32,0,0,0" Font="Times New Roman, 14pt, style=Bold" ShadowOffset="3" Text="女性屈臂懸垂" ForeColor="26,59,105"></asp:Title>
                        </Titles>
                        <Legends>
                            <asp:Legend LegendStyle="Column" IsTextAutoFit="false" DockedToChartArea="PieChartArea" Docking="Right" Name="Pie" BackColor="Transparent" Font="Times New Roman, 7pt"></asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                        <Series>
                            <asp:Series IsValueShownAsLabel="true" ChartArea="PieChartArea" Name="Pie" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105" LabelFormat="" Font="Times New Roman, 16pt, style=Bold"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="PieChartArea" BorderColor="64,64,64,64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64,165,191,228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="false" WallWidth="0" IsClustered="false" />
                                <AxisY LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle">
                                    <LabelStyle Font="Times New Roman, 10pt, style=Bold" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisY>
                                <AxisX LineColor="64,64,64,64" IsLabelAutoFit="false" ArrowStyle="Triangle">
                                    <LabelStyle Font="Times New Roman, 10pt, style=Bold" IsStaggered="true" />
                                    <MajorGrid LineColor="64,64,64,64" />
                                </AxisX>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>

                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>


    </ajaxToolkit:TabContainer>
</asp:Content>

