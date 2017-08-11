﻿using AppQuestionario.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppQuestionario.Models;
using AppQuestionario.BasePages;

namespace AppQuestionario
{
    public partial class _Default : BasePage
    {
        QuestionarioDAO questionarioDAO = new QuestionarioDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // A função 'posEdicao' irá mostrar a parte de inserção de novos registros ao invés da parte de edição
                this.posEdicao();
                this.carregaValores();
            }
        }

        private void carregaValores()
        {
            this.tabelaQuestionarios.DataSource = questionarioDAO.getAllQuestionarios();
            this.tabelaQuestionarios.DataBind();
            this.ddlTipos.Items.Clear();
            this.ddlTipos.Items.Add(new ListItem("Pesquisa", "P"));
            this.ddlTipos.Items.Add(new ListItem("Avaliação", "A"));
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
                        this.AddAlertSuccessMessage("Questionário criado com Sucesso!");
                        carregaValores();
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Link deve começar com 'http://'";
                }
            }
            catch (Exception ex)
            {
                this.AddAlertErrorMessage(ex.Message);
            }
            
        }

        private bool validaLinkDoQuestionario(string text)
        {
            return Link.Text.StartsWith("http://");
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
                        this.AddAlertErrorMessage("Questionário possui uma pergunta e portanto não pode ser deletado!");
                    }
                    else
                    {
                        if (questionarioDAO.deletarQuestionario(id))
                        {
                            this.AddAlertSuccessMessage("Questionário excluído com Sucesso!");
                            carregaValores();
                        }
                        else
                        {
                            this.AddAlertErrorMessage("Não foi possível deletar o questionário!");
                        }
                    }
                   
                }
                catch (Exception ex)
                {
                    this.AddAlertErrorMessage(ex.Message);
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
                    ddlTipos.SelectedValue = (tabelaQuestionarios.Rows[LinhaSelecionada].FindControl("lblTipo") as Label).Text.Equals("Pesquisa") ? "P" : "A";
                    preEdicao(id);
                }
                catch(Exception ex)
                {
                    this.AddAlertErrorMessage(ex.Message);
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
                        this.AddAlertSuccessMessage("Questionário Atualizado com Sucesso!");
                        ddlTipos.Enabled = true;
                        posEdicao();
                        carregaValores();
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Link deve começar com 'http://'";
                }
            }
            catch (Exception ex)
            {
                this.AddAlertErrorMessage(ex.Message);
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
            lblError.Visible = false;
            lblEditingId.Visible = false;
            lblIdEdit.Visible = false;
            lblAcao.Text = "Criar Questionário";
            btnCriar.Visible = true;
            btnEditar.Visible = false;
            Nome.Text = "";
            Link.Text = "";
            lblError.Visible = false;
        }
                    
    }
}