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
                    SqlCommand comando = new SqlCommand("SELECT * FROM PER_PERGUNTA_ifalcao WHERE per_id_questionario = @id ORDER BY per_nu_ordem", conexao);
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

        public BindingList<Pergunta> getAllPerguntas()
        {
            BindingList<Pergunta> listaPerguntas = new BindingList<Pergunta>();

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("SELECT * FROM PER_PERGUNTA_ifalcao ORDER BY per_id_pergunta", conexao);

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
                    HttpContext.Current.Response.Write("<script>alert('Erro na execução do processo getAllPerguntas.');</script>");
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
                    HttpContext.Current.Response.Write("<script>alert('Erro na execução do método de criação da pergunta')<script>");
                }

            return sucesso;
        }

        public static int getLastId()
        {
            return GenericDAO.getLastId("PER_PERGUNTA_ifalcao", "per_id_pergunta");
        }

        public string getNome(int idPergunta)
        {
            return GenericDAO.getNomeFromId("PER_PERGUNTA_ifalcao", "per_ds_pergunta", "per_id_pergunta", idPergunta);
        }

        public bool editarPergunta(Pergunta pergunta)
        {
            bool sucesso = false;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("UPDATE PER_PERGUNTA_ifalcao SET per_id_questionario = @idQuestionario, per_ds_pergunta = @descricao, per_tp_pergunta = @tipo , per_ch_resposta_obrigatoria = @obrigatoria, per_nu_ordem = @ordem WHERE per_id_pergunta = @id", conexao);
                    comando.Parameters.Add(new SqlParameter("@id", pergunta.Id));
                    comando.Parameters.Add(new SqlParameter("@idQuestionario", pergunta.IdQuestionario));
                    comando.Parameters.Add(new SqlParameter("@descricao", pergunta.Descricao));
                    comando.Parameters.Add(new SqlParameter("@tipo", pergunta.Tipo));
                    comando.Parameters.Add(new SqlParameter("@obrigatoria", pergunta.Obrigatoria));
                    comando.Parameters.Add(new SqlParameter("@ordem", pergunta.Ordem));

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

        public bool possuiOrdemDiferente(int idQuestionario, int ordem)
        {
            bool possui = true;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM PER_PERGUNTA_ifalcao WHERE per_id_questionario = @idQuestionario AND per_nu_ordem = @ordem", conexao);
                    comando.Parameters.Add(new SqlParameter("@idQuestionario", idQuestionario));
                    comando.Parameters.Add(new SqlParameter("@ordem", ordem));

                    conexao.Open();
                    if (Convert.ToInt32(comando.ExecuteScalar()) > 0)
                    {
                        possui = false;
                    }
                }
                catch (Exception)
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na execução do método')<script>");
                }

            return possui;
        }

        public bool possuiAlgumaOpcaoResposta(int idPergunta)
        {
            bool possui = false;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM OPR_OPCAO_RESPOSTA_ifalcao opcao INNER JOIN PER_PERGUNTA_ifalcao perg ON opcao.opr_id_pergunta = perg.per_id_pergunta WHERE perg.per_id_pergunta = @id", conexao);
                    comando.Parameters.Add(new SqlParameter("@id", idPergunta));

                    conexao.Open();
                    if (Convert.ToInt32(comando.ExecuteScalar()) > 0)
                    {
                        possui = true;
                    }
                }
                catch (Exception)
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na execução do método')<script>");
                }

            return possui;
        }

        public bool deletarPergunta(int idPergunta)
        {
            return GenericDAO.ExclusaoGenericaDeRegistros("PER_PERGUNTA_ifalcao", "per_id_pergunta", idPergunta);
        }
    }
}