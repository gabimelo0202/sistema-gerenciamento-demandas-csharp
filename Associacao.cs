using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO
{
    internal class Associacao
    {
        private List<Associado> associados = new List<Associado>();
        private List<Demanda> demandas = new List<Demanda>();
        private List<Habilidade> habilidades = new List<Habilidade>();

        public void RegistrarAssociado(Associado associado)
        {
            if (associado != null && !associados.Contains(associado))
            {
                associados.Add(associado);
            }
        }

        public void RegistrarDemanda(Demanda demanda)
        {
            if (demanda != null)
            {
                demandas.Add(demanda);
            }
        }

        public void RegistrarHabilidade(Habilidade habilidade)
        {
            if (habilidade == null)
                throw new ArgumentException("Habilidade invalida");

            bool jaExiste = false;

            foreach (Habilidade hab in habilidades)
            {
                if (hab.Id == habilidade.Id)
                {
                    jaExiste = true;
                    break;
                }
            }

            if (!jaExiste)
            {
                habilidades.Add(habilidade);
            }
        }

        public Habilidade BuscarHabilidadePorNome(string nome)
        {
            foreach (Habilidade hab in habilidades)
            {
                if (hab.Nome.ToLower() == nome.ToLower())
                    return hab;
            }

            return null;
        }

        /// <summary>
        /// Demandas que podem ser solucionadas com um conjunto de habilidades escolhidas pelo usuário
        /// </summary>
        /// <returns></returns>
        public List<Demanda> BuscarDemandasResolvidasPor(List<Habilidade> habilidadesUsuario)
        {
            return demandas.Where(d =>
                d.ObterHabilidadesNecessarias().All(h => habilidadesUsuario.Contains(h))
            ).ToList();
        }


        /// <summary>
        /// 10 associados com o maior saldo de créditos
        /// </summary>
        /// <returns></returns>

        public List<Associado> ObterTop10AssociadosComMaisCreditos()
        {
            return associados
                .OrderByDescending(a => a.CalcularSaldo())
                .Take(10)
                .ToList();
        }

        /// <summary>
        /// Associados que possuem todas as habilidades para a demanda
        /// </summary>
        public List<Associado> LocalizarPrestadores(Demanda demanda)
        {
            if (demanda == null) return new List<Associado>();

            List<Associado> candidatos = new List<Associado>();

            foreach (Associado associado in associados)
            {
                if (associado.VerificarHabilidades(demanda))
                {
                    candidatos.Add(associado);
                }
            }

            return candidatos;
        }

        public List<Associado> ObterAssociadosOrdenadosPorCredito(Demanda demanda)
        {
            List<Associado> habilitados = LocalizarPrestadores(demanda);

            for (int i = 0; i < habilitados.Count - 1; i++)
            {
                for (int j = i + 1; j < habilitados.Count; j++)
                {
                    if (habilitados[j].CalcularSaldo() > habilitados[i].CalcularSaldo())
                    {
                        Associado temp = habilitados[i];
                        habilitados[i] = habilitados[j];
                        habilitados[j] = temp;
                    }
                }
            }

            return habilitados;
        }

        /// <summary>
        /// Media da diferença em horas entre o atendimento e o prazo das demandas atendidas
        /// </summary>
        public double CalcularMediaDiferencaHoras() // sprint 2 (relatório 3) (media)
        {
            double somaDiferencas = 0;
            int totalAtendidas = 0;

            for (int i = 0; i < demandas.Count; i++)
            {
                Demanda demanda = demandas[i];

                if (demanda.FoiAtendida())
                {
                    somaDiferencas += demanda.ObterDiferencaAtendimentoHoras();
                    totalAtendidas++;
                }
            }

            if (totalAtendidas == 0)
                return 0;

            return somaDiferencas / totalAtendidas;
        }

        public List<Demanda> GetDemandas()
        {
            return demandas;
        }

        public Demanda BuscarDemandaPorDescricao(string descricao)
        {
            return demandas.FirstOrDefault(d => d.ObterDescricao().Equals(descricao));
        }

        /// <summary>
        /// Desempata os candidatos habilitados pela demanda com base no maior saldo de creditos
        /// </summary>
        public Associado DesempateAssociadoCapacitado(List<Associado> candidatos)
        {
            return candidatos
                .OrderByDescending(associado => associado.CalcularSaldo())
                .FirstOrDefault();
        }

        public List<Associado> AssociadosNaoPodemRegistrarDemanda()
        {
            return associados
                .Where(a => !a.GetHabilidades().Any() || a.CalcularSaldo() < 0)
                .ToList();
        }

        public List<Demanda> DemandasNaoAlocadas()
        {
            return demandas
                .Where(d => !d.FoiAtendida())
                .ToList();
        }

        public void ListarTop10Demandas()
        {
            Console.WriteLine("== Top 10 demandas que dão mais créditos ==");

            List<Demanda> listaDemandas = GetDemandas();

            IEnumerable<Demanda> topDemandas = listaDemandas
                .OrderByDescending(d => d.CalcularCreditoGanho())
                .Take(10);

            foreach (Demanda deman in topDemandas)
            {
                Console.WriteLine(deman);
                Console.WriteLine("----------------------------------");
            }
        }

        public void ListarDemandasQueAssociadoPodeAtender(Associado associado)
        {
            Console.WriteLine("== Demandas que o associado pode atender ==");

            List<Demanda> listaDemandas = GetDemandas();
            List<Habilidade> habilidadesAssociado = associado.GetHabilidades();

            IEnumerable<Demanda> demandasAtendiveis = listaDemandas.Where(
                d => d.ObterHabilidadesNecessarias().All(
                    hNecessaria => habilidadesAssociado.Any(
                        h => h.Descricao == hNecessaria.Descricao && h.Dificuldade >= hNecessaria.Dificuldade
                    )
                )
            );

            foreach (Demanda deman in demandasAtendiveis)
            {
                Console.WriteLine(deman);
                Console.WriteLine("----------------------------------");
            }
        }

        public Associado BuscarAssociadoPorCPF(string cpf)
        {
            return associados.FirstOrDefault(a => a.GetCPF() == cpf);
        }

        public List<Habilidade> ObterHabilidades()
        {
            return habilidades;
        }

        public void RegistrarPrestacaoServico(string cpfAssociado, string descricaoDemanda)
        {
            Associado associado = BuscarAssociadoPorCPF(cpfAssociado);
            Demanda demanda = BuscarDemandaPorDescricao(descricaoDemanda);

            if (associado == null)
            {
                Console.WriteLine("Associado não encontrado.");
                return;
            }

            if (demanda == null)
            {
                Console.WriteLine("Demanda não encontrada.");
                return;
            }

            if (!associado.VerificarHabilidades(demanda))
            {
                Console.WriteLine("Associado não tem as habilidades necessárias.");
                return;
            }

            demanda.RegistrarAtendimento(DateTime.Now);

            if (associado.GetPerfil() is Prestador prestador)
            {
                prestador.RegistrarAtendimento(demanda.CalcularHoras());
            }

            Console.WriteLine("Demanda atribuída com sucesso!");
        }

        public List<Associado> ObterAssociados()
        {
            return associados;
        }


    }
}

