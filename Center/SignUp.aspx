<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="SignUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>競賽報進</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-top:5px; margin-bottom:5px">報進名單範例 : <asp:HyperLink 
                                ID="HyperLink2" runat="server" ForeColor="Blue" 
                                NavigateUrl="~/TeamReserver.txt">請下載名單範例</asp:HyperLink>。</div>
                               
    <div>
        
        <asp:RadioButton ID="RB_Group" runat="server" Text="團體組" Checked="true"  AutoPostBack="true"
            oncheckedchanged="RB_Group_CheckedChanged"/>
        <asp:RadioButton ID="RB_Single" runat="server" Text="個人組" AutoPostBack="true" 
            oncheckedchanged="RB_Single_CheckedChanged"/>
              
    </div>
    <div>
       <asp:Label ID="Label1" runat="server" Text="檔案路徑 : "></asp:Label>          
        <asp:FileUpload ID="FileUpload1" runat="server" Width="407px"/>
    </div>
    <div>
        <asp:Button ID="Button2" runat="server" Text="檢查名單" onclick="Button2_Click" />
    </div>
    <div>
        <asp:Label ID="Label2" runat="server" Text="檢查結果 : "></asp:Label>  
    </div>
    <div>
        <asp:Label ID="Label3" runat="server" Text="------------------------------------------------------------------------"></asp:Label>  
    </div>
    <div>
        <asp:Label ID="Label4" runat="server" Text="資料筆數 : "></asp:Label><asp:Label ID="Label11" runat="server" Text="0"></asp:Label>
    </div>
    <div>
        <asp:Label ID="Label5" runat="server" Text="資料正確筆數 : "></asp:Label><asp:Label ID="Label10" runat="server" Text="0"></asp:Label>
    </div>
    <div>
        <asp:Label ID="Label6" runat="server" Text="格式錯誤筆數 : "></asp:Label><asp:Label ID="Label9" runat="server" Text="0"></asp:Label>
    </div>
    <div>
        <asp:Label ID="Label7" runat="server" Text="格式錯誤行號 : "></asp:Label>
    </div>
    <div>
        <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
    </div>
    <div>
        <asp:Button ID="Button3" runat="server" Text="匯入" Enabled="false" 
            onclick="Button3_Click" />
        <input id="input1" type="button" runat="server" value="檢視名單" onclick="window.open('DeSignup.aspx');" />
    </div>
    <div>
        <asp:Label ID="Label12" runat="server" Text="匯入結果 : "></asp:Label>  
    </div>
    <div>
        <asp:Label ID="Label13" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>

