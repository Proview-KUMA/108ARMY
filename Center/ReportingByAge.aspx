<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportingByAge.aspx.cs" Inherits="ReportingByAge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="account" runat="server" />
    <div>
        <table>
            <tr>
                <td style="border: solid 1px black">
                    <asp:Menu ID="Menu1" runat="server" OnMenuItemClick="Menu1_MenuItemClick">
                    <Items>
                            <asp:MenuItem Text="陸軍" Value="Army"></asp:MenuItem>
                            <asp:MenuItem Text="海軍" Value="Navy"></asp:MenuItem>
                            <asp:MenuItem Text="空軍" Value="AirForce"></asp:MenuItem>
                            <asp:MenuItem Text="憲兵" Value="MP"></asp:MenuItem>
                            <asp:MenuItem Text="聯勤" Value="BackUp"></asp:MenuItem>
                            <asp:MenuItem Text="後備" Value="AfterService"></asp:MenuItem>
                        </Items>
                    </asp:Menu>
                </td>
                <td rowspan="4" style="border: solid 1px black;text-align:center;vertical-align:top">
                <div style="margin-top:1px;top:1px;background-color:#FDF5E6">
                <table id="resultTable" style="margin-top:1px;padding-top:1px">
                        <thead>
                            <tr>
                                <th colspan="38" align="center">
                                    國軍體能測驗年齡成績分析統計表
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2" rowspan="2" style="width: 50px;border:solid 1px black">
                                </td>
                                <td colspan="3">
                                    19-22歲
                                </td>
                                <td colspan="3">
                                    23-26歲
                                </td>
                                <td colspan="3">
                                    27-30歲
                                </td>
                                <td colspan="3">
                                    31-34歲
                                </td>
                                <td colspan="3">
                                    35-38歲
                                </td>
                                <td colspan="3">
                                    39-42歲
                                </td>
                                <td colspan="3">
                                    43-46歲
                                </td>
                                <td colspan="3">
                                    47-50歲
                                </td>
                                <td colspan="3">
                                    51-54歲
                                </td>
                                <td colspan="3">
                                    55-58歲
                                </td>
                                <td colspan="3">
                                    59歲
                                </td>
                                <td colspan="3">小計</td>
                            </tr>
                            <tr>
                            <td>人數</td>
                            <td>合格數</td>
                            <td>合格率</td>
                            <td>人數</td>
                            <td>合格數</td>
                            <td>合格率</td>
                            <td>人數</td>
                            <td>合格數</td>
                            <td>合格率</td>
                            <td>人數</td>
                            <td>合格數</td>
                            <td>合格率</td>
                            <td>人數</td>
                            <td>合格數</td>
                            <td>合格率</td>
                            <td>人數</td>
                            <td>合格數</td>
                            <td>合格率</td>
                            <td>人數</td>
                            <td>合格數</td>
                            <td>合格率</td>
                            <td>人數</td>
                            <td>合格數</td>
                            <td>合格率</td>
                            <td>人數</td>
                            <td>合格數</td>
                            <td>合格率</td>
                            <td>人數</td>
                            <td>合格數</td>
                            <td>合格率</td>
                            <td>人數</td>
                            <td>合格數</td>
                            <td>合格率</td>
                            <td>人數</td>
                            <td>合格數</td>
                            <td>合格率</td>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
                    
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px black">
                    <asp:GridView ID="GridView1" runat="server">
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px black">
                    <asp:Button ID="Button1" runat="server" Text="重選" OnClick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px black">
                    <table>
                        <tr>
                            <td>
                                開始日期
                            </td>
                            <td>
                                <input type="text" id="date_start" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                結束日期
                            </td>
                            <td>
                                <input type="text" id="date_stop" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="button" value="成績統計" id="calculate" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
