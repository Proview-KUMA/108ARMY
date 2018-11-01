using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
//using System.Windows.Forms;
using Lib.Center;

public partial class Update_106_Score : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    #region 統計未上傳筆數函式
    private DataTable Score_count(string starttime, string endtime, string constring)//統計未上傳筆數
    {
        string st = starttime;
        string et = endtime;
        string costring = txb_ConnString.Text;
        SqlConnection con = new SqlConnection(costring);
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        DataTable dt = new DataTable();
        cmd.CommandText = string.Format("select (select COUNT(*) from Result where status='102' and (result is null or result='') and date between '{0}' and '{1}' ) as '102',(select COUNT(*) from Result where status='103' and (result is null or result='') and date between '{0}' and '{1}') as '103',(select COUNT(*) from Result where status='105' and (result is null or result='') and date between '{0}' and '{1}') as '105',(select COUNT(*) from Result where status='102' and result = '666' and date between '{0}' and '{1}') as '102_666', (select COUNT(*) from Result where status='103' and result = '666' and date between '{0}' and '{1}') as '103_666',(select COUNT(*) from Result where status='105' and result = '666' and date between '{0}' and '{1}') as '105_666',(select COUNT(*) from Result where status in('102','103','105') and (result='666' or result='' or result is null) and date between '{0}' and '{1}') as total", st, et);
        try
        {        
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    #endregion
    #region 連線測試、復原備份資料
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txb_ConnString.Text))
        {
            try
            {

                SqlConnection con_test = new SqlConnection(txb_ConnString.Text);
                con_test.Open();

                //MessageBox.Show("連線測試正常!!");
                lab_con_Msg.Text = "連線測試正常!!";
                con_test.Close();
                con_test.Dispose();
                //return con;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("與資料庫連線失敗：" + ex.Message);
                lab_con_Msg.Text = "與資料庫連線失敗：" + ex.Message;
                //return null;
            }
        }
        else
        {
            //MessageBox.Show("連線字串欄位空白，請輸入正確字串");
            lab_con_Msg.Text = "連線字串欄位空白，請輸入正確字串";

        }
    }

    //復原備份資料
    protected void btn_reduction_Click(object sender, EventArgs e)
    {
        lab_ReBak_Msg.Text = null;
        lab_Count.Text = null;
        if (!string.IsNullOrEmpty(txb_ReBak_tb_Name.Text))
        {
            dgv_Result.DataSource = null;
            dgv_Result.DataBind();
            SqlConnection con = new SqlConnection(txb_ConnString.Text);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from "+txb_ReBak_tb_Name.Text;
            try
            {
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count > 0)
                {
                    int count = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("id", dt.Rows[i]["id"].ToString());
                        cmd.Parameters.AddWithValue("date", dt.Rows[i]["date"]);
                        cmd.Parameters.AddWithValue("sit_ups", dt.Rows[i]["sit_ups"].ToString());
                        cmd.Parameters.AddWithValue("sit_ups_score", dt.Rows[i]["sit_ups_score"].ToString());
                        cmd.Parameters.AddWithValue("push_ups", dt.Rows[i]["push_ups"].ToString());
                        cmd.Parameters.AddWithValue("push_ups_score", dt.Rows[i]["push_ups_score"].ToString());
                        cmd.Parameters.AddWithValue("run", dt.Rows[i]["run"].ToString());
                        cmd.Parameters.AddWithValue("run_score", dt.Rows[i]["run_score"].ToString());
                        cmd.Parameters.AddWithValue("result", dt.Rows[i]["result"].ToString());
                        cmd.Parameters.AddWithValue("status", dt.Rows[i]["status"].ToString());
                        cmd.CommandText = "update Result set sit_ups=@sit_ups,sit_ups_score=@sit_ups_score,push_ups=@push_ups,push_ups_score=@push_ups_score,run=@run,result=@result,status=@status where id=@id and date=@date";
                        cmd.ExecuteNonQuery();
                        count++;
                    }
                    lab_Count.Text = "讀取備份資料共計「" + dt.Rows.Count.ToString() + "」筆，成功復原備份資料「" + count.ToString() + "」筆";
                }
            }
            catch (Exception ex)
            {
                lab_Count.Text = ex.Message;
                lab_ReBak_Msg.Text = ex.Message;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        else
        {
            lab_ReBak_Msg.Text = "請先輸入復原資料表名稱";
        }

    }
    #endregion
    #region 資料查詢、變更
    protected void btn_Search_Click(object sender, EventArgs e)//讀取已上傳資料
    {
        //DialogResult dialogResult = MessageBox.Show("確認讀取已上傳資料?", "確認Yes/No", MessageBoxButtons.YesNo);
        //if (dialogResult == DialogResult.Yes)
        //{
            lab_Count.Text = null;
            dgv_Result.DataSource = null;
            dgv_Result.DataBind();
            
            SqlConnection con = new SqlConnection(txb_ConnString.Text);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            DataTable dt = new DataTable();
            string StartTime = string.Empty;
            string EndTime = string.Empty;
            StartTime = txb_StartTime.Text;
            EndTime = txb_EndTime.Text;
            cmd.CommandType = CommandType.Text;
            //只找狀態202(合格已上傳)、203(不合格已上傳)、205(補測已上傳)
            cmd.CommandText = "select sid,id,name,age,sit_ups,sit_ups_score,push_ups,push_ups_score,run,run_score,date,result,status,clothesNum,memo from Result where date between '" + StartTime + "' and '" + EndTime + "' and status in ('202','203','205')";
            try
            {
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count > 0)
                {
                    dgv_Result.DataSource = dt;
                    dgv_Result.DataBind();
                    lab_Count.Text = "成功讀取已上傳資料「" + dt.Rows.Count.ToString() + "」筆";
                    //MessageBox.Show("資料讀取完成，共計：「" + dt.Rows.Count + "」筆。");
                }
                else
                {
                    lab_Count.Text = "查無資料";
                    //MessageBox.Show("查無資料");
                }
            }
            catch (Exception ex)
            {
                lab_Count.Text = ex.Message;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            
            
        //}
        //else if (dialogResult == DialogResult.No)
        //{
        //    //do something else
        //}

    }
    protected void btn_Copy_Table_Click(object sender, EventArgs e)//備份106年成績
    {
        lab_Bak_Msg.Text = null;
        lab_Count.Text = null;
        if (!string.IsNullOrEmpty(txb_Bak_tb_Name.Text))
        {
            SqlConnection con = new SqlConnection(txb_ConnString.Text);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            string StartTime = string.Empty;
            string EndTime = string.Empty;
            StartTime = txb_StartTime.Text;
            EndTime = txb_EndTime.Text;
            cmd.CommandType = CommandType.Text;
            try
            {
                //備份日期內所有資料
                cmd.CommandText = "select * into "+txb_Bak_tb_Name+" from Result where date between '" + StartTime + "' and '" + EndTime + "'";
                int count = 0;
                count = cmd.ExecuteNonQuery();
                lab_Count.Text = "成功備份資料至["+txb_Bak_tb_Name.Text+"]資料表，共計「" + count.ToString() + "」筆";
            }
            catch (Exception ex)
            {
                lab_Count.Text = ex.Message;
                lab_Bak_Msg.Text = ex.Message;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        else
        {
            lab_Bak_Msg.Text = "請先輸入備份資料表名稱";
        }

    }
    protected void btn_RE_status_Click(object sender, EventArgs e)//更新鑑狀態(已上傳→未上傳)
    {
        //DialogResult dialogResult = MessageBox.Show("確認將鑑測狀態更改為「未上傳」?", "確認Yes/No", MessageBoxButtons.YesNo);
        //if (dialogResult == DialogResult.Yes)
        //{
            lab_Count.Text = null;
            SqlConnection con = new SqlConnection(txb_ConnString.Text);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            string StartTime = string.Empty;
            string EndTime = string.Empty;
            StartTime = txb_StartTime.Text;
            EndTime = txb_EndTime.Text;
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.Text;
            //2017-1-4將現報狀態777改回666
            cmd.CommandText = "update Result set result='666' where result='777' and date between '" + StartTime + "' and '" + EndTime + "' and status in ('202','203','205')";
            try
            {
                int count_666 = 0;
                count_666 = cmd.ExecuteNonQuery();
                lab_Count.Text += "現報狀態由777改成666共計「" + count_666.ToString() + "」 筆，";
            }
            catch (Exception ex)
            {
                lab_Count.Text = "更新現報人員狀態：" + ex.Message;
            }
            finally
            {
                
            }

            try
            {
                cmd.CommandText = "update Result set status=REPLACE(status,'20','10') where date between '" + StartTime + "' and '" + EndTime + "' and status in ('202','203','205')";
                int count = 0;
                count = cmd.ExecuteNonQuery();
                lab_Count.Text += "更新鑑測狀態資料共計「" + count.ToString() + "」筆，";
                cmd.CommandText = "select sid,id,name,age,sit_ups,sit_ups_score,push_ups,push_ups_score,run,run_score,date,result,status,clothesNum,memo from Result where date between '" + StartTime + "' and '" + EndTime + "' and status in ('102','103','105')";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count > 0)
                {
                    dgv_Result.DataSource = dt;
                    dgv_Result.DataBind();
                    lab_Count.Text += "總計未上傳資料共計「" + dt.Rows.Count + "」筆。";
                }
                else
                {
                    lab_Count.Text = "查無資料";
                }
            }
            catch (Exception ex)
            {
                lab_Count.Text = ex.Message;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        //}
        //else if (dialogResult == DialogResult.No)
        //{
        //    //do something else
        //}

    }
    protected void btn_Re_Score_Click(object sender, EventArgs e)//重新計算成績
    {
        //DialogResult dialogResult = MessageBox.Show("確認將106年成績重新計算?", "確認Yes/No", MessageBoxButtons.YesNo);
        //if (dialogResult == DialogResult.Yes)
        //{
            lab_Count.Text = null;
            dgv_Result.DataSource = null;
            dgv_Result.DataBind();
            string StartTime = string.Empty;
            string EndTime = string.Empty;
            StartTime = txb_StartTime.Text;
            EndTime = txb_EndTime.Text;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(txb_ConnString.Text);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select sid,id,name,age,sit_ups,sit_ups_score,push_ups,push_ups_score,run,run_score,date,result,status,clothesNum,memo from Result where date between '" + StartTime + "' and '" + EndTime + "' and status in ('102','103','105')";
            dt.Load(cmd.ExecuteReader());
            try
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = @"Ex106_CalResultByID";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("id", dt.Rows[i]["id"].ToString());
                        cmd.Parameters.AddWithValue("date", Convert.ToDateTime(dt.Rows[i]["date"].ToString()));
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    lab_Count.Text = "查無資料，請先讀取資料!!";
                }
                dgv_Result.DataSource = null;
                dgv_Result.DataBind();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select sid,id,name,age,sit_ups,sit_ups_score,push_ups,push_ups_score,run,run_score,date,result,status,clothesNum,memo from Result where (date between '" + StartTime + "' and '" + EndTime + "' and status in ('102','103','105')) and (id is not null and id!='') and (date is not null and date!='')";
                dt.Clear();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count > 0)
                {
                    lab_Count.Text = "成功換算成績共計「" + dt.Rows.Count + "」筆";
                    dgv_Result.DataSource = dt;
                    dgv_Result.DataBind();
                }
                else
                {
                    lab_Count.Text = "查無資料";
                }
            }
            catch (Exception ex)
            {
                lab_Count.Text = ex.Message;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        //}
        //else if (dialogResult == DialogResult.No)
        //{
        //    //do something else
        //}

    }
    protected void btn_Search_1xx_Click(object sender, EventArgs e)//讀取未上傳資料
    {
        //DialogResult dialogResult = MessageBox.Show("確認讀取未上傳資料?", "確認Yes/No", MessageBoxButtons.YesNo);
        //if (dialogResult == DialogResult.Yes)
        //{
            lab_Count.Text = null;
            dgv_Result.DataSource = null;
            dgv_Result.DataBind();
            SqlConnection con = new SqlConnection(txb_ConnString.Text);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            DataTable dt = new DataTable();
            string StartTime = string.Empty;
            string EndTime = string.Empty;
            StartTime = txb_StartTime.Text;
            EndTime = txb_EndTime.Text;
            cmd.CommandType = CommandType.Text;
            //只找狀態202(合格已上傳)、203(不合格已上傳)、205(補測已上傳)
            try
            {
                cmd.CommandText = "select sid,id,name,age,sit_ups,sit_ups_score,push_ups,push_ups_score,run,run_score,date,result,status,clothesNum,memo from Result where date between '" + StartTime + "' and '" + EndTime + "' and status in ('102','103','105')";
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count > 0)
                {
                    dgv_Result.DataSource = dt;
                    dgv_Result.DataBind();
                    lab_Count.Text = "成功讀取未上傳資料「" + dt.Rows.Count.ToString() + "」筆";
                    //MessageBox.Show("資料讀取完成，共計：「" + dt.Rows.Count + "」筆。");
                }
                else
                {
                    lab_Count.Text = "查無資料";
                    //MessageBox.Show("查無資料");
                }
            }
            catch (Exception ex)
            {
                lab_Count.Text = ex.Message;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }    
        //}
        //else if (dialogResult == DialogResult.No)
        //{
        //    //do something else
        //}
    }

    protected void btn_no_upload_Click(object sender, EventArgs e)//查詢未上傳資料筆數
    {
        try
        {
            lab_Count.Text = null;
            dgv_Result.DataSource = null;
            dgv_Result.DataBind();
            DataTable dt = new DataTable();
            dt = Score_count(txb_StartTime.Text, txb_EndTime.Text, txb_ConnString.Text);
            string Count102 = "0";
            string Count103 = "0";
            string Count105 = "0";
            string Count102_666 = "0";
            string Count103_666 = "0";
            string Count105_666 = "0";
            string total = "0";
            Count102 = dt.Rows[0]["102"].ToString();
            Count103 = dt.Rows[0]["103"].ToString();
            Count105 = dt.Rows[0]["105"].ToString();
            Count102_666 = dt.Rows[0]["102_666"].ToString();
            Count103_666 = dt.Rows[0]["103_666"].ToString();
            Count105_666 = dt.Rows[0]["105_666"].ToString();
            total = dt.Rows[0]["total"].ToString();
            lab_102.Text = Count102;
            lab_103.Text = Count103;
            lab_105.Text = Count105;
            lab_102_666.Text = Count102_666;
            lab_103_666.Text = Count103_666;
            lab_105_666.Text = Count105_666;
            lab_total.Text = total;
            lab_Count.Text = "查詢未上傳資料筆數完成";
        }
        catch (Exception ex)
        {
            lab_Count.Text = ex.Message;
        }
    }

    protected void btn_105_to_103_Click(object sender, EventArgs e)//將105補測轉換成103
    {
        lab_Count.Text = null;
        SqlConnection con = new SqlConnection(txb_ConnString.Text);
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        string StartTime = string.Empty;
        string EndTime = string.Empty;
        StartTime = txb_StartTime.Text;
        EndTime = txb_EndTime.Text;
        DataTable dt = new DataTable();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "update Result set status='103' where date between '" + StartTime + "' and '" + EndTime + "' and status = '105' ";
        try
        {
            int count_666 = 0;
            count_666 = cmd.ExecuteNonQuery();
            lab_Count.Text = "105補測更新狀態為103不合格：「" + count_666.ToString() + "」 筆，";
        }
        catch (Exception ex)
        {
            lab_Count.Text = "更新補測人員狀態：" + ex.Message;
        }
        finally
        {
            con.Close();
            con.Dispose();
            btn_no_upload_Click(btn_no_upload, e);
        }
    }
    #endregion
    #region 成績上傳
    protected void btn_Upload_102_Click(object sender, EventArgs e)//102合格成績上傳
    {
        //DialogResult dialogResult = MessageBox.Show("確認批次上傳「合格」成績?", "確認Yes/No", MessageBoxButtons.YesNo);
        //if (dialogResult == DialogResult.Yes)
        //{
            lab_Count.Text = null;
            string data_count = "1";
            data_count = txb_P_count.Text;//上傳筆數
            Lib.DataUtility du = new Lib.DataUtility();
            DataTable dt = du.getDataTableByText("select top " + data_count + " * from result where status = @status and result is NULL and date between '" + txb_StartTime.Text + "' and '" + txb_EndTime.Text + "' order by date", "status", "102"); // 102 未上傳合格
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有「合格」成績');", true);
            }
            else
            {
                //上傳更新總部資料
                dt.TableName = "upload";
                MainWS.WebService MainWs = new MainWS.WebService();
                string msg = MainWs.UploadResult(dt, "102");
                if (msg == "done")
                {
                    List<Dictionary<string, object>> list_u = new List<Dictionary<string, object>>();
                    foreach (DataRow row in dt.Rows)
                    {
                        Dictionary<string, object> d_u = new Dictionary<string, object>();
                        d_u.Add("sid", row["sid"]);
                        list_u.Add(d_u);
                    }
                    try
                    {
                        // 更新鑑測站資料狀態
                        du.executeNonQueryByText("update result set status = '202' where sid = @sid", list_u);
                        dt.Dispose();
                        list_u.Clear();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('上傳合格成績成功');", true);
                        lab_Count.Text = "成功上傳合格成績共計「" + dt.Rows.Count + "」筆";
                    }
                    catch (Exception ex)
                    {
                        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + msg + "\");", true);
                }
            }
            btn_no_upload_Click(btn_no_upload, e);
        //}
        //else if (dialogResult == DialogResult.No)
        //{
        //    //do something else
        //}
    }
    protected void btn_Upload_103_Click(object sender, EventArgs e)//103不合格成績上傳
    {
        //DialogResult dialogResult = MessageBox.Show("確認批次上傳「不合格」成績?", "確認Yes/No", MessageBoxButtons.YesNo);
        //if (dialogResult == DialogResult.Yes)
        //{
            lab_Count.Text = null;
            string data_count = "1";
            data_count = txb_P_count.Text;//上傳筆數
            Lib.DataUtility du = new Lib.DataUtility();
            DataTable dt = du.getDataTableByText("select top " + data_count + " * from result where status = @status and result is NULL and date between '" + txb_StartTime.Text + "' and '" + txb_EndTime.Text + "' order by date", "status", "103"); // 103 未上傳不合格
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有「不合格」成績');", true);
            }
            else
            {
                //上傳更新總部資料
                dt.TableName = "upload";
                MainWS.WebService MainWs = new MainWS.WebService();
                string msg = MainWs.UploadResult(dt, "103");
                if (msg == "done")
                {
                    List<Dictionary<string, object>> list_u = new List<Dictionary<string, object>>();
                    foreach (DataRow row in dt.Rows)
                    {
                        Dictionary<string, object> d_u = new Dictionary<string, object>();
                        d_u.Add("sid", row["sid"]);
                        list_u.Add(d_u);
                    }
                    try
                    {
                        // 更新鑑測站資料狀態
                        du.executeNonQueryByText("update result set status = '203' where sid = @sid", list_u);
                        dt.Dispose();
                        list_u.Clear();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('上傳不合格成績成功');", true);
                        lab_Count.Text = "成功上傳不合格成績共計「" + dt.Rows.Count + "」筆";
                    }
                    catch (Exception ex)
                    {
                        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + msg + "\");", true);
                }
            }
            btn_no_upload_Click(btn_no_upload, e);
        //}
        //else if (dialogResult == DialogResult.No)
        //{
        //    //do something else
        //}
    }
    protected void btn_Upload_105_Click(object sender, EventArgs e)//105補測成績上傳
    {
        //DialogResult dialogResult = MessageBox.Show("確認批次上傳「補測」成績?", "確認Yes/No", MessageBoxButtons.YesNo);
        //if (dialogResult == DialogResult.Yes)
        //{
            lab_Count.Text = null;
            string data_count = "1";
            data_count = txb_P_count.Text;//上傳筆數
            Lib.DataUtility du = new Lib.DataUtility();
            DataTable dt = du.getDataTableByText("select top " + data_count + " * from result where status = @status and result is NULL and date between '" + txb_StartTime.Text + "' and '" + txb_EndTime.Text + "' order by date", "status", "105"); // 105 未上傳補測
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有「補測」成績');", true);
            }
            else
            {
                //上傳更新總部資料
                dt.TableName = "upload";
                MainWS.WebService MainWs = new MainWS.WebService();
                string msg = MainWs.UploadResult(dt, "105");
                if (msg == "done")
                {
                    List<Dictionary<string, object>> list_u = new List<Dictionary<string, object>>();
                    foreach (DataRow row in dt.Rows)
                    {
                        Dictionary<string, object> d_u = new Dictionary<string, object>();
                        d_u.Add("sid", row["sid"]);
                        list_u.Add(d_u);
                    }
                    try
                    {
                        // 更新鑑測站資料狀態
                        du.executeNonQueryByText("update result set status = '205' where sid = @sid", list_u);
                        dt.Dispose();
                        list_u.Clear();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('上傳補測成績成功');", true);
                        lab_Count.Text = "成功上傳補測成績共計「" + dt.Rows.Count + "」筆";
                    }
                    catch (Exception ex)
                    {
                        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + msg + "\");", true);
                }
            }
            btn_no_upload_Click(btn_no_upload, e);
        //}
        //else if (dialogResult == DialogResult.No)
        //{
        //    //do something else
        //}
    }
    protected void btn_Upload_102_666_Click(object sender, EventArgs e)//102合格成績上傳(現報)
    {
        //DialogResult dialogResult = MessageBox.Show("確認批次上傳「合格」成績(現報)?", "確認Yes/No", MessageBoxButtons.YesNo);
        //if (dialogResult == DialogResult.Yes)
        //{
            lab_Count.Text = null;
            string data_count = "1";
            data_count = txb_P_count.Text;//上傳筆數
            Lib.DataUtility du = new Lib.DataUtility();
            DataTable dt = du.getDataTableByText("select top " + data_count + " * from result where status = @status and result = '666' and date between '" + txb_StartTime.Text + "' and '" + txb_EndTime.Text + "' order by date", "status", "102"); // 102 未上傳合格
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有「合格」成績(現報)');", true);
            }
            else
            {
                //上傳更新總部資料
                dt.TableName = "upload";
                MainWS.WebService MainWs = new MainWS.WebService();
                string msg = MainWs.UploadResult(dt, "present");
                if (msg == "done")
                {
                    List<Dictionary<string, object>> list_u = new List<Dictionary<string, object>>();
                    foreach (DataRow row in dt.Rows)
                    {
                        Dictionary<string, object> d_u = new Dictionary<string, object>();
                        d_u.Add("id", row["id"]);
                        list_u.Add(d_u);
                    }
                    try
                    {
                        // 更新鑑測站資料狀態
                        du.executeNonQueryByText("update result set status = '202' , result = '777' where id = @id and result = '666' and status = '102' ", list_u);
                        dt.Dispose();
                        list_u.Clear();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('上傳合格成績成功(現報)');", true);
                        lab_Count.Text = "成功上傳合格成績(現報)共計「" + dt.Rows.Count + "」筆";
                    }
                    catch (Exception ex)
                    {
                        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + msg + "\");", true);
                }
            }
            btn_no_upload_Click(btn_no_upload, e);
        //}
        //else if (dialogResult == DialogResult.No)
        //{
        //    //do something else
        //}
    }
    protected void btn_Upload_103_666_Click(object sender, EventArgs e)//103不合格成績上傳(現報)
    {
        //DialogResult dialogResult = MessageBox.Show("確認批次上傳「不合格」成績(現報)?", "確認Yes/No", MessageBoxButtons.YesNo);
        //if (dialogResult == DialogResult.Yes)
        //{
            lab_Count.Text = null;
            string data_count = "1";
            data_count = txb_P_count.Text;//上傳筆數
            Lib.DataUtility du = new Lib.DataUtility();
            DataTable dt = du.getDataTableByText("select top " + data_count + " * from result where status = @status and result = '666' and date between '" + txb_StartTime.Text + "' and '" + txb_EndTime.Text + "' order by date", "status", "103"); // 103 未上傳不合格
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有「不合格」成績(現報)');", true);
            }
            else
            {
                //上傳更新總部資料
                dt.TableName = "upload";
                MainWS.WebService MainWs = new MainWS.WebService();
                string msg = MainWs.UploadResult(dt, "present");
                if (msg == "done")
                {
                    List<Dictionary<string, object>> list_u = new List<Dictionary<string, object>>();
                    foreach (DataRow row in dt.Rows)
                    {
                        Dictionary<string, object> d_u = new Dictionary<string, object>();
                        d_u.Add("id", row["id"]);
                        list_u.Add(d_u);
                    }
                    try
                    {
                        // 更新鑑測站資料狀態
                        du.executeNonQueryByText("update result set status = '203' , result = '777' where id = @id and result = '666' and status = '103'", list_u);
                        dt.Dispose();
                        list_u.Clear();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('上傳不合格成績成功(現報)');", true);
                        lab_Count.Text = "成功上傳不合格成績(現報)共計「" + dt.Rows.Count + "」筆";
                    }
                    catch (Exception ex)
                    {
                        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + msg + "\");", true);
                }
            }
            btn_no_upload_Click(btn_no_upload, e);
        //}
        //else if (dialogResult == DialogResult.No)
        //{
        //    //do something else
        //}
    }
    protected void btn_Upload_105_666_Click(object sender, EventArgs e)//105補測成績上傳(現報)
    {
        //DialogResult dialogResult = MessageBox.Show("確認批次上傳「補測」成績(現報)?", "確認Yes/No", MessageBoxButtons.YesNo);
        //if (dialogResult == DialogResult.Yes)
        //{
            lab_Count.Text = null;
            string data_count = "1";
            data_count = txb_P_count.Text;//上傳筆數
            Lib.DataUtility du = new Lib.DataUtility();
            DataTable dt = du.getDataTableByText("select top " + data_count + " * from result where status = @status and result = '666' and date between '" + txb_StartTime.Text + "' and '" + txb_EndTime.Text + "' order by date", "status", "105"); // 105 未上傳補測
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('目前沒有「補測」成績(現報)');", true);
            }
            else
            {
                //上傳更新總部資料
                dt.TableName = "upload";
                MainWS.WebService MainWs = new MainWS.WebService();
                string msg = MainWs.UploadResult(dt, "present");
                if (msg == "done")
                {
                    List<Dictionary<string, object>> list_u = new List<Dictionary<string, object>>();
                    foreach (DataRow row in dt.Rows)
                    {
                        Dictionary<string, object> d_u = new Dictionary<string, object>();
                        d_u.Add("id", row["id"]);
                        list_u.Add(d_u);
                    }
                    try
                    {
                        // 更新鑑測站資料狀態
                        du.executeNonQueryByText("update result set status = '205' , result = '777' where id = @id and result = '666' ", list_u);
                        dt.Dispose();
                        list_u.Clear();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('上傳補測成績成功(現報)');", true);
                        lab_Count.Text = "成功上傳補測成績(現報)共計「" + dt.Rows.Count + "」筆";
                    }
                    catch (Exception ex)
                    {
                        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + ex.Message + "\");", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(\"" + msg + "\");", true);
                }
            }
            btn_no_upload_Click(btn_no_upload, e);
        //}
        //else if (dialogResult == DialogResult.No)
        //{
        //    //do something else
        //}
    }
    
    #endregion


}