using AppQuestionario.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppQuestionario.DAO
{
    public class QuestionarioDAO
    {

        public bool criarQuestionario(Questionario questionario)
        {
            bool sucesso = false;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("INSERT INTO QST_QUESTIONARIO_ifalcao VALUES(@id, @nome, @tipo, @link) ", conexao);
                    comando.Parameters.Add(new SqlParameter("@id", questionario.Id));
                    comando.Parameters.Add(new SqlParameter("@nome", questionario.Nome));
                    comando.Parameters.Add(new SqlParameter("@tipo", questionario.Tipo));
                    comando.Parameters.Add(new SqlParameter("@link", questionario.Link));
                    
                    conexao.Open();
                    if (comando.ExecuteNonQuery() > 0){
                        sucesso = true;
                    }
                }
                catch (Exception)
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na execução do método')<script>");
                }

            return sucesso;
        }

    }
}