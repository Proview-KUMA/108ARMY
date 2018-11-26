<%@ Page Title="成績試算" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="108_ScoreTrial.aspx.cs" Inherits="_108_ScoreTrial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
    <style type="text/css">
        #DIV1 {
            float: left;
        width: 50%;
        height: 600px;
        background-color:khaki;
        }

        #DIV2 {
            margin-left: 50%;
            height: 600px;
            background-color:aquamarine
        }
    </style>
    <script type="text/jscript">
        function gender_chenge() {
            var slt_gender = document.getElementById("slt_gender");
            var slt_age = document.getElementById("slt_age");
            var div_M = document.getElementById("div_item_M");
            var div_F = document.getElementById("div_item_F");
            var div_inq = document.getElementById("div_inq");
            var gender = slt_gender.value.toString();
            var age = slt_age.value.toString();
            if (gender == "0" || age == "0") {
                div_M.style.display = "none";
                div_F.style.display = "none";
                div_inq.style.display = "none";
            }
            else {
                switch (gender) {
                    case "M":
                        div_M.style.display = ""
                        div_F.style.display = "none";
                        div_inq.style.display = "";
                        break;
                    case "F":
                        div_F.style.display = "";
                        div_M.style.display = "none";
                        div_inq.style.display = "";
                        break;
                    default:
                        div_M.style.display = "none";
                        div_F.style.display = "none";
                        div_inq.style.display = "none";
                        break;
                }
            }
            item_change();
        }
        function item_change() {
            var slt_gender = document.getElementById("slt_gender");
            var gender = slt_gender.value.toString();
            var input_min = document.getElementById("input_min");
            var input_sec_count = document.getElementById("input_sec_count");
            var b_min = document.getElementById("b_min");
            var b_sec_count = document.getElementById("b_sec_count");
            input_min.innerText = "";
            input_sec_count.innerText = "";
            var item;
            if (gender == "M")
                item = document.getElementById("slt_item_M").value.toString();
            else
                item = document.getElementById("slt_item_F").value.toString();
            if (item == "run" || item == "F" || item == "G" || item == "J") {//時間
                input_min.style.display = "";
                input_sec_count.style.display = "";
                b_min.style.display = "";
                b_sec_count.style.display = "";
                b_min.innerText = "分";
                input_sec_count.maxLength = 2;
                b_sec_count.innerText = "秒";
            }
            else {//次數
                input_min.style.display = "none";
                input_sec_count.style.display = "";
                b_min.style.display = "none";
                b_sec_count.style.display = "";
                b_sec_count.innerText = "次";
                input_sec_count.maxLength = 4;
                b_min.innerText = "";
            }
        }
        function senddata() {
            var hf_gender = document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_hf_gender");
            var hf_age = document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_hf_age");
            var hf_item = document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_hf_item");
            var hf_grade = document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_hf_grade");
            var hf_itemName = document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_hf_itemName");
            var slt_gender = document.getElementById("slt_gender");
            var slt_age = document.getElementById("slt_age");
            hf_gender.value = slt_gender.value.toString()
            hf_age.value = slt_age.value.toString();
            if (hf_gender.value != '0' && hf_age.value != '0') {
                if (hf_gender.value == 'M') {
                    var slt_item_M = document.getElementById("slt_item_M")
                    hf_item.value = slt_item_M.value.toString();
                    hf_itemName.value = slt_item_M.options[slt_item_M.selectedIndex].text;
                }
                else {
                    var slt_item_F = document.getElementById("slt_item_F")
                    hf_item.value = slt_item_F.value.toString();
                    hf_itemName.value = slt_item_F.options[slt_item_F.selectedIndex].text;
                }
                var input_min = document.getElementById("input_min");
                var input_sec_count = document.getElementById("input_sec_count");
                if (input_min.value == '' && input_sec_count.value == '') {
                    alert("請輸入成績!!")
                    return false;
                }
                else {
                    var grade = 0;//成績
                    var checkNum = /^[0-9]+$/i;   //<--正規式的內容 ^[0-9]+$ 文字必須為 0到9
                    if (input_min.value != '') {
                        if (input_min.value.search(checkNum)) {
                            alert("成績欄位請輸入數字!!")
                            return false;
                        }
                        else {
                            grade = input_min.value * 60;
                        }
                    }
                    if (input_sec_count.value != '') {
                        if (input_sec_count.value.search(checkNum)) {
                            alert("成績欄位請輸入數字!!")
                            return false;
                        }
                        else {
                            grade += parseInt(input_sec_count.value);
                        }
                    }
                    hf_grade.value = grade.toString();
                    //alert("總成績：" + grade.toString());
                }
            }
            else {
                alert("請選取性別及年齡!!")
                return false;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
        <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="成績試算">
            <ContentTemplate>
                <div id="DIV1">
                    <div id="div_gender">
                         <asp:Label ID="Label12" runat="server" Text="請輸入資料：" Font-Size="X-Large" Font-Bold="true" ForeColor="Green"></asp:Label>
                        <br />
                    <asp:Label ID="Label1" runat="server" Text="性別：" Font-Size="Larger"></asp:Label>
                    <select id="slt_gender" name="slt_gender" onchange="gender_chenge()" >
                        <option value="0" style="font-size:larger">請選取性別</option>
                        <option value="M" style="font-size:larger;color:blue">男性</option>
                        <option value="F" style="font-size:larger;color:red">女生</option>
                    </select>
                </div>
                <br />
                <div id="div_age">
                     <asp:Label ID="Label4" runat="server" Text="年齡：" Font-Size="Larger"></asp:Label>
                    <select id="slt_age" name="slt_age" onchange="gender_chenge()" >
                        <option value="0" style="font-size:larger">請選取年齡</option>
                        <option value="18" style="font-size:larger"">18</option>
                        <option value="19" style="font-size:larger"">19</option>
                        <option value="20" style="font-size:larger"">20</option>
                        <option value="21" style="font-size:larger"">21</option>
                        <option value="22" style="font-size:larger"">22</option>
                        <option value="23" style="font-size:larger"">23</option>
                        <option value="24" style="font-size:larger"">24</option>
                        <option value="25" style="font-size:larger"">25</option>
                        <option value="26" style="font-size:larger"">26</option>
                        <option value="27" style="font-size:larger"">27</option>
                        <option value="28" style="font-size:larger"">28</option>
                        <option value="29" style="font-size:larger"">29</option>
                        <option value="30" style="font-size:larger"">30</option>
                        <option value="31" style="font-size:larger"">31</option>
                        <option value="32" style="font-size:larger"">32</option>
                        <option value="33" style="font-size:larger"">33</option>
                        <option value="34" style="font-size:larger"">34</option>
                        <option value="35" style="font-size:larger"">35</option>
                        <option value="36" style="font-size:larger"">36</option>
                        <option value="37" style="font-size:larger"">37</option>
                        <option value="38" style="font-size:larger"">38</option>
                        <option value="39" style="font-size:larger"">39</option>
                        <option value="40" style="font-size:larger"">40</option>
                        <option value="41" style="font-size:larger"">41</option>
                        <option value="42" style="font-size:larger"">42</option>
                        <option value="43" style="font-size:larger"">43</option>
                        <option value="44" style="font-size:larger"">44</option>
                        <option value="45" style="font-size:larger"">45</option>
                        <option value="46" style="font-size:larger"">46</option>
                        <option value="47" style="font-size:larger"">47</option>
                        <option value="48" style="font-size:larger"">48</option>
                        <option value="49" style="font-size:larger"">49</option>
                        <option value="50" style="font-size:larger"">50</option>
                        <option value="51" style="font-size:larger"">51</option>
                        <option value="52" style="font-size:larger"">52</option>
                        <option value="53" style="font-size:larger"">53</option>
                        <option value="54" style="font-size:larger"">54</option>
                        <option value="55" style="font-size:larger"">55</option>
                        <option value="56" style="font-size:larger"">56</option>
                        <option value="57" style="font-size:larger"">57</option>
                        <option value="58" style="font-size:larger"">58</option>
                        <option value="59" style="font-size:larger"">59</option>
                    </select>
                </div>
                <br />
                <div id="div_item_M" style="display:none">
                    <asp:Label ID="Label2" runat="server" Text="請選取項目(男)：" Font-Size="Larger"></asp:Label>
                    <select id="slt_item_M" name="slt_item_M" onchange="item_change()">
                        <option value="sit_ups" style="font-size:larger">仰臥起坐</option>
                        <option value="push_ups" style="font-size:larger">俯地挺身</option>
                        <option value="run" style="font-size:larger">3000M跑步</option>
                        <option value="F" style="font-size:larger;color:green">800M游走</option>
                        <option value="G" style="font-size:larger;color:green">5km健走</option>
                        <option value="H" style="font-size:larger;color:green">5分鐘跳繩</option>
                        <option value="I" style="font-size:larger;color:blue">引體向上</option>
                    </select>
                </div>
                <div id="div_item_F" style="display:none">
                    <asp:Label ID="Label3" runat="server" Text="請選取項目(女)：" Font-Size="Larger"></asp:Label>
                    <select id="slt_item_F" name="slt_item_F" onchange="item_change()">
                        <option value="sit_ups" style="font-size:larger">仰臥起坐</option>
                        <option value="push_ups" style="font-size:larger">俯地挺身</option>
                        <option value="run" style="font-size:larger">3000M跑步</option>
                        <option value="F" style="font-size:larger;color:green">800M游走</option>
                        <option value="G" style="font-size:larger;color:green">5km健走</option>
                        <option value="H" style="font-size:larger;color:green">5分鐘跳繩</option>
                        <option value="J" style="font-size:larger;color:red">屈臂懸垂</option>
                    </select>
                </div>
                <br />
                <div id="div_inq" style="display:none">
                    <asp:Label ID="Label9" runat="server" Text="請輸入成績：" Font-Size="Larger"></asp:Label>
                    <input type="text" id="input_min" maxlength="2" style="width:50px" />&nbsp<b id="b_min"></b>
                    &nbsp&nbsp
                    <input type="text" id="input_sec_count" maxlength="2" style="width:50px" />&nbsp<b id="b_sec_count"></b>
                     <br />
                     <br />
                    <asp:Button ID="btn_Inq" runat="server" Text="查  詢" OnClientClick="return senddata();" OnClick="btn_Inq_Click" Width="100px" BackColor="#00FFCC" Font-Size="Larger" Font-Bold="true" />
                    <asp:HiddenField ID="hf_gender" runat="server" />
                    <asp:HiddenField ID="hf_age" runat="server" />
                    <asp:HiddenField ID="hf_item" runat="server" />
                    <asp:HiddenField ID="hf_itemName" runat="server" />
                    <asp:HiddenField ID="hf_grade" runat="server" />
                </div>
                <br />
                <br />
                </div>
                <div id="DIV2">
                    <div id="div_inq_result">
                    <asp:Label ID="Label5" runat="server" Text="查詢結果：" Font-Size="X-Large" Font-Bold="true" ForeColor="Blue"></asp:Label>
                    <br />
                    <table style="border:3px #0000FF solid;" cellpadding="10" border='1'>
                        <tr>
                            <td><asp:Label ID="Label6" runat="server" Text="性別：" Font-Size="Larger" Font-Bold="True"></asp:Label></td>
                            <td><asp:Label ID="lab_gender" runat="server" Text="" Font-Size="Larger"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="Label7" runat="server" Text="年齡：" Font-Size="Larger" Font-Bold="True" ></asp:Label></td>
                            <td><asp:Label ID="lab_age" runat="server" Text="" Font-Size="Larger" ForeColor="#996633"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="Label8" runat="server" Text="項目名稱：" Font-Size="Larger" Font-Bold="True"></asp:Label></td>
                            <td><asp:Label ID="lab_item" runat="server" Text="" Font-Size="Larger" ForeColor="#996633" ></asp:Label></td>
                        </tr>
                        <tr>
                             <td><asp:Label ID="Label10" runat="server" Text="成績：" Font-Size="Larger"  Font-Bold="True"></asp:Label></td>
                            <td><asp:Label ID="lab_grade" runat="server" Text="" Font-Size="Larger" ForeColor="#996633"></asp:Label></td>
                        </tr>
                        <tr>                      
                            <td><asp:Label ID="Label11" runat="server" Text="分數：" Font-Size="Larger" Font-Bold="True"></asp:Label></td>
                            <td><asp:Label ID="lab_score" runat="server" Text="" Font-Size="Larger"></asp:Label></td>
                        </tr>
                    </table>
 
                </div>
                </div>
                <div style="clear:both;"></div><!--這是用來清除上方的浮動效果-->
                
                

            </ContentTemplate>
        </ajaxToolkit:TabPanel>






    </ajaxToolkit:TabContainer>


</asp:Content>

