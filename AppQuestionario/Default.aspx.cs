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
                ddlTipos.Items.Add(new ListItem("Pesquisa", "P"));
                ddlTipos.Items.Add(new ListItem("Avaliação", "A"));
            }
        }

        protected void btnCriar_Click(object sender, EventArgs e)
        {
            Questionario novoQuestionario = new Questionario(Nome.Text, char.Parse(ddlTipos.SelectedValue), Link.Text);
            if (questDAO.criarQuestionario(novoQuestionario))
            {
                Response.Write("<script>alert('Questionário Criado com Sucesso!')<script>");
            }
        }
    }
}