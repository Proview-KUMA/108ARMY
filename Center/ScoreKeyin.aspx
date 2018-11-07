<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ScoreKeyin.aspx.cs" Inherits="ScoreKeyin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
    <link rel="Stylesheet" type="text/css" href="css/jquery-ui.css" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
    <script type="text/javascript" src="Script/jquery-1.3.2-vsdoc2.js"></script>
    <script type="text/javascript">
        if (typeof (JSON) == 'undefined') { //Fix IE JSON
            $.getScript('JS/json2.js');
        }
    </script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        //按下儲存成績時
        $(function () {
            $('#savescore').click(function () {
                var gender = $('#ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_player_gender').text();
                var birth = $('#ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_player_birth').text();
                var name = $('#ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_player_name').text();
                var unit_code = document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_unit_code").value;
                var rank_code = document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_rank_code").value;

                var situps_score;
                var pushups_score;
                var run_score;
                var sit_ups;
                var push_ups;
                var run;
                var op_id;
                var item1_e = document.getElementById("Item1");
                var item2_e = document.getElementById("Item2");
                var item3_e = document.getElementById("Item3");
                var item1_sid = item1_e.options[item1_e.selectedIndex].value;
                var item2_sid = item2_e.options[item2_e.selectedIndex].value;
                var item3_sid = item3_e.options[item3_e.selectedIndex].value;

                var memo = item1_sid + item2_sid + item3_sid;

                if (item1_e.options[item1_e.selectedIndex].text == item2_e.options[item2_e.selectedIndex].text || item3_e.options[item3_e.selectedIndex].text == item2_e.options[item2_e.selectedIndex].text
                   || item1_e.options[item1_e.selectedIndex].text == item3_e.options[item3_e.selectedIndex].text) {
                    alert('鑑測項目不得相同 , 請重新選取!');
                    return;
                }

                if (document.getElementById("situps_time").style.display == 'block') {
                    if ($('#situps_min').val() != '' && $('#situps_sec').val() != '') {
                        situps_score = $('#situps_min').val() + '分 ' + $('#situps_sec').val() + '秒';
                        sit_ups = $('#situps_min').val() * 60;
                        sit_ups += parseInt($('#situps_sec').val());
                        //alert(sit_ups);
                    }
                    else {
                        alert('請輸入分與秒');
                        return;
                    }
                }
                else {
                    if ($('#situps_times').val() != '') {
                        situps_score = $('#situps_times').val() + '次';
                        sit_ups = $('#situps_times').val();
                        //alert(sit_ups);
                    }
                    else {
                        alert('請輸入次數');
                        return;
                    }
                }

                if (document.getElementById("pushups_time").style.display == 'block') {
                    if ($('#pushups_min').val() != '' && $('#pushups_sec').val() != '') {
                        pushups_score = $('#pushups_min').val() + '分 ' + $('#pushups_sec').val() + '秒';
                        push_ups = $('#pushups_min').val() * 60;
                        push_ups += parseInt($('#pushups_sec').val());
                    }
                    else {
                        alert('請輸入分與秒');
                        return;
                    }
                }
                else {
                    if ($('#pushups_times').val() != '') {
                        pushups_score = $('#pushups_times').val() + '次';
                        push_ups = $('#pushups_times').val();
                    }
                    else {
                        alert('請輸入次數');
                        return;
                    }
                }

                if (document.getElementById("run_time").style.display == 'block') {
                    if ($('#run_min').val() != '' && $('#run_sec').val() != '') {
                        run_score = $('#run_min').val() + '分 ' + $('#run_sec').val() + '秒';
                        run = $('#run_min').val() * 60;
                        run += parseInt($('#run_sec').val());
                    }
                    else {
                        alert('請輸入分與秒');
                        return;
                    }
                }
                else {
                    if ($('#run_times').val() != '') {
                        run_score = $('#run_times').val() + '次';
                        run = $('#run_times').val();
                    }
                    else {
                        alert('請輸入次數');
                        return;
                    }
                }

                var sit_ups;
                var push_ups;
                var run;
                var memo;
                $.postJson('GetValueByCode.ashx', {
                    mode: 'InsertScore', id: $('#id').val(), name: name, birth: birth, gender: gender
                    , unit_code: unit_code, rank_code: rank_code, sit_ups: sit_ups, push_ups: push_ups, run: run,
                    date: $('#ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_date').val(), memo: memo
                }, function (data, s) {
                    if (s == 'success') {
                        if (data["status"] == "OK") {

                            $('#id').parent().next().html('人工鑑測成績輸入完成!');
                            document.getElementById("playerinfo").style.display = 'none';
                            document.getElementById("InputScore").style.display = 'none';

                            if (confirm('是否列印成績單?')) {
                                window.open("ScoreView.aspx?id=" + $('#id').val() + "&date=" + $('#ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_date').val(), "web");
                            }

                            $('#InputScore :text').val('');
                            $('#div_id :text').val('');
                            $('#playerinfo :text').val('');
                        }
                        else {
                            $('#InputScore :text').val('');
                            $('#div_id :text').val('');
                            $('#playerinfo :text').val('');
                            $('#id').parent().next().html(data["status"]);
                            document.getElementById("playerinfo").style.display = 'none';
                            document.getElementById("InputScore").style.display = 'none';
                        }
                    }
                });



            });

            //項目一變更時(black：顯示、none：隱藏)
            $('#Item1').change(function () {
                var e = document.getElementById("Item1");
                var strUser = e.options[e.selectedIndex].value;
                if (strUser != "0") {
                    $.postJson('GetValueByCode.ashx', { mode: 'GetItemUnit', value: strUser }, function (data, s) {
                        if (s == 'success') {
                            if (data["status"] != "false") {
                                if (data["status"] == "秒") {
                                    document.getElementById("situps_time").style.display = 'block';
                                    document.getElementById("situps_count").style.display = 'none';
                                } else {
                                    document.getElementById("situps_count").style.display = 'block';
                                    document.getElementById("situps_time").style.display = 'none';
                                }
                            }
                            else {

                            }
                        }
                    });
                }
                else {
                    document.getElementById("situps_count").style.display = 'block';
                    document.getElementById("situps_time").style.display = 'none';
                }
            });
            //項目二變更時(black：顯示、none：隱藏)
            $('#Item2').change(function () {
                var e = document.getElementById("Item2");
                var strUser = e.options[e.selectedIndex].value;
                if (strUser != "0") {
                    $.postJson('GetValueByCode.ashx', { mode: 'GetItemUnit', value: strUser }, function (data, s) {
                        if (s == 'success') {
                            if (data["status"] != "false") {
                                if (data["status"] == "秒") {
                                    document.getElementById("pushups_time").style.display = 'block';
                                    document.getElementById("pushups_count").style.display = 'none';
                                } else {
                                    document.getElementById("pushups_count").style.display = 'block';
                                    document.getElementById("pushups_time").style.display = 'none';
                                }
                            }
                            else {

                            }
                        }
                    });
                }
                else {
                    document.getElementById("pushups_count").style.display = 'block';
                    document.getElementById("pushups_time").style.display = 'none';
                }
            });
            //項目三變更時(black：顯示、none：隱藏)
            $('#Item3').change(function () {
                var e = document.getElementById("Item3");
                var strUser = e.options[e.selectedIndex].value;
                if (strUser != "0") {
                    $.postJson('GetValueByCode.ashx', { mode: 'GetItemUnit', value: strUser }, function (data, s) {
                        if (s == 'success') {
                            if (data["status"] != "false") {
                                if (data["status"] == "秒") {
                                    document.getElementById("run_time").style.display = 'block';
                                    document.getElementById("run_count").style.display = 'none';
                                } else {
                                    document.getElementById("run_count").style.display = 'block';
                                    document.getElementById("run_time").style.display = 'none';
                                }
                            }
                            else {

                            }
                        }
                    });
                }
                else {
                    document.getElementById("run_time").style.display = 'block';
                    document.getElementById("run_count").style.display = 'none';
                }
            });

            $('#id').blur(function () {
                //$('#id').change(function () {
                var id_length = $('#id').val().length;
                if ($('#ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_date').val() == "") {
                    alert("請先選取日期!!");
                    return;
                }
                else {
                    if (id_length == 10) {
                        $.postJson('GetValueByCode.ashx', { mode: 'checkScorreKeyin', id: $('#id').val() }, function (data, s) {
                            if (s == 'success') {
                                if (data["status"] == "ok") {
                                    $.postJson('GetValueByCode.ashx', { mode: 'QueryplayerExist', id: $('#id').val(), date: $('#ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_date').val() }, function (data, s) {
                                        if (s == 'success') {
                                            if (data["status"] == "true") {
                                                $('#status').html('');
                                                $('#ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_player_gender').html(data["gender"]);
                                                $('#ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_player_birth').html(data["birth"]);
                                                $('#ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_player_name').html(data["name"]);
                                                document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_unit_code').value = data["unit_code"];
                                                document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_rank_code').value = data["rank_code"];
                                                //$('#ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_unit_code').html(data["unit_code"]);
                                                //$('#ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_rank_code').html(data["rank_code"]);
                                                $('#ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_unit_code_title').html(data["unit_title"]);
                                                $('#ctl00_ContentPlaceHolder1_TabContainer1_TabPanel1_rank_code_title').html(data["rank_title"]);
                                                document.getElementById("playerinfo").style.display = 'block';
                                                document.getElementById("InputScore").style.display = 'block';
                                                $.postJson('GetValueByCode.ashx', { mode: 'GetSitUpsItem', value: data["gender"] }, function (datatest, s) {

                                                    var items = JSON.stringify(datatest);
                                                    var myObject = eval('(' + items + ')');
                                                    var Id_Item1 = document.getElementById("Item1");
                                                    var Id_Item2 = document.getElementById("Item2");
                                                    var Id_Item3 = document.getElementById("Item3");
                                                    $('#Item1').empty();
                                                    $('#Item2').empty();
                                                    $('#Item3').empty();
                                                    new_option = new Option('俯地挺身', '0');
                                                    Id_Item1.options.add(new_option);
                                                    new_option = new Option('仰臥起坐', '0');
                                                    Id_Item2.options.add(new_option);
                                                    new_option = new Option('三千公尺跑步', '0');
                                                    Id_Item3.options.add(new_option);
                                                    for (i in myObject) {
                                                        var new_option1 = new Option(myObject[i]["rep_title"], myObject[i]["sid"]);
                                                        var new_option2 = new Option(myObject[i]["rep_title"], myObject[i]["sid"]);
                                                        var new_option3 = new Option(myObject[i]["rep_title"], myObject[i]["sid"]);
                                                        Id_Item1.options.add(new_option1);
                                                        Id_Item2.options.add(new_option2);
                                                        Id_Item3.options.add(new_option3);
                                                    }

                                                    document.getElementById("situps_count").style.display = 'block';
                                                    document.getElementById("situps_time").style.display = 'none';
                                                    document.getElementById("pushups_count").style.display = 'block';
                                                    document.getElementById("pushups_time").style.display = 'none';
                                                    document.getElementById("run_time").style.display = 'block';
                                                    document.getElementById("run_count").style.display = 'none';

                                                });

                                            }
                                            else {
                                                $('#status').html(data["status"]);
                                                document.getElementById("playerinfo").style.display = 'none';
                                                document.getElementById("InputScore").style.display = 'none';
                                            }
                                        }
                                    });
                                }
                                else {
                                    $('#status').html(data["status"]);
                                    document.getElementById("playerinfo").style.display = 'none';
                                    document.getElementById("InputScore").style.display = 'none';
                                    return;
                                }
                            }
                        });
                    }
                    else {
                        $('#id').parent().next().html('身分證長度不足10碼!');
                        document.getElementById("playerinfo").style.display = 'none';
                        document.getElementById("InputScore").style.display = 'none';
                    }
                }
            });
        });
    </script>
    <script type="text/javascript">
        //成績補正用
        //支援IE寫法
        function CheckNum() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_sit2")) {
                if (document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_sit1"))//有sit1
                {
                    if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_sit1").value) |
                    isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_sit2").value))//檢查sit1跟sit2
                    {
                        //檢查錯誤
                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '0';
                        alert('成績輸入包含錯誤字元，請重新輸入!!');
                    }
                    else //檢查正確
                    {
                        //再來檢查push1
                        if (document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_push1"))//有push1
                        {
                            if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_push1").value) |
                    isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_push2").value))//檢查push1跟push2
                            {
                                //檢查錯誤
                                document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '0';
                                alert('成績輸入包含錯誤字元，請重新輸入!!');
                            }
                            else {
                                //檢查正確
                                //再來檢查run1
                                if (document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run1"))//有run1
                                {
                                    if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run1").value) |
                    isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run2").value))//檢查run1跟run2
                                    {
                                        //檢查錯誤
                                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '0';
                                        alert('成績輸入包含錯誤字元，請重新輸入!!');
                                    }
                                    else {
                                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '1';
                                        //alert('正確')
                                    }
                                }
                                else//沒有run1
                                {
                                    if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run2").value))//只檢查run2
                                    {
                                        //檢查錯誤
                                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '0';
                                        alert('成績輸入包含錯誤字元，請重新輸入!!');
                                    }
                                    else {
                                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '1';
                                        //alert('正確')
                                    }
                                }
                            }
                        }
                        else//沒有push1
                        {
                            if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_push2").value))//只檢查push2
                            {
                                //檢查錯誤
                                document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '0';
                                alert('成績輸入包含錯誤字元，請重新輸入!!');
                            }
                            else //檢查正確
                            {
                                //檢查正確
                                //再來檢查run1
                                if (document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run1"))//有run1
                                {
                                    if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run1").value) |
                    isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run2").value))//檢查run1跟run2
                                    {
                                        //檢查錯誤
                                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '0';
                                        alert('成績輸入包含錯誤字元，請重新輸入!!');
                                    }
                                    else {
                                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '1';
                                        //alert('正確')
                                    }
                                }
                                else//沒有run1
                                {
                                    if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run2").value))//只檢查run2
                                    {
                                        //檢查錯誤
                                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '0';
                                        alert('成績輸入包含錯誤字元，請重新輸入!!');
                                    }
                                    else {
                                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '1';
                                        //alert('正確')
                                    }
                                }
                            }
                        }
                    }
                }
                else//沒有sit1
                {
                    if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_sit2").value))//只檢查sit2
                    {
                        //檢查錯誤
                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '0';
                        alert('成績輸入包含錯誤字元，請重新輸入!!');
                    }
                    else //檢查正確
                    {
                        //再來檢查push1
                        if (document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_push1"))//有push1
                        {
                            if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_push1").value) |
                    isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_push2").value))//檢查push1跟push2
                            {
                                //檢查錯誤
                                document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '0';
                                alert('成績輸入包含錯誤字元，請重新輸入!!');
                            }
                            else {
                                //檢查正確
                                //再來檢查run1
                                if (document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run1"))//有run1
                                {
                                    if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run1").value) |
                    isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run2").value))//檢查run1跟run2
                                    {
                                        //檢查錯誤
                                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '0';
                                        alert('成績輸入包含錯誤字元，請重新輸入!!');
                                    }
                                    else {
                                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '1';
                                        //alert('正確')
                                    }
                                }
                                else//沒有run1
                                {
                                    if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run2").value))//只檢查run2
                                    {
                                        //檢查錯誤
                                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '0';
                                        alert('成績輸入包含錯誤字元，請重新輸入!!');
                                    }
                                    else {
                                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '1';
                                        //alert('正確')
                                    }
                                }
                            }
                        }
                        else//沒有push1
                        {
                            if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_push2").value))//只檢查push2
                            {
                                //檢查錯誤
                                document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '0';
                                alert('成績輸入包含錯誤字元，請重新輸入!!');
                            }
                            else //檢查正確
                            {
                                //檢查正確
                                //再來檢查run1
                                if (document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run1"))//有run1
                                {
                                    if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run1").value) |
                    isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run2").value))//檢查run1跟run2
                                    {
                                        //檢查錯誤
                                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '0';
                                        alert('成績輸入包含錯誤字元，請重新輸入!!');
                                    }
                                    else {
                                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '1';
                                        //alert('正確')
                                    }
                                }
                                else//沒有run1
                                {
                                    if (isNaN(document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run2").value))//只檢查run2
                                    {
                                        //檢查錯誤
                                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '0';
                                        alert('成績輸入包含錯誤字元，請重新輸入!!');
                                    }
                                    else {
                                        document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '1';
                                        //alert('正確')
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else {
                document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_check_num").value = '0';
                alert('請先查詢成績!!');
            }

        }
    </script>
    <script type="text/javascript">
        function SetSelected_sit1() {
            document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_sit1").select();
        }
        function SetSelected_sit2() {
            document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_sit2").select();
        }
        function SetSelected_push1() {
            document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_push1").select();
        }
        function SetSelected_push2() {
            document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_push2").select();
        }
        function SetSelected_run1() {
            document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run1").select();
        }
        function SetSelected_run2() {
            document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_txb_run2").select();
        }

    </script>

    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }
    </style>

    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
        <ajaxToolkit:TabPanel runat="server" ID="TabPanel1" HeaderText="人工鑑測成績輸入">
            <ContentTemplate>
                <div>
                    <p>
                        <asp:Calendar ID="datepicker" runat="server" BackColor="White"
                            BorderColor="#3366CC" BorderWidth="1px" CellPadding="1"
                            DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                            ForeColor="#003399" Height="200px"
                            OnSelectionChanged="datepicker_SelectionChanged" Width="220px"
                            OnDayRender="datepicker_DayRender">
                            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                            <OtherMonthDayStyle ForeColor="#999999" />
                            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                            <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px"
                                Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                            <WeekendDayStyle BackColor="#CCCCFF" />
                        </asp:Calendar>
                    </p>
                    <p>鑑測日期 :
                        <asp:TextBox ID="date" runat="server" Enabled="False"></asp:TextBox></p>
                </div>

                <div id="div_id">
                    <p>身分證字號 :
                        <input type="text" id="id" /></p>
                    <p id="status" style="color:red"></p>
                </div>

                <div id="playerinfo" style="display: none">
                    <table>
                        <tbody>
                            <tr>
                                <td>性別 : </td>
                                <td>
                                    <asp:Label ID="player_gender" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>生日 : </td>
                                <td>
                                    <asp:Label ID="player_birth" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>姓名 : </td>
                                <td>
                                    <asp:Label ID="player_name" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>單位 : </td>
                                <td>
                                    <asp:Label ID="unit_code_title" runat="server"></asp:Label>
                                    <asp:HiddenField ID="unit_code" runat="server" />
                                    <%-- <asp:Label ID="unit_code" runat="server" Visible="false"></asp:Label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>級職 : </td>
                                <td>
                                    <asp:Label ID="rank_code_title" runat="server"></asp:Label>
                                    <asp:HiddenField ID="rank_code" runat="server" />
                                    <%-- <asp:Label ID="rank_code" runat="server" Visible="false"></asp:Label>--%>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <div id="InputScore" style="display: none">
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
                                    <span style="margin-left: 3px">項目一</span><br>
                                    <select id="Item1">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </select>
                                    <br />
                                    <div id="situps_time">
                                        <input type="text" id="situps_min" onkeypress="return isNumberKey(event)" style="width: 40px; margin-top: 3px" />
                                        <span>分</span><input type="text" id="situps_sec" onkeypress="return isNumberKey(event)" style="width: 40px" />
                                        <span>秒</span><br />
                                    </div>
                                    <div id="situps_count">
                                        <input type="text" id="situps_times" onkeypress="return isNumberKey(event)" style="width: 40px; margin-top: 3px" />
                                        <span>次</span><br />
                                    </div>

                                </td>

                                <td>
                                    <span style="margin-left: 3px">項目二</span><br>
                                    <select id="Item2">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </select>
                                    <br />
                                    <div id="pushups_time">
                                        <input type="text" id="pushups_min" onkeypress="return isNumberKey(event)" style="width: 40px; margin-top: 3px" />
                                        <span>分</span><input type="text" id="pushups_sec" onkeypress="return isNumberKey(event)" style="width: 40px" />
                                        <span>秒</span><br />
                                    </div>
                                    <div id="pushups_count">
                                        <input type="text" id="pushups_times" onkeypress="return isNumberKey(event)" style="width: 40px; margin-top: 3px" />
                                        <span>次</span><br />
                                    </div>

                                </td>

                                <td>
                                    <span style="margin-left: 3px">項目三</span><br />
                                    <select id="Item3">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </select>
                                    <br />
                                    <div id="run_time">
                                        <input type="text" id="run_min" onkeypress="return isNumberKey(event)" style="width: 40px; margin-top: 3px" />
                                        <span>分</span><input type="text" id="run_sec" onkeypress="return isNumberKey(event)" style="width: 40px" />
                                        <span>秒</span><br />
                                    </div>
                                    <div id="run_count">
                                        <input type="text" id="run_times" onkeypress="return isNumberKey(event)" style="width: 40px; margin-top: 3px" />
                                        <span>次</span><br />
                                    </div>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="3"><b style="color: red">備註：若單項「未測」或「未完測」請輸入：「0次」或「0分0秒」</b></td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <input id="savescore" type="button" value="成績儲存" /></td>
                            </tr>

                        </tbody>
                    </table>
                </div>

                <div>
                </div>

            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="人工輸入成績補正">
            <ContentTemplate>
                <body>
                    <div>

                        <table>

                            <tr>
                                <td>身分證號 </td>
                                <td>
                                    <input type="text" id="id_2" runat="server" maxlength="10" style="text-transform: uppercase;" autofocus="autofocus" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server"
        Text="搜尋" OnClick="Button1_Click" Font-Bold="True" Font-Size="Large" ForeColor="Blue" Font-Names="標楷體" Style="cursor: pointer; background-color: #f0e68c" />


                                </td>
                            </tr>
                            <tr>
                                <td>姓名
                                </td>
                                <td>
                                    <input type="text" id="name" runat="server" readonly="readonly" disabled="disabled" style="background-color: #CCCCFF; color: red; border-style: groove" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="sit_ups_name" runat="server"></asp:Label>

                                </td>
                                <td>
                                    <asp:TextBox ID="txb_sit1" runat="server" Width="50px" Visible="False" CssClass="auto-style1" MaxLength="4" Height="19px"></asp:TextBox>
                                    <asp:Label ID="lab_sit1" runat="server" Text="分" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txb_sit2" runat="server" Width="50px" Visible="False" MaxLength="4"></asp:TextBox>
                                    <asp:Label ID="lab_sit2" runat="server" Text="秒" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="push_ups_name" runat="server"></asp:Label>

                                </td>
                                <td>
                                    <asp:TextBox ID="txb_push1" runat="server" Width="50px" Visible="False" MaxLength="4"></asp:TextBox>
                                    <asp:Label ID="lab_push1" runat="server" Text="分" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txb_push2" runat="server" Width="50px" Visible="False" MaxLength="4"></asp:TextBox>
                                    <asp:Label ID="lab_push2" runat="server" Text="秒" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="run_name" runat="server"></asp:Label></td>
                                <td>&nbsp;
        <asp:TextBox ID="txb_run1" runat="server" Width="50px" Visible="False" MaxLength="4"></asp:TextBox>
                                    <asp:Label ID="lab_run1" runat="server" Text="分" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txb_run2" runat="server" Width="50px" Visible="False" MaxLength="4"></asp:TextBox>
                                    <asp:Label ID="lab_run2" runat="server" Text="秒" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>

                                <td colspan="2">
                                    <li style="margin-left: 15px">如有未測驗之項目則該項目不輸入任何值。</li>
                                </td>
                                <tr>
                                    <td colspan="2" style="margin-left: 15px">
                                        <asp:Button ID="btn_UpdateResult" runat="server" Font-Bold="True" Font-Names="標楷體" Font-Size="Large" ForeColor="Red" OnClick="btn_UpdateResult_Click" OnClientClick="CheckNum()" Style="cursor: pointer; background-color: #7fffd4" Text="補正成績" AccessKey="z" />
                                        <asp:HiddenField ID="dateValue" runat="server" />
                                        <asp:HiddenField ID="checkid" runat="server" />
                                        <asp:HiddenField ID="check_num" runat="server" />
                                    </td>
                                </tr>
                            </tr>
                        </table>
                    </div>

                </body>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>

    </ajaxToolkit:TabContainer>
</asp:Content>

