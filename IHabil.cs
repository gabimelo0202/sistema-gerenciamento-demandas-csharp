using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public interface IHabil
{
    string Descricao { get; }
    int Dificuldade { get; }

    int GetPontosDificuldade();
}
