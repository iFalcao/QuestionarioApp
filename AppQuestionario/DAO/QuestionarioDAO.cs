﻿using AppQuestionario.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
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

        public BindingList<Questionario> getAllQuestionarios()
        {
            BindingList<Questionario> listaQuestionarios = new  BindingList<Questionario>();

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("SELECT * FROM QST_QUESTIONARIO_ifalcao ORDER BY qst_id_questionario", conexao);

                    conexao.Open();
                    using (SqlDataReader reader = comando.ExecuteReader())
                        while (reader.Read())
                        {
                            object[] registro = new object[reader.FieldCount];
                            reader.GetSqlValues(registro);
                            Questionario novo = new Questionario(Convert.ToInt32(reader.GetDecimal(0)), reader.GetString(1), Convert.ToChar(registro[2].ToString()), reader.GetString(3));
                            listaQuestionarios.Add(novo);
                        }
                }
                catch (Exception)
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na execução do processo getAllQuestionarios.');</script>");
                }
            return listaQuestionarios;
        }

        public bool deletarQuestionario(int idQuestionario)
        {
            return GenericDAO.ExclusaoGenericaDeRegistros("QST_QUESTIONARIO_ifalcao", "qst_id_questionario", idQuestionario);
        }

        public string getNome(int idQuestionario)
        {
            return GenericDAO.getNomeFromId("QST_QUESTIONARIO_ifalcao", "qst_nm_questionario", "qst_id_questionario", idQuestionario);
        }

        public bool possuiPerguntaMultiplaEscolha(int idQuestionario)
        {
            bool possui = false;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM PER_PERGUNTA_ifalcao perg INNER JOIN QST_QUESTIONARIO_ifalcao quest ON quest.qst_id_questionario = perg.per_id_questionario WHERE perg.per_tp_pergunta = 'M' 	AND quest.qst_id_questionario = @id", conexao);
                    comando.Parameters.Add(new SqlParameter("@id", idQuestionario));

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

        public bool possuiAlgumaPergunta(int idQuestionario)
        {
            bool possui = false;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM PER_PERGUNTA_ifalcao perg INNER JOIN QST_QUESTIONARIO_ifalcao quest ON perg.per_id_questionario = quest.qst_id_questionario WHERE quest.qst_id_questionario = @id", conexao);
                    comando.Parameters.Add(new SqlParameter("@id", idQuestionario));

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

        public static int getLastId()
        {
            return GenericDAO.getLastId("QST_QUESTIONARIO_ifalcao", "qst_id_questionario");
        }

        // Passa a instância do questionário com os novos atributos mas com o mesmo id
        public bool editarQuestionario(Questionario questionario)
        {
            bool sucesso = false;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("UPDATE QST_QUESTIONARIO_ifalcao SET qst_nm_questionario = @nome, qst_tp_questionario = @tipo, qst_ds_link_instrucoes = @link WHERE qst_id_questionario = @id ", conexao);
                    comando.Parameters.Add(new SqlParameter("@id", questionario.Id));
                    comando.Parameters.Add(new SqlParameter("@nome", questionario.Nome));
                    comando.Parameters.Add(new SqlParameter("@tipo", questionario.Tipo));
                    comando.Parameters.Add(new SqlParameter("@link", questionario.Link));

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


        public bool ehAvaliacao(int idQuestionario)
        {
            bool questionarioDeAvaliacao = false;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("SELECT * FROM QST_QUESTIONARIO_ifalcao WHERE qst_id_questionario = @id", conexao);
                    comando.Parameters.Add(new SqlParameter("@id", idQuestionario));
                    conexao.Open();
                    using (SqlDataReader reader = comando.ExecuteReader())
                        while (reader.Read())
                        {
                            object[] registro = new object[reader.FieldCount];
                            reader.GetSqlValues(registro);
                            questionarioDeAvaliacao = Convert.ToChar(registro[2].ToString()).Equals('A') ? true : false;
                        }
                }
                catch (Exception)
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na execução do processo ehAvaliacao.');</script>");
                }

            return questionarioDeAvaliacao;
        }
    }
}