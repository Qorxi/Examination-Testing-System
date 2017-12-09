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
    public partial class Change_Password : Form
    {
        public UserBusinessLL user;

        public Change_Password()
        {
            InitializeComponent();
            user = new UserBusinessLL();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Istifadeci_Girisi giris = new Istifadeci_Girisi();
            giris.Show();
            this.Hide();
        }

        private void Change_Password_FormClosing(object sender, FormClosingEventArgs e)
        {
            SistemeGiris sistem = new SistemeGiris();
            sistem.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text !="" && textBox2.Text !="")
            {
             var netice =   user.ChangePassword(textBox1.Text, textBox2.Text);
                if (netice.Success)
                {
                    DialogResult dr = new DialogResult();
                    dr =  MessageBox.Show("Sifre deyisdirildi");
                    if (dr == DialogResult.OK)
                    {
                        SistemeGiris sistem = new SistemeGiris();
                        sistem.Show();
                        this.Hide();
                    }
                }
                else
                {
                    if (netice.Error != null)
                    {
                        textBox1.Clear(); textBox2.Clear();
                        MessageBox.Show("Sistemde xeta bas verdi");
                        
                    }
                    else
                    {
                        textBox1.Clear();
                        textBox2.Clear();
                        MessageBox.Show(netice.Mesaj);

                    }
                }
            }
            else
            {
                MessageBox.Show("Email ve ya yeni sifreni daxil edin");
            }
        }

        private void Change_Password_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Images.email;
        }
    }
}
