using AppQuestionario.DAO;
using AppQuestionario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppQuestionario.BasePages;

namespace AppQuestionario
{
    public partial class Respostas : BasePage
    {
        PerguntaDAO perguntaDAO = new PerguntaDAO();
        OpcaoRespostaDAO opcaoDAO = new OpcaoRespostaDAO();
        QuestionarioDAO questionarioDAO = new QuestionarioDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlQuestionarios.DataSource = questionarioDAO.getAllQuestionarios();
                ddlQuestionarios.DataTextField = "Nome";
                ddlQuestionarios.DataValueField = "Id";
                ddlQuestionarios.DataBind();

                ddlOrdem.Items.Clear();
                ddlOrdem.Items.AddRange(Enumerable.Range(1, 15).Select(x => new ListItem(x.ToString())).ToArray());

                ddlPerguntas.DataBind();
                posEdicao();

                if (Session["perguntaSelecionada"] != null)
                {
                    carregarRespostas((int)Session["perguntaSelecionada"]);
                }
            }
        }

        private void carregarRespostas(int idPergunta)
        {
            lblIdPergunta.Text = idPergunta.ToString();
            lblListandoRespostas.Text = "Listando respostas de '" + perguntaDAO.getNome(idPergunta) + "'";
            tabelaRespostas.DataSource = opcaoDAO.listarOpcoesDaResposta(idPergunta);
            tabelaRespostas.DataBind();
            if (opcaoDAO.possuiOpcaoCorretaParaPergunta(idPergunta))
            {
                chkCorreta.Checked = false;
                chkCorreta.Enabled = false;
            }
            else
            {
                chkCorreta.Enabled = true;
            }
        }

        protected void btnListarRespostas_Click(object sender, EventArgs e)
        {
            // O drop down de  Perguntas é comparado com o seu texto pois não possui uma opção default de valor ""
            if (ddlQuestionarios.SelectedValue == "" || ddlPerguntas.Text == "")
            {
                lblErroListarRespostas.Text = "Primeiro selecione um questionário e uma pergunta";
            }
            else
            {
                lblIdPergunta.Text = ddlPerguntas.SelectedValue;
                carregarRespostas(Convert.ToInt32(ddlPerguntas.SelectedValue));
            }
        }

        protected void btnCriar_Click(object sender, EventArgs e)
        {
            if (lblIdPergunta.Text == "")
            {
                this.AddAlertErrorMessage("Precisa selecionar um questionário antes!");
            }
            else
            {
                int idPergunta = int.Parse(lblIdPergunta.Text);
                int ordem = int.Parse(ddlOrdem.SelectedValue);

                if (opcaoDAO.possuiOrdemDiferente(ordem, idPergunta))
                {
                    OpcaoResposta novaResposta = new OpcaoResposta(idPergunta, txtDescricao.Text, chkCorreta.Checked ? 'S' : 'N', ordem);
                    if (opcaoDAO.criarOpcaoResposta(novaResposta))
                    {
                        Response.Write("<script>alert('Resposta criada com sucesso!');</script>");
                        carregarRespostas(Convert.ToInt32(lblIdPergunta.Text));
                    }
                    else
                    {
                        this.AddAlertErrorMessage("Não foi possível criar a resposta!");
                    }
                }
                else
                {
                    this.AddAlertErrorMessage("Já existe uma resposta com essa ordem! Mude a ordem e tente novamente.");
                }

            }
        }

        protected void tabelaRespostas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int LinhaSelecionada = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32((tabelaRespostas.Rows[LinhaSelecionada].FindControl("lblId") as Label).Text);

            if (e.CommandName == "Excluir")
            {
                if (opcaoDAO.deletarResposta(id))
                {
                    Response.Write("<script>alert('Resposta excluída com sucesso!');</script>");
                    carregarRespostas(Convert.ToInt32(lblIdPergunta.Text));
                }
                else
                {
                    this.AddAlertErrorMessage("Não foi possível excluir a pergunta.");
                }
            }
            else if (e.CommandName == "Editar")
            {
                lblIdResposta.Text = id.ToString();
                txtDescricao.Text = (tabelaRespostas.Rows[LinhaSelecionada].FindControl("lblDescricao") as Label).Text;
                chkCorreta.Checked = (tabelaRespostas.Rows[LinhaSelecionada].FindControl("lblCorreta") as Label).Text.Equals("Sim") ? true : false;
                preEdicao(id);
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            OpcaoResposta respostaEditada = new OpcaoResposta(Convert.ToInt32(lblIdResposta.Text), Convert.ToInt32(lblIdPergunta.Text), txtDescricao.Text, chkCorreta.Checked ? 'S' : 'N', int.Parse(ddlOrdem.SelectedValue));
            if (opcaoDAO.editarOpcaoResposta(respostaEditada))
            {
                Response.Write("<script>alert('Resposta editada com sucesso!');</script>");
                posEdicao();
                carregarRespostas(Convert.ToInt32(lblIdPergunta.Text));
            }
            else
            {
                this.AddAlertErrorMessage("Erro ao editar resposta");
            }
        }

        private void preEdicao(int idSelecionado)
        {
            lblIdResposta.Visible = true;
            lblEditingId.Visible = true;
            lblAcao.Text = "Editar resposta '" + opcaoDAO.getNome(idSelecionado) + "'";
            btnCriar.Visible = false;
            btnEditar.Visible = true;
        }
        private void posEdicao()
        {
            lblEditingId.Visible = false;
            lblIdResposta.Visible = false;
            lblAcao.Text = "Criar Resposta.";
            btnCriar.Visible = true;
            btnEditar.Visible = false;
            txtDescricao.Text = "";
        }

        protected void ddlQuestionarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Se selecionar um valor, a opção padrão 'Selecione' fica indisponível
            ddlQuestionarios.Items[0].Enabled = false;

            ddlPerguntas.DataSource = perguntaDAO.listaPerguntasDoQuestionario(int.Parse(ddlQuestionarios.SelectedValue));
            ddlPerguntas.DataTextField = "Descricao";
            ddlPerguntas.DataValueField = "Id";
            ddlPerguntas.DataBind();
        }
    }
}