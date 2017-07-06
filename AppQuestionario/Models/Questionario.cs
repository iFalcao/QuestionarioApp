using AppQuestionario.DAO;
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
        private static int count = QuestionarioDAO.getLastId();

        // Construtores

        // Criando um novo questionário não é passado um id no construtor
        public Questionario(string nome, char tipo, string link)
        {
            this.Count++;
            this.Id = this.Count;
            this.Nome = nome;
            this.Tipo = tipo;
            this.Link = link;
        }

        // Recuperando a lista de Questionários do Banco de Dados não altera no count de objetos criados
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
        public int Count
        {
            get { return count; }
            set { count = value; }
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