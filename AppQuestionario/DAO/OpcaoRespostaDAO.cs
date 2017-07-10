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
    public class OpcaoRespostaDAO
    {
        public BindingList<OpcaoResposta> listarOpcoesDaResposta(int idPergunta)
        {
            BindingList<OpcaoResposta> listaOpcoesDeResposta = new BindingList<OpcaoResposta>();

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("SELECT * FROM OPR_OPCAO_RESPOSTA_ifalcao WHERE opr_id_pergunta = @id ORDER BY opr_nu_ordem", conexao);
                    comando.Parameters.Add(new SqlParameter("@id", idPergunta));

                    conexao.Open();
                    using (SqlDataReader reader = comando.ExecuteReader())
                        while (reader.Read())
                        {
                            object[] registro = new object[reader.FieldCount];
                            reader.GetSqlValues(registro);
                            OpcaoResposta novo = new OpcaoResposta(Convert.ToInt32(reader.GetDecimal(0)), Convert.ToInt32(reader.GetDecimal(1)), reader.GetString(2), Convert.ToChar(registro[3].ToString()), reader.GetInt32(4));
                            listaOpcoesDeResposta.Add(novo);
                        }
                }
                catch (Exception)
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na execução do processo.');</script>");
                }

            return listaOpcoesDeResposta;
        }

        public static int getLastId()
        {
            return GenericDAO.getLastId("OPR_OPCAO_RESPOSTA_ifalcao", "opr_id_opcao_resposta");
        }

        public bool criarOpcaoResposta(OpcaoResposta novaOpcaoResposta)
        {
            bool sucesso = false;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("INSERT INTO OPR_OPCAO_RESPOSTA_ifalcao VALUES(@id, @idPergunta, @descricao, @correta, @ordem)", conexao);
                    comando.Parameters.Add(new SqlParameter("@id", novaOpcaoResposta.Id));
                    comando.Parameters.Add(new SqlParameter("@idPergunta", novaOpcaoResposta.IdPerguntaRelacionada));
                    comando.Parameters.Add(new SqlParameter("@descricao", novaOpcaoResposta.Descricao));
                    comando.Parameters.Add(new SqlParameter("@correta", novaOpcaoResposta.Correta));
                    comando.Parameters.Add(new SqlParameter("@ordem", novaOpcaoResposta.Ordem));

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

        public bool possuiOrdemDiferente(int ordem, int idPergunta)
        {
            bool possui = true;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM OPR_OPCAO_RESPOSTA_ifalcao WHERE opr_id_pergunta = @idPergunta AND opr_nu_ordem = @ordem", conexao);
                    comando.Parameters.Add(new SqlParameter("@idPergunta", idPergunta));
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

        public bool editarOpcaoResposta(OpcaoResposta opcaoResposta)
        {
            bool sucesso = false;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("UPDATE OPR_OPCAO_RESPOSTA_ifalcao SET opr_id_pergunta = @idPergunta, opr_ds_opcao_resposta = @descricao, opr_ch_resposta_correta = @correta, opr_nu_ordem = @ordem WHERE opr_id_opcao_resposta = @id", conexao);
                    comando.Parameters.Add(new SqlParameter("@id", opcaoResposta.Id));
                    comando.Parameters.Add(new SqlParameter("@idPergunta", opcaoResposta.IdPerguntaRelacionada));
                    comando.Parameters.Add(new SqlParameter("@descricao", opcaoResposta.Descricao));
                    comando.Parameters.Add(new SqlParameter("@correta", opcaoResposta.Correta));
                    comando.Parameters.Add(new SqlParameter("@ordem", opcaoResposta.Ordem));

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

        public bool deletarResposta(int idResposta)
        {
            return GenericDAO.ExclusaoGenericaDeRegistros("OPR_OPCAO_RESPOSTA_ifalcao", "opr_id_opcao_resposta", idResposta);
        }

        public string getNome(int idPergunta)
        {
            return GenericDAO.getNomeFromId("OPR_OPCAO_RESPOSTA_ifalcao", "opr_ds_opcao_resposta", "opr_id_opcao_resposta", idPergunta);
        }

        // Verifica se a pergunta possui alguma opção resposta selecionada como correta
        public bool possuiOpcaoCorretaParaPergunta(int idPergunta)
        {
            bool possui = false;

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM OPR_OPCAO_RESPOSTA_ifalcao opcao INNER JOIN PER_PERGUNTA_ifalcao perg ON opcao.opr_id_pergunta = perg.per_id_pergunta WHERE perg.per_id_pergunta = @id AND opr_ch_resposta_correta = 'S'", conexao);
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
    }
}