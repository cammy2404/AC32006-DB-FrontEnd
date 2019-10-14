using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseFrontEnd
{
    public partial class Form1 : Form
    {
        DBConnection db;

        public Form1()
        {
            InitializeComponent();
            db = new DBConnection("silva.computing.dundee.ac.uk", "19ac3d07", "19ac3u07", "ac33b1");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string>[] output = db.Select();

            for (int i = 0; i < output.Length; i++)
            {
                foreach (string j in output[i])
                {
                    tb_output.Text += j;
                }
                tb_output.Text += Environment.NewLine;
            }
        }
        
    }
}
