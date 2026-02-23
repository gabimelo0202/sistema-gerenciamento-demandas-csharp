using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO
{
    internal class DemandaFacil : IDemandavel
    {
        private const int Multiplicador = 1;

        public List<Habilidade> DificuldadeHabilidades { get; set; }

        public DemandaFacil(List<Habilidade> habilidades)
        {
            DificuldadeHabilidades = habilidades;
        }

        public int MultiplicadorCreditos()
        {
            return Multiplicador;
        }
    }
}
