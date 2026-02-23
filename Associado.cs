using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_POO;

public class Associado
{
    private string nome;
    private string cpf;
    private string email;
    private IPerfil perfil;
    private List<Habilidade> habilidades;
    private int saldoCreditos;

    public Associado(string nome, string cpf, string email, IPerfil perfil)
    {
        this.nome = nome;
        this.cpf = cpf;
        this.email = email;
        this.perfil = perfil;
        this.habilidades = new List<Habilidade>();
        this.saldoCreditos = 0;
    }

    public void AdicionarHabilidade(Habilidade habilidade)
    {
        if (habilidade != null && !habilidades.Contains(habilidade))
        {
            habilidades.Add(habilidade);
        }
    }

    public void ListarHabilidades()
    {
        foreach (Habilidade hab in habilidades)
        {
            Console.WriteLine(hab);
        }
    }

    public int CalcularSaldo()
    {
        saldoCreditos = perfil.CalcularCreditos();
        return saldoCreditos;
    }

    public List<Habilidade> GetHabilidades()
    {
        return habilidades;
    }

    public string GetNome()
    {
        return nome;
    }

    public string GetCPF()
    {
        return cpf;
    }

    public IPerfil GetPerfil()
    {
        return perfil;
    }
    public bool VerificarHabilidades(Demanda demanda)
    {
        List<Habilidade> habilidadesNecessarias = demanda.ObterHabilidadesNecessarias();

        foreach (Habilidade habilidadeNecessaria in habilidadesNecessarias)
        {
            bool possuiHabilidade = false;

            foreach (Habilidade habilidadeAssociado in habilidades)
            {
                if (habilidadeAssociado.Equals(habilidadeNecessaria))
                {
                    possuiHabilidade = true;
                    break;
                }
            }

            if (!possuiHabilidade)
            {
                return false;
            }
        }

        return true;
    }

    public override string ToString()
    {
        string habilidadesFormatadas = string.Join(", ", habilidades.Select(h => h.Nome));
        return $"Nome: {nome}, CPF: {cpf}, Email: {email}, Créditos: {saldoCreditos}, Habilidades: {habilidadesFormatadas}";
    }

}

