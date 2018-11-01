using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Race_FindPerson : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Account"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = new Lib.DataUtility().getDataTableByText("select id [身分證號], name [姓名], dbo.F_GetUnitTitle(unit_code) [單位], status [狀態], result from result where id = '" + TextBox1.Text + "'");
            GridView1.DataSource = dt;
            GridView1.DataBind();
            image1.ImageUrl = @"";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (TextBox2.Text != "")
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = new Lib.DataUtility().getDataTableByText("select id [身分證號], name [姓名], dbo.F_GetUnitTitle(unit_code) [單位], status [狀態], result from result where name like '%" + TextBox2.Text.Trim() + "%'");
            GridView1.DataSource = dt;
            GridView1.DataBind();
            image1.ImageUrl = @"";
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text == "999")
            {
                e.Row.Cells[3].Text = "未檢錄";
                return;
            }
            if (e.Row.Cells[3].Text == "001")
            {
                e.Row.Cells[3].Text = "已檢錄";
                return;
            }
            if (e.Row.Cells[3].Text.Substring(2, 1) == "2")
            {
                e.Row.Cells[3].Text = "合格";
                return;
            }
            if (e.Row.Cells[3].Text.Substring(2, 1) == "3")
            {
                e.Row.Cells[3].Text = "不合格";
                return;
            }

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (txtIndexID.Text != "" && txtName.Text != "")
        {
            try
            {
                new Lib.DataUtility().executeNonQueryByText("update result set name = '" + txtName.Text.Trim() + "' where id = '" + txtIndexID.Text.Trim() + "'");
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('更新完畢');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "');", true);
            }
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (txtIndexID.Text != "" && txtID.Text != "")
        {
            try
            {
                new Lib.DataUtility().executeNonQueryByText("update result set id = '" + txtID.Text.Trim() + "' where id = '" + txtIndexID.Text.Trim() + "'");
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('更新完畢');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "');", true);
            }
        }

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        if (txtResetID.Text != "")
        {
            try
            {
                string sqlcmd = string.Empty;
                sqlcmd = "update result set status = '999', age = null, clothesNum = null, LF_Tag_ID = null, UHF_Tag_ID = null, code = null, photo = null, sit_ups = null, push_ups = null, run = null,op_id = null, memo = null where id = @id";
                new Lib.DataUtility().executeNonQueryByText(sqlcmd, "@id", txtResetID.Text.Trim());
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('該員已經可以重新檢錄 !');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "');", true);
            }
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        Lib.DataUtility du = new Lib.DataUtility();
        string sqlcmd = string.Empty;
        if (txtID_BMI.Text.Trim() != "" && txtCode.Text.Trim() != "" && txtCloNO.Text.Trim() != "")
        {
            try
            {
                sqlcmd = "select id from result where status in ('113','123') and id = @id";
                dt = du.getDataTableByText(sqlcmd, "@id", txtID_BMI.Text.Trim());
                if (dt.Rows.Count != 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('BMI不合格出現筆數錯誤');", true);
                }
                else
                {
                    // 有比對出資料
                    #region
                    dt.Clear();
                    sqlcmd = "select code from result where code = @code and status = '001'";
                    dt = du.getDataTableByText(sqlcmd, "@code", txtCode.Text.Trim());
                    if (dt.Rows.Count != 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('該晶片使用中');", true);
                    }
                    else
                    {
                        dt.Clear();
                        sqlcmd = "select * from rfid where code = @code";
                        dt = du.getDataTableByText(sqlcmd, "@code", txtCode.Text.Trim());
                        if (dt.Rows.Count != 1)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('該晶片內碼出現筆數錯誤');", true);
                        }
                        else
                        {
                            string lf_tag = dt.Rows[0]["LF_Tag_ID"].ToString();
                            string uhf_tag = dt.Rows[0]["UHF_Tag_ID"].ToString();
                            string memo = string.Empty;
                            //memo = DropDownList1.SelectedValue + DropDownList2.SelectedValue + DropDownList3.SelectedValue;

                            Dictionary<string, object> list = new Dictionary<string, object>();
                            list.Add("@id", txtID_BMI.Text.Trim());
                            list.Add("@code", txtCode.Text.Trim());
                            list.Add("@clothesNum", txtCloNO.Text.Trim());
                            list.Add("@LF_Tag_ID", lf_tag);
                            list.Add("@UHF_Tag_ID", uhf_tag);
                            sqlcmd = "update result set [status] = '001', [code] = @code, [clothesNum] = @clothesNum, [LF_Tag_ID] = @LF_Tag_ID, [UHF_Tag_ID] = @UHF_Tag_ID where id = @id";
                            du.executeNonQueryByText(sqlcmd, list);
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('該員重新檢錄完畢');", true);
                            // FOR 官校
                            #region FOR 官校
                            if (Lib.SysSetting.CenterCode == "9")
                            {
                                try
                                {
                                    dt.Clear();
                                    dt = du.getDataTableByText("select * from result where id = @id", "@id", txtID_BMI.Text.Trim());
                                    dt.TableName = "forrunadd";
                                    RemoteWS.WebService ws = new RemoteWS.WebService();
                                    ws.Url = "http://10.116.53.41/Webservice.asmx";
                                    ws.Discover();
                                    ws.AddResultFor3KRun(dt);
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('該員重新檢錄完畢');", true);
                                }
                                catch (Exception ex)
                                {
                                    sqlcmd = "update result set [op_id] = 'Fail' where id = @id";
                                    du.executeNonQueryByText(sqlcmd, "@id", txtID_BMI.Text.Trim());
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('該員重新檢錄完畢，惟傳送受測資料至步校出現錯誤，請檢錄站人員稍後重新傳送');", true);
                                }
                            }
                            #endregion
                        }
                    }

                    #endregion

                }
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message.Replace("'", "").Replace(@"/", "").Replace(@"\","");
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + msg + "')", true);
            }

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('資料未輸入完畢');", true);
        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        try
        {
            string memo = DropDownList1.SelectedValue + DropDownList2.SelectedValue + DropDownList3.SelectedValue;
            if (txtID_repl.Text != "")
            {
                new Lib.DataUtility().executeNonQueryByText("update result set memo = '" + memo + "' where id = '" + txtID_repl.Text.Trim() + "'");
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('完畢');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "');", true);
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            Lib.DataUtility du = new Lib.DataUtility();
            Dictionary<string, object> d = new Dictionary<string, object>();
            if (TextBox3.Text.Trim().Length == 10)
            {
                d.Add("id", TextBox3.Text.Trim());
                dt = du.getDataTableByText(@"select top 1 id [身分證字號], name [姓名], dbo.F_GetUnitTitle(unit_code) [單位], dbo.F_GetStatusMeaning(status) [狀態], clothesNum [背號], dbo.F_GetROCDate(date) [鑑測日期],
                (case when substring(memo,1,1) = '0' then (case when sit_ups is null then '未測' else CONVERT(nvarchar(10),sit_ups)end)
			  when substring(memo,1,1) != '0'then (select rep_title from repment where sid = substring(memo,1,1)) + (case when sit_ups is not null then [dbo].[F_GetReplMentFormate] (substring(memo,1,1),sit_ups) when sit_ups is null then '未測' else '' end) end) as [仰臥起坐],
                (case when substring(memo,2,1) = '0' then (case when push_ups is null then '未測' else CONVERT(nvarchar(10),push_ups)end)
			  when substring(memo,2,1) != '0'then (select rep_title from repment where sid = substring(memo,2,1)) + (case when push_ups is not null then [dbo].[F_GetReplMentFormate] (substring(memo,2,1),push_ups) when push_ups is null then '未測' else '' end) end) as [伏地挺身],
                (case when substring(memo,3,1) = '0' then (case when run is null and (run_score = 9999 or run_score is null) then '未測' when run = 9999 and (run_score = 9999 or run_score is null) then '-' else CONVERT(nvarchar(10),run/60)+':'+CONVERT(nvarchar(10),run%60) end)
			  when substring(memo,3,1) != '0'then (select rep_title from repment where sid = substring(memo,3,1)) + (case when run is not null then[dbo].[F_GetReplMentFormate] (substring(memo,3,1),run) when run is null then '未測' else '' end) end) as [三千公尺], result from result where id = @id", d);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                image1.ImageUrl = @"~/ImageHandler.ashx?id=" + TextBox3.Text.Trim() + @"&date=" +  dt.Rows[0][5].ToString().Replace('.','/');
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('身分證字號長度錯誤');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "');", true);
        }
    }
}
