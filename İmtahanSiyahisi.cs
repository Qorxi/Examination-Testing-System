using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestSystem.DesktopVersion.Bussiness_Logic_Layer;
using TestSystem.DesktopVersion.NObjects;

namespace TestSystem.DesktopVersion
{
    public partial class İmtahanSiyahisi : Form
    {
        public static string _examname;
        public static int _examid;
        public ExamBussinessLL examlist;
        public İmtahanSiyahisi()
        {
            InitializeComponent();
            examlist = new ExamBussinessLL();
        }

        private void İmtahan_Load(object sender, EventArgs e)
        {
            var netice = examlist.GetAllExamList();

            if (netice.Success)
            {
                comboBox1.DataSource = netice.Data;
                comboBox1.DisplayMember = "ExamName";
                comboBox1.ValueMember = "ExamID";
            }
            else
            {
                MessageBox.Show("Sistemde xeta");
            }
        }

        private void İmtahan_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0)
            {
                SinaqImtahani sinaq = new SinaqImtahani();
                sinaq.Show();
                this.Hide();
            }
               
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _examname = comboBox1.Text;
            _examid = ((NListExam)comboBox1.SelectedItem).ExamID;

        }
    }
}
