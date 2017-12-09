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
using TestSystem.DesktopVersion.System_BLL;

namespace TestSystem.DesktopVersion
{
    public partial class SinaqImtahani : Form
    {
        public QuestionBusinessLL examlist;
        public List<NQuestionList> questions;
        public OptionBussinessLL options;
        public List<SUserOptions> useroptions;
        public ResultBussinessLL result;
        public SResultHelper istifadecicavablari;
        public CorrectOptionBussinessLL correct;
        public static List<Netice> cap;
       
        public int _sirayoxla = 0;

        public SinaqImtahani()
        {

            InitializeComponent();
            examlist = new QuestionBusinessLL();
            questions = new List<NQuestionList>();
            options = new OptionBussinessLL();
            result = new ResultBussinessLL();
            correct = new CorrectOptionBussinessLL();
            useroptions = new List<SUserOptions>();

            var deyisen = examlist.GetAllQuestion(İmtahanSiyahisi._examid);
            if (deyisen.Success)
            {
                questions = deyisen.Data;
                listBox1.DataSource = deyisen.Data;
                listBox1.DisplayMember = "Sira";
                listBox1.ValueMember = "QuesionID";
            }
            else
            {
                MessageBox.Show("Suallar sistemden yuklene bilmedi, Sistemde xeta!","Sistem melumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listBox1.Enabled = true;
            }
            this.WindowState = FormWindowState.Maximized;
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void SinaqImtahani_Load(object sender, EventArgs e)
        {
           
            istifadecicavablari = new SResultHelper();
            cap = new List<Netice>();

        }

  
      


        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int _secilibuton = 0;

            for (int i = 0; i < this.groupBox1.Controls.Count; i++)
            {
                if (this.groupBox1.Controls[i] is RadioButton)
                {
                    if (((RadioButton)this.groupBox1.Controls[i]).Checked)
                    {
                        _secilibuton++;
                    }
                }
            }
            if (_secilibuton == 0)
            {
                MessageBox.Show("Sualin duzun cavabini isareleyin!", "Melumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                for (int i = 0; i < this.groupBox1.Controls.Count; i++)
                {
                    if (this.groupBox1.Controls[i] is RadioButton)
                    {
                        if (((RadioButton)this.groupBox1.Controls[i]).Checked)
                        {
                            useroptions.Add(new SUserOptions
                            {
                                Sira = Convert.ToInt32(listBox1.SelectedIndex + 1),
                                QuestionID = Convert.ToInt32(listBox1.SelectedValue),
                                UserOption = ((RadioButton)this.groupBox1.Controls[i]).Text
                            });
                            ((RadioButton)this.groupBox1.Controls[i]).Checked = false;
                            groupBox1.Enabled = false;
                        }
                    }
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (useroptions.Count() == listBox1.Items.Count)
            {
                var correctneticeler = correct.GetAllCorrectOption(İmtahanSiyahisi._examid);
                if (correctneticeler.Success)
                {
                    var suallarinduzgucavablari = correctneticeler.Data;
                    var neticeler = istifadecicavablari.SetUserResult(suallarinduzgucavablari, useroptions);
                    SResultHelper.SortedUserOptions(useroptions);

                    if (neticeler.Success)
                    {
                        var bax = neticeler.Data;
                        var baxis = result.AddResult(new Netice
                        {
                            Examad = İmtahanSiyahisi._examname,
                            UserID = Istifadeci_Girisi._userID,
                            DuzgunSay = bax.Dungunsay,
                            Sehvsay = bax.Sehvsayi,
                            ImtahanVermTarixi = DateTime.Now,
                            Faiz = (decimal) ((float)bax.Dungunsay / (float)(bax.Dungunsay + bax.Sehvsayi) * 100)
                        });
                        cap.Add(new Netice
                        {
                            Examad = İmtahanSiyahisi._examname,
                            UserID = Istifadeci_Girisi._userID,
                            DuzgunSay = bax.Dungunsay,
                            Sehvsay = bax.Sehvsayi,
                            ImtahanVermTarixi = DateTime.Now,
                            Faiz = (decimal)((float)bax.Dungunsay / (float)(bax.Dungunsay + bax.Sehvsayi) * 100)
                        });
                        if (baxis.Success)
                        {
                            MessageBox.Show("Nətitəcləriniz hazırlanır və çap olunur","Melumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            NeticeÇap netice = new NeticeÇap();
                            this.Hide();
                            netice.Show();
                        } 
                        else
                        {
                            MessageBox.Show("Neticeleriniz sisteme elave edilmede xeta","Sistem melumat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Imtahanin duzgun cavablari silinib ve ya deyisdirilib xeta", "Melumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Cavablandirilan sual sayi Imtahan sual sayindan azdir, imtahan suallarını tamamlayın","Melumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public bool SiyahidavarmiYoxla(int index)
        {
            for (int i = 0; i < useroptions.Count(); i++)
            {
                if (useroptions[i].Sira == index)
                {
                    return true;
                }
            }
            return false;
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var yoxla = SiyahidavarmiYoxla(listBox1.SelectedIndex + 1);

            if (yoxla)
            {
                groupBox1.Enabled = false;
                GetQuestionAndOptions();
            }
            else
            {
                groupBox1.Enabled = true;

                GetQuestionAndOptions();
            }
        }

        private void SinaqImtahani_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void SinaqImtahani_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        public void GetQuestionAndOptions()
        {
            richTextBox2.Clear(); richTextBox3.Clear(); richTextBox4.Clear(); richTextBox5.Clear();

            richTextBox1.Text = questions.FirstOrDefault(get => get.QuesionID == Convert.ToInt32(listBox1.SelectedValue)).Sual;
            var suallar = options.GetOptions(Convert.ToInt32(listBox1.SelectedValue));
            if (suallar.Success)
            {
                


                var cavablar = suallar.Data;
                try
                {
                    for (int i = 0; i < this.groupBox2.Controls.Count; i++)
                    {
                        if (this.groupBox2.Controls[i] is RichTextBox)
                        {
                            var accesname = ((RichTextBox)this.groupBox2.Controls[i]).AccessibleName;
                            ((RichTextBox)this.groupBox2.Controls[i]).Text = cavablar.FirstOrDefault(dey => dey.CavabIsare == accesname).Cavab;
                        }
                    }
                }
                catch (Exception ex)
                {
                    groupBox1.Enabled = false;
                    MessageBox.Show("Sualin bir ve ya bir nece varianti sistemde daxil edilmeyib", "Melumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            else
            {
                if (suallar.Error != null)
                {
                    groupBox1.Enabled = false;
                    MessageBox.Show("Cavablar sistemden alina bilmedi xeta", "Sistem melumat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    groupBox1.Enabled = false;
                    MessageBox.Show(suallar.Mesaj);
                }
            }
        }
    }

}




