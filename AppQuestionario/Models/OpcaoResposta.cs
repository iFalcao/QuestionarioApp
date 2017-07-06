using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppQuestionario.Models
{
    public class OpcaoResposta
    {
        private Pergunta perguntaRelacionada;
        private int id;
        private char correta;
        private string descricao;
        private int ordem;

        // Construtores
        public OpcaoResposta(Pergunta pergunta, int id, string descricao, char correta, char obrigatoria, int ordem)
        {
            this.PerguntaRelacionada = pergunta;
            this.Id = id;
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
        public Pergunta PerguntaRelacionada
        {
            get { return perguntaRelacionada; }
            set { perguntaRelacionada = value; }
        }
    }
}