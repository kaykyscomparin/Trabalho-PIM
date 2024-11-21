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
    public partial class TelaCad : UserControl
    {
        public TelaCad()
        {
            InitializeComponent();
        }

        private void TelaCad_Load(object sender, EventArgs e)
        {
            //load
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //label username
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //text box nome
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //text box nome completo
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //textBox1 box email
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //textbox cpf
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //textbox tel
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //textbox senha
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            //text box senha C
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            //textbox cep
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            //TextBox cidade
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            //text box barirro
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            //textbox rua
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            //textbox numero
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //btn cad
            Cadastro cadastro = new Cadastro();
            cadastro.Name = textBox1.Text;
            cadastro.Nome_completo = textBox2.Text;
            cadastro.Email_corporativo = textBox3.Text;
            cadastro.Cpf = textBox4.Text;
            cadastro.Tel = textBox5.Text;
            cadastro.Senha = textBox6.Text;
            cadastro.SenhaC = textBox7.Text;
            cadastro.Cep = textBox8.Text;
            cadastro.Cidade = textBox9.Text;
            cadastro.Bairro = textBox10.Text;
            cadastro.Rua = textBox11.Text;
            cadastro.Numero = textBox12.Text;

            cadastro.Cadastrar();

            var parentform = this.ParentForm as Form1;
            if (parentform != null && cadastro._cadScss)
            {
                parentform.MudarTela(new TelaPreLogin());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //btn voltar
            var parentform = this.ParentForm as Form1;
            if (parentform != null)
            {
                parentform.MudarTela(new TelaPreLogin());
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
