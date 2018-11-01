using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lib;

public partial class AdminUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin"] == null && (string)Session["admin"] != "true")
        {
            Response.Redirect("~/Index.aspx");
        }
        else
        {

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            new Lib.DataUtility().executeNonQueryByText(@"update Result set status = '1' + SUBSTRING(status , 2, 2) where SUBSTRING(status , 1 ,1) = '2' and SUBSTRING(status, 3, 1) = '4'");
            Label1.Text = "update done!!!";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        }
        

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            new Lib.DataUtility().executeNonQueryByText(@"update result set status = '1' + substring(status, 2, 2) where id = 'L124807510'");
            Label1.Text = "update done!!!";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            new Lib.DataUtility().executeNonQueryByText(@"update ReplaceStandard set start = 0 where start = 19");
            new Lib.DataUtility().executeNonQueryByText(@"update ReplaceStandard set [end] = 99 where [end] = 59");
            Label1.Text = "update done!!!";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            new Lib.DataUtility().executeNonQueryByText(@"update result set age = DATEDIFF(yyyy, birth, getdate()) - 
		CASE WHEN CONVERT(nvarchar(20), '2012-12-04', 126) < DATEADD(yyyy, DATEDIFF(yy, birth, CONVERT(nvarchar(20), '2012-12-04', 126)), birth)   
            THEN 1   
            ELSE 0   
        END , [date] = CONVERT(nvarchar(20), '2012-12-04', 126), status = '1' + SUBSTRING(status, 2, 2) where  unit_code in ('63471','63355','31R43','150N4')");
            Label1.Text = "update done!!!";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add("id", TextBox1.Text.Trim());
            new Lib.DataUtility().executeNonQueryByText(@"update Result set status = '1' + SUBSTRING(status , 2, 2) where SUBSTRING(status , 1 ,1) = '2' and id = @id", d);
            Label1.Text = "update done!!!";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        }
    }
}
