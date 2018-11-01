using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class DemoTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MainWS.WebService m = new MainWS.WebService();
        Lib.DataUtility du = new Lib.DataUtility();
        DataTable dt = du.getDataTableByText("select unit_title from unit where parent_unit_code = @code", "code", "10001");
        dt.TableName = "toAdd";
        string toget = m.Datatable(dt);
    }
}
