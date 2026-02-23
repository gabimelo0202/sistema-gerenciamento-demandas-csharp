using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Produto
{
    private string descricao;
    private int valorPontos;

    public Produto(string descricao, int valorPontos)
    {
        this.descricao = descricao;
        this.valorPontos = valorPontos;
    }

    public int GetValor()
    {
        return valorPontos;
    }
}
