using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QueryNonHandle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        today.Text = Lib.SysSetting.ToRocDateFormat(System.DateTime.Today.ToLongDateString());
        yesterday.Text = Lib.SysSetting.ToRocDateFormat(System.DateTime.Today.AddDays(-1).ToLongDateString());
    }   
}
