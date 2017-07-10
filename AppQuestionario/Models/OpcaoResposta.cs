using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppQuestionario.DAO;

namespace AppQuestionario.Models
{
    public class OpcaoResposta
    {
        private int idPerguntaRelacionada;
        private int id;
        private char correta;
        private string descricao;
        private int ordem;
        private static int count = OpcaoRespostaDAO.getLastId();


        // Construtores

        // Criando uma nova resposta não é passado um id no construtor
        public OpcaoResposta(int idPergunta, string descricao, char correta, int ordem)
        {
            this.Count++;
            this.Id = this.Count;
            this.IdPerguntaRelacionada = idPergunta;
            this.Descricao = descricao;
            this.Correta = correta;
            this.Ordem = ordem;
        }

        // Recuperando a lista de Respostas do Banco de Dados não altera no count de objetos criados
        public OpcaoResposta(int id, int idPergunta, string descricao, char correta, int ordem)
        {
            this.Id = id;
            this.IdPerguntaRelacionada = idPergunta;
            this.Descricao = descricao;
            this.Correta = correta;
            this.Ordem = ordem;
        }

        // Getters e Setters
        public int Ordem
        {
            get { return ordem; }
            set { ordem = value; }
        }
        
        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
        public char Correta
        {
            get { return correta; }
            set { correta = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int IdPerguntaRelacionada
        {
            get { return idPerguntaRelacionada; }
            set { idPerguntaRelacionada = value; }
        }
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
    }
}