using System;
using System.Collections.Generic;
using System.Linq;
using TP_POO;

public class GeradorAssociados
{
    private List<Habilidade> habilidadesDisponiveis;
    private Random rand = new Random();

    private List<string> nomes = new List<string> {
        "Lucas", "Ana", "Carlos", "Mariana", "Felipe", "Juliana", "Pedro", "Larissa", "Bruno", "Camila",
        "Gabriel", "Letícia", "Mateus", "Isabela", "João", "Bianca", "André", "Patrícia", "Rafael", "Thaís"
    };

    private List<string> sobrenomes = new List<string> {
        "Silva", "Souza", "Oliveira", "Santos", "Costa", "Pereira", "Almeida", "Ferreira", "Rodrigues", "Martins",
        "Gomes", "Lima", "Araujo", "Barbosa", "Cardoso", "Teixeira", "Dias", "Ramos", "Nascimento", "Correia"
    };

    public GeradorAssociados(List<Habilidade> habilidades)
    {
        habilidadesDisponiveis = habilidades;
    }

    public List<Associado> GerarAssociados(int total)
    {
        List<Associado> lista = new List<Associado>();

        int qtd7 = (int)(total * 0.10);
        int qtd5 = (int)(total * 0.20);
        int qtd4 = qtd5;
        int qtd3 = qtd5;
        int qtd2 = total - (qtd7 + qtd5 + qtd4 + qtd3);

        lista.AddRange(Gerar(qtd7, 7));
        lista.AddRange(Gerar(qtd5, 5));
        lista.AddRange(Gerar(qtd4, 4));
        lista.AddRange(Gerar(qtd3, 3));
        lista.AddRange(Gerar(qtd2, 2));

        return lista;
    }

    private List<Associado> Gerar(int qtd, int habilidadesPorAssociado)
    {
        List<Associado> lista = new List<Associado>();

        for (int i = 0; i < qtd; i++)
        {
            string nome = nomes[rand.Next(nomes.Count)] + " " + sobrenomes[rand.Next(sobrenomes.Count)];
            string cpf = rand.Next(100000000, 999999999).ToString("000000000");
            string email = nome.ToLower().Replace(" ", ".") + "@amx.org";

            IPerfil perfil = new Prestador();
            Associado a = new Associado(nome, cpf, email, perfil);

            List<Habilidade> escolhidas = habilidadesDisponiveis.OrderBy(x => rand.Next()).Take(habilidadesPorAssociado).ToList();
            foreach (Habilidade h in escolhidas)
            {
                a.AdicionarHabilidade(h);
            }

            lista.Add(a);
        }

        return lista;
    }
}
