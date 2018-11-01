using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ReportingSumByItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["account"] != null)
            {
                // 設定預設日期
                date_start.Value = (DateTime.Now.Year - 1911).ToString() + "/1/1";
                date_stop.Value = (DateTime.Now.Year - 1911).ToString() + "/12/31";

                #region 鑑測站程式碼
                DataTable dt = new DataTable();
                dt.Columns.Add("單位名稱");
                dt.Columns.Add("單位代碼");
                DataRow row = null;
                Lib.Unit unit = new Lib.UnitTree().GetUnitWithChild("00001");
                if (unit.ChildUnit != null && unit.ChildUnit.Count != 0)
                {
                    foreach (KeyValuePair<string, Lib.Unit> pair in unit.ChildUnit)
                    {
                        Lib.Unit child = pair.Value as Lib.Unit;
                        row = dt.NewRow();
                        row[0] = child.Unit_Title;
                        row[1] = child.Unit_Code;
                        dt.Rows.Add(row);
                    }
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
                #endregion

                #region 入口網站的程式碼
                //DropDownList2.Visible = false;
                //Lib.Center.Account_c acc = Session["account"] as Lib.Center.Account_c;
                //ListItem item = null;
                //Lib.Unit unit = new Lib.UnitTree().GetUnitWithChild(acc.Unit_Code);
                //item = new ListItem();
                //item.Text = unit.Unit_Title;
                //item.Value = unit.Unit_Code;
                //DropDownList1.Items.Add(item);
                //if (unit.ChildUnit != null && unit.ChildUnit.Count != 0)
                //{
                //    foreach (KeyValuePair<string, Lib.Unit> pair in unit.ChildUnit)
                //    {
                //        Lib.Unit child = pair.Value as Lib.Unit;
                //        item = new ListItem();
                //        item.Text = child.Unit_Title;
                //        item.Value = child.Unit_Code;
                //        DropDownList1.Items.Add(item);
                //    }
                //}
                #endregion
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem item = null;
        Lib.Unit unit = new Lib.UnitTree().GetUnitWithChild(DropDownList1.SelectedValue);
        DropDownList2.Items.Clear();
        DropDownList3.Items.Clear();
        DropDownList4.Items.Clear();
        DropDownList5.Items.Clear();
        if (unit.ChildUnit != null && unit.ChildUnit.Count != 0)
        {
            foreach (KeyValuePair<string, Lib.Unit> pair in unit.ChildUnit)
            {
                Lib.Unit child = pair.Value as Lib.Unit;
                item = new ListItem();
                item.Text = child.Unit_Title;
                item.Value = child.Unit_Code;
                DropDownList2.Items.Add(item);
            }
        }
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem item = null;
        Lib.Unit unit = new Lib.UnitTree().GetUnitWithChild(DropDownList2.SelectedValue);
        DropDownList3.Items.Clear();
        DropDownList4.Items.Clear();
        DropDownList5.Items.Clear();
        if (unit.ChildUnit != null && unit.ChildUnit.Count != 0)
        {
            foreach (KeyValuePair<string, Lib.Unit> pair in unit.ChildUnit)
            {
                Lib.Unit child = pair.Value as Lib.Unit;
                item = new ListItem();
                item.Text = child.Unit_Title;
                item.Value = child.Unit_Code;
                DropDownList3.Items.Add(item);
            }
            if (unit.ChildUnit.Count == 1)
            {
                Lib.Unit onlyChild = new Lib.UnitTree().GetUnitWithChild(DropDownList3.Items[0].Value);
                if (onlyChild.ChildUnit != null && onlyChild.ChildUnit.Count != 0)
                {
                    foreach (KeyValuePair<string, Lib.Unit> pair in onlyChild.ChildUnit)
                    {
                        Lib.Unit child = pair.Value as Lib.Unit;
                        item = new ListItem();
                        item.Text = child.Unit_Title;
                        item.Value = child.Unit_Code;
                        DropDownList4.Items.Add(item);
                    }
                }
            }
        }
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem item = null;
        Lib.Unit unit = new Lib.UnitTree().GetUnitWithChild(DropDownList3.SelectedValue);
        DropDownList4.Items.Clear();
        DropDownList5.Items.Clear();
        if (unit.ChildUnit != null && unit.ChildUnit.Count != 0)
        {

            foreach (KeyValuePair<string, Lib.Unit> pair in unit.ChildUnit)
            {
                Lib.Unit child = pair.Value as Lib.Unit;
                item = new ListItem();
                item.Text = child.Unit_Title;
                item.Value = child.Unit_Code;
                DropDownList4.Items.Add(item);
            }
            if (unit.ChildUnit.Count == 1)
            {

                Lib.Unit onlyChild = new Lib.UnitTree().GetUnitWithChild(DropDownList4.Items[0].Value);
                if (onlyChild.ChildUnit != null && onlyChild.ChildUnit.Count != 0)
                {
                    foreach (KeyValuePair<string, Lib.Unit> pair in onlyChild.ChildUnit)
                    {
                        Lib.Unit child = pair.Value as Lib.Unit;
                        item = new ListItem();
                        item.Text = child.Unit_Title;
                        item.Value = child.Unit_Code;
                        DropDownList5.Items.Add(item);
                    }
                }
            }

        }
    }

    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem item = null;
        Lib.Unit unit = new Lib.UnitTree().GetUnitWithChild(DropDownList4.SelectedValue);
        DropDownList5.Items.Clear();
        if (unit.ChildUnit != null && unit.ChildUnit.Count != 0)
        {

            foreach (KeyValuePair<string, Lib.Unit> pair in unit.ChildUnit)
            {
                Lib.Unit child = pair.Value as Lib.Unit;
                item = new ListItem();
                item.Text = child.Unit_Title;
                item.Value = child.Unit_Code;
                DropDownList5.Items.Add(item);
            }

        }
    }

    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("單位名稱");
        dt.Columns.Add("單位代碼");
        DataRow row = null;
        if (DropDownList5.Items.Count == 0)
        {
            if (DropDownList4.Items.Count == 0)
            {
                if (DropDownList3.Items.Count == 0)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('" + DropDownList1.SelectedItem.Text + "');", true);
                    titleUnit.Value = DropDownList1.SelectedItem.Text;
                    foreach (ListItem items in DropDownList2.Items)
                    {
                        row = dt.NewRow();
                        row[0] = items.Text;
                        row[1] = items.Value;
                        dt.Rows.Add(row);
                    }
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                }

                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('" + DropDownList2.SelectedItem.Text + "');", true);
                    titleUnit.Value = DropDownList2.SelectedItem.Text;
                    foreach (ListItem items in DropDownList3.Items)
                    {
                        row = dt.NewRow();
                        row[0] = items.Text;
                        row[1] = items.Value;
                        dt.Rows.Add(row);
                    }
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('" + DropDownList3.SelectedItem.Text + "');", true);
                titleUnit.Value = DropDownList3.SelectedItem.Text;
                foreach (ListItem items in DropDownList4.Items)
                {
                    row = dt.NewRow();
                    row[0] = items.Text;
                    row[1] = items.Value;
                    dt.Rows.Add(row);
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
        else
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('" + DropDownList4.SelectedItem + "');", true);
            titleUnit.Value = DropDownList4.SelectedItem.Text;
            foreach (ListItem items in DropDownList5.Items)
            {
                row = dt.NewRow();
                row[0] = items.Text;
                row[1] = items.Value;
                dt.Rows.Add(row);
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }

    void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Lib.SysSetting.ExceptionLog(ex.GetType().ToString(), ex.Message, sender.ToString());
        Server.ClearError();
    }
}
