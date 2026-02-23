using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Prestador : IPerfil
{
    private double horasPrestadas;

    public Prestador()
    {
        horasPrestadas = 0;
    }

    public void RegistrarAtendimento(double horas)
    {
        horasPrestadas += horas;
    }

    public int CalcularCreditos()
    {
        return (int)(horasPrestadas * 10); // Exemplo: 1h = 10 créditos
    }

    public override string ToString()
    {
        return "Prestador";
    }
}
