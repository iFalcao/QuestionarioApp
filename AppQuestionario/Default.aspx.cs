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
        QuestionarioDAO questionarioDAO = new QuestionarioDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // A função 'posEdicao' irá mostrar a parte de inserção de novos registros ao invés da parte de edição
                posEdicao();
                carregaValores();
            }
        }

        private void carregaValores()
        {
            tabelaQuestionarios.DataSource = questionarioDAO.getAllQuestionarios();
            tabelaQuestionarios.DataBind();
            ddlTipos.Items.Clear();
            ddlTipos.Items.Add(new ListItem("Pesquisa", "P"));
            ddlTipos.Items.Add(new ListItem("Avaliação", "A"));
        }

        protected void btnCriar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaLinkDoQuestionario(Link.Text))
                {
                    Questionario novoQuestionario = new Questionario(Nome.Text, char.Parse(ddlTipos.SelectedValue), Link.Text);
                    if (questionarioDAO.criarQuestionario(novoQuestionario))
                    {
                        Response.Write("<script>alert('Questionário Criado com Sucesso!')<script>");
                        carregaValores();
                    }
                }
                else
                {
                    lblError.Text = "Link deve começar com 'Http://'";
                }
            } catch (Exception)
            {
                Response.Write("<script>alert('Erro ao executar a criação do questionário')<script>");
            }
            
        }

        private bool validaLinkDoQuestionario(string text)
        {
            return Link.Text.StartsWith("Http://");
        }

        protected void tabelaQuestionarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int LinhaSelecionada = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32((tabelaQuestionarios.Rows[LinhaSelecionada].FindControl("lblId") as Label).Text);

            if (e.CommandName == "Excluir")
            {
                try
                {
                    if (questionarioDAO.possuiAlgumaPergunta(id))
                    {
                        Response.Write("<script>alert('Questionário possui uma pergunta e portanto não pode ser deletado!');</script>");
                    }
                    else
                    {
                        if (questionarioDAO.deletarQuestionario(id))
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
                    Response.Write("<script>alert('Erro ao executar método de exclusão do questionário');</script>");
                }
            }
            else if (e.CommandName == "Editar")
            {
                try
                {
                    lblIdEdit.Text = id.ToString();
                    if (questionarioDAO.possuiPerguntaMultiplaEscolha(id))
                    {
                        ddlTipos.Enabled = false;
                    }
                    else
                    {
                        ddlTipos.Enabled = true;
                    }
                    Nome.Text = (tabelaQuestionarios.Rows[LinhaSelecionada].FindControl("lblNome") as Label).Text;
                    Link.Text = (tabelaQuestionarios.Rows[LinhaSelecionada].FindControl("lblLink") as Label).Text;
                    preEdicao(id);
                }
                catch(Exception)
                {
                    Response.Write("<script>alert('Erro ao editar o registro');</script>");
                }
            }
            else if (e.CommandName == "VisualizarPerguntas")
            {
                Session["questionarioSelecionado"] = id;
                Response.Redirect("Perguntas.aspx");
            }

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaLinkDoQuestionario(Link.Text))
                {
                    Questionario novoQuestionario = new Questionario(int.Parse(lblIdEdit.Text), Nome.Text, char.Parse(ddlTipos.SelectedValue), Link.Text);
                    if (questionarioDAO.editarQuestionario(novoQuestionario))
                    {
                        Response.Write("<script>alert('Questionário Atualizado com Sucesso!')<script>");
                        ddlTipos.Enabled = true;
                        posEdicao();
                        carregaValores();
                    }
                }
                else
                {
                    lblError.Text = "Link deve começar com 'Http://'";
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Erro ao executar a criação do questionário')<script>");
            }
        }
        private void preEdicao(int idSelecionado)
        {
            lblEditingId.Visible = true;
            lblIdEdit.Visible = true;
            lblAcao.Text = "Editar questionário '" + questionarioDAO.getNome(idSelecionado) + "'";
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