using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Romana_AppVendimia.Modelo
{
    public class Session
    {
        public int Id_Cooperado { get; set; }
        public int Id_Ticket { get; set; }
        public int Id_RecepcionUva { get; set; }
        public int Intento { get; set; }
        public Decimal Temperatura { get; set; }
        public string Hora { get; set; }
        public string Fecha { get; set; }
        public Decimal Volumen { get; set; }
        public Decimal Grado { get; set; }
        public byte[] Imagen { get; set; }
        public string Planta { get; set; }
        public int Id_Planta { get; set; }

        public Session()
        {

        }

        public void Clear_Session()
        {
            Id_Cooperado = 0;
            Id_Ticket = 0;
            Id_RecepcionUva = 0;
            Intento = 0;
            Temperatura = 0;
            Hora = string.Empty;
            Fecha = string.Empty;
            Volumen = 0;
            Grado = 0;
            Imagen = null;
            Planta = string.Empty;
            Id_Planta = 0;
        }
    }
}
