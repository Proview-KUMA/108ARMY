using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace InI
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
                {
                    TB_FileName.Text = folderBrowserDialog1.SelectedPath;
                }
                else
                {
                    MessageBox.Show("Folder can't be empty.");
                }
            }
            else
            {
                MessageBox.Show("Cancel Select Folder.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (RB_Standard.Checked == true)
                {
                    if (!string.IsNullOrEmpty(TB_FileName.Text))
                    {
                        Dictionary<string, object> d = new Dictionary<string, object>();
                        Lib.DataUtility du = new Lib.DataUtility();
                        string[] FileLists = Directory.GetFiles(TB_FileName.Text);
                        int Total = 0;
                        foreach (string Name in FileLists)
                        {
                            var reader = new StreamReader(File.OpenRead(@Name), Encoding.GetEncoding("Big5"));
                            int count = 0;
                            while (reader.Peek() >= 0)
                            {
                                var line = reader.ReadLine();
                                string[] operater = { "," };
                                string[] values = line.Split(operater, StringSplitOptions.None);
                                if (values.Length > 0)
                                {
                                    if (values[0] != "項目" && values.Length == 6)
                                    {
                                        d.Clear();
                                        d.Add("item", values[0]);
                                        d.Add("gender", values[1]);
                                        d.Add("agemin", values[2]);
                                        d.Add("agemax", values[3]);
                                        d.Add("standard", values[4]);
                                        d.Add("score", values[5]);
                                        du.executeNonQueryByText(@"Insert into Standard (item, gender, agemin, agemax, standard, score) values (@item, @gender, @agemin, @agemax, @standard, @score)", d);
                                        du.executeNonQueryByText(@"Insert into StandardEncrypt (item, gender, agemin, agemax, standard, score) values (@item, @gender, @agemin, @agemax, @standard, @score)", d);
                                        count++;
                                    }
                                }
                            }
                            Total += count;
                            //MessageBox.Show("Load File " + Name + " 共" + count + "筆成績");
                        }
                        MessageBox.Show("Load score success." + " 共" + Total + "筆.");
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(TB_FileName.Text))
                    {
                        Dictionary<string, object> d = new Dictionary<string, object>();
                        Lib.DataUtility du = new Lib.DataUtility();
                        string[] FileLists = Directory.GetFiles(TB_FileName.Text);
                        int Total = 0;
                        foreach (string Name in FileLists)
                        {
                            var reader = new StreamReader(File.OpenRead(@Name), Encoding.GetEncoding("Big5"));
                            int count = 0;
                            while (reader.Peek() >= 0)
                            {
                                var line = reader.ReadLine();
                                string[] operater = { "," };
                                string[] values = line.Split(operater, StringSplitOptions.None);
                                if (values.Length > 0)
                                {
                                    if (values[0] != "項目" && values.Length == 5)
                                    {
                                        d.Clear();
                                        d.Add("item_id", values[0]);
                                        d.Add("gender", values[1]);
                                        d.Add("start", values[2]);
                                        d.Add("end", values[3]);
                                        d.Add("standard", values[4]);
                                        du.executeNonQueryByText(@"Insert into ReplaceStandard (item_id, gender, start, [end], standard) values (@item_id, @gender, @start, @end, @standard)", d);
                                        du.executeNonQueryByText(@"Insert into ReplaceStandardEncrypt (item_id, gender, start, [end], standard) values (@item_id, @gender, @start, @end, @standard)", d);
                                        count++;
                                    }
                                }
                            }
                            Total += count;
                            //MessageBox.Show("Load File " + Name + " 共" + count + "筆成績");
                        }
                        MessageBox.Show("Load score success." + " 共" + Total + "筆.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RB_Standard_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_Standard.Checked == true)
            {
                RB_Replaceitem.Checked = false;
            }
            else
            {
                RB_Replaceitem.Checked = true;
            }
        }

        private void RB_Replaceitem_CheckedChanged(object sender, EventArgs e)
        {
            
        }

    }
}
