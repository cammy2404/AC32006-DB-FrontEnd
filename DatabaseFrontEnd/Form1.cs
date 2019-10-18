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

        string[] PeopleSelect = { "ID", "Surname", "Age" };

        public Form1()
        {
            InitializeComponent();
            db = new DBConnection("silva.computing.dundee.ac.uk", "19ac3d07", "19ac3u07", "ac33b1");

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //List<string>[] output = db.Select(tb_table_name.Text, PeopleSelect);
            //PrintData(output);

            //List<string>[] output1 = db.Select("Select Name, ID From People");
            //PrintData(output1);

            List<string>[] output2 = db.Select(db.SelectQueryBuilder("People", PeopleSelect, "1=1", "ID", false));
            PrintData(output2);
            
            //tb_output.Text += db.SelectQueryBuilder("People", PeopleSelect);
            //tb_output.Text += Environment.NewLine;
            //tb_output.Text += Environment.NewLine;

            //tb_output.Text +=db.SelectQueryBuilder("People", PeopleSelect, "name='cammy'");
            //tb_output.Text += Environment.NewLine;
            //tb_output.Text += Environment.NewLine;

            //tb_output.Text +=db.SelectQueryBuilder("People", PeopleSelect, "name='cammy'", PeopleSelect[1]);
            //tb_output.Text += Environment.NewLine;
            //tb_output.Text += Environment.NewLine;

            //tb_output.Text +=db.SelectQueryBuilder("People", PeopleSelect, "name='cammy'", PeopleSelect[1], true);
            //tb_output.Text += Environment.NewLine;
            //tb_output.Text += Environment.NewLine;

            //tb_output.Text += db.SelectQueryBuilder("People", PeopleSelect, "name='cammy'", PeopleSelect[1], false);

        }

        private void PrintData(List<string>[] output)
        {
            if (output != null)
            {

                for (int i = 0; i < output[0].Count; i++)
                {
                    for (int j = 0; j < output.Length; j++)
                    {
                        tb_output.Text += output[j][i];
                    }
                    tb_output.Text += Environment.NewLine;
                }
            }
            else
            {
                tb_output.Text = "Error connecting to the database " + Environment.NewLine + "    - table name might contain ilegal characters";
            }
        }
        
    }
}
