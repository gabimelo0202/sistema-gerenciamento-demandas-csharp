using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Produtor : IPerfil, IProdutor
{
    private List<Produto> produtos;

    public Produtor()
    {
        produtos = new List<Produto>();
    }

    public void CadastrarProduto(Produto p)
    {
        produtos.Add(p);
    }

    public int CalcularCreditos()
    {
        int total = produtos.Sum(p => p.GetValor());
        return total;
    }

    public override string ToString()
    {
        int totalValor = produtos.Sum(p => p.GetValor());
        return $"Produtor (valor total dos produtos: {totalValor})";
    }
}
