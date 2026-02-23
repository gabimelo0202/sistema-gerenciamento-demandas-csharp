using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO
{
    public class Demanda
    {
        private string descricao;
        private List<Habilidade> habilidadesNecessarias;
        private int tempoEstimado;
        private DateTime prazoMaximo;
        private DateTime? dataAtendimento;
        private IDemandavel dificuldade;

        public Demanda(string descricao, List<Habilidade> habilidades, int tempoEstimado, DateTime prazoMaximo, IDemandavel dificuldade)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Descrição inválida.");

            if (habilidades == null || habilidades.Count == 0)
                throw new ArgumentException("A demanda deve ter ao menos uma habilidade.");

            if (tempoEstimado <= 0)
                throw new ArgumentException("Tempo estimado deve ser positivo.");

            if (prazoMaximo <= DateTime.Now)
                throw new ArgumentException("Prazo máximo deve estar no futuro.");

            if (dificuldade == null)
                throw new ArgumentException("Dificuldade não pode ser nula.");

            this.descricao = descricao;
            this.habilidadesNecessarias = habilidades;
            this.tempoEstimado = tempoEstimado;
            this.prazoMaximo = prazoMaximo;
            this.dificuldade = dificuldade;
            this.dataAtendimento = null;
        }

        public string ObterDescricao()
        {
            return descricao;
        }

        public List<Habilidade> ObterHabilidadesNecessarias()
        {
            return habilidadesNecessarias;
        }

        public int CalcularHoras()
        {
            return tempoEstimado;
        }

        public DateTime ObterPrazoMaximo()
        {
            return prazoMaximo;
        }

        public DateTime? ObterDataAtendimento()
        {
            return dataAtendimento;
        }

        public IDemandavel ObterDificuldade()
        {
            return dificuldade;
        }

        public bool FoiAtendida()
        {
            return dataAtendimento != null;
        }

        public void RegistrarAtendimento(DateTime data)
        {
            if (FoiAtendida())
                throw new InvalidOperationException("Demanda já foi atendida.");

            dataAtendimento = data;
        }

        public bool EhDemandaDificil()
        {
            bool temMuitasHabilidades = habilidadesNecessarias.Count >= 4;

            int somaDificuldade = 0;
            foreach (Habilidade h in habilidadesNecessarias)
            {
                somaDificuldade += h.GetPontosDificuldade();
            }

            bool dificuldadeAlta = somaDificuldade >= 5;

            return temMuitasHabilidades || dificuldadeAlta;
        }

        public int CalcularCreditoGanho()
        {
            int multiplicador = dificuldade.MultiplicadorCreditos();
            return tempoEstimado * multiplicador;
        }
        public double ObterDiferencaAtendimentoHoras()
        {
            if (!FoiAtendida())
                return 0;

            return (dataAtendimento.Value - prazoMaximo).TotalHours;
        }

        public override string ToString()
        {
            return $"Descrição: {descricao}, Tempo: {tempoEstimado}h, Prazo: {prazoMaximo.ToShortDateString()}, Habilidades: {string.Join(", ", habilidadesNecessarias.Select(h => h.Nome))}";
        }
    }
}
