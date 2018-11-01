using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //Dictionary<string , object> d = new Dictionary<string,object>();
        //Lib.DataUtility du = new Lib.DataUtility();
        //float _weight = 60;
        //float _height = 170;
        //float _BMI = 20;
        //string _UHF_Tag_ID = "AA020101";
        //string _LF_Tag_ID = "0108F23970";
        //string TagIDText = "B001";
        //d.Add("id", "P150950941");
        //DataTable dt_photo = du.getDataTableByText("select photo from result where id = @id", d);
        //d.Clear();
        //d.Add("status", "999");
        //DataTable dt = du.getDataTableByText("select id from result where status = @status", d);
        //try
        //{
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        d.Clear();
        //        d.Add("weight", _weight);
        //        d.Add("height", _height);
        //        d.Add("BMI", _BMI);
        //        d.Add("clothesNum", i);
        //        d.Add("LF_Tag_ID", _LF_Tag_ID);
        //        d.Add("UHF_Tag_ID", _UHF_Tag_ID);
        //        d.Add("code", TagIDText);
        //        d.Add("id", dt.Rows[i]["id"]);
        //        d.Add("date", System.DateTime.Today);
        //        d.Add("photo", dt_photo.Rows[0]["photo"]);
        //        du.executeNonQueryByText("update Result set photo = @photo, height = @height , weight = @weight , BMI = @BMI , clothesNum = @clothesNum , LF_Tag_ID = @LF_Tag_ID , UHF_Tag_ID = @UHF_Tag_ID , code =@code , status = '001' where id = @id and status = '999' and date = @date ", d);
        //        Response.Write("成功更新" + i);
        //    }
        //    Response.Write("成功更新" + dt.Rows.Count + "筆資料");
        //}
        //catch (Exception ex)
        //{
        //    Response.Write("error!!");
        //}

        //d.Add("date" , DateTime.Today);
        //d.Add("status" , "001");
        //DataTable dt = du.getDataTableByText("select id from result where status = @status and date = @date ", d);
        //for (int i = 0; i < 50; i++)
        //{
        //    d.Clear();
        //    d.Add("sit_ups", new Random().Next(60 , 100));
        //    d.Add("push_ups", new Random().Next(60, 100));
        //    d.Add("run", new Random().Next(600, 720));
        //    d.Add("id" , dt.Rows[i]["id"].ToString());
        //    d.Add("date", DateTime.Today);
        //    du.executeNonQueryByText("update result set sit_ups = @sit_ups , push_ups = @push_ups , run = @run where id = @id and status = '001' and date = @date", d);
        //}

        //d.Add("date", DateTime.Today);
        //d.Add("status", "001");
        //DataTable dt = du.getDataTableByText("select id from result where status = @status and date = @date and run between 1020 and 1140  ", d);
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    d.Clear();
        //    d.Add("sit_ups", new Random().Next(60, 100));
        //    d.Add("push_ups", new Random().Next(60, 100));
        //    //d.Add("run", new Random().Next(1020, 1140));
        //    d.Add("id", dt.Rows[i]["id"].ToString());
        //    d.Add("date", DateTime.Today);
        //    du.executeNonQueryByText("update result set sit_ups = @sit_ups , push_ups = @push_ups , run = @run where id = @id and status = '001' and date = @date", d);
        //}
        //Random outerRnd = new Random();
        //for (int i = 0; i < 1000; i++)
        //{
        //    int t = outerRnd.Next(2);
        //    Response.Write(t.ToString());
        //}

        
        //Random _Random = new Random();
        //Random _sit_ups = new Random();
        //Random _push_ups = new Random();
        //Random _run = new Random();
        //d.Add("date", DateTime.Today);
        //d.Add("status", "001");
        //DataTable dt = du.getDataTableByText("select id from result where status = @status and date = @date and run is NULL ", d);
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    switch (_Random.Next(2))
        //    {
        //        case 0:

        //            switch (_Random.Next(2))
        //            {
        //                case 0:
        //                    switch (_Random.Next(2))
        //                    {
        //                        case 0:

        //                            break;
        //                        case 1:
        //                            d.Clear();
        //                            d.Add("run", _run.Next(600, 720));
        //                            d.Add("id", dt.Rows[i]["id"].ToString());
        //                            d.Add("date", DateTime.Today);
        //                            du.executeNonQueryByText("update result set run = @run where id = @id and status = '001' and date = @date", d);
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                    break;
        //                case 1:
        //                    switch (_Random.Next(2))
        //                    {
        //                        case 0:
        //                            d.Clear();
        //                            d.Add("push_ups", _push_ups.Next(60, 100));
        //                            d.Add("id", dt.Rows[i]["id"].ToString());
        //                            d.Add("date", DateTime.Today);
        //                            du.executeNonQueryByText("update result set push_ups = @push_ups where id = @id and status = '001' and date = @date", d);
        //                            break;
        //                        case 1:
        //                            d.Clear();
        //                            d.Add("run", _run.Next(600, 720));
        //                            d.Add("push_ups", _push_ups.Next(60, 100));
        //                            d.Add("id", dt.Rows[i]["id"].ToString());
        //                            d.Add("date", DateTime.Today);
        //                            du.executeNonQueryByText("update result set  push_ups = @push_ups , run = @run where id = @id and status = '001' and date = @date", d);
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                    break;
        //                default:
        //                    break;
        //            }
        //            break;
        //        case 1:
        //            switch (_Random.Next(2))
        //            {
        //                case 0:
        //                    switch (_Random.Next(2))
        //                    {
        //                        case 0:
        //                            d.Clear();
        //                            d.Add("sit_ups", _sit_ups.Next(60, 100));
        //                            d.Add("id", dt.Rows[i]["id"].ToString());
        //                            d.Add("date", DateTime.Today);
        //                            du.executeNonQueryByText("update result set sit_ups = @sit_ups where id = @id and status = '001' and date = @date", d);
        //                            break;
        //                        case 1:
        //                            d.Clear();
        //                            d.Add("sit_ups", _sit_ups.Next(60, 100));
        //                            d.Add("run", _run.Next(600, 720));
        //                            d.Add("id", dt.Rows[i]["id"].ToString());
        //                            d.Add("date", DateTime.Today);
        //                            du.executeNonQueryByText("update result set sit_ups = @sit_ups , run = @run where id = @id and status = '001' and date = @date", d);
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                    break;
        //                case 1:
        //                    switch (_Random.Next(2))
        //                    {
        //                        case 0:
        //                            d.Clear();
        //                            d.Add("sit_ups", _sit_ups.Next(60, 100));
        //                            d.Add("push_ups", _push_ups.Next(60, 100));
        //                            d.Add("id", dt.Rows[i]["id"].ToString());
        //                            d.Add("date", DateTime.Today);
        //                            du.executeNonQueryByText("update result set sit_ups = @sit_ups , push_ups = @push_ups where id = @id and status = '001' and date = @date", d);
        //                            break;
        //                        case 1:

        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                    break;
        //                default:
        //                    break;
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //DataTable dt = du.getDataTableByText("select * from player where oversea = @oversea", "oversea", "0");
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    Dictionary<string, object> d = new Dictionary<string,object>();
        //    d.Add("id",dt.Rows[i]["id"].ToString());
        //    d.Add("name", dt.Rows[i]["name"].ToString());
        //    d.Add("gender", dt.Rows[i]["gender"].ToString());
        //    d.Add("unit_code", dt.Rows[i]["unit_code"].ToString());
        //    d.Add("rank_code", dt.Rows[i]["rank_code"].ToString());
        //    d.Add("date", DateTime.Now.AddDays(Convert.ToDouble(new Random().Next(1, 9))));
        //    d.Add("center_code", new Random().Next(1, 8));
        //    d.Add("status", "000");
        //    d.Add("op_id", "system");
        //    //du.executeNonQueryByText("insert into result (id,name,gender,unit_code,rank_code,date,center_code,status,op_id) values(@id,@name,@gender,@unit_code,@rank_code,@date,@center_code,@status,@op_id)", d);
        //}


        //List<DateTime> birthList = new List<DateTime>();
        //Lib.DataUtility du = new Lib.DataUtility();
        //DataTable dt = du.getDataTableByText("select sid from player where oversea = @oversea","oversea","0");

        //string[] firsName = new string[] { "Alison", "Albert", "Argus", "Artemus", "Baxter", "Bellamy", "Brian", "Charles", "Chaney", "Cullen", "Gustave", "Guyenne" };
        //string[] lastName = new string[] { "Kelby", "Kerby", "Kenneth", "Maynard", "Meldon", "Merlin", "Marcel", "Martin", "Marvin", "Matthew", "Morrell", "Mitchell", "Morris" };
        //Random r1 = new Random();
        //Random r2 = new Random();
        //foreach (DataRow row in dt.Rows)
        //{
        //    string mail = firsName[r1.Next(0, 12)] + lastName[r2.Next(0, 12)] + row["sid"].ToString();
        //    du.executeNonQueryByText("update player set mail = @mail where sid = " + row["sid"].ToString(), "mail", mail);
        //}


        //Lib.Unit _Unit = new Lib.Unit("18600");
        //int t = 0;
        //for (int i = 1; i < 1922; i++)
        //{
        //    //DateTime birth = DateTime.Now.AddYears(-33).AddDays(Convert.ToDouble(i));
        //    t = i;
        //    string _rank = string.Empty;
        //    string _unit = string.Empty;
        //    switch (i % 6)
        //    {
        //        case 0:
        //            _rank = "43";
        //            _unit = "18681";
        //            break;
        //        case 1:
        //            _rank = "42";
        //            _unit = "18683";
        //            break;
        //        case 2:
        //            _rank = "41";
        //            _unit = "18684";
        //            break;
        //        case 3:
        //            _rank = "33";
        //            _unit = "18685";
        //            break;
        //        case 4:
        //            _rank = "32";
        //            _unit = "18686";
        //            break;
        //        case 5:
        //            _rank = "31";
        //            _unit = "18600";
        //            break;
        //        default:
        //            break;
        //    }
        //    Dictionary<string, object> d = new Dictionary<string, object>();
        //    //d.Add("birth", birth);
        //    d.Add("rank", _rank);
        //    d.Add("unit", _unit);
        //    du.executeNonQueryByText("update player set rank_code = @rank, unit_code = @unit where sid = " + i.ToString(), d);
        //    //    du.executeNonQueryByText("update player set birth = @birth, rank_code = @rank, unit_code = @unit where sid = " + i.ToString(), d);

        //}
        //Response.Write(t.ToString() + "筆以前更新完畢");       
    }
}
