using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace AppQuestionario.BasePages
{
    // Classe com funcionalidade comum para maioria das telas do sistema. As páginas do sistema devem herdar de BasePage.
    public class BasePage : Page
    {
        protected string jsFunction;

        protected void AddAlertErrorMessage(string mensagemDeErro)
        {
            this.jsFunction = "erroAlert('" + mensagemDeErro + "')";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alertaErro", this.jsFunction, true);
        }

        protected void AddAlertSuccessMessage(string mensagemDeSucesso)
        {
            this.jsFunction = "successAlert('" + mensagemDeSucesso + "')";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alertaSucesso", this.jsFunction, true);
        }
    }
}