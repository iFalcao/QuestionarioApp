using AppQuestionario.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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

        public BindingList<Questionario> getAllQuestionarios()
        {
            BindingList<Questionario> listaQuestionarios = new  BindingList<Questionario>();

            using (SqlConnection conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                try
                {
                    SqlCommand comando = new SqlCommand("SELECT * FROM QST_QUESTIONARIO_ifalcao", conexao);

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
                    HttpContext.Current.Response.Write("<script>alert('Erro na execução do processo.');</script>");
                }
            return listaQuestionarios;
        }

    }
}