using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using Lib;

public partial class _108_ScoreTrial : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    protected void btn_Inq_Click(object sender, EventArgs e)
    {
        string gender = string.Empty;//性別
        string age = string.Empty;//年齡
        string item = string.Empty;//項目代碼
        string itemName = string.Empty;//項目名稱
        string grade = string.Empty;//成績
        string score = string.Empty;//分數

        lab_gender.Text = null;
        lab_age.Text = null;
        lab_item.Text = null;
        lab_grade.Text = null;

        if (!string.IsNullOrEmpty(hf_gender.Value) && !string.IsNullOrEmpty(hf_age.Value) && !string.IsNullOrEmpty(hf_item.Value))
        {
            gender = hf_gender.Value;
            age = hf_age.Value;
            item = hf_item.Value;
            itemName = hf_itemName.Value;
            grade = hf_grade.Value;
            lab_gender.Text = (gender == "M") ? "男性" : "女性";
            lab_gender.ForeColor = (gender == "M") ? Color.Blue : Color.Red;
            lab_age.Text = age;
            lab_item.Text = itemName;

            if (item == "run" || item == "F" || item == "G" || item == "J")
            {
                if (Convert.ToInt16(grade) >= 60)
                {
                    lab_grade.Text = (Convert.ToInt16(grade) / 60).ToString() + "分" + (Convert.ToInt16(grade) % 60).ToString() + "秒";
                }
                else
                {
                    lab_grade.Text = grade + "秒";
                }
            }
            else
            {
                lab_grade.Text = grade + "次";
            }
            bool checkOK = false;
            try
            {
                
                Dictionary<string, object> di = new Dictionary<string, object>();
                DataUtility du = new DataUtility();
                DataTable dt = new DataTable();
                di.Add("gender", gender);
                di.Add("age", age);
                di.Add("grade", grade);
                di.Add("item", item);
                dt = du.getDataTableBysp("Ex108_GetScoreTrial", di);
                if (dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[0]["score"].ToString()))
                    {
                        score = dt.Rows[0]["score"].ToString();
                        checkOK = true;
                    }                 
                    else
                        score = "查無分數";
                }
                else
                {
                    score = "查無分數";
                }
            }
            catch(Exception ex)
            {
                score = "查詢失敗";
            }
            finally
            {
                lab_score.Text = score;
                if (checkOK == true)
                {
                    lab_score.ForeColor = (Convert.ToInt16(score) >= 60) ? Color.Green : Color.Red;
                }
            }
            
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('輸入資料不完整!!');", true);
        }

    }
}