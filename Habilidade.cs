using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO
{
    public class Habilidade
    {

        private int id;
        private string nome;
        private string descricao;
        private int pontosDificuldade;

        public Habilidade(int id, string nome, string descricao, int pontosDificuldade)
        {
            this.id = id;
            this.nome = nome;
            this.descricao = descricao;
            this.pontosDificuldade = pontosDificuldade;
        }

        public int Id => id;
        public string Nome => nome;
        public string Descricao => descricao;
        public int Dificuldade => pontosDificuldade;
        public int GetPontosDificuldade()
        {
            return pontosDificuldade;
        }

        public override bool Equals(object obj)
        {
            return obj is Habilidade outra && this.id == outra.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public override string ToString()
        {
            return $"ID: {id}, Nome: {nome}, Dificuldade: {pontosDificuldade}";
        }

    }
}
