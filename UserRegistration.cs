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
    public partial class UserRegistration : Form
    {
        public UserRegistration()
        {
            InitializeComponent();
        }

        private void UserRegistration_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && txtEmail.Text !="" && txtSifre.Text !="" )
            {
                var newuser = new UserBusinessLL();
              var netice =  newuser.AddUser(new NUser
                {
                    Ad = textBox1.Text,
                    Soyad = textBox2.Text,
                    Email = txtEmail.Text,
                    Sifre = txtSifre.Text,
                    Adres = textBox3.Text,
                    Yas = Convert.ToInt32(textBox4.Text),
                    Sinif = textBox5.Text,
                    Qrup = textBox6.Text
                 });
                if (netice.Success)
                {
                    DialogResult dr = new DialogResult();
                    dr =   MessageBox.Show("User sisteme elave edildi");
                    if (dr == DialogResult.OK)
                    {
                        SistemeGiris st = new SistemeGiris();
                        st.Show();
                        this.Hide();
                    }
                }
                else
                {
                    if (netice.Error != null)
                    {
                        MessageBox.Show("Sisteme daxil edilmede yanlis!");
                        textBox1.Clear();textBox2.Clear(); textBox3.Clear(); textBox4.Clear();textBox5.Clear(); textBox6.Clear();
                    }
                    else
                    {
                        MessageBox.Show(netice.Mesaj);
                        textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear();

                    }
                }


            }
            else
            {
                MessageBox.Show("Melumatlatlari doldurun!");
            }
        }

        private void UserRegistration_FormClosed(object sender, FormClosedEventArgs e)
        {
            SistemeGiris st = new SistemeGiris();
            st.Show();
            this.Hide();
        }
    }
}
