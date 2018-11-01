using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InI
{
    public partial class SystemTimeForm : Form
    {
        public SystemTimeForm()
        {
            InitializeComponent();
        }

        private void SystemTimeForm_Load(object sender, EventArgs e)
        {
            string today = DateTime.Now.ToString("西元yyyy年MM月dd日(dddd) HH點mm分");
            lab_SysTime.Text = today;
        }

        private void btn_Check_Close_Click(object sender, EventArgs e)
        {
            if (Form1.ST_Form != null)
            {
                Form1.ST_Form.Dispose();
                Form1.ST_Form = null;
                this.Close();
            }
        }

        private void SystemTimeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Form1.ST_Form != null)
            {
                Form1.ST_Form.Dispose();
                Form1.ST_Form = null;
                this.Close();
            }
        }
    }
}
