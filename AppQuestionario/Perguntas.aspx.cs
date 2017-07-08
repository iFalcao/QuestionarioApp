using AppQuestionario.DAO;
using AppQuestionario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppQuestionario
{
    public partial class Perguntas : System.Web.UI.Page
    {
        QuestionarioDAO questDAO = new QuestionarioDAO();
        PerguntaDAO perguntaDAO = new PerguntaDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlQuestionarios.DataSource = questDAO.getAllQuestionarios();
                ddlQuestionarios.DataTextField = "Nome";
                ddlQuestionarios.DataValueField = "Id";
                ddlQuestionarios.DataBind();
                ddlTipos.Items.Clear();
                ddlTipos.Items.Add(new ListItem("Única Escolha", "U"));
                ddlTipos.Items.Add(new ListItem("Múltipla Escolha", "M"));
            }
        }

        private void carregarPerguntas(int idQuestionario)
        {
            tabelaPerguntas.DataSource = perguntaDAO.listaPerguntasDoQuestionario(idQuestionario);
            if (questDAO.ehAvaliacao(idQuestionario))
            {
                // Não permite selecionar o tipo de múltipla escolha para questionários do tipo AVALIAÇÃO
                ddlTipos.Enabled = false;
            }
            else
            {
                ddlTipos.Enabled = true;
            }
            tabelaPerguntas.DataBind();
        }

        protected void btnCriar_Click(object sender, EventArgs e)
        {
            if (lblIdQuestionario.Text == "")
            {
                Response.Write("<script>alert('Precisa selecionar um questionário antes!');</script>");
            }
            else
            {
                char obrigatoria = chkObrigatoria.Checked ? 'S' : 'N';
                Pergunta novaPergunta = new Pergunta(Convert.ToInt32(lblIdQuestionario.Text), txtDescricao.Text, char.Parse(ddlTipos.SelectedValue), obrigatoria, int.Parse(txtOrdem.Text));
                if (perguntaDAO.possuiOrdemDiferente(novaPergunta))
                {
                    if (perguntaDAO.criarPergunta(novaPergunta))
                    {
                        Response.Write("<script>alert('Pergunta criada com sucesso!');</script>");
                        carregarPerguntas(Convert.ToInt32(lblIdQuestionario.Text));
                    }
                    else
                    {
                        Response.Write("<script>alert('Não foi possível criar a pergunta!');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Já existe uma pergunta com essa ordem! Mude a ordem e tente novamente.');</script>");
                }
            
            }
        }

        protected void btnListarPerguntas_Click(object sender, EventArgs e)
        {
            lblIdQuestionario.Text = ddlQuestionarios.SelectedValue;
            carregarPerguntas(Convert.ToInt32(ddlQuestionarios.SelectedValue));
        }

        protected void tabelaPerguntas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32((tabelaPerguntas.Rows[index].FindControl("lblId") as Label).Text);

            if (e.CommandName == "Excluir")
            {
                try
                {
                    if (perguntaDAO.possuiAlgumaOpcaoResposta(id))
                    {
                        Response.Write("<script>alert('A pergunta possui uma opção de resposta e portanto não pode ser deletada!');</script>");
                    }
                    else
                    {
                        if (perguntaDAO.deletarPergunta(id))
                        {
                            Response.Write("<script>alert('Pergunta excluída com sucesso!');</script>");
                            carregarPerguntas(Convert.ToInt32(lblIdQuestionario.Text));
                        }
                        else
                        {
                            Response.Write("<script>alert('Não foi possível deletar a pergunta!');</script>");
                        }
                    }
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('Erro ao executar método');</script>");
                }
            }
            else if (e.CommandName == "VisualizarRespostas")
            {
                Session["perguntaSelecionada"] = id;
                Response.Redirect("Respostas.aspx");
            }
        }
    }
}