using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestSystem.DesktopVersion.NObjects;
using TestSystem.DesktopVersion.Entity;

namespace TestSystem.DesktopVersion
{
    public partial class NeticeÇap : Form
    {
        public NeticeÇap()
        {
            InitializeComponent();
        }

        private void NeticeÇap_Load(object sender, EventArgs e)
        {

            NeticeBindingSource.DataSource = SinaqImtahani.cap;
            this.reportViewer1.RefreshReport();
        }

        private void NeticeÇap_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
