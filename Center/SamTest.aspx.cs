using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class SamTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        #region 測試抄寫三千成績資料
        Lib.DataUtility du = new Lib.DataUtility();
        DataTable dt = new DataTable();
        dt = du.getDataTableByText("select * from resulted where id in('AA00000001')");
        SqlConnection con = new SqlConnection(du.connectionString);
        try
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Race_UpdateResult3KRun";
            cmd.Parameters.AddWithValue("result", dt);
            int check = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {

        }
        #endregion
    }
}
