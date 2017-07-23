using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;

namespace AppQuestionario.DAO
{
    public class GenericDAO
    {

        private static SqlConnection getConexao()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        public static bool deleteFromId(string nomeTabela, string nomeColunaId, int id)
        {
            bool sucesso = false;

            using (SqlConnection conexao = getConexao())
                try
                {
                    string sql = String.Format("DELETE FROM {0} WHERE {1} = @id", nomeTabela, nomeColunaId);
                    SqlCommand comando = new SqlCommand(sql, conexao);
                    comando.Parameters.Add(new SqlParameter("@id", id));

                    conexao.Open();
                    if (comando.ExecuteNonQuery() > 0)
                        sucesso = true;
                }
                catch (Exception ex)
                {
                    exibeMensagemErro(ex.Message);
                }
            return sucesso;
        }

        // Retorna a quantidade de ids registrados recebendo o nome da tabela que deseja consultar
        public static int getLastId(string nomeTabela, string nomeColunaId)
        {
            int linhasAfetadas = 0;

            using (SqlConnection conexao = getConexao())
                try
                {
                    string sql = String.Format("SELECT MAX({0}) FROM {1}", nomeColunaId, nomeTabela);
                    SqlCommand comando = new SqlCommand(sql, conexao);

                    conexao.Open();
                    linhasAfetadas = Convert.ToInt32(comando.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    exibeMensagemErro(ex.Message);
                }

            return linhasAfetadas;
        }

        public static string getNomeFromId(string tabela, string colunaNome, string colunaId, int valorId)
        {
            string nome = null;

            using (SqlConnection conexao = getConexao())
                try
                {
                    string sql = String.Format("SELECT {0} FROM {1} WHERE {2} = @id", colunaNome, tabela, colunaId);
                    SqlCommand comando = new SqlCommand(sql, conexao);
                    comando.Parameters.Add(new SqlParameter("@id", valorId));

                    conexao.Open();
                    nome = comando.ExecuteScalar().ToString();
                }
                catch (Exception ex)
                {
                    exibeMensagemErro(ex.Message);
                }

            return nome;
        }

        private static void exibeMensagemErro(string mensagem)
        {
            string msgErro = String.Format("<script>alert('Erro: {0}')<script>", mensagem);
            HttpContext.Current.Response.Write(msgErro);
        }
    }
}