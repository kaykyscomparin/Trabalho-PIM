using PIM_desktop.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIM_desktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            MudarTela(new TelaPreLogin());
        }
        public void MudarTela(UserControl newTel)
        {
            this.Controls.Clear();
            newTel.Dock = DockStyle.Fill;
            this.Controls.Add(newTel);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
