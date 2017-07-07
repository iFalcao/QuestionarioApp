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
            }
        }

        protected void btnCriar_Click(object sender, EventArgs e)
        {
            char obrigatoria = chkObrigatoria.Checked ? 'S' : 'N';
            Pergunta novaPergunta = new Pergunta(Convert.ToInt32(lblIdQuestionario.Text), txtDescricao.Text, char.Parse(ddlTipos.SelectedValue), obrigatoria, int.Parse(txtOrdem.Text));
            if (perguntaDAO.criarPergunta(novaPergunta))
            {
                Response.Write("<script>alert('Pergunta criada com sucesso!');</script>");
            }
            else
            {
                Response.Write("<script>alert('Não foi possível criar a pergunta!');</script>");
            }
            
        }

        protected void btnListarPerguntas_Click(object sender, EventArgs e)
        {
            lblIdQuestionario.Text = ddlQuestionarios.SelectedValue;
            tabelaPerguntas.DataSource = perguntaDAO.listaPerguntasDoQuestionario(Convert.ToInt32(ddlQuestionarios.SelectedValue));
            tabelaPerguntas.DataBind();
        }
    }
}