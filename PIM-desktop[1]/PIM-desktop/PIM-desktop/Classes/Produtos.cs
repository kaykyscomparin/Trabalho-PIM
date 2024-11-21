using PIM_desktop.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PIM_desktop
{
    internal class Produtos
    {
        

        string addpro = "INSERT INTO [dbo].[Produtos] (nome_produto, preco, imgPrd, estoque) " +
            "VALUES (@nome_produto , @preco, @imgPrd, @estoque)";
        string verpro = "SELECT COUNT(*) FROM [dbo].[Produtos] WHERE nome_produto = @nome_produto";

        public string crrgPrd = "SELECT nome_produto FROM Produtos";


        private int pk = -1;
        private string nomeProduto;
        private string nomeProduto2;
        private float preco;
        private byte[] imgPrd;
        private int estoque;

        public int Pk { get => pk; set => pk = value; }
        public string NomeProduto { get => nomeProduto; set => nomeProduto = value; }
        public string NomeProduto2 { get => nomeProduto2; set => nomeProduto2 = value; }
        public float Preco { get => preco; set => preco = value; }
        public byte[] ImgPrd { get => imgPrd; set => imgPrd = value; }
        public int Estoque { get => estoque; set => estoque = value; }

        public bool AddPrd()
        {
            Cadastro cadastro = new Cadastro();
            if (string.IsNullOrWhiteSpace(NomeProduto))
            {
                MessageBox.Show("Nome do produto esta vazio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                using (SqlConnection con = new SqlConnection(cadastro.strcon))
                {
                    con.Open();

                    using (SqlCommand cmdVer = new SqlCommand(verpro, con))
                    {
                        cmdVer.Parameters.AddWithValue("@nome_produto", NomeProduto);


                        int cont = (int)cmdVer.ExecuteScalar();
                        if (cont > 0)
                        {
                            MessageBox.Show("Já existe um produto com esse nome", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }

                    using (SqlCommand cmdCadPro = new SqlCommand(addpro, con))
                    {
                        cmdCadPro.Parameters.AddWithValue("@nome_produto", NomeProduto);
                        cmdCadPro.Parameters.AddWithValue("@preco", Preco);
                        cmdCadPro.Parameters.AddWithValue("@imgPrd", ImgPrd);
                        cmdCadPro.Parameters.AddWithValue("@estoque", Estoque);

                        int resultado = cmdCadPro.ExecuteNonQuery();
                        if (resultado == 1)
                        {
                            MessageBox.Show("Cadastro bem sucedido", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public int RecuperarID(string nomeProdutoProcurado)//recupera id do produto
        {
            try
            {
                Cadastro cadastro = new Cadastro();
                string recId = "SELECT produto_id FROM [dbo].[Produtos] WHERE nome_produto = @nome_produto";
                using (SqlConnection con = new SqlConnection(cadastro.strcon))
                {
                    con.Open();

                    SqlCommand cmdRecId = new SqlCommand(recId, con);
                    cmdRecId.Parameters.AddWithValue("@nome_produto", nomeProdutoProcurado);
                    object resultt = cmdRecId.ExecuteScalar();

                    if (resultt == null)
                    {
                        MessageBox.Show("Produto não foi encontrado", "Produto não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    pk = Convert.ToInt32(resultt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"RECUPERAR ID:{ex.Message}");
            }
            return pk;
        }

        public void Alterarproduto(int pk, string nomeProdutoAlterado, float precoAlterado, int estoqueAlterado)
        {
            string AltPrd = "UPDATE [dbo].[Produtos] " +
                "SET nome_produto = @nome_produto, preco = @preco, estoque = @estoque " +
                "WHERE produto_id = @produto_id";

            try
            {
                Cadastro cadastro = new Cadastro();
                using (SqlConnection con = new SqlConnection(cadastro.strcon))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(AltPrd, con))
                    {
                        cmd.Parameters.AddWithValue("@produto_id", pk);
                        cmd.Parameters.AddWithValue("@nome_produto", nomeProdutoAlterado);
                        cmd.Parameters.AddWithValue("@preco", precoAlterado);
                        cmd.Parameters.AddWithValue("@estoque", estoqueAlterado);
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Produto alterado com sucesso");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ALTERAR PRODUTO:{ex.Message}");
            }
        }

        public void ExcluirProduto(int id)
        {
            string querry = "DELETE FROM [dbo].[Produtos] WHERE produto_id = @id";
            Cadastro cadastro = new Cadastro();

            try
            {
                using (SqlConnection con = new SqlConnection(cadastro.strcon))
                {
                    con.Open();
                    using (SqlCommand excluir = new SqlCommand(querry, con))
                    {
                        excluir.Parameters.AddWithValue("@id", id);
                        int r = excluir.ExecuteNonQuery();
                        if (r > 0)
                        {
                            MessageBox.Show("Produto excluido com sucesso", "Produto excluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Falha ao excluir", "Falha ao excluir produto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
