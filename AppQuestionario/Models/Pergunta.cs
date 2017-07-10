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

        private static int count = PerguntaDAO.getLastId();

        // Construtores

        // Criando uma nova pergunta não é passado um id no construtor
        public Pergunta(int idQuestionario, string descricao, char tipo, char obrigatoria, int ordem)
        {
            this.Count++;
            this.Id = this.Count;
            this.IdQuestionario = idQuestionario;
            this.Descricao = descricao;
            this.Tipo = tipo;
            this.Obrigatoria = obrigatoria;
            this.Ordem = ordem;
        }

        // Recuperando a lista de Perguntas do Banco de Dados não altera no count de objetos criados
        public Pergunta(int idPergunta, int idQuestionario, string descricao, char tipo, char obrigatoria, int ordem)
        {
            this.Id = idPergunta;
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
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
     
    }
}