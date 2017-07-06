using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppQuestionario.Models
{
    public class Questionario
    {
        private int id;
        private string nome;
        private char tipo;
        private string link;

        // Construtores
        public Questionario(int id, string nome, char tipo, string link)
        {
            this.Id = id;
            this.Nome = nome;
            this.Tipo = tipo;
            this.Link = link;
        }

        // Getters e Setters
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public char Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string Link
        {
            get { return link; }
            set { link = value; }
        }
        
    }
}