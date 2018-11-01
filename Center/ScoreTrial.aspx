<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ScoreTrial.aspx.cs" Inherits="ScoreTrial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager> 
<ajaxToolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
<ajaxToolkit:TabPanel runat="server" ID="TabPanel1" HeaderText="成績試算">
<ContentTemplate>    
<span style="margin-left:3px">請選擇性別</span><asp:DropDownList ID="DropDownList1" runat="server"  AutoPostBack="True"
        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
    <asp:ListItem>男性</asp:ListItem>
    <asp:ListItem>女性</asp:ListItem>
</asp:DropDownList>
<span style="margin-left:8px">請選擇年齡</span>
    <asp:DropDownList ID="DropDownList2" runat="server">
    </asp:DropDownList>
<div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:Center %>" SelectCommand="Ex104_GetRepMentByScoreTrail" 
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter Name="Gender" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
    <table>
        <tbody>
            <tr>
                <td>
                    <span style="margin-left:3px">項目一</span><br>
                    <asp:DropDownList ID="DropDownList3" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="rep_title" 
                        DataValueField="sid" onprerender="DropDownList3_PreRender" 
                        onselectedindexchanged="DropDownList3_SelectedIndexChanged" 
                        AutoPostBack="True">
                    </asp:DropDownList>                    
                    <br />
                    <input type="text" id="situps_min" runat="server" onkeypress="return isNumberKey(event)" style="width:40px; margin-top:3px" />
                    <span>分</span><input type="text" id="situps_sec" runat="server" onkeypress="return isNumberKey(event)" style="width:40px" />
                    <span>秒</span><br />
                    <input type="text" id="situps_times" runat="server" onkeypress="return isNumberKey(event)" style="width:40px; margin-top:3px" />
                    <span>次</span><br />
                    
                    
                </td>
                
                <td>
                    <span style="margin-left:3px">項目二</span><br>
                    <asp:DropDownList ID="DropDownList4" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="rep_title" 
                        DataValueField="sid" onprerender="DropDownList4_PreRender" 
                        onselectedindexchanged="DropDownList4_SelectedIndexChanged" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <br />
                    <input type="text" id="pushups_min" runat="server" onkeypress="return isNumberKey(event)" style="width:40px; margin-top:3px"/>
                    <span>分</span><input type="text" id="pushups_sec" runat="server" onkeypress="return isNumberKey(event)"  style="width:40px"/>
                    <span>秒</span><br />
                    <input type="text" id="pushups_times" runat="server" onkeypress="return isNumberKey(event)" style="width:40px; margin-top:3px"/>
                    <span>次</span><br />
                    
                    
                </td>
                
                <td>
                    <span style="margin-left:3px">項目三</span><br />
                    <asp:DropDownList ID="DropDownList5" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="rep_title" 
                        DataValueField="sid" onprerender="DropDownList5_PreRender" 
                        onselectedindexchanged="DropDownList5_SelectedIndexChanged" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <br />
                    <input type="text" id="run_min" runat="server" onkeypress="return isNumberKey(event)" style="width:40px; margin-top:3px"/>
                    <span>分</span><input type="text" id="run_sec" runat="server" onkeypress="return isNumberKey(event)" style="width:40px"/>
                    <span>秒</span><br />
                    <input type="text" id="run_times" runat="server" onkeypress="return isNumberKey(event)" style="width:40px; margin-top:3px"/>
                    <span>次</span><br />
                    
                    
                </td>
            </tr>
            <tr>
                <td colspan="3"><asp:Button ID="Button1" runat="server" Text="成績試算" onclick="Button1_Click" /> </td>
            </tr>
            <tr>
                <td><span>項目一成績 : </span> <span id="situps_score" runat="server"></span></td>
                <td><span>項目二成績 : </span><span id="pushups_score" runat="server"></span></td>
                <td><span>項目三成績 : </span><span id="run_score" runat="server"></span></td>
            </tr>
            <tr>
                <td colspan="3"><span>總評 : </span><span id="status" runat="server"></span></td></td>
            </tr>
        </tbody>
    </table>
</div>
<div>
       
</div>

</ContentTemplate>
</ajaxToolkit:TabPanel>

</ajaxToolkit:TabContainer>
</asp:Content>

