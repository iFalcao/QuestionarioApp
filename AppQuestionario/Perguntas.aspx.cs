using AppQuestionario.DAO;
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

        }

        protected void btnListarPerguntas_Click(object sender, EventArgs e)
        {
            tabelaPerguntas.DataSource = perguntaDAO.listaPerguntasDoQuestionario(Convert.ToInt32(ddlQuestionarios.SelectedValue));
            tabelaPerguntas.DataBind();
        }
    }
}