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
            else if (e.CommandName == "Editar")
            {
                lblIdResposta.Text = id.ToString();
                txtDescricao.Text = (tabelaRespostas.Rows[index].FindControl("lblDescricao") as Label).Text;
                preEdicao();
            }
        }


        protected void btnEditar_Click(object sender, EventArgs e)
        {
            OpcaoResposta respostaEditada = new OpcaoResposta(Convert.ToInt32(lblIdResposta.Text), Convert.ToInt32(lblIdPergunta.Text), txtDescricao.Text, chkCorreta.Checked ? 'S' : 'N', int.Parse(ddlOrdem.SelectedValue));
            if (opcaoDAO.editarOpcaoResposta(respostaEditada))
            {
                posEdicao();
                carregarRespostas(Convert.ToInt32(lblIdResposta.Text));
            }
            else
            {
                Response.Write("<script>alert('Erro ao editar resposta');</script>");
            }
        }

        private void preEdicao()
        {
            lblIdResposta.Visible = true;
            lblEditingId.Visible = true;
            lblAcao.Text = "Editar Resposta";
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
    }
}