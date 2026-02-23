using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO
{
    public class GeradorHabilidades
    {
        public List<Habilidade> GerarHabilidades()
        {
            List<Habilidade> lista = new List<Habilidade>();

            string[] nomes = {
            "Programação", "Carpintaria", "Jardinagem", "Pintura", "Eletricidade",
            "Faxina", "Cozinha", "Costura", "Pedreiro", "Mecânica",
            "Encanamento", "Design Gráfico", "Marketing Digital", "Fotografia", "Aula Particular"
        };

            string[] descricoes = {
            "Lógica, algoritmos e codificação",
            "Trabalhos em madeira e marcenaria",
            "Cuidado e manutenção de jardins",
            "Pintura residencial e artística",
            "Instalações elétricas residenciais",
            "Limpeza de ambientes diversos",
            "Preparação e organização de alimentos",
            "Reparo e criação de roupas",
            "Construção e reformas em alvenaria",
            "Manutenção e conserto de veículos",
            "Reparo e instalação hidráulica",
            "Criação de peças visuais e digitais",
            "Estratégias de divulgação online",
            "Fotografar e editar imagens",
            "Ensino individual em domicílio"
        };

            int[] dificuldades = { 3, 2, 1, 2, 3, 2, 3, 2, 4, 4, 3, 2, 3, 2, 3 };

            for (int i = 0; i < nomes.Length; i++)
            {
                Habilidade h = new Habilidade(i + 1, nomes[i], descricoes[i], dificuldades[i]);
                lista.Add(h);
            }

            return lista;
        }
    }
}
