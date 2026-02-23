using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO
{
    internal class GeradorDemandas
    {

        private List<Habilidade> todasHabilidades;

        public GeradorDemandas(List<Habilidade> habilidadesDisponiveis)
        {
            this.todasHabilidades = habilidadesDisponiveis;
        }

        public List<Demanda> GerarDemandas(int quantidade)
        {
            List<Demanda> demandas = new List<Demanda>();
            Random rand = new Random();

            for (int i = 1; i <= quantidade; i++)
            {
                string descricao = $"Demanda {i}";
                int qtdHabilidades = DefinirQtdHabilidades(i, quantidade);
                List<Habilidade> habilidadesSelecionadas = todasHabilidades
                    .OrderBy(h => rand.Next())
                    .Take(qtdHabilidades)
                    .ToList();

                if (habilidadesSelecionadas.Count == 0 && todasHabilidades.Count > 0)
                {
                    habilidadesSelecionadas.Add(todasHabilidades[rand.Next(todasHabilidades.Count)]);
                }

                int tempoEstimado = GerarTempoEstimado(qtdHabilidades, rand);

                DateTime prazoMaximo = DateTime.Now.AddDays(rand.Next(1, 15)); // prazo entre 1 e 15 dias

                IDemandavel dificuldade = AvaliarDificuldade(habilidadesSelecionadas);

                Demanda demanda = new Demanda(descricao, habilidadesSelecionadas, tempoEstimado, prazoMaximo, dificuldade);
                demandas.Add(demanda);
            }

            return demandas;
        }

        private int DefinirQtdHabilidades(int indice, int total)
        {
            double percentual = (double)indice / total;

            if (percentual < 0.10) return 5;
            else if (percentual < 0.30) return 4;
            else if (percentual < 0.50) return 3;
            else if (percentual < 0.70) return 2;
            else return 1;
        }

        private int GerarTempoEstimado(int qtdHabilidades, Random rand)
        {
            if (qtdHabilidades <= 2)
                return rand.Next(1, 7); // 1 a 6 meia-horas = 30 min a 3h
            else if (qtdHabilidades <= 4)
                return rand.Next(6, 17); // 6 a 16 meia-horas = 3h a 8h
            else
                return rand.Next(16, 33); // 8h a 16h
        }

        private IDemandavel AvaliarDificuldade(List<Habilidade> habilidades)
        {
            int soma = habilidades.Sum(h => h.GetPontosDificuldade());
            if (habilidades.Count >= 4 || soma >= 5)
                return new DemandaDificil(habilidades);
            else
                return new DemandaFacil(habilidades);
        }


    }
}

