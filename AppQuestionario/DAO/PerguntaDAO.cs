using AppQuestionario.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppQuestionario.DAO
{
    public class PerguntaDAO
    {
        public BindingList<Pergunta> listaPerguntasDoQuestionario(int idQuestionario)
        {
            BindingList<Pergunta> listaPerguntas = new BindingList<Pergunta>();

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("SELECT * FROM PER_PERGUNTA_ifalcao WHERE per_id_questionario = @id", conexao);
                    comando.Parameters.Add(new SqlParameter("@id", idQuestionario));

                    conexao.Open();
                    using (SqlDataReader reader = comando.ExecuteReader())
                        while (reader.Read())
                        {
                            object[] registro = new object[reader.FieldCount];
                            reader.GetSqlValues(registro);
                            Pergunta novo = new Pergunta(Convert.ToInt32(reader.GetDecimal(0)), Convert.ToInt32(reader.GetDecimal(1)), reader.GetString(2), Convert.ToChar(registro[3].ToString()), Convert.ToChar(registro[4].ToString()), reader.GetInt32(5));
                            listaPerguntas.Add(novo);
                        }
                }
                catch (Exception)
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na execução do processo.');</script>");
                }

            return listaPerguntas;
        }

        public bool criarPergunta(Pergunta novaPergunta)
        {
            bool sucesso = false;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("INSERT INTO PER_PERGUNTA_ifalcao VALUES(@id, @idQuestionario, @descricao, @tipo, @obrigatoria, @ordem) ", conexao);
                    comando.Parameters.Add(new SqlParameter("@id", novaPergunta.Id));
                    comando.Parameters.Add(new SqlParameter("@idQuestionario", novaPergunta.IdQuestionario));
                    comando.Parameters.Add(new SqlParameter("@descricao", novaPergunta.Descricao));
                    comando.Parameters.Add(new SqlParameter("@tipo", novaPergunta.Tipo));
                    comando.Parameters.Add(new SqlParameter("@obrigatoria", novaPergunta.Obrigatoria));
                    comando.Parameters.Add(new SqlParameter("@ordem", novaPergunta.Ordem));

                    conexao.Open();
                    if (comando.ExecuteNonQuery() > 0)
                    {
                        sucesso = true;
                    }
                }
                catch (Exception)
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na execução do método')<script>");
                }

            return sucesso;
        }

        public static int getLastId()
        {
            int resultado = 0;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("SELECT MAX(per_id_questionario) FROM PER_PERGUNTA_ifalcao", conexao);

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