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
    public partial class Respostas : System.Web.UI.Page
    {
        PerguntaDAO perguntaDAO = new PerguntaDAO();
        OpcaoRespostaDAO opcaoDAO = new OpcaoRespostaDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlPerguntas.DataSource = perguntaDAO.getAllPerguntas();
                ddlPerguntas.DataTextField = "Descricao";
                ddlPerguntas.DataValueField = "Id";
                ddlPerguntas.DataBind();

                if (Session["perguntaSelecionada"] != null)
                {
                    carregarRespostas((int)Session["perguntaSelecionada"]);
                }
            }
            
        }

        private void carregarRespostas(int idPergunta)
        {
            tabelaRespostas.DataSource = opcaoDAO.listarOpcoesDaResposta(idPergunta);
            if (opcaoDAO.possuiOpcaoCorretaParaPergunta(idPergunta))
            {
                chkCorreta.Enabled = false;
            }
            else
            {
                chkCorreta.Enabled = true;
            }
            tabelaRespostas.DataBind();
        }

        protected void btnListarRespostas_Click(object sender, EventArgs e)
        {
            lblIdPergunta.Text = ddlPerguntas.SelectedValue;
            carregarRespostas(Convert.ToInt32(ddlPerguntas.SelectedValue));
        }

        protected void btnCriar_Click(object sender, EventArgs e)
        {
            if (lblIdPergunta.Text == "")
            {
                Response.Write("<script>alert('Precisa selecionar uma resposta antes!');</script>");
            }
            else
            {
                char correta = chkCorreta.Checked ? 'S' : 'N';
                OpcaoResposta novaResposta = new OpcaoResposta(Convert.ToInt32(lblIdPergunta.Text), txtDescricao.Text, correta, int.Parse(txtOrdem.Text));
                if (opcaoDAO.possuiOrdemDiferente(novaResposta))
                {
                    if (opcaoDAO.criarOpcaoResposta(novaResposta))
                    {
                        Response.Write("<script>alert('Resposta criada com sucesso!');</script>");
                        carregarRespostas(Convert.ToInt32(lblIdPergunta.Text));
                    }
                    else
                    {
                        Response.Write("<script>alert('Não foi possível criar a resposta!');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Já existe uma resposta com essa ordem! Mude a ordem e tente novamente.');</script>");
                }

            }
        }

        protected void tabelaRespostas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32((tabelaRespostas.Rows[index].FindControl("lblId") as Label).Text);

            if (e.CommandName == "Excluir")
            {
                try
                {
                    if (opcaoDAO.deletarResposta(id))
                     {
                        Response.Write("<script>alert('Resposta excluída com sucesso!');</script>");
                        carregarRespostas(Convert.ToInt32(lblIdPergunta.Text));
                    }
                    else
                    {
                        Response.Write("<script>alert('Não foi possível deletar a resposta!');</script>");
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