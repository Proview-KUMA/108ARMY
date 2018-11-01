using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class SignUp : System.Web.UI.Page
{
    enum ErrorType
    {
        ID,
        Birthday,
        UnitCode,
        RankCode,
        ServiceType,
        NewMember,
        UnitRepeat,
        IdRepeat
    }

   struct ErrorStruct
    {
        public ErrorType errortype;
        public string data;

        public ErrorStruct(ErrorType _errortype, string _data)
        {
            errortype = _errortype;
            data = _data;
        }
    };

    enum ServiceType
    { 
        A,
        B
    }

    enum NewMember : int
    { 
        One = 1,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6
    }

    public DataTable CanAccess = new DataTable();
    public string unit_code = string.Empty;
    public Dictionary<string, object> d_id = new Dictionary<string, object>();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["account"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        try
        {
            if (Lib.SysSetting.CurrentSystemMode() != Lib.SysSetting.SystemMode.Race)
            {
                Response.Redirect("~/index.aspx");
            }
        }
        catch (Exception ex)
        {

        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        CanAccess = new DataTable();
        CanAccess.Columns.Add("身份證");
        CanAccess.Columns.Add("姓名");
        CanAccess.Columns.Add("生日");
        CanAccess.Columns.Add("性別");
        CanAccess.Columns.Add("單位代碼");
        CanAccess.Columns.Add("軍階代碼");
        CanAccess.Columns.Add("鑑測站代碼");
        CanAccess.Columns.Add("役別");
        CanAccess.Columns.Add("新進人員區分代碼");
        CanAccess.Columns.Add("組別");

        d_id.Clear();
        unit_code = string.Empty;
        Button3.Enabled = false;
        Label8.Text = string.Empty;
        Label13.Text = string.Empty;
        Label9.Text = "0";
        Label10.Text = "0";
        Label11.Text = "0";
        int Reserver_Count = 0;
        int Reserver_Correct = 0;
        int isSingle = 0;
        Dictionary<string, object> ErrorDic = new Dictionary<string, object>();
        List<int> ErrorNum = new List<int>();
        if (FileUpload1.HasFile)
        {

            if (RB_Group.Checked)    //作組別的設定
                isSingle = 2;
            else
                isSingle = 1;

            HttpPostedFile file = FileUpload1.PostedFile;
            StreamReader reader = new StreamReader(file.InputStream, Encoding.Default);

            try
            {
                while (reader.Peek() >= 0)
                {
                    string result = reader.ReadLine();
                    List<ErrorStruct> EList = new List<ErrorStruct>();
                    string[] operater = { "," };
                    string[] info = result.Split(operater, StringSplitOptions.None);
                    if (result.Trim().Length != 0)
                    {
                        try
                        {
                            if (info[0] != "身分證字號")
                            {
                                string _gender = "";
                                char[] _id = info[0].ToCharArray();
                                if (_id[1] == '1')
                                    _gender = "M";
                                if (_id[1] == '2')
                                    _gender = "F";
                                Reserver_Count++;
                                EList = CheckFormat(info[0].Trim(), info[2].Trim(), info[3].Trim(), info[4].Trim(), info[5].Trim(), info[6].Trim(), isSingle);
                                if (EList.Count != 0)
                                {
                                    ErrorDic.Add(Reserver_Count.ToString(), EList);
                                    //ErrorNum.Add(Reserver_Count);
                                }
                                else
                                {
                                    Reserver_Correct++;
                                    CanAccess.Rows.Add(info[0].Trim(), info[1].Trim(), Convert.ToDateTime(info[2].Trim()), _gender, info[3].Trim(), info[4].Trim(), Lib.SysSetting.CenterCode, info[5].Trim(), info[6].Trim(), isSingle.ToString());
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Label8.Text = Label8.Text + "第" + Reserver_Count + "筆資料 , 身分證(" + info[0].Trim() + ")  => " + "此筆資料欄位不足 , 請檢查分隔符號(,)是否正確" + "<br>"; ;
                        }
                    }
                }

                Session["CanAccess"] = CanAccess;

                Label11.Text = Reserver_Count.ToString();
                Label10.Text = Reserver_Correct.ToString();
                Label9.Text = (Reserver_Count - Reserver_Correct).ToString();
                string message = string.Empty;
                foreach (KeyValuePair<string, object> item in ErrorDic)
                {
                    List<ErrorStruct> _list = (List<ErrorStruct>)item.Value;
                    foreach (ErrorStruct Etype in _list)
                    {
                        switch (Etype.errortype)
                        { 
                            case ErrorType.ID :
                                message = "第" + item.Key + "筆資料 , 身分證(" + Etype.data + ")  => " + "身分證字號錯誤" + "<br>";                   
                                break;
                            case ErrorType.Birthday:
                                message = "第" + item.Key + "筆資料  , 身分證(" + Etype.data + ") => " + "生日錯誤" + "<br>";
                                break;
                            case ErrorType.NewMember:
                                message = "第" + item.Key + "筆資料  , 身分證(" + Etype.data + ") => " + "新進人員區分代碼錯誤" + "<br>";
                                break;
                            case ErrorType.RankCode:
                                message = "第" + item.Key + "筆資料  , 身分證(" + Etype.data + ") => " + "軍階代碼錯誤" + "<br>";
                                break;
                            case ErrorType.ServiceType:
                                message = "第" + item.Key + "筆資料  , 身分證(" + Etype.data + ") => " + "役別錯誤" + "<br>";
                                break;
                            case ErrorType.UnitCode:
                                message = "第" + item.Key + "筆資料  , 身分證(" + Etype.data + ") => " + "單位代碼錯誤" + "<br>";
                                break;
                            case ErrorType.UnitRepeat:
                                message = "第" + item.Key + "筆資料 , 身分證(" + Etype.data + ") => " + "單位代碼不一致" + "<br>";
                                break;
                            case ErrorType.IdRepeat:
                                message = "第" + item.Key + "筆資料 , 身分證(" + Etype.data + ") => " + "身分證字號重覆" + "<br>";
                                break;
                            default:
                                break;
                            
                        }
                        Label8.Text = Label8.Text + message;
                    }
                }

                if (ErrorDic.Count == 0)
                {
                    Button3.Enabled = true;
                    Session["FileName"] = FileUpload1.FileName;
                }
            }

            catch (Exception ex)
            { 
            
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
    }
    protected void RB_Single_CheckedChanged(object sender, EventArgs e)
    {
        if (RB_Single.Checked)
            RB_Group.Checked = false;
    }
    protected void RB_Group_CheckedChanged(object sender, EventArgs e)
    {
        if (RB_Group.Checked)
            RB_Single.Checked = false;
    }

    private List<ErrorStruct> CheckFormat(string id, string birthday, string unitcode, string rankcode, string servicetype, string newmember, int IsSingle)
    {
        List<ErrorStruct> result = new List<ErrorStruct>();
        Dictionary<string, object> d = new Dictionary<string, object>();
        Lib.DataUtility du = new Lib.DataUtility();
        DataTable dt = new DataTable();
        
        //check id
        try
        {
            char[] _id = id.ToCharArray();
            if (id.Length != 10 || (_id[1] != '1' && _id[1] != '2'))
            {
                result.Add(new ErrorStruct(ErrorType.ID, id));
            }

            d_id.Add(id, id);
        }
        catch (Exception ex)
        {
            result.Add(new ErrorStruct(ErrorType.IdRepeat, id));
        }
        //check birthday
        try
        {
            DateTime dtime = Convert.ToDateTime(birthday);        
        }
        catch (Exception ex)
        {
            result.Add(new ErrorStruct(ErrorType.Birthday, id));
        }

        //check unitcode
        d.Clear();
        d.Add("unit_code", unitcode);
        try
        {
            if (unitcode.Length == 5)
            {
                dt = du.getDataTableBysp("Race_QueryUnitCode", d);
                if (dt.Rows.Count != 1)
                {
                    result.Add(new ErrorStruct(ErrorType.UnitCode, id));
                }
            }
            else
            {
                result.Add(new ErrorStruct(ErrorType.UnitCode, id));
            }
        }
        catch (Exception ex)
        {
            result.Add(new ErrorStruct(ErrorType.UnitCode, id));
        }

        //check rankcode
        d.Clear();
        d.Add("rank_code", rankcode);
        try
        {
            if (rankcode.Length == 2)
            {
                dt = du.getDataTableBysp("Race_QueryRankCode", d);
                if (dt.Rows.Count != 1)
                {
                    result.Add(new ErrorStruct(ErrorType.RankCode, id));
                }
            }
            else
            {
                result.Add(new ErrorStruct(ErrorType.RankCode, id));
            }
        }
        catch (Exception ex)
        {
            result.Add(new ErrorStruct(ErrorType.RankCode, id));
        }

        //check servicetype 
        if (servicetype != ServiceType.A.ToString() && servicetype != ServiceType.B.ToString())
        {
            result.Add(new ErrorStruct(ErrorType.ServiceType, id));
        }

        if (id.Substring(1, 1) == "2" && servicetype == ServiceType.B.ToString())
        {
            result.Add(new ErrorStruct(ErrorType.ServiceType, id));
        }

        //check newmember
        try
        {
            int newmember_int = Convert.ToInt32(newmember);
            if (newmember_int != (int)NewMember.One && newmember_int != (int)NewMember.Three && newmember_int != (int)NewMember.Four && newmember_int != (int)NewMember.Five && newmember_int != (int)NewMember.Six)
            {
                result.Add(new ErrorStruct(ErrorType.NewMember, id));
            }
        }
        catch(Exception ex)
        {
            result.Add(new ErrorStruct(ErrorType.NewMember, id));
        }

        //check UnitRepeate
        try
        {
            if (IsSingle == 2)
            {
                if (unit_code == string.Empty)
                {
                    unit_code = unitcode;
                }
                else
                {
                    if (unit_code != unitcode)
                    {
                        result.Add(new ErrorStruct(ErrorType.UnitRepeat, id));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            result.Add(new ErrorStruct(ErrorType.UnitRepeat, id));
        }

        return result;    
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Dictionary<string, object> d = new Dictionary<string, object>();
        Lib.DataUtility du = new Lib.DataUtility();
        try
        {
            DataTable checkid = new DataTable();
            DataTable ResultCall = new DataTable();
            ResultCall.Columns.Add("身份證");
            ResultCall.Columns.Add("姓名");
            ResultCall.Columns.Add("生日");
            ResultCall.Columns.Add("性別");
            ResultCall.Columns.Add("單位代碼");
            ResultCall.Columns.Add("軍階代碼");
            ResultCall.Columns.Add("鑑測站代碼");
            ResultCall.Columns.Add("役別");
            ResultCall.Columns.Add("新進人員區分代碼");
            ResultCall.Columns.Add("組別");
            ResultCall.Columns.Add("訊息");

            List<string> Id_Exist = new List<string>();
            int count = 0;
            DataTable dt = (DataTable)Session["CanAccess"];
            List<SqlParameter> list = new List<SqlParameter>();
            foreach (DataRow DR in dt.Rows)
            {
                d.Clear();
                d.Add("id", DR[0].ToString());
                checkid = du.getDataTableBysp("Race_QueryPlayer", d);
                if (checkid.Rows.Count != 0)
                {
                    Id_Exist.Add(DR[0].ToString());
                }
            }

            if (Id_Exist.Count == 0)
            {
                foreach (DataRow DR in dt.Rows)
                {
                    //CanAccess.Columns.Add("身份證"); 0
                    //CanAccess.Columns.Add("姓名");   1
                    //CanAccess.Columns.Add("生日");   2
                    //CanAccess.Columns.Add("性別");    3
                    //CanAccess.Columns.Add("單位代碼");4
                    //CanAccess.Columns.Add("軍階代碼");5
                    //CanAccess.Columns.Add("鑑測站代碼");6
                    //CanAccess.Columns.Add("役別");7
                    //CanAccess.Columns.Add("新進人員區分代碼");8
                    //CanAccess.Columns.Add("組別");9
                    #region New SqlParameter
                    SqlParameter p1 = new SqlParameter("message", SqlDbType.NVarChar, 50);
                    SqlParameter p2 = new SqlParameter("id", DR[0].ToString());
                    SqlParameter p3 = new SqlParameter("gender", DR[3].ToString());
                    SqlParameter p4 = new SqlParameter("birth", Convert.ToDateTime(DR[2]));
                    SqlParameter p5 = new SqlParameter("name", DR[1]);
                    SqlParameter p6 = new SqlParameter("unit_code", DR[4].ToString());
                    SqlParameter p7 = new SqlParameter("rank_code", DR[5].ToString());
                    SqlParameter p8 = new SqlParameter("center_code", DR[6].ToString());
                    SqlParameter p9 = new SqlParameter("result", DR[9].ToString() + DR[7].ToString() + DR[8].ToString());
                    //SqlParameter p10 = new SqlParameter("date", System.DateTime.Today);
                    #endregion
                    p1.Direction = ParameterDirection.Output;
                    list.Clear();
                    #region List.Add SqlParameter
                    list.Add(p1);
                    list.Add(p2);
                    list.Add(p3);
                    list.Add(p4);
                    list.Add(p5);
                    list.Add(p6);
                    list.Add(p7);
                    list.Add(p8);
                    list.Add(p9);
                    //list.Add(p10);
                    #endregion
                    SqlParameter[] sqls = list.ToArray();
                    du.executeNonQueryBysp("Race_AddPlayer", sqls);

                    if (p1.Value.ToString() == "canreserve")
                    {
                        count++;
                    }
                }
                Label13.Text = "共 " + count.ToString() + " 名完成報進.";
                Button3.Enabled = false;
            }
            else
            {
                Label13.Text = string.Empty;
                foreach (string s in Id_Exist)
                {
                    Label13.Text = Label13.Text + " " + s + " 已報進 , 不可重覆報進" + "<br>";
                }
            }
        }
        catch (Exception ex)
        {
            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
        }
    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
}
