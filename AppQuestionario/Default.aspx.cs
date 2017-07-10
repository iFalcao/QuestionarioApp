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
                posEdicao();
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
                            Response.Write("<script>alert('Não foi possível deletar o questionário!');</script>");
                        }
                    }
                   
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('Erro ao executar método');</script>");
                }
            }

            else if (e.CommandName == "Editar")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    int id = Convert.ToInt32((tabelaQuestionarios.Rows[index].FindControl("lblId") as Label).Text);

                    lblIdEdit.Text = id.ToString();
                    Nome.Text = (tabelaQuestionarios.Rows[index].FindControl("lblNome") as Label).Text;
                    Link.Text = (tabelaQuestionarios.Rows[index].FindControl("lblLink") as Label).Text;
                    preEdicao();
                }
                catch(Exception)
                {
                    Response.Write("<script>alert('Erro ao editar o registro');</script>");
                }
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (Link.Text.StartsWith("Http://"))
            {
                Questionario novoQuestionario = new Questionario(int.Parse(lblIdEdit.Text), Nome.Text, char.Parse(ddlTipos.SelectedValue), Link.Text);
                if (questDAO.editarQuestionario(novoQuestionario))
                {
                    Response.Write("<script>alert('Questionário Atualizado com Sucesso!')<script>");
                    posEdicao();
                    carregaValores();
                }
            }
            else
            {
                lblError.Text = "Link deve começar com 'Http://'";
            }
        }
        private void preEdicao()
        {
            lblEditingId.Visible = true;
            lblIdEdit.Visible = true;
            lblAcao.Text = "Editar Questionário";
            btnCriar.Visible = false;
            btnEditar.Visible = true;
        }
        private void posEdicao()
        {
            lblEditingId.Visible = false;
            lblIdEdit.Visible = false;
            lblAcao.Text = "Criar Questionário";
            btnCriar.Visible = true;
            btnEditar.Visible = false;
            Nome.Text = "";
            Link.Text = "";
        }
                    
    }
}