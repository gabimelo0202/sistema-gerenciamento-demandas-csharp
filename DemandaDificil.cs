using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO
{
    internal class DemandaDificil : IDemandavel
    {
        private const int Multiplicador = 2;

        public List<Habilidade> DificuldadeHabilidades { get; set; }

        public DemandaDificil(List<Habilidade> habilidades)
        {
            DificuldadeHabilidades = habilidades;
        }

        public int MultiplicadorCreditos()
        {
            return Multiplicador;
        }
    }
}
