using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Printing;
using System.Drawing.Printing;
using Lib;
using System.Net;
using System.Drawing.Imaging;

namespace ScoreClose
{
    public partial class ScoreCloseForm1 : Form
    {
        private DataTable dt_Score = null;
        Lib.DataUtility du = new Lib.DataUtility();
        Dictionary<string, object> d = new Dictionary<string, object>();
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]//应用API函数
        private static extern bool BitBlt(
        IntPtr hdcDest, // 目标设备的句柄 
        int nXDest, // 目标对象的左上角的X坐标 
        int nYDest, // 目标对象的左上角的X坐标 
        int nWidth, // 目标对象的矩形的宽度 
        int nHeight, // 目标对象的矩形的长度 
        IntPtr hdcSrc, // 源设备的句柄 
        int nXSrc, // 源对象的左上角的X坐标 
        int nYSrc, // 源对象的左上角的X坐标 
        System.Int32 dwRop // 光栅的操作值 
        );
        private Bitmap memImage = null;
        private Image print_image = null;

        public ScoreCloseForm1(DataTable dt)
        {
            InitializeComponent();
            dt_Score = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Activate();

            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            this.Size = new Size(Convert.ToInt32(21 / 2.54 * g.DpiX), Convert.ToInt32(29.7 / 2.54 * g.DpiY));
            //panel1.Size = New Size(CInt(21 / 2.54 * g.DpiX), CInt(29.7 / 2.54 * g.DpiY)); 
            try
            {
                var request = WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["Logo"].ToString());
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    panel2.BackgroundImage = Bitmap.FromStream(stream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }   

            try
            {
                LB_center.Text = dt_Score.Rows[0]["center_name"].ToString() + "鑑測站成績單";
                LB_date.Text = dt_Score.Rows[0]["date"].ToString();
                LB_unit.Text = dt_Score.Rows[0]["unit_code"].ToString();
                LB_rank.Text = dt_Score.Rows[0]["rank_code"].ToString();
                LB_birth.Text = dt_Score.Rows[0]["birth"].ToString() + " (" + dt_Score.Rows[0]["age"].ToString() + "歲)"; 
                if (dt_Score.Rows[0]["status"].ToString().Substring(0, 1) == "2")
                {
                    LB_today_title.Visible = true;
                    LB_today.Visible = true;
                    LB_today.Text = Lib.SysSetting.ToRocDateFormat(System.DateTime.Today.ToString("yyyy/MM/dd"));
                }
                else
                {
                    LB_today_title.Text = string.Empty;
                    LB_today.Visible = false;
                    LB_today.Text = string.Empty;
                }

                if (dt_Score.Rows[0]["status"].ToString().Substring(2, 1) == "4")
                {
                    LB_bodyfatover.Visible = true;
                }
                else
                {
                    LB_bodyfatover.Visible = false;
                    LB_bodyfatover.Text = string.Empty;
                }

                if (!String.IsNullOrEmpty(dt_Score.Rows[0]["BMI"].ToString()))
                    LB_bmi.Text = dt_Score.Rows[0]["BMI"].ToString() + " %";
                LB_name.Text = dt_Score.Rows[0]["name"].ToString();
                LB_id.Text = dt_Score.Rows[0]["id"].ToString();
                if (!String.IsNullOrEmpty(dt_Score.Rows[0]["bodyfat"].ToString()))
                    LB_bodyfat.Text = dt_Score.Rows[0]["bodyfat"].ToString() + " %";

                d.Clear();
                if (dt_Score.Rows[0]["sit_ups"].ToString().Length == 0)
                    d.Add("sit_ups", DBNull.Value);
                else
                    d.Add("sit_ups", dt_Score.Rows[0]["sit_ups"]);

                d.Add("sit_ups_score", dt_Score.Rows[0]["sit_ups_score"]);

                if (dt_Score.Rows[0]["push_ups"].ToString().Length == 0)
                    d.Add("push_ups", DBNull.Value);
                else
                    d.Add("push_ups", dt_Score.Rows[0]["push_ups"]);

                d.Add("push_ups_score", dt_Score.Rows[0]["push_ups_score"]);

                if (dt_Score.Rows[0]["run"].ToString().Length == 0)
                    d.Add("run", DBNull.Value);
                else
                    d.Add("run", dt_Score.Rows[0]["run"]);

                d.Add("run_score", dt_Score.Rows[0]["run_score"]);

                d.Add("memo", dt_Score.Rows[0]["memo"].ToString());
                d.Add("status", dt_Score.Rows[0]["status"].ToString());
                DataTable dt = du.getDataTableBysp(@"GetItemTitleAndScore", d);
                if (dt.Rows.Count == 1)
                {
                    LB_item_sit_ups.Text = dt.Rows[0]["sit_ups_name"].ToString();
                    LB_sit_ups.Text = dt.Rows[0]["sit_ups"].ToString();
                    LB_sit_ups_score.Text = dt.Rows[0]["sit_ups_score"].ToString();
                    LB_result_sit_ups.Text = dt.Rows[0]["sit_ups_result"].ToString();

                    LB_item_push_ups.Text = dt.Rows[0]["push_ups_name"].ToString();
                    LB_push_ups.Text = dt.Rows[0]["push_ups"].ToString();
                    LB_push_ups_score.Text = dt.Rows[0]["push_ups_score"].ToString();
                    LB_result_push_ups.Text = dt.Rows[0]["push_ups_result"].ToString();

                    LB_item_run.Text = dt.Rows[0]["run_name"].ToString();
                    LB_run.Text = dt.Rows[0]["run"].ToString();
                    LB_run_score.Text = dt.Rows[0]["run_score"].ToString();
                    LB_result_run.Text = dt.Rows[0]["run_result"].ToString();

                    LB_total_result.Text = dt.Rows[0]["status"].ToString();
                }


                LB_center.Left = (this.panel1.ClientSize.Width - LB_center.Width) / 2;
                LB_Title.Left = (this.panel1.ClientSize.Width - LB_Title.Width) / 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }          
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }


        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            GetPrintArea(panel1);
            //PrintDocument _PrintDocument = new PrintDocument();
            //_PrintDocument.PrinterSettings.DefaultPageSettings.PrinterResolution.Kind = PrinterResolutionKind.High;
            //_PrintDocument.PrintPage += new PrintPageEventHandler(_PrintDocument_PrintPage);
            ////_PrintDocument.PrinterSettings.PrinterName = "SHARP MX-2700N PCL6";
            //PrintDialog pdi = new PrintDialog();            
            //pdi.Document = _PrintDocument;
            //pdi.ShowDialog();
            //if (pdi.ShowDialog() == DialogResult.OK)
            //{
            //    _PrintDocument.Print();
            //}
            //else
            //{
            //    MessageBox.Show("取消列印");
            //}
        }

        void _PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //GetPrintArea(panel1, e.Graphics);
        }

        //public void GetPrintArea(Panel pnl, Graphics gr)
        public void GetPrintArea(Panel pnl)
        {
            Graphics gr = this.panel1.CreateGraphics();
            // scale to fit on width of page...
            if (pnl.Width > 0)
            {
                gr.PageScale = gr.VisibleClipBounds.Width / pnl.Width;
            }
            // this should recurse...
            // just for demo so kept it simple
            foreach (var ctl in pnl.Controls)
            {
                // for every control type
                // come up with a way to Draw its
                // contents
                if (ctl is Label)
                {
                    var lbl = (Label)ctl;
                    gr.DrawString(
                        lbl.Text,
                        lbl.Font,
                        new SolidBrush(lbl.ForeColor),
                        lbl.Location.X,  // simple based on the position in the panel
                        lbl.Location.Y);
                }
                if (ctl is PictureBox)
                {
                    var pic = (PictureBox)ctl;
                    gr.DrawImageUnscaledAndClipped(
                        pic.Image,
                        new Rectangle(
                            pic.Location.X,
                            pic.Location.Y,
                            pic.Width,
                            pic.Height));
                }
                if (ctl is TextBox)
                {
                    var pic = (TextBox)ctl;
                    gr.DrawRectangle(new Pen(Color.Black),
                        new Rectangle(
                            pic.Location.X,
                            pic.Location.Y,
                            pic.Width,
                            pic.Height));
                }
                //if (ctl is Panel)
                //{
                //    var pic = (Panel)ctl;
                //    int width = pic.Size.Width;
                //    int height = pic.Size.Height;

                //    Bitmap bm = new Bitmap(width, height);
                //    pic.DrawToBitmap(bm, new Rectangle(0, 0, width, height));
                //    gr.DrawImageUnscaledAndClipped(bm, new Rectangle(pic.Location.X, pic.Location.Y, pic.Width, pic.Height));
                //}
                if (ctl is Panel)
                {
                    var pic = (Panel)ctl;
                    int width = pic.Size.Width;
                    int height = pic.Size.Height;

                    Bitmap bm = (Bitmap)pic.BackgroundImage;
                    gr.DrawImageUnscaledAndClipped(bm, new Rectangle((width - bm.Width) / 2, pic.Location.Y, bm.Width, bm.Height));

                    foreach (var ctl2 in pic.Controls)
                    {
                        if (ctl2 is Label)
                        {
                            var lbl = (Label)ctl2;
                            gr.DrawString(
                                lbl.Text,
                                lbl.Font,
                                new SolidBrush(lbl.ForeColor),
                                lbl.Location.X + pic.Location.X,  // simple based on the position in the panel
                                lbl.Location.Y + pic.Location.Y);
                        }

                        if (ctl2 is TableLayoutPanel)
                        {
                            var pic2 = (TableLayoutPanel)ctl2;
                            int width2 = pic.Size.Width;
                            int height2 = pic.Size.Height;

                            foreach (var ctl3 in pic2.Controls)
                            {
                                if (ctl3 is Label)
                                {
                                    var lbl = (Label)ctl3;
                                    gr.DrawString(
                                        lbl.Text,
                                        lbl.Font,
                                        new SolidBrush(lbl.ForeColor),
                                        lbl.Location.X + pic.Location.X + pic2.Location.X,  // simple based on the position in the panel
                                        lbl.Location.Y + pic.Location.Y + pic2.Location.Y);
                                }
                            }

                            //畫表格的線條 , 先畫橫線 , 因為 Rows = 4 , 所以每條線得間距是 25% * Height
                            gr.DrawLine(new Pen(Color.Black), new Point(pic2.Location.X + pic.Location.X, pic2.Location.Y + pic.Location.Y), new Point(pic2.Location.X + pic2.Width + pic.Location.X, pic2.Location.Y + pic.Location.Y));
                            gr.DrawLine(new Pen(Color.Black), new Point(pic2.Location.X + pic.Location.X, pic2.Location.Y + pic.Location.Y + (int)(pic2.Height * 0.25)), new Point(pic2.Location.X + pic2.Width + pic.Location.X, pic2.Location.Y + pic.Location.Y + (int)(pic2.Height * 0.25)));
                            gr.DrawLine(new Pen(Color.Black), new Point(pic2.Location.X + pic.Location.X, pic2.Location.Y + pic.Location.Y + (int)(pic2.Height * 0.5)), new Point(pic2.Location.X + pic2.Width + pic.Location.X, pic2.Location.Y + pic.Location.Y + (int)(pic2.Height * 0.5)));
                            gr.DrawLine(new Pen(Color.Black), new Point(pic2.Location.X + pic.Location.X, pic2.Location.Y + pic.Location.Y + (int)(pic2.Height * 0.75)), new Point(pic2.Location.X + pic2.Width + pic.Location.X, pic2.Location.Y + pic.Location.Y + (int)(pic2.Height * 0.75)));
                            gr.DrawLine(new Pen(Color.Black), new Point(pic2.Location.X + pic.Location.X, pic2.Location.Y + pic.Location.Y + pic2.Height), new Point(pic2.Location.X + pic2.Width + pic.Location.X, pic2.Location.Y + pic.Location.Y + pic2.Height));

                            // 再來畫直線 , Columns = 4 , 參照TableLayoutPanel1的屬性Colums採用裡面設定的比例 , 每天線得間距依序為 : 30% 24% 25% 21%
                            gr.DrawLine(new Pen(Color.Black), new Point(pic2.Location.X + pic.Location.X, pic2.Location.Y + pic.Location.Y), new Point(pic2.Location.X + pic.Location.X, pic2.Location.Y + pic.Location.Y + pic2.Height));
                            gr.DrawLine(new Pen(Color.Black), new Point(pic2.Location.X + pic.Location.X + (int)(pic2.Width * 0.3), pic2.Location.Y + pic.Location.Y), new Point(pic2.Location.X + pic.Location.X + (int)(pic2.Width * 0.3), pic2.Location.Y + pic.Location.Y + pic2.Height));
                            gr.DrawLine(new Pen(Color.Black), new Point(pic2.Location.X + pic.Location.X + (int)(pic2.Width * 0.54), pic2.Location.Y + pic.Location.Y), new Point(pic2.Location.X + pic.Location.X + (int)(pic2.Width * 0.54), pic2.Location.Y + pic.Location.Y + pic2.Height));
                            gr.DrawLine(new Pen(Color.Black), new Point(pic2.Location.X + pic.Location.X + (int)(pic2.Width * 0.79), pic2.Location.Y + pic.Location.Y), new Point(pic2.Location.X + pic.Location.X + (int)(pic2.Width * 0.79), pic2.Location.Y + pic.Location.Y + pic2.Height));
                            gr.DrawLine(new Pen(Color.Black), new Point(pic2.Location.X + pic.Location.X + (int)(pic2.Width * 1), pic2.Location.Y + pic.Location.Y), new Point(pic2.Location.X + pic.Location.X + (int)(pic2.Width * 1), pic2.Location.Y + pic.Location.Y + pic2.Height));
                        }
                    }

                    Bitmap bm2 = new Bitmap(pnl.Width, pnl.Height);
                    pnl.DrawToBitmap(bm2, new Rectangle(0, 0, pnl.Width, pnl.Height));
                    gr.DrawImageUnscaledAndClipped(bm2, new Rectangle(pnl.Location.X, pnl.Location.Y, pnl.Width, pnl.Height));
                    bm2.Save(@"D:\test.bmp", ImageFormat.Bmp);
                }

            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ScoreCloseForm1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                //System.Windows.Forms.PaintEventArgs pe = new System.Windows.Forms.PaintEventArgs();
                //toolStripLabel2_Click(sender, pe);
            }
        }


    }
}
