using AppQuestionario.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppQuestionario.Models
{
    public class Pergunta
    {
        private int id;
        private int idQuestionario;
        private char tipo;
        private string descricao;
        private char obrigatoria;
        private int ordem;

        // Construtores
        public Pergunta(int idQuestionario, string descricao, char tipo, char obrigatoria, int ordem)
        {
            this.Id = PerguntaDAO.getLastId() + 1;
            this.IdQuestionario = idQuestionario;
            this.Descricao = descricao;
            this.Tipo = tipo;
            this.Obrigatoria = obrigatoria;
            this.Ordem = ordem;
        }

        // Getters e Setters
        public int Ordem
        {
            get { return ordem; }
            set { ordem = value; }
        }
        public char Obrigatoria
        {
            get { return obrigatoria; }
            set { obrigatoria = value; }
        }
        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
        public char Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int IdQuestionario
        {
            get { return idQuestionario; }
            set { idQuestionario = value; }
        }
        
     
    }
}