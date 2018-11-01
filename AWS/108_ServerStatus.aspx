<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="108_ServerStatus.aspx.cs" Inherits="_108_ServerStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
    <script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme" Height="400px" Width="1020px" ActiveTabIndex="0">
        <ajaxToolkit:TabPanel runat="server" ID="TabPanel1" HeaderText="伺服器狀態">
            <ContentTemplate>
                <div style="margin-top: 5px; margin-bottom: 5px; vertical-align: bottom; height: 22px;">
                    <asp:Label ID="Label5" runat="server" Text="一、連線狀態" Font-Size="XX-Large" Font-Bold="True" Font-Overline="False" ForeColor="#6600FF" Font-Underline="True"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="伺服器ip位置：" Font-Size="Larger"></asp:Label>
                    <asp:Label ID="lab_SvIp" runat="server" Font-Size="Larger"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="預設閘道ip位置：" Font-Size="Larger"></asp:Label>
                    <asp:Label ID="lab_GatewayIp" runat="server" Font-Size="Larger"></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btn_PingIp" runat="server" Text="檢查預設閘道連線狀態" OnClick="btn_PingIp_Click" Font-Size="Larger" BackColor="#66FFFF" Width="340px"/>
                    <asp:Label ID="Label3" runat="server" Text="檢查結果：" Font-Size="Larger"></asp:Label>
                    <asp:Label ID="lab_PingGatewayResult" runat="server" ForeColor="Red" Font-Size="Larger"></asp:Label>

                    <br />
                    <br />
                    <asp:Button ID="btn_PingFec" runat="server" Text="檢查人事資料伺服器連線狀態" OnClick="btn_PingFec_Click" Font-Size="Larger" BackColor="#66FF33" Width="340px" />
                    <asp:Label ID="Label4" runat="server" Text="檢查結果：" Font-Size="Larger"></asp:Label>
                    <asp:Label ID="lab_PingFecResult" runat="server" ForeColor="Red" Font-Size="Larger"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label6" runat="server" Text="二、系統時間" Font-Size="XX-Large" Font-Bold="True" Font-Overline="False" ForeColor="#FF9900" Font-Underline="True"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="伺服器時間：" Font-Size="Larger"></asp:Label>
                    <asp:Label ID="lab_SvTime" runat="server" Font-Size="Larger"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label9" runat="server" Text="資料庫時間：" Font-Size="Larger"></asp:Label>
                    <asp:Label ID="lab_DBTime" runat="server" Font-Size="Larger"></asp:Label>
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>

    </ajaxToolkit:TabContainer>
</asp:Content>

