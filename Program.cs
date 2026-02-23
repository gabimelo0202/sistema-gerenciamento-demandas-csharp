using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO
{
    internal class Program
    {
        static void Pause()
        {
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        static int LerNumero(string mensagem)
        {
            int opcao;
            Console.Write($"{mensagem}: ");
            try
            {
                opcao = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                opcao = -1;
                Console.WriteLine("Favor digitar somente números.");
            }
            return opcao;
        }

        static void Cabecalho()
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("          Associação AMX - POO           ");
            Console.WriteLine("         Grupo - sem nome                ");
            Console.WriteLine("=========================================");
        }

        static void MenuPrincipal()
        {
            Console.Clear();
            Cabecalho();
            Console.WriteLine("\n==== MENU PRINCIPAL ====");
            Console.WriteLine("1 - Gerenciar associados");
            Console.WriteLine("2 - Gerenciar demandas");
            Console.WriteLine("3 - Análise de desempenho");
            Console.WriteLine("4 - Relatórios");
            Console.WriteLine("5 - Gerenciar Habilidades");
            Console.WriteLine("6 - Sair");
        }

        static void SubmenuAssociado(Associacao sistema)
        {
            Console.WriteLine("\n-- Menu Associado --");
            Console.WriteLine("1 - Registrar associado");
            Console.WriteLine("2 - Adicionar habilidade a um associado");
            Console.WriteLine("3 - Atribuir demanda ao associado");
            Console.WriteLine("4 - Listar associados");
            Console.WriteLine("5 - Registrar mercadoria para produtor");
            Console.WriteLine("6 - Voltar");
            int opcao = LerNumero("Escolha uma opção");
            switch (opcao)
            {
                case 1:
                    RegistrarAssociado(sistema);
                    break;

                case 2:
                    AssociarHabilidadeAoAssociado(sistema);
                    break;

                case 3:
                    Console.Write("CPF do associado: ");
                    string cpf = Console.ReadLine();

                    Console.Write("Descrição da demanda: ");
                    string descricao = Console.ReadLine();

                    sistema.RegistrarPrestacaoServico(cpf, descricao);
                    Pause();
                    break;
                case 4:
                    Pause();
                    ListarAssociados(sistema);
                    break;

                case 5:
                    RegistrarMercadoriaParaProdutor(sistema);
                    break;

                case 6:
                    return;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }

        static void SubmenuDemanda(Associacao sistema)
        {
            Console.WriteLine("\n-- Menu Demanda --");
            Console.WriteLine("1 - Registrar demanda");
            Console.WriteLine("2 - Localizar prestadores para demanda");
            Console.WriteLine("3 - Listar todas as demandas");
            Console.WriteLine("4 - Voltar");
            int opcao = LerNumero("Escolha uma opção: ");

            switch (opcao)
            {
                case 1:
                    RegistrarDemanda(sistema);
                    break;
                case 2:
                    Pause();
                    LocalizarPrestadores(sistema);
                    break;
                case 3:
                    Pause();
                    ListarDemandas(sistema);
                    break;
                case 4:
                    return;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }

        static void SubmenuDesempenho(Associacao sistema)
        {
            Console.WriteLine("\n-- Menu Desempenho --");
            Console.WriteLine("1 - Localizar associado capacitado para demanda");
            Console.WriteLine("2 - Ver melhor desempenho entre associados");
            Console.WriteLine("3 - Voltar");
            int opcao = LerNumero("Escolha uma opção: ");

            switch (opcao)
            {
                case 1:
                    LocalizarAssociadoCapacitado(sistema);
                    break;
                case 2:
                    DesempenhoAssociadoCapacitado(sistema);
                    break;
                case 3:
                    return;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }

        static void SubmenuHabilidade(Associacao sistema)
        {
            Console.WriteLine("\n-- Menu Habilidades --");
            Console.WriteLine("1 - Registrar nova habilidade");
            Console.WriteLine("2 - Listar habilidades cadastradas");
            Console.WriteLine("3 - Buscar habilidade por nome");
            Console.WriteLine("4 - Voltar");

            int opcao = LerNumero("Escolha uma opção");

            switch (opcao)
            {
                case 1:
                    RegistrarHabilidade(sistema);
                    break;

                case 2:
                    List<Habilidade> todas = sistema.ObterHabilidades();
                    Console.WriteLine("== Habilidades cadastradas ==");
                    foreach (Habilidade hab in todas)
                    {
                        Console.WriteLine(hab);
                    }
                    Pause();
                    break;

                case 3:
                    Console.Write("Digite o nome da habilidade: ");
                    string nomeBusca = Console.ReadLine();
                    Habilidade encontrada = sistema.BuscarHabilidadePorNome(nomeBusca);
                    if (encontrada != null)
                        Console.WriteLine("Encontrada: " + encontrada.Nome + " - Dificuldade: " + encontrada.GetPontosDificuldade());
                    else
                        Console.WriteLine("Habilidade não encontrada.");
                    Pause();
                    break;

                case 4:
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        static void SubmenuRelatorios(Associacao sistema)
        {
            Console.WriteLine("\n-- Relatórios --");
            Console.WriteLine("1 - Associados que não podem registrar demanda");
            Console.WriteLine("2 - Demandas ainda não alocadas");
            Console.WriteLine("3 - Demandas que podem ser atendidas por um associado escolhido pelo usuário");
            Console.WriteLine("4 - 10 demandas que dão a maior quantidade de créditos");
            Console.WriteLine("5 - Associados hábeis para uma demanda, ordenados por saldo de créditos");
            Console.WriteLine("6 - Média da diferença de tempo de atendimento para prazos pedidos das demandas");
            Console.WriteLine("7 - 10 associados com o maior saldo de créditos");
            Console.WriteLine("8 - Demandas que podem ser solucionadas com um conjunto de habilidades escolhidas pelo usuário");
            Console.WriteLine("9 - Voltar");

            int opcao = LerNumero("Escolha uma opção: ");

            switch (opcao)
            {
                case 1:
                    foreach (Associado associado in sistema.AssociadosNaoPodemRegistrarDemanda())
                        Console.WriteLine(associado);
                    Pause();
                    break;

                case 2:
                    foreach (Demanda deman in sistema.DemandasNaoAlocadas())
                        Console.WriteLine(deman);
                    Pause();
                    break;

                case 3:
                    Console.Write("Informe o CPF do associado: ");
                    string cpf = Console.ReadLine();
                    Associado escolhido = sistema.BuscarAssociadoPorCPF(cpf);

                    if (escolhido != null)
                        sistema.ListarDemandasQueAssociadoPodeAtender(escolhido);
                    else
                        Console.WriteLine("Associado não encontrado.");
                    Pause();
                    break;

                case 4:
                    sistema.ListarTop10Demandas();
                    Pause();
                    break;

                case 5:
                    Console.Write("Descrição da demanda: ");
                    string descricao = Console.ReadLine();

                    Demanda demanda = sistema.BuscarDemandaPorDescricao(descricao);
                    if (demanda == null)
                    {
                        Console.WriteLine("Demanda não encontrada.");
                        Pause();
                        break;
                    }

                    List<Associado> associadosOrdenados = sistema.ObterAssociadosOrdenadosPorCredito(demanda);
                    Console.WriteLine("Associados hábeis para a demanda (ordenados por saldo de créditos):");

                    foreach (Associado associado in associadosOrdenados)
                    {
                        Console.WriteLine(associado);
                    }
                    Pause();
                    break;

                case 6:
                    double media = sistema.CalcularMediaDiferencaHoras();
                    Console.WriteLine($"Média da diferença (em horas): {media:F2}");
                    Pause();
                    break;


                case 7:
                    List<Associado> top10 = sistema.ObterTop10AssociadosComMaisCreditos();
                    Console.WriteLine("Top 10 associados com maior saldo de créditos:");

                    foreach (Associado associado in top10)
                    {
                        Console.WriteLine(associado);
                    }

                    Pause();
                    break;

                case 8:
                    List<Habilidade> habilidadesInformadas = new List<Habilidade>();

                    Console.WriteLine("Informe habilidades (digite 'fim' para encerrar):");

                    while (true)
                    {
                        Console.Write("Nome da habilidade: ");
                        string nomeHab = Console.ReadLine();

                        if (nomeHab.ToLower() == "fim") break;

                        Habilidade h = sistema.BuscarHabilidadePorNome(nomeHab);
                        if (h != null)
                        {
                            habilidadesInformadas.Add(h);
                        }
                        else
                        {
                            Console.WriteLine("Habilidade não encontrada.");
                        }
                    }

                    if (habilidadesInformadas.Count == 0)
                    {
                        Console.WriteLine("Nenhuma habilidade válida foi informada.");
                    }
                    else
                    {
                        List<Demanda> resolviveis = sistema.BuscarDemandasResolvidasPor(habilidadesInformadas);

                        if (resolviveis.Count == 0)
                        {
                            Console.WriteLine("Nenhuma demanda pode ser resolvida com esse conjunto de habilidades.");
                        }
                        else
                        {
                            Console.WriteLine("Demandas que podem ser resolvidas:");
                            foreach (Demanda deman in resolviveis)
                            {
                                Console.WriteLine(deman);
                            }
                        }
                    }
                    Pause();
                    break;

                case 9:
                    return;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }


        static void RegistrarAssociado(Associacao sistema)
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("CPF: ");
            string cpf = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Tipo de perfil (prestador/produtor): ");
            string tipoPerfil = Console.ReadLine().Trim().ToLower();

            IPerfil perfil;
            if (tipoPerfil == "produtor")
                perfil = new Produtor();

            else
                perfil = new Prestador();

            Associado associado = new Associado(nome, cpf, email, perfil);

            sistema.RegistrarAssociado(associado);

            Console.WriteLine("Associado registrado com sucesso.");
        }


        static void AssociarHabilidadeAoAssociado(Associacao sistema)
        {
            Console.Write("Digite o CPF do associado: ");
            string cpf = Console.ReadLine();

            Associado associado = sistema.BuscarAssociadoPorCPF(cpf);

            if (associado == null)
            {
                Console.WriteLine("Associado não encontrado.");
                Pause();
                return;
            }

            Console.Write("Digite o nome da habilidade que deseja adicionar: ");
            string nomeHab = Console.ReadLine();

            Habilidade habilidade = sistema.BuscarHabilidadePorNome(nomeHab);

            if (habilidade == null)
            {
                Console.WriteLine("Habilidade não encontrada.");
            }
            else
            {
                associado.AdicionarHabilidade(habilidade);
                Console.WriteLine("Habilidade associada com sucesso.");
            }

            Pause();
        }
        static void RegistrarDemanda(Associacao sistema)
        {
            Console.Write("Descrição da demanda: ");
            string descricao = Console.ReadLine();

            Console.Write("Tempo estimado (em horas): ");
            int tempo = int.Parse(Console.ReadLine());

            Console.Write("Prazo máximo (DD/MM/AAAA): ");
            DateTime prazo = DateTime.Parse(Console.ReadLine());

            List<Habilidade> habilidades = new List<Habilidade>();
            Console.WriteLine("Informe ao menos uma habilidade (digite 'fim' para encerrar):");
            while (true)
            {
                Console.Write("Habilidade: ");
                string nomeHab = Console.ReadLine();
                if (nomeHab.ToLower() == "fim") break;

                Habilidade hab = sistema.BuscarHabilidadePorNome(nomeHab);
                if (hab != null) habilidades.Add(hab);
                else Console.WriteLine("Habilidade não encontrada.");
            }

            if (habilidades.Count == 0)
            {
                Console.WriteLine("Demanda deve conter ao menos uma habilidade.");
                return;
            }

            Console.Write("Dificuldade (fácil/difícil): ");
            string difStr = Console.ReadLine().ToLower();

            IDemandavel dificuldade;

            if (difStr.Equals("fácil"))
            {
                dificuldade = new DemandaFacil(habilidades);
            }
            else
            {
                dificuldade = new DemandaDificil(habilidades);
            }

            Demanda demanda = new Demanda(descricao, habilidades, tempo, prazo, dificuldade);
            sistema.RegistrarDemanda(demanda);

            Console.WriteLine("Demanda registrada com sucesso.");
        }

        static void RegistrarHabilidade(Associacao sistema)
        {
            Console.Write("ID da habilidade: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Nome da habilidade: ");
            string nome = Console.ReadLine();

            Console.Write("Descrição da habilidade: ");
            string descricao = Console.ReadLine();

            Console.Write("Dificuldade (pontos): ");
            int pontos = int.Parse(Console.ReadLine());

            Habilidade nova = new Habilidade(id, nome, descricao, pontos);
            sistema.RegistrarHabilidade(nova);
        }

        static void LocalizarPrestadores(Associacao sistema)
        {
            Console.Write("Descrição da demanda: ");
            string descricao = Console.ReadLine();
            Demanda demanda = sistema.BuscarDemandaPorDescricao(descricao);
            if (demanda == null)
            {
                Console.WriteLine("Demanda não encontrada.");
                return;
            }
            foreach (Associado associado in sistema.LocalizarPrestadores(demanda))
                Console.WriteLine(associado);
        }

        static void LocalizarAssociadoCapacitado(Associacao sistema)
        {
            Console.Write("Descrição da demanda: ");
            string descricao = Console.ReadLine();
            Demanda demanda = sistema.BuscarDemandaPorDescricao(descricao);
            if (demanda == null)
            {
                Console.WriteLine("Demanda não encontrada.");
                return;
            }
            Console.WriteLine("Associado capacitado: " + sistema.ObterAssociadosOrdenadosPorCredito(demanda));
        }

        static void DesempenhoAssociadoCapacitado(Associacao sistema)
        {
            Console.Write("Descrição da demanda: ");
            string descricao = Console.ReadLine();
            Demanda demanda = sistema.BuscarDemandaPorDescricao(descricao);
            if (demanda == null)
            {
                Console.WriteLine("Demanda não encontrada.");
                return;
            }
            List<Associado> candidatos = sistema.LocalizarPrestadores(demanda);
            Associado melhor = sistema.DesempateAssociadoCapacitado(candidatos);
            Console.WriteLine(melhor != null ? $"Associado com melhor desempenho: {melhor}" : "Nenhum candidato encontrado.");
        }

        static void ListarDemandas(Associacao sistema)
        {
            List<Demanda> demandas = sistema.DemandasNaoAlocadas();

            if (demandas.Count == 0)
            {
                Console.WriteLine("Nenhuma demanda encontrada.");
            }
            else
            {
                foreach (Demanda deman in demandas)
                {
                    Console.WriteLine(deman);
                }
            }
            Pause();
        }

        static void ListarAssociados(Associacao sistema)
        {
            List<Associado> associados = sistema.ObterAssociados();
            if (associados.Count == 0)
            {
                Console.WriteLine("Nenhum associado encontrado.");
            }
            else
            {
                foreach (Associado associado in associados)
                {
                    Console.WriteLine(associado);
                }
            }
            Pause();
        }

        static void RegistrarMercadoriaParaProdutor(Associacao sistema)
        {
            Console.Write("CPF do produtor: ");
            string cpf = Console.ReadLine();

            Associado associado = sistema.BuscarAssociadoPorCPF(cpf);

            if (associado == null)
            {
                Console.WriteLine("Associado não encontrado.");
                return;
            }

            Produtor produtor = associado.GetPerfil() as Produtor;
            if (produtor == null)
            {
                Console.WriteLine("Este associado não é um produtor.");
                Pause();
                return;
            }

            Console.Write("Descrição da mercadoria: ");
            string descricao = Console.ReadLine();

            Console.Write("Informe o valor da mercadoria (pontos): ");
            if (!int.TryParse(Console.ReadLine(), out int valor))
            {
                Console.WriteLine("Valor inválido.");
                return;
            }

            Produto produto = new Produto(descricao, valor);
            produtor.CadastrarProduto(produto);

            Console.WriteLine("Mercadoria registrada com sucesso!");

            Pause();
        }

        static void Main(string[] args)
        {
            Associacao sistema = new Associacao();

            GeradorHabilidades geradorHabilidades = new GeradorHabilidades();
            List<Habilidade> habilidadesGeradas = geradorHabilidades.GerarHabilidades();

            foreach (Habilidade habilidade in habilidadesGeradas)
            {
                sistema.RegistrarHabilidade(habilidade);
            }

            GeradorDemandas geradorDemanda = new GeradorDemandas(sistema.ObterHabilidades());
            List<Demanda> novas = geradorDemanda.GerarDemandas(25);

            foreach (Demanda deman in novas)
            {
                sistema.RegistrarDemanda(deman);
            }
            int opcao;

            GeradorAssociados geradorAssociado = new GeradorAssociados(sistema.ObterHabilidades());
            List<Associado> novosAssociados = geradorAssociado.GerarAssociados(250);

            foreach (Associado associado in novosAssociados)
            {
                sistema.RegistrarAssociado(associado);
            }


            do
            {
                MenuPrincipal();
                opcao = LerNumero("Escolha uma opção");
                switch (opcao)
                {
                    case 1: SubmenuAssociado(sistema); break;
                    case 2: SubmenuDemanda(sistema); break;
                    case 3: SubmenuDesempenho(sistema); break;
                    case 4: SubmenuRelatorios(sistema); break;
                    case 5: SubmenuHabilidade(sistema); break;
                    case 6: Console.WriteLine("Sistema encerrado."); break;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            } while (opcao != 6);
        }
    }
}


