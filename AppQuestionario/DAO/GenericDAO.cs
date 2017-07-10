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
        public static bool ExclusaoGenericaDeRegistros(string nomeTabela, string nomeColuna, int id)
        {
            bool sucesso = false;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    string sql = String.Format("DELETE FROM {0} WHERE {1} = @id", nomeTabela, nomeColuna);
                    SqlCommand comando = new SqlCommand(sql, conexao);
                    comando.Parameters.Add(new SqlParameter("@id", id));

                    conexao.Open();
                    if (comando.ExecuteNonQuery() > 0)
                    {
                        sucesso = true;
                    }
                }
                catch (Exception)
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na execução do método de exclusão')<script>");
                }

            return sucesso;
        }

        // Retorna a quantidade de ids registrados recebendo o nome da tabela que deseja consultar
        public static int getLastId(string nomeTabela, string nomeColunaId)
        {
            int resultado = 0;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    string sql = String.Format("SELECT MAX({0}) FROM {1}", nomeColunaId, nomeTabela);
                    SqlCommand comando = new SqlCommand(sql, conexao);

                    conexao.Open();
                    resultado = Convert.ToInt32(comando.ExecuteScalar());
                }
                catch (Exception)
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na execução do método')<script>");
                }

            return resultado;
        }
    }
}