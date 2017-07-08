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
        public static bool ExclusaoGenericaDeRegistros(string sql, int id)
        {
            bool sucesso = false;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
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

        public static int getLastId(string sql)
        {
            int resultado = 0;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
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