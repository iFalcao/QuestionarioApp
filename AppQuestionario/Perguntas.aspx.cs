using AppQuestionario.DAO;
using AppQuestionario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppQuestionario.BasePages;

namespace AppQuestionario
{
    public partial class Perguntas : BasePage
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

                ddlOrdem.Items.Clear();
                ddlOrdem.Items.AddRange(Enumerable.Range(1, 50).Select (x => new ListItem(x.ToString())).ToArray());

                ddlTipos.Items.Clear();
                ddlTipos.Items.Add(new ListItem("Única Escolha", "U"));
                ddlTipos.Items.Add(new ListItem("Múltipla Escolha", "M"));
                posEdicao();

                if (Session["questionarioSelecionado"] != null)
                {
                    carregarPerguntas((int)Session["questionarioSelecionado"]);
                }
            }
        }

        private void carregarPerguntas(int idQuestionario)
        {
            lblIdQuestionario.Text = idQuestionario.ToString();
            tabelaPerguntas.DataSource = perguntaDAO.listaPerguntasDoQuestionario(idQuestionario);
            lblListandoPerguntas.Text = "Listando perguntas de '" + questDAO.getNome(idQuestionario) + "'";
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
                this.AddAlertErrorMessage("Precisa selecionar um questionário antes!");
            }
            else
            {
                char obrigatoria = chkObrigatoria.Checked ? 'S' : 'N';
                int idQuestionario = int.Parse(lblIdQuestionario.Text);
                int ordem = int.Parse(ddlOrdem.SelectedValue);

                if (perguntaDAO.possuiOrdemDiferente(idQuestionario, ordem))
                {
                    Pergunta novaPergunta = new Pergunta(idQuestionario, txtDescricao.Text, char.Parse(ddlTipos.SelectedValue), obrigatoria, ordem);
                    if (perguntaDAO.criarPergunta(novaPergunta))
                    {
                        this.AddAlertSuccessMessage("Pergunta criada com sucesso!");
                        carregarPerguntas(Convert.ToInt32(lblIdQuestionario.Text));
                    }
                    else
                    {
                        this.AddAlertErrorMessage("Não foi possível criar a pergunta!");
                    }
                }
                else
                {
                    this.AddAlertErrorMessage("Já existe uma pergunta com essa ordem! Mude a ordem e tente novamente.");
                }
            
            }
        }

        protected void btnListarPerguntas_Click(object sender, EventArgs e)
        {
            carregarPerguntas(Convert.ToInt32(ddlQuestionarios.SelectedValue));
        }

        protected void tabelaPerguntas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int LinhaSelecionada = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32((tabelaPerguntas.Rows[LinhaSelecionada].FindControl("lblId") as Label).Text);

            if (e.CommandName == "Excluir")
            {
                try
                {
                    if (perguntaDAO.possuiAlgumaOpcaoResposta(id))
                    {
                        this.AddAlertErrorMessage("A pergunta possui uma opção de resposta e portanto não pode ser deletada!");
                    }
                    else
                    {
                        if (perguntaDAO.deletarPergunta(id))
                        {
                            this.AddAlertSuccessMessage("Resposta excluída com sucesso!");
                            carregarPerguntas(Convert.ToInt32(lblIdQuestionario.Text));
                        }
                        else
                        {
                            this.AddAlertErrorMessage("Não foi possível deletar a pergunta!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.AddAlertErrorMessage(ex.Message);
                }
            }
            else if (e.CommandName == "VisualizarRespostas")
            {
                Session["perguntaSelecionada"] = id;
                Response.Redirect("Respostas.aspx");
            }
            else if (e.CommandName == "Editar")
            {
                lblIdPergunta.Text = id.ToString();
                txtDescricao.Text = (tabelaPerguntas.Rows[LinhaSelecionada].FindControl("lblDescricao") as Label).Text;
                ddlTipos.SelectedValue = (tabelaPerguntas.Rows[LinhaSelecionada].FindControl("lblTipos") as Label).Text.Equals("Única Escolha") ? "U" : "M";
                preEdicao(id);
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Pergunta perguntaEditada = new Pergunta(int.Parse(lblIdPergunta.Text), int.Parse(lblIdQuestionario.Text), txtDescricao.Text, char.Parse(ddlTipos.SelectedValue), chkObrigatoria.Checked ? 'S' : 'N', int.Parse(ddlOrdem.SelectedValue));
            if (perguntaDAO.editarPergunta(perguntaEditada))
            {
                this.AddAlertSuccessMessage("Resposta editada com sucesso!");
                posEdicao();
                carregarPerguntas(Convert.ToInt32(ddlQuestionarios.SelectedValue));
            }
            else
            {
                this.AddAlertErrorMessage("Erro ao editar pergunta");
            }
        }

        private void preEdicao(int idSelecionado)
        {
            lblIdPergunta.Visible = true;
            lblEditingId.Visible = true;
            lblAcao.Text = "Editar pergunta '" + perguntaDAO.getNome(idSelecionado) + "'";
            btnCriar.Visible = false;
            btnEditar.Visible = true;
        }
        private void posEdicao()
        {
            lblEditingId.Visible = false;
            lblIdPergunta.Visible = false;
            lblAcao.Text = "Criar Pergunta.";
            btnCriar.Visible = true;
            btnEditar.Visible = false;
            txtDescricao.Text = "";
        }
    }
}