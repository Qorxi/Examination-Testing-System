using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestSystem.DesktopVersion
{
    public partial class SistemeGiris : Form
    {
        public Settings.Settings frm;
        public SistemeGiris()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
             frm = new Settings.Settings();
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Istifadeci_Girisi giri = new Istifadeci_Girisi();
            giri.Show();
            this.Hide();
        }

        private void SistemeGiris_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void SistemeGiris_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void SistemeGiris_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Images.ExamInput;
            pictureBox2.Image = Images.StudentInput;
            pictureBox3.Image = Images.AdminInput;
            pictureBox5.Image = Images.ReportInput; 
        }
    }
}
