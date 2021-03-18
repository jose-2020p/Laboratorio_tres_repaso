using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_tres_repaso
{
    class datos_en__conjunto
    {
        int numero_de_casa;
        int dpi;
        string nombre;
        string apellido;
        double cuota_mantenimiento;
        int cantidad_casas;

        public int Numero_de_casa { get => numero_de_casa; set => numero_de_casa = value; }
        public int Dpi { get => dpi; set => dpi = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public double Cuota_mantenimiento { get => cuota_mantenimiento; set => cuota_mantenimiento = value; }
        public int Cantidad_casas { get => cantidad_casas; set => cantidad_casas = value; }
    }
}
