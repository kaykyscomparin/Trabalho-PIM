using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIM_desktop.View
{
    public partial class TelaPreLogin : UserControl
    {
        public TelaPreLogin()
        {
            InitializeComponent();
        }

        private void TelaPreLogin_Load(object sender, EventArgs e)
        {
            //load
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //btn cadastrar-se
            var parentform = this.ParentForm as Form1;
            if (parentform != null)
            {
                parentform.MudarTela(new TelaCad());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //btn login
            Cadastro cadastro = new Cadastro();
            cadastro.Name = textBox3.Text;
            cadastro.Senha = textBox4.Text;

            cadastro.login();
            

            if (cadastro._estLgd)
            {
                TelaDeProdutos telaDeProdutos = new TelaDeProdutos();
                telaDeProdutos.label1.Text = cadastro.Name;

                var parentform = this.ParentForm as Form1;
                if (parentform != null)
                {
                    parentform.MudarTela(telaDeProdutos);
                }
            }
            


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //text box nome
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //textbox senha
        }
    }
}
