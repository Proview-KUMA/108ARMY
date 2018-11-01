using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Race_StatPerson : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //GridView1.DataSource = new Lib.DataUtility().getDataTableBysp("Race_StatPerson");
        
        //GridView1.DataBind();

        SqlConnection con = new SqlConnection(new Lib.DataUtility().connectionString);
        try
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Race_StatPerson";
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet myDS = new DataSet();
            adp.Fill(myDS);
            if (myDS.Tables.Count == 4)
            {
                DataRow r0 = myDS.Tables[0].NewRow();
                r0[0] = "男子甲組(40歲以上)";
                myDS.Tables[0].Rows.InsertAt(r0, 0);
                DataRow rr = myDS.Tables[0].NewRow();
                rr[0] = "男子乙組(30-40歲)";
                myDS.Tables[0].Rows.Add(rr);
                foreach (DataRow row in myDS.Tables[1].Rows)
                {
                    myDS.Tables[0].ImportRow(row);
                    //myDS.Tables[0].Rows.Add(row);
                }
                DataRow r1 = myDS.Tables[0].NewRow();
                r1[0] = "男子丙組(30歲以下)";
                myDS.Tables[0].Rows.Add(r1);
                foreach (DataRow row in myDS.Tables[2].Rows)
                {
                    myDS.Tables[0].ImportRow(row);
                    //myDS.Tables[0].Rows.Add(row);
                }
                DataRow r2 = myDS.Tables[0].NewRow();
                r2[0] = "女子組";
                myDS.Tables[0].Rows.Add(r2);
                foreach (DataRow row in myDS.Tables[3].Rows)
                {
                    myDS.Tables[0].ImportRow(row);
                    //myDS.Tables[0].Rows.Add(row);
                }

                GridView1.DataSource = myDS.Tables[0];
                GridView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "');", true);
        }
        finally
        {
            con.Close();
        }


    }
}
