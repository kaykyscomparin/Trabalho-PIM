using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIM_desktop.View
{
    public partial class TelaDeProdutos : UserControl
    {
        public TelaDeProdutos()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //label username
        }

        private void TelaDeProdutos_Load(object sender, EventArgs e)
        {
            //load
            label2.Visible = false;
            

            comboBox1.Items.Add("Adicionar produto");

            Cadastro cadastro = new Cadastro();
            using (SqlConnection connection = new SqlConnection(cadastro.strcon))
            {
                connection.Open();
                Produtos produtos = new Produtos();
                try
                {
                    SqlCommand command = new SqlCommand(produtos.crrgPrd , connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //TelaDeProdutos telaDeProdutos = new TelaDeProdutos();
                        comboBox1.Items.Add(reader["nome_produto"].ToString());  // Adiciona cada item à ComboBox
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);

                }
            }
        }
        void AtualizarCombobox()
        {
            //limpa a combobox para atualiza-la
            pictureBox1.Image = null;
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Adicionar produto");

            //atualza combobox
            Cadastro cadastro = new Cadastro();
            using (SqlConnection connection = new SqlConnection(cadastro.strcon))
            {
                connection.Open();
                string query = produtos.crrgPrd; // Ajuste conforme necessário
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Associa cada nome_produto ao seu ID e nome na ComboBox
                    comboBox1.Items.Add(reader["nome_produto"].ToString());
                }
                reader.Close();
            }
            comboBox1.SelectedItem = textBox1.Text;
            textBox4.Clear();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //combobox

            var selectitem = (dynamic)comboBox1.SelectedItem;
            if (selectitem == "Adicionar produto")
            {
                // Limpa os campos e a imagem
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                pictureBox1.Image = null;
                return; // Sai do método para não executar a lógica de carregamento de dados
            }

            //carrega o produto na combo box
            Cadastro cadastro = new Cadastro();
            using (SqlConnection connection = new SqlConnection(cadastro.strcon))
            {
                connection.Open();
                string query = "SELECT preco, estoque, imgPrd FROM Produtos WHERE nome_produto = @nome_produto";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nome_produto", selectitem);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // Define o conteúdo das TextBox
                    textBox1.Text = selectitem.ToString();
                    textBox2.Text = reader["preco"].ToString();
                    textBox3.Text = reader["estoque"].ToString();
                    label2.Text = selectitem;

                    // Verifica e converte a imagem
                    if (reader["imgPrd"] != DBNull.Value)
                    {
                        byte[] imageBytes = (byte[])reader["imgPrd"];

                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Imagem não encontrada");
                        pictureBox1.Image = null; // Limpa a PictureBox caso não tenha imagem
                    }
                }
                reader.Close();
            }

        }
        

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //Path da imagem 
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //qtd em estoque
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //preço
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //nome
        }

        Produtos produtos = new Produtos();

        private void button1_Click(object sender, EventArgs e)
        {
            //btn add produto

            
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Adicionar produto")
            {
                produtos.NomeProduto = textBox1.Text;

                if (float.TryParse(textBox2.Text, out float val))
                {
                    produtos.Preco = val;
                }
                else
                {
                    MessageBox.Show("Preço inválido", "falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (int.TryParse(textBox3.Text, out int valint))
                {
                    produtos.Estoque = valint;
                }
                else
                {
                    MessageBox.Show("Estoque Inválido", "falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                produtos.AddPrd();
                AtualizarCombobox();
            }
            else
            {
                MessageBox.Show("Por favor, selecione a opção adicionar produto na lista de produtos antes de clicar em adicionar");
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //selecionar imagem
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog.Title = "Selecione uma Imagem do produto";

            // Verificar se o usuário selecionou o arquivo
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName; // Caminho do arquivo de imagem
                textBox4.Text = imagePath;

                // Carregar a imagem
                Image selectImage = Image.FromFile(imagePath);

                // Converter a imagem em array de bytes
                byte[] imageBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    selectImage.Save(ms, selectImage.RawFormat);
                    imageBytes = ms.ToArray();
                }
                produtos.ImgPrd = imageBytes;
            }
            else
            {
                MessageBox.Show("Imagem não foi selecionada", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //img do produto
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //alterar produto

            //recupera o id
            int id = produtos.RecuperarID(label2.Text);

            if (float.TryParse(textBox2.Text, out float novoPreco)) { }
            else
            {
                MessageBox.Show("O novo preço é inválido", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (int.TryParse(textBox3.Text, out int novoEstoque)) { }
            else
            {
                MessageBox.Show("O novo estoque é inválido", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //altera produto
            produtos.Alterarproduto(id, textBox1.Text, novoPreco, novoEstoque);

            AtualizarCombobox();

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //excluir produto
            int id = produtos.RecuperarID(textBox1.Text);
            produtos.ExcluirProduto(id);
            AtualizarCombobox();

        }

        private void label2_Click(object sender, EventArgs e)
        {
            //label nome do produto
        }

        private void label7_Click(object sender, EventArgs e)
        {
            //btn Sair
            var q = MessageBox.Show("Deseja mesmo sair", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (q == DialogResult.Yes)
            {
                var parentform = ParentForm as Form1;
                if (parentform != null)
                {
                    parentform.MudarTela(new TelaPreLogin());
                }
            }
        }
    }
}
