using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaEntidades
{
    public class ReservaFullDay
    {
        public int Pk_Reserva_FullDay { get; set; }
        public DateTime Fecha_Viaje { get; set; }
        public DateTime Fecha_Generado { get; set; }
        public int Fk_Origen { get; set; }
        public int Fk_Destino { get; set; }
        public double precioUnitario { get; set; }
        public int cantidad { get; set; }
        public double precioTotal { get; set; }
        public string Dni_cliente { get; set; }
        public string Nombre { get; set; }
        public string  Apellido { get; set; }
        public string Telefono { get; set; }
        public string correo { get; set; }
        public string  Estado { get; set; }
 
    }
}
