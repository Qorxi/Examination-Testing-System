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
    public partial class Istifadeci_Girisi : Form
    {
        public UserBusinessLL user;
        public  static int _userID;
        public Istifadeci_Girisi()
        {
            InitializeComponent();
            user = new UserBusinessLL();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UserRegistration regist = new UserRegistration();
            regist.Show();
            this.Hide();
        }

        private void Istifadeci_Girisi_FormClosed(object sender, FormClosedEventArgs e)
        {
            SistemeGiris st = new SistemeGiris();
            st.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text !="" && textBox2.Text !="")
            {
                var netice = user.GirisYap(textBox1.Text, textBox2.Text);
                if (netice.Success)
                {
                    _userID = netice.Data;
                    İmtahanSiyahisi imtahan = new İmtahanSiyahisi();
                    imtahan.Show();
                    this.Hide();
                }
                else
                {
                    if (netice.Error != null)
                    {
                        MessageBox.Show("Sisteme daxil olarken xeta!");
                        textBox1.Clear();
                        textBox2.Clear();
                    }
                    else
                    {
                        MessageBox.Show(netice.Mesaj);
                        textBox1.Clear();textBox2.Clear();
                    }
                }
            }
            else
            {
                MessageBox.Show("Email ve sifreni daxil edin");
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SistemeGiris giris = new SistemeGiris();
            giris.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Change_Password password = new Change_Password();
            password.Show();
            this.Hide();
        }

        private void Istifadeci_Girisi_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Images.email;
            pictureBox2.Image = Images.password;
        }
    }
}
