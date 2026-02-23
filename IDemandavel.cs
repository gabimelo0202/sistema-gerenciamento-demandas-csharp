using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO
{
    public interface IDemandavel
    {
        List<Habilidade> DificuldadeHabilidades { get; }
        int MultiplicadorCreditos();
    }
}
