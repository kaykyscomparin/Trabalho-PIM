using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIM_desktop
{
    internal class Cadastro
    {
        //LOGO A BAIXO ESTA A STRING DE CONEXAO "STRCON", VOCE DEVE COLOCAR O NOME DO SEU SERVIDOR ONDE ESTA ESCRITO "NOME_DO_SERVIDOR" 
        public string strcon = @"Data Source=KAYKY\SQLEXPRESS;" +
                       "Initial Catalog=PIMDB_desktop;Integrated Security=True";

        string strcadastrar = "INSERT INTO [dbo].[usuario] (nome_usuario, senha, nome_completo, email_corporativo, tel, cpf, endereco_id)" +
            "VALUES (@nome, @senha, @nome_completo, @email_corporativo, @tel, @cpf, @endereco_id)";
        String strVer = " SELECT COUNT(*) FROM [dbo].[usuario] WHERE nome_usuario  COLLATE SQL_Latin1_General_CP1_CS_AS = @nome";
        string dfnEnd = "INSERT INTO [dbo].[endereco] (cep,cidade,bairro,rua,numero) " +
            "VALUES (@cep ,@cidade ,@bairro ,@rua ,@numero)";

        string strLog = "SELECT * FROM [dbo].[usuario] WHERE nome_usuario COLLATE SQL_Latin1_General_CP1_CS_AS = @nome AND senha COLLATE SQL_Latin1_General_CP1_CS_AS = @senha";

        private string _name;
        private string _senha;
        private string _senhaC;

        private string _nome_completo;
        private string _email_corporativo;
        private string _tel;
        private string _cpf;

        private string _cep;
        private string _cidade;
        private string _bairro;
        private string _rua;
        private string _numero;


        public bool _estLgd = false; //LOGIN BEM SUCEDIDO OU MAL SUCEDIDO
        public bool _cadScss = false; // CAD BEM SUCEDIDO OU MAL SUCEDIDO

        public string Name { get => _name; set => _name = value; }
        public string Senha { get => _senha; set => _senha = value; }
        public string SenhaC { get => _senhaC; set => _senhaC = value; }
        public string Nome_completo { get => _nome_completo; set => _nome_completo = value; }
        public string Email_corporativo { get => _email_corporativo; set => _email_corporativo = value; }
        public string Tel { get => _tel; set => _tel = value; }
        public string Cpf { get => _cpf; set => _cpf = value; }
        public string Cep { get => _cep; set => _cep = value; }
        public string Cidade { get => _cidade; set => _cidade = value; }
        public string Bairro { get => _bairro; set => _bairro = value; }
        public string Rua { get => _rua; set => _rua = value; }
        public string Numero { get => _numero; set => _numero = value; }

        public void Cadastrar()
        {
            Form1 telacad = new Form1();
            if(Senha != SenhaC)
            {
                MessageBox.Show("Senhas divergentes", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("Campo username está vazio", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (string.IsNullOrWhiteSpace(Senha))
            {
                MessageBox.Show("campo de senha está vazio", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    using (SqlCommand ver = new SqlCommand(strVer, con))
                    {
                        ver.Parameters.AddWithValue("@nome", Name);

                        int count = (int)ver.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Usuario ja existe, tente novamente", "Cadastro", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    int endereco_id;

                    using (SqlCommand cmdCad = new SqlCommand(dfnEnd, con))
                    {
                        cmdCad.Parameters.AddWithValue("@cep ", Cep);
                        cmdCad.Parameters.AddWithValue("@cidade ", Cidade);
                        cmdCad.Parameters.AddWithValue("@bairro ", Bairro);
                        cmdCad.Parameters.AddWithValue("@rua ", Rua);
                        cmdCad.Parameters.AddWithValue("@numero ", Numero);

                        // Executa a inserção do endereço e retorna o endereço_id gerado
                        cmdCad.CommandText += ";SELECT SCOPE_IDENTITY();";// Recupera o ID gerado
                        endereco_id = Convert.ToInt32(cmdCad.ExecuteScalar()); //Captura o endereco_id gerado


                    }

                    using (SqlCommand cmd = new SqlCommand(strcadastrar, con))
                    {

                        cmd.Parameters.AddWithValue("@nome", Name);
                        cmd.Parameters.AddWithValue("@senha", Senha);

                        cmd.Parameters.AddWithValue("@nome_completo", Nome_completo);
                        cmd.Parameters.AddWithValue("@email_corporativo", Email_corporativo);
                        cmd.Parameters.AddWithValue("@tel", Tel);
                        cmd.Parameters.AddWithValue("@cpf", Cpf);
                        cmd.Parameters.AddWithValue("@endereco_id", endereco_id);

                        int resultado = cmd.ExecuteNonQuery();
                        if (resultado == 1)
                        {
                            MessageBox.Show("Cadastro concluido", "cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _cadScss = true;
                        }
                        else
                        {
                            MessageBox.Show("gravação mal sucedida", "cadastro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERRO:{ex.Message}", "Erro", MessageBoxButtons.OK);
            }
        }

        public void login()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    using (SqlCommand cmdLog = new SqlCommand(strLog, con))
                    {
                        cmdLog.Parameters.AddWithValue("@nome", Name);
                        cmdLog.Parameters.AddWithValue("@senha", Senha);

                        using (SqlDataReader reader = cmdLog.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                MessageBox.Show("Usuario logado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _estLgd = true;
                            }
                            else
                            {
                                MessageBox.Show("Usuario ou senha estão incorretos", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERRO:{ex.Message}", "Erro", MessageBoxButtons.OK);
            }
        }
    }
}
