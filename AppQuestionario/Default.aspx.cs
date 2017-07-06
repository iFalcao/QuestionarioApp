using AppQuestionario.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppQuestionario.Models;

namespace AppQuestionario
{
    public partial class _Default : Page
    {
        QuestionarioDAO questDAO = new QuestionarioDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregaValores();
            }
        }

        private void carregaValores()
        {
            tabelaQuestionarios.DataSource = questDAO.getAllQuestionarios();
            tabelaQuestionarios.DataBind();
            ddlTipos.Items.Clear();
            ddlTipos.Items.Add(new ListItem("Pesquisa", "P"));
            ddlTipos.Items.Add(new ListItem("Avaliação", "A"));
        }

        protected void btnCriar_Click(object sender, EventArgs e)
        {
            // Valida link
            if (Link.Text.StartsWith("Http://"))
            {
                Questionario novoQuestionario = new Questionario(Nome.Text, char.Parse(ddlTipos.SelectedValue), Link.Text);
                if (questDAO.criarQuestionario(novoQuestionario))
                {
                    Response.Write("<script>alert('Questionário Criado com Sucesso!')<script>");
                    carregaValores();
                }
            }
            else
            {
                lblError.Text = "Link deve começar com 'Http://'";
            }
            
        }

        protected void tabelaQuestionarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    int id = Convert.ToInt32((tabelaQuestionarios.Rows[index].FindControl("lblId") as Label).Text);
                    if (questDAO.possuiAlgumaPergunta(id))
                    {
                        Response.Write("<script>alert('Questionário possui uma pergunta e portanto não pode ser deletado!');</script>");
                    }
                    else
                    {
                        if (questDAO.deletarQuestionario(id))
                        {
                            Response.Write("<script>alert('Questionário excluído com sucesso!');</script>");
                            carregaValores();
                        }
                        else
                        {
                            Response.Write("<script>alert('Questionário não deletado!');</script>");
                        }
                    }
                   
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('Erro ao executar método');</script>");
                }
            }
        }
    }
}