<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Re_106Score.aspx.cs" Inherits="Update_106_Score" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>更新鑑測中心106年成績</title>
</head>
<body>
    <form id="form1" runat="server">
    <div aria-busy="True">
        <p style="font-size:x-large;color:blue">更新鑑測中心106年成績</p>
        <hr color="#FF0000" size="5"/>
        <br />
        連線字串：<asp:TextBox ID="txb_ConnString" runat="server" Width="676px">Data Source=192.168.0.6;Initial Catalog=Center;User ID=myap;Password=proview</asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="連線測試" Font-Bold="True" Font-Size="Medium" BackColor="Silver" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 復原資料表名稱：<asp:TextBox ID="txb_ReBak_tb_Name" runat="server" >Result_Ch106</asp:TextBox>
    
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_reduction" runat="server"  Text="復原備份資料" Font-Bold="True" Font-Size="Small" BackColor="#009933" ForeColor="#99CC00" OnClick="btn_reduction_Click" Width="101px" />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        <asp:Label ID="lab_con_Msg" runat="server" ForeColor="Red"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lab_ReBak_Msg" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <br />
        <hr color="#00FF00" size="5"/><br />
        <br />
        起始日期：<asp:TextBox ID="txb_StartTime" runat="server">2018/1/1</asp:TextBox>
        <br />
        <br />
        結束日期：<asp:TextBox ID="txb_EndTime" runat="server">2018/1/18</asp:TextBox>
    
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 備份資料表名稱：<asp:TextBox ID="txb_Bak_tb_Name" runat="server" >Result_Ch107</asp:TextBox>
    
        &nbsp;&nbsp;&nbsp;
        <asp:Label ID="lab_Bak_Msg" runat="server" ForeColor="Red"></asp:Label>
    
        <br />
        <br />
        <asp:Button ID="btn_Search" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Black" OnClick="btn_Search_Click" Text="讀取已上傳資料" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_Copy_Table" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#6600FF" OnClick="btn_Copy_Table_Click" Text="備份已上傳資料" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_RE_status" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF6600" OnClick="btn_RE_status_Click" Text="更新鑑測狀態" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_Re_Score" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#CC0066" OnClick="btn_Re_Score_Click" Text="成績重新計算" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_Search_1xx" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#669900"  Text="讀取未上傳資料" OnClick="btn_Search_1xx_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_105_to_103" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Maroon"  Text="105補測-&gt;103不合格" OnClick="btn_105_to_103_Click"   />
        <br />
        <hr color="#FFFF00" size="2"/>
    
        <asp:Button ID="btn_no_upload" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#006699"  Text="未上傳成績筆數查詢" OnClick="btn_no_upload_Click" Width="195px" BackColor="#FF99FF" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:Label ID="lab3" runat="server" Text="合格：" ForeColor="Black"></asp:Label>
        <asp:Label ID="lab_102" runat="server" ForeColor="Blue"></asp:Label>
        &nbsp; <asp:Label ID="lab4" runat="server" Text="不合格：" ForeColor="Black"></asp:Label>
        <asp:Label ID="lab_103" runat="server" ForeColor="Blue"></asp:Label>
        &nbsp; <asp:Label ID="lab5" runat="server" Text="補測：" ForeColor="Black"></asp:Label>
        <asp:Label ID="lab_105" runat="server" ForeColor="Blue"></asp:Label>
        &nbsp; <asp:Label ID="lab6" runat="server" Text="合格(現報)：" ForeColor="Black"></asp:Label>
        <asp:Label ID="lab_102_666" runat="server" ForeColor="#009933"></asp:Label>
        &nbsp; <asp:Label ID="lab7" runat="server" Text="不合格(現報)：" ForeColor="Black"></asp:Label>
        <asp:Label ID="lab_103_666" runat="server" ForeColor="#009933"></asp:Label>
        &nbsp; <asp:Label ID="lab8" runat="server" Text="補測(現報)：" ForeColor="Black"></asp:Label>
        <asp:Label ID="lab_105_666" runat="server" ForeColor="#009933"></asp:Label>
        &nbsp; <asp:Label ID="lab9" runat="server" Text="總數：" ForeColor="Black"></asp:Label>
        <asp:Label ID="lab_total" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lab2" runat="server" Text="分批上傳資料筆數設定：" ForeColor="#6600FF" Font-Bold="True" Font-Size="14pt"></asp:Label>
        <asp:TextBox ID="txb_P_count" runat="server" Width="76px">100</asp:TextBox>
    
        <br />
        <asp:Button ID="btn_Upload_102" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#009933"  Text="合格成績上傳"  Width="202px" OnClick="btn_Upload_102_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_Upload_103" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Red"  Text="不合格成績上傳" OnClick="btn_Upload_103_Click"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_Upload_105" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#CC9900"  Text="補測成績上傳" OnClick="btn_Upload_105_Click"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <asp:Button ID="btn_Upload_102_666" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#009933"  Text="合格成績上傳(現報)"  Width="202px" OnClick="btn_Upload_102_666_Click"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_Upload_103_666" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Red"  Text="不合格成績上傳(現報)"  Width="200px" OnClick="btn_Upload_103_666_Click"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_Upload_105_666" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#CC9900"  Text="補測成績上傳(現報)"  Width="174px" OnClick="btn_Upload_105_666_Click"  />
        <br />
        <br />
        <asp:Label ID="lab1" runat="server" Text="資料狀態：" ForeColor="Black"></asp:Label>
        &nbsp;<asp:Label ID="lab_Count" runat="server" ForeColor="Red"></asp:Label>
    
        <br />
        <asp:GridView ID="dgv_Result" runat="server">
        </asp:GridView>
        <br />
    
    </div>
    </form>
</body>
</html>
